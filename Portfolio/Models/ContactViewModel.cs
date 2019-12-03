using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ContactViewModel
    {
        //just building a class basically - fields, properties, ctors, method
        //set up error messages for contact page
        //first and last name
        [Required(ErrorMessage = "* Required")]
        public string name { get; set; }

        //email
        [Required(ErrorMessage = "* Required")]
        [EmailAddress(ErrorMessage = "* Please enter a valid email")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")]
        public string email { get; set; }

        //subject
        [StringLength(100, ErrorMessage = "*Less than 100 characters")]
        public string subject { get; set; }

        //message
        [Required(ErrorMessage = "* Required")]
        public string message { get; set; }
    }
}