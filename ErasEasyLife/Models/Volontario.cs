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

    public class Volontario
    {

        [DataMember]
        [Required(ErrorMessage ="Required volunteer ID")]
        [DisplayName("ID")]
        public int IdVolont { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required volunteer first name")]
        [DisplayName("First name")]
        public string nome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required volunteer last name")]
        [DisplayName("Last name")]
        public string cognome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required volunteer birth of date")]
        [DataType(DataType.Date, ErrorMessage = "La data di nascita non è valida")]
        [DisplayName("Date of birth")]
        public string data_n { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required volunteer email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Required volunteer email")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please insert a valid email")]
        [DisplayName("Email")]
        public string email { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required volunteer phone number")]
        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string telefono { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required volunteer date of registration")]
        [DataType(DataType.Date, ErrorMessage = "Registration date not valid")]
        [DisplayName("Registration date")]
        public string data_iscr { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required password")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 alphanumeric characters")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Password must contain at least 1 uppercase letter, 1 lowercase letter, and 1 number, and must be at least 6 characters long")]
        [DisplayName("Password")]
        public string password { get; set; }

        [DataMember]
        
        public string ruolo { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Write your membership association")]
        [DisplayName("Association")]
        public int ass { get; set; }
    }
}