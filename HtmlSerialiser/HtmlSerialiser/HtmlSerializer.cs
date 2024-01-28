using System.Text.RegularExpressions;
using static HtmlSerialiser.HtmlElement;

namespace HtmlSerialiser
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

            HtmlElement root = null;
            var current = root;
            foreach (var line in htmlLines)
            {
                if (line.Equals("/html"))
                    break;

                var firstWord = line[..(line.IndexOf(' ') == -1 ? line.Length : line.IndexOf(' '))];

                if (line[0] == '/')
                    current = current?.Parent;
                else if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
                {
                    var element = new HtmlElement() { Name = firstWord, Parent = current };
                    if (root == null)
                        root = current = element;
                    else
                        current?.Children.Add(element);

                    var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line.Replace(firstWord, ""));
                    foreach (var attribute in attributes)
                    {
                        var attributeKV = attribute?.ToString()?.Split('=');
                        attributeKV[1] = attributeKV[1].Replace("\"", "");

                        if (attributeKV[0] == "id")
                            element.Id = attributeKV[1];
                        else if (attributeKV[0] == "class")
                            attributeKV[1].Split(' ').ToList().ForEach(cName => current.Classes.Add(cName));
                        else
                            element.Attributes.Add(new ObjectKV(attributeKV));
                    }

                    if (!HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord) && line[^1] != '/')
                        current = element;
                }
                else if (current != null)
                    current.InnerHtml = line;
            }

            return root;
        }
    }
}
