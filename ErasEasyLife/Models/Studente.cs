//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace ErasEasyLife.Models
{
    [Serializable]
    [DataContract]
    public class Studente
    {
        /*
        *  Model contente i campi dello studente nel db
        */

        [DataMember]
        [Required(ErrorMessage ="Required student ID")]
        [DisplayName("ID")]
        public int IdStud { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required student first name")]
        [DisplayName("First Name")]
        public string nome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required student last name")]
        [DisplayName("Last Name")]
        public string cognome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required student email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please insert a valid email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please insert a valid email")]
        [DisplayName("Email")]
        public string email { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required student phone number")]
        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string tel { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required student date of birth")]
        [DataType(DataType.Date, ErrorMessage = "Birth date not valid")]
        [DisplayName("Date of birth")]

        public string data_n { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required student city")]
        [DisplayName("City")]
        public string citta { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required student state")]
        [DisplayName("State")]
        public string stato { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required student nationality")]
        [DisplayName("Nationality")]
        public string nazionalita { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required Password ")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 alphanumeric characters")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Password must contain at least 1 uppercase letter, 1 lowercase letter, and 1 number, and must be at least 6 characters long")]

        [DisplayName("Password")]
        public string password { get; set; }

        [DataMember]
        [DisplayName("Instagram")]
        [Url]
        public string instagram { get; set; }

        [DataMember]
        [DisplayName("Facebook")]
        [Url]
        public string facebook { get; set; }
    
    }
}