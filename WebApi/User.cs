using System.ComponentModel.DataAnnotations;



    //In C#, public properties are written in PascalCase (Email, FirstName, LastName, Password, Id).
    //If you rename them, text.txt still has the old names, so read it with
    //new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
    public class User
    {
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public int password { get; set; }
        public int id { get; set; }
    }

