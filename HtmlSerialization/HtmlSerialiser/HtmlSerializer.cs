using System.Text.RegularExpressions;
using static HtmlSerialization.HtmlElement;

namespace HtmlSerialization
{
    internal class HtmlSerializer
    {
        public static async Task<string> Load(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

        public static HtmlElement Serialize(string html)
        {
            var cleanHtml = new Regex("\\s{2,}").Replace(html, "");
            var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

            htmlLines = htmlLines.Where(line => !line.StartsWith("!DOCTYPE"));

            HtmlElement? root = null;
            var current = root;
            foreach (var line in htmlLines)
            {
                if (line.Equals("/html"))
                    break;

                var tagName = line[..(line.Contains(' ') ? line.IndexOf(' ') : line.Length)];

                if (line[0] == '/')
                    current = current?.Parent;
                else if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
                {
                    var element = new HtmlElement() { Name = tagName, Parent = current };
                    if (root == null)
                        root = current = element;
                    else
                        current?.Children.Add(element);

                    var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line.Replace(tagName, ""));
                    foreach (var attribute in attributes)
                    {
                        var attributeKV = attribute?.ToString()?.Split('=');
                        attributeKV[1] = attributeKV[1].Replace("\"", "");

                        if (attributeKV[0] == "id")
                            element.Id = attributeKV[1];
                        else if (attributeKV[0] == "class")
                            attributeKV[1].Split(' ').ToList().ForEach(cName => current?.Classes.Add(cName));
                        else
                            element.Attributes.Add(attribute?.ToString());
                    }

                    if (!HtmlHelper.Instance.HtmlVoidTags.Contains(tagName) && line[^1] != '/')
                        current = element;
                }
                else if (current != null)
                    current.InnerHtml += line;
            }

            return root;
        }
    }
}
