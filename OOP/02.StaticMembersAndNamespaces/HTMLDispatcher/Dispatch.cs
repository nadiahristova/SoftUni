namespace HTMLDispatcher
{
    using System;

    using HTMLBuilders;

    class Dispatch
    {
        public static void Main()
        {
            ElementBuilder div = new ElementBuilder("div");
            div.AddAttribute("id", "page");
            div.AddAttribute("class", "big");
            div.AddContent("<p>Hello</p>");
            Console.WriteLine(div * 2);

            Console.WriteLine();

            Console.WriteLine(HTMLDispatcher.CreateImage("MyDocuments/Pictures", "Beach", "California"));
            Console.WriteLine(HTMLDispatcher.CreateURL("www.metallica.com", "Metallica", "Official Metallica Website"));
            Console.WriteLine(HTMLDispatcher.CreateInput("button", "login", "Enter"));
        }
    }
}
