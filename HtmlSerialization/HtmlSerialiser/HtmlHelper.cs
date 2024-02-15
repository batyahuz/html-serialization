using System.Text.Json;

namespace HtmlSerialization
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new();

        public static HtmlHelper Instance => _instance;

        public string[] HtmlTags { get; set; }

        public string[] HtmlVoidTags { get; set; }

        private HtmlHelper()
        {
            HtmlTags = (string[])JsonSerializer.Deserialize(File.ReadAllText("Json-Files/HtmlTags.json"), typeof(string[]));
            HtmlVoidTags = (string[])JsonSerializer.Deserialize(File.ReadAllText("Json-Files/HtmlVoidTags.json"), typeof(string[]));
        }
    }
}
