using HtmlSerialization;
using static HtmlSerialization.HtmlSerializer;

var html = await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/#_4");
var dom = Serialize(html);

var result = dom.Query(Selector.FromString("header nav.md-grid a"));

result.ToList().ForEach(r => Console.WriteLine(r));

Console.ReadLine();