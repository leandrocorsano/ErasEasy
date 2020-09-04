using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ErasEasyLife.Models
{
    [Serializable]
    [DataContract]
    public class CambioPass
    {
        //[DataMember]
        //[Required]
        //[DataType(DataType.Password)]
        //[DisplayName("Password attuale")]

        //public string password { get; set; }
        [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        [MinLength(6, ErrorMessage = "Password must contain at least 6 alphanumeric characters")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "Password must contain at least 1 uppercase letter, 1 lowercase letter, and 1 number, and must be at least 6 characters long")]
        public string nuova_pass { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("nuova_pass", ErrorMessage = "New password and confirm password do not match")]
        public string conferma_pass { get; set; }


    }
}