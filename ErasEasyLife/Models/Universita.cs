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

    public class Universita
    {
        [DataMember]
        [Required(ErrorMessage = "Required University ID")]
        [DisplayName("ID")]
        public int IdUni { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required University name")]
        [DisplayName("Name")]
        public string nome { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required University city")]
        [DisplayName("City")]
        public string citta { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required University state")]
        [DisplayName("State")]
        public string stato { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required University type")]
        [DisplayName("Type")]
        public string type { get; set; }

    }
}