

namespace HtmlSerialization
{
    internal class HtmlElement
    {
        public string? Id { get; set; } = null;

        public string? Name { get; set; } = null;

        public List<string> Attributes { get; set; } = new();

        public List<string> Classes { get; set; } = new();

        public string? InnerHtml { get; set; } = "";

        public HtmlElement? Parent { get; set; }

        public List<HtmlElement> Children { get; set; } = new();


        public IEnumerable<HtmlElement> Descendants()
        {
            var queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.ToArray().Length > 0)
            {
                var first = queue.Dequeue();
                yield return first;

                foreach (var child in first.Children)
                    queue.Enqueue(child);
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            for (var curr = this; curr != null; curr = curr.Parent)
                yield return curr;
        }

        public override string ToString()
        {
            var str = $"<{Name} id=\"{Id}\" class=\"";
            Classes.ForEach(c => str += c + " ");
            str = str[..^1];
            str += "\" ";
            Attributes.ForEach(a => str += a + " ");
            return str + $">{InnerHtml}</{Name}>";
        }

    }
}
