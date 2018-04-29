namespace Point3D.Manipulators
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using Models;
    using BindingModel;    

    using Newtonsoft.Json;   

    public static class Storage
    {
        public const string DefaultFolderName = "3DPaths";
        public static readonly string DefaultFolderPath = Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "");        
        private const string FileExtension = "json";

        private const string DefaultPathDetails = "No info provided.";

        public static bool WritePath(Path3D pathInSpace, string pathName, string pathDescription = null, string outputFolderName = null, string ouputFolderPath = null)
        {
            outputFolderName = outputFolderName ?? DefaultFolderName;
            ouputFolderPath = ouputFolderPath ?? DefaultFolderPath;

            var directory = Directory.CreateDirectory(Path.Combine(ouputFolderPath, outputFolderName)); //if it doesn't exist create directory
            string filePath = Path.Combine(ouputFolderPath, outputFolderName, pathName + "." + FileExtension);

            if (string.IsNullOrWhiteSpace(pathName) || File.Exists(filePath))
                return false;

            var pathInfo = new PathInfo()
            {
                PathName = pathName,
                PathDescription = pathDescription ?? DefaultPathDetails,
                Path = pathInSpace.Points
            };

            string obj = JsonConvert.SerializeObject(pathInfo, Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(filePath))
                writer.Write(obj);

            return true;        
        }

        public static string ReadPath(string pathName, string folderPath = null)
        {
            string filePath = (folderPath == null)
                ? Path.Combine(DefaultFolderPath, DefaultFolderName, pathName.Trim() + "." + FileExtension) 
                : Path.Combine(folderPath, pathName.Trim() + "." + FileExtension);

            if (!File.Exists(filePath))
                return "File does not exist!";

            PathInfo pathInfo;

            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                pathInfo = (PathInfo)serializer.Deserialize(file, typeof(PathInfo));
            }

            var pathOutput = new StringBuilder();

            pathOutput.AppendLine("Path " + pathInfo.PathName + ":");
            string data = pathInfo != null ? string.Join("\n", pathInfo.Path.ToList()) : "This file does not contain data about point path.";
            pathOutput.AppendLine(data);
            pathOutput.AppendLine("\nAdditional information:");
            pathOutput.AppendLine(pathInfo.PathDescription);

            return pathOutput.ToString();
        }
    }
}
