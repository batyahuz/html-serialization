

namespace HtmlSerialiser
{
    internal class HtmlElement
    {
        public string? Id { get; set; } = null;

        public string? Name { get; set; } = null;

        public List<ObjectKV> Attributes { get; set; } = new();

        public List<string> Classes { get; set; } = new();

        public string? InnerHtml { get; set; } = null;

        public HtmlElement? Parent { get; set; }

        public List<HtmlElement> Children { get; set; } = new();


        public IEnumerable<HtmlElement> Descendants()
        {
            var queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            var list = new List<HtmlElement>();
            while (queue.ToArray().Length > 0)
            {
                var first = queue.Dequeue();
                //yield return first;
                list.Add(first);
                foreach (var child in first.Children)
                    queue.Enqueue(child);
            }
            return list;
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            var current = this;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }

        public override string ToString()
        {
            var str = $"<{Name} id=\"{Id}\" class=\"";
            Classes.ForEach(c => str += c + " ");
            str += "\" ";
            Attributes.ForEach(a => str += a + " ");
            return str + $" >{InnerHtml}</{Name}>";
        }

        internal class ObjectKV
        {
            public string Key { get; set; }
            public string Value { get; set; }

            public ObjectKV(params string[] attributeKV)
            {
                Key = attributeKV[0];
                Value = attributeKV[1];
            }
            public override string ToString()
            {
                return $"{Key}=\"{Value}\"";
            }
        }
    }
}
