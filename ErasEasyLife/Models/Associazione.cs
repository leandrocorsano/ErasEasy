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
        public int IdAss { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Il nome dell'associazione è obbligatorio")]
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La città dell'associazione è obbligatoria")]
        [Display(Name = "Città")]
        public string citta { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lo stato dell'associazione è obbligatorio")]
        [DisplayName("Stato")]
        public string stato { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La via della sede è obbligatoria")]
        [DisplayName("Via")]
        public string via { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Il cellulare è obbligatorio")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string tel { get; set; }
        [DataMember]
        [Required(ErrorMessage = "L'email è obbligatoria")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Perfavore inserisci un email valida") ]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Perfavore inserisci una mail valida")]
        public string email { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Password obbligatoria")]
        [DisplayName("Password")]
        [MinLength(6, ErrorMessage = "La password de contenere almeno 6 caratteri alfanumerici")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "La password deve contenere almeno 1 lettera maiuscola, 1 minuscola e 1 numero e deve essere di almeno 6 caratteri")]
        //[PasswordPropertyText] //per mettere gli asterischi
        public string password { get; set; }
    }
}
