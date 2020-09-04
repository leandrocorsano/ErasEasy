using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/* https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations */
namespace ErasEasyLife.Models
{
    [Serializable]
    [DataContract]
    public class Associazione
    {
        [DataMember]
        [Required(ErrorMessage = "Required association ID")]
        [DisplayName("ID")]
        public int IdAss { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required association name")]
        [Display(Name = "Name")]
        public string nome { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required association city")]
        [Display(Name = "City")]
        public string citta { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required association state")]
        [DisplayName("State")]
        public string stato { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required association address")]
        [DisplayName("Address")]
        public string via { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required association phone")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string tel { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required association email")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please insert a valid email") ]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please insert a valid email")]
        public string email { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required password")]
        [DisplayName("Password")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 alphanumeric characters")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Password must contain at least 1 uppercase letter, 1 lowercase letter, and 1 number, and must be at least 6 characters long")]
        //[PasswordPropertyText] //per mettere gli asterischi
        public string password { get; set; }
    }
}
