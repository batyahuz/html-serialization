using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerialiser
{
    internal class HtmlElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Dictionary<string, string>> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }


    }
}
