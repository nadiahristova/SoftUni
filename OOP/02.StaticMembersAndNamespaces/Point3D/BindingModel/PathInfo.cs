namespace Point3D.BindingModel
{
    using Models;

    using Newtonsoft.Json;    

    class PathInfo
    {
        [JsonProperty("Path Name")]
        public string PathName { get; set; }

        [JsonProperty("About")]
        public string PathDescription { get; set; }

        [JsonProperty("Path")]
        public Point3D[] Path { get; set; }
    }
}
