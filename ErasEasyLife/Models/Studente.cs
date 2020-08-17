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

        [DataMember]
        public int IdStud { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Il nome dello studente è obbligatorio")]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Il cognome dello studente è obbligatorio")]
        [DisplayName("Cognome")]
        public string cognome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "La email dello studente è obbligatoria")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Perfavore inserisci un email valida")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Perfavore inserisci una mail valida")]
        [DisplayName("Email")]
        public string email { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Il cellulare dello studente è obbligatorio")]
        [DisplayName("Telefono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string tel { get; set; }

        [DataMember]
        [Required(ErrorMessage = "La data di nascita dello studente è obbligatoria")]
        [DataType(DataType.Date, ErrorMessage = "La data di nascita non è valida")]
        [DisplayName("Data di nascita")]

        public string data_n { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La città dello studente è obbligatoria")]
        [DisplayName("Città")]
        public string citta { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lo stato dello studente è obbligatorio")]
        [DisplayName("Stato")]
        public string stato { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La nazionalità dello studente è obbligatoria")]
        [DisplayName("Nazionalità")]
        public string nazionalita { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Password obbligatoria")]
        [MinLength(6, ErrorMessage = "La password de contenere almeno 6 caratteri alfanumerici")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}", ErrorMessage = "La password deve contenere almeno 1 lettera maiuscola, 1 minuscola e 1 numero e deve essere di almeno 6 caratteri")]

        [DisplayName("Password")]
        public string password { get; set; }
        [DataMember]
        
        [DisplayName("Instagram")]
        public string instagram { get; set; }
        [DataMember]
        [DisplayName("Facebook")]
        public string facebook { get; set; }
    
    }
}