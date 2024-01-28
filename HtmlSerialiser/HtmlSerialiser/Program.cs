using HtmlSerialization;
using static HtmlSerialization.HtmlSerializer;


var html = await Load("https://hebrewbooks.org/beis");
var dom = Serialize(html);

var result = dom.Query(Selector.FromString(""));

result.ToList().ForEach(r => Console.WriteLine(r));

Console.ReadLine();