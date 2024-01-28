using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            var root = new HtmlElement();
            var current = root;

            foreach (var line in htmlLines)
            {
                if (line.Equals("/html"))
                    break;

                var firstWord = line[..(line.IndexOf(' ') == -1 ? line.Length : line.IndexOf(' '))];

                if (line[0] == '/')
                    current = current.Parent;
                else if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
                {
                    var element = new HtmlElement() { Name = firstWord, Parent = current };
                    current?.Children.Add(element);

                    var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line.Replace(firstWord, ""));
                    foreach (var attribute in attributes)
                    {
                        var attributeKV = attribute.ToString().Split('=');
                        attributeKV[1] = attributeKV[1].Replace("\"", "");

                        if (attributeKV[0] == "id")
                            current.Id = attributeKV[1];
                        else if (attributeKV[0] == "class")
                            attributeKV[1].Split(' ').ToList().ForEach(cName => current.Classes.Add(cName));
                        else
                            current.Attributes.Add(attributeKV[0], attributeKV[1]);
                    }

                    if (!HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord) && line[^1] != '/')
                        current = element;
                }
                else
                    current.InnerHtml = line;
            }

            return root;
        }
    }
}
