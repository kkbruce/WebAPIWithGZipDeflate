namespace WebAPIWithGZipDeflate.Models
{
    /// <summary>
    /// From: https://www.json-generator.com/
    /// </summary>
    public class DataModel
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string _id { get; set; }
        public int index { get; set; }
        public string guid { get; set; }
        public bool isActive { get; set; }
        public string balance { get; set; }
        public string picture { get; set; }
        public int age { get; set; }
        public string eyeColor { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string company { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string about { get; set; }
        public string registered { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string[] tags { get; set; }
        public Friend[] friends { get; set; }
        public string greeting { get; set; }
        public string favoriteFruit { get; set; }
    }

    public class Friend
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}