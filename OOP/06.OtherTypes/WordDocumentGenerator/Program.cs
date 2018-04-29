namespace WordDocumentGenerator
{
    using Managers;
    using Enums;

    class Program
    {
        static void Main()
        {
            DocXManager.FileName = "SoftUni RPG Contest";
            DocXManager.CreateDocument();

            string headLine = "SoftUni OOP Game Contest";
            DocXManager.AddParagraph(headLine, 18, 5, Font.ArialBlack, true);            

            string paragraph = @"SoftUni is organizing a contest for the best role playing" +
@"game from the OOP teamwork projects. The winning teams will receive a grand prize! 
The game should be:";

            DocXManager.AddImage("rpg-game");

            DocXManager.AddParagraph(paragraph, 10, 1, Font.Arial);

            string[] bulletList= { "Properly structured and follow all good OOP practices", "Awesome", "..Very Awesome" };
            DocXManager.AddUList(bulletList, 10, 1, Font.Arial);

            string[][] table = new string[4][];
            table[0] = new string[] { "Team", "Game", "Points"};
            for (int i = 1; i < table.GetLength(0); i++)
                table[i] = new string[] { "-", "-", "-" };

            DocXManager.AddTable(table, 10, Font.Arial); 
            paragraph = "The top 3 teams will receive a SPECTACULAR prize:";
            DocXManager.AddParagraph(paragraph, 10, 5, Font.Arial, true);

            //DocXManager.AddHyperLink("A HANDSHAKE FROM NAKOV", "http://www.nakov.com");
        }
    }
}
