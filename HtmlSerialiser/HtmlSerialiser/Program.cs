using HtmlSerialiser;
using System.Text.RegularExpressions;


var html = await Load("https://hebrewbooks.org/beis");

var cleanHtml = new Regex("\\s{2,}").Replace(html, "");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

var root = BuildHtmlObjectsTree(htmlLines);



//MatchCollection att;
//foreach (var line in htmlLines)
//    att = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line);

//var htmlElement = "<div id=\"til\" class=\"class-1 class-2\" width=\"100%\">title</div>";

//var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlElement);


Console.ReadLine();

async Task<string> Load(string url)
{
    var client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

HtmlElement BuildHtmlObjectsTree(IEnumerable<string> htmlLines)
{
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
            foreach (var attr in attributes)
            {
                //TODO
                var temp = attr;
                Console.WriteLine(temp);
            }

            if (!HtmlHelper.Instance.HtmlVoidTags.Contains(firstWord) && line[^1] != '/')
                current = element;
        }
        else
            current.InnerHtml = line;
    }

    return root;
}