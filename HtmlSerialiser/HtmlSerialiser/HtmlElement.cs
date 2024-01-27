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
        public string? Name { get; set; }
        public List<Dictionary<string, string>>? Attributes { get; set; } = new();
        public List<string> Classes { get; set; } = new();
        public string? InnerHtml { get; set; }
        public HtmlElement? Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new();

        public List<HtmlElement> Descendants()
        {
            var queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.ToArray().Length > 0)
            {
                var first = queue.Dequeue();
                //TODO
            }
            return Children;
        }

    }
}
