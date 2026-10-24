using System.ComponentModel.DataAnnotations;


    public class LoginUser
    {
        
        //public int id { get; set; }
        [Required]
        public int password { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }

