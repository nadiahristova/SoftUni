namespace WordDocumentGenerator.Managers
{
    using Enums;
    using Novacode;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;    

    public static class DocXManager  
    {
        private const string DefaultFolderName = "WordDocs";
        private static readonly string DefaultFolderPath = Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "");
        private const string FileExtension = "docx";

        private const string DefaultResourcesFolderName = "Resources";
        private const string DefaultResourcesFolderPath = @"../../";
        private const string ImageFileExtension = "png";

        public static string FileName { get; set; }
        public static string FolderName { get; set; }
        public static string FolderPath { get; set; }

        public static bool CreateDocument(string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        {
            IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

            var directory = Directory.CreateDirectory(Path.Combine(file["path"], file["folder"])); //if it doesn't exist create directory           

            if (string.IsNullOrWhiteSpace(file["name"]) || File.Exists(file["fullPath"]))
                return false;

            using (var doc = DocX.Create(file["fullPath"]))
                  doc.Save();

            return true;
        }

        private static IDictionary<string, string> ReturnFileMembers(string fileName, string ouputFolderPath, string outputFolderName)
        {
            IDictionary<string, string> members = new Dictionary<string, string>();

            members["name"] = fileName ?? FileName;
            members["path"] = ouputFolderPath ?? FolderPath ?? DefaultFolderPath;
            members["folder"] = outputFolderName ?? FolderName ?? DefaultFolderName;
            members["fullPath"] = Path.Combine(members["path"], members["folder"], members["name"] + "." + FileExtension);

            return members;
        }

        public static bool AddParagraph(string paragraph, int fontSize, int position, Font font, bool isHeadline = false, string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        {
            IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

            if (string.IsNullOrWhiteSpace(file["name"]) || !File.Exists(file["fullPath"]))
                return false;

            using (var document = DocX.Load(file["fullPath"]))
            {
                var paragraphFormat = new Formatting();
                paragraphFormat.FontFamily = new System.Drawing.FontFamily(ToEnumString(font));
                paragraphFormat.Size = fontSize;
                paragraphFormat.Position = position;

                var prgph = document.InsertParagraph(paragraph, false, paragraphFormat);
                if (isHeadline)
                    prgph.Alignment = Alignment.center;

                document.InsertParagraph();//for space

                document.Save();
            }
            
            return true;
        }

        //public static bool AddHyperLink(string linkText, string href, string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        //{
        //    IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

        //    if (string.IsNullOrWhiteSpace(file["name"]) || !File.Exists(file["fullPath"]))
        //        return false;

        //    using (var document = DocX.Load(file["fullPath"]))
        //    {

        //        document.AddHyperlink(linkText, new Uri(href));//this doesn't seem to work

        //        document.Save();
        //    }

        //    return true;
        //}

        public static bool AddImage(string imgName, string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        {
            var imgPath = Path.Combine(DefaultResourcesFolderPath, DefaultResourcesFolderName, imgName + "." + ImageFileExtension);
            IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

            if (string.IsNullOrWhiteSpace(file["name"]) || !File.Exists(file["fullPath"]) || !File.Exists(imgPath))
                return false;

            using (var document = DocX.Load(file["fullPath"]))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    System.Drawing.Image myImg = System.Drawing.Image.FromFile(imgPath);

                    myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                    ms.Seek(0, SeekOrigin.Begin);
                    
                    Novacode.Image img = document.AddImage(ms); // Create image.

                    var paragraphFormat = new Formatting();
                    paragraphFormat.Position = 5;

                    Paragraph p = document.InsertParagraph("", false, paragraphFormat);                    

                    Picture pic1 = img.CreatePicture();     // Create picture.                    
                    var picDimensions = ReturnPicDimensions(document, pic1);
                    pic1.Width = picDimensions[0];
                    pic1.Height = picDimensions[1];

                    p.InsertPicture(pic1, 0); // Insert picture into paragraph.                    
                    p.Alignment = Alignment.center;

                    document.Save();
                }
            }          

            return true;
        }

        public static bool AddUList(string[] listElements, int fontSize, int position, Font font, bool isHeadline = false, string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        {
            IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

            if (string.IsNullOrWhiteSpace(file["name"]) || !File.Exists(file["fullPath"]))
                return false;

            using (var document = DocX.Load(file["fullPath"]))
            {
                var list = document.AddList(listType: ListItemType.Bulleted);
                foreach(var item in listElements)
                    document.AddListItem(list, item, 1, ListItemType.Bulleted);

                document.InsertList(list);
                document.InsertParagraph();

                document.Save();
            }

            return true;
        }

        public static bool AddTable(string[][] tableBody, int fontSize, Font font, string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        {
            IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

            if (string.IsNullOrWhiteSpace(file["name"]) || !File.Exists(file["fullPath"]))
                return false;

            using (var document = DocX.Load(file["fullPath"]))
            {
                var table = document.AddTable(tableBody.GetLength(0), tableBody[0][0].Length-1);
                table.Rows[0].TableHeader = true;

                for (int row = 0; row < table.RowCount; row++)
                    for (int col = 0; col < table.ColumnCount; col++)
                        table.Rows[row].Cells[col].Paragraphs.First().Append(tableBody[row][col]);

                document.InsertParagraph();
                document.InsertTable(table);
                document.InsertParagraph();

                document.Save();
            }

            return true;
        }


        private static int[] ReturnPicDimensions(DocX document, Picture pic1)
        {
            int[] dimensions = new int[2];

            var widthPx = pic1.Width;
            var heightPx = pic1.Height;

            var docWidth = document.PageWidth;

            dimensions[0] = (int)docWidth - 140;
            dimensions[1] = dimensions[0] * heightPx / widthPx;

            return dimensions;
        }

        public static bool OpenDocument(string fileName = null, string ouputFolderPath = null, string outputFolderName = null)
        {
            IDictionary<string, string> file = ReturnFileMembers(fileName, ouputFolderPath, outputFolderName);

            if (string.IsNullOrWhiteSpace(file["name"]) || File.Exists(file["fullPath"]))
                return false;

            Process.Start("WINWORD.EXE", file["fullPath"]);

            return true;
        }

        public static string ToEnumString<T>(T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), false)).FirstOrDefault();
            if (enumMemberAttribute != null)
                return enumMemberAttribute.Value;
            else
                return name;
        }
    }
}
