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
    public class Evento
    {
        [DataMember]
        public int IdEv { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Il nome dell'evento è obbligatorio")]
        [DisplayName("Nome")]
        public string nome { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Inserire la tipologia")]
        [DisplayName("Tipologia")]
        public string tipologia { get; set; }
        [DataMember]
        [Required(ErrorMessage ="Inserire il numero minimo di partecipanti")]
        [DisplayName("Numero minimo di partecipanti")]
        public int min_p { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Inserire il numero massimo di partecipanti")]
        [DisplayName("Numero massimo di partecipanti")]
        public int max_p { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Inserire il numero minimo di volontari da assegnare")]
        [DisplayName("Numero minimo di volontari")]
        public int min_v { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Inserire il numero massimo di volontari da assegnare")]
        [DisplayName("Numero massimo di volontari")]
        public int max_v { get; set; }
        [DataMember]
        [DisplayName("Costo")]
        public int costo { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Inserire una descrizione")]
        [DisplayName("Descrizione")]
        public string descrizione { get; set; }
        [DataMember]
        public int ass { get; set; }

    }
}