using System.ComponentModel.DataAnnotations;


    //In C#, public properties are written in PascalCase (Password, Email)
    public class LoginUser
    {
        
        //public int id { get; set; }
        [Required]
        public int password { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }

