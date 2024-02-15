using HtmlSerialization;
using static HtmlSerialization.HtmlSerializer;

var html = await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/");
var dom = Serialize(html);

var result1 = dom.Query(Selector.FromString("div.md-grid"));

Console.WriteLine("----result1----");
result1.ToList().ForEach(r => Console.WriteLine(r));

var result2 = dom.Query(Selector.FromString("#_1"));

Console.WriteLine("----result2----");
result2.ToList().ForEach(r => Console.WriteLine(r));

var result3 = dom.Query(Selector.FromString("title"));

Console.WriteLine("----result3----");
result3.ToList().ForEach(r => Console.WriteLine(r));

Console.ReadLine();