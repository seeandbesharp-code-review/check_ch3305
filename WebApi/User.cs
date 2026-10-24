using System.ComponentModel.DataAnnotations;



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

