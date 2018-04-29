namespace HTMLDispatcher.HTMLBuilders
{
    static class HTMLDispatcher
    {
        public static string CreateImage(string source, string alt, string title)
        {
            ElementBuilder img = new ElementBuilder("img");

            img.AddAttribute("src", source);
            img.AddAttribute("alt", alt);
            img.AddAttribute("title", title);

            return img.ToString();
        }

        public static string CreateURL(string url, string title, string text)
        {
            ElementBuilder link = new ElementBuilder("a");

            link.AddAttribute("href", url);
            link.AddContent(text);
            link.AddAttribute("title", title);

            return link.ToString();
        }

        public static string CreateInput(string inputType, string name, string text)
        {
            ElementBuilder input = new ElementBuilder("input");

            input.AddAttribute("type", inputType);
            input.AddAttribute("name", name);
            input.AddContent(text);

            return input.ToString();
        }
    }
}
