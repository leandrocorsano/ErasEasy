using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ErasEasyLife.Models
{
    public class CambioPass
    {
        [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password attuale")]

        public string password { get; set; }
        [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Nuova password")]
        [MinLength(6, ErrorMessage = "La password de contenere almeno 6 caratteri alfanumerici")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "La password deve contenere almeno 1 lettera maiuscola, 1 minuscola e 1 numero e deve essere di almeno 6 caratteri")]
        public string nuova_pass { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Conferma password")]
        [Compare("nuova_pass", ErrorMessage = "Nuova password e conferma password non coincidono")]
        public string conferma_pass { get; set; }


    }
}