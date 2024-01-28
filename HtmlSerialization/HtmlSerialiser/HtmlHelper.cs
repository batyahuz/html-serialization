﻿using System.Text.Json;

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
            string jsonTags = File.ReadAllText("Json-Files/HtmlTags.json");
            HtmlTags = (string[])JsonSerializer.Deserialize(jsonTags, typeof(string[]));

            var jsonVoidTags = File.ReadAllText("Json-Files/HtmlVoidTags.json");
            HtmlVoidTags = (string[])JsonSerializer.Deserialize(jsonVoidTags, typeof(string[]));
        }
    }
}