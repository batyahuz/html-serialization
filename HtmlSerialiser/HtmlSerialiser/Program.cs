using HtmlSerialiser;
using System.Text.RegularExpressions;


var html = await Load("https://hebrewbooks.org/beis");

var cleanHtml = new Regex("\\s{2,}").Replace(html, "");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

HtmlElement root = new() { Name = "html" }, current = root;

foreach (var line in htmlLines)
{
    if (line.Equals("/html"))
    {
        Console.WriteLine("end of document!! :)");
        break;
    }
    if (line[0] == '/')
    {
        current = current.Parent;
    }
    var firstWord = line[..line.IndexOf(' ')];
    if (HtmlHelper.Instance.HtmlTags.Contains(firstWord))
    {

    }

}

//MatchCollection att;
//foreach (var line in htmlLines)
//    att = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line);

//var htmlElement = "<div id=\"til\" class=\"class-1 class-2\" width=\"100%\">title</div>";

//var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlElement);


Console.ReadLine();

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
