using HtmlSerialiser;
using static HtmlSerialiser.HtmlSerializer;

//HtmlSerialization
// https://hebrewbooks.org/beis
//https://glat-chat.onrender.com/
var html = await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/#_4");
var dom = Serialize(html);
//body #root .bg-slate-100.flex.h-full.relative div div div div svg
//body div div h2
//.md-header.md-header--shadow.md-header--lifted div
var result = dom.Query(Selector.FromString(".md-grid"));

result.ToList().ForEach(r => Console.WriteLine(r));

Console.ReadLine();