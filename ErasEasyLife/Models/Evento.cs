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
        [Required(ErrorMessage = "Il nome dell'evento è obbligatorio")]
        [DisplayName("Numero evento")]
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
        [Required(ErrorMessage ="Inserire il numero minimo di studenti")]
        [DisplayName("Numero minimo di partecipanti")]
        public int min_p { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Inserire il numero massimo di studenti")]
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
        [Required(ErrorMessage = "L'ora di inzio dell'evento è obbligatoria")]
        [DisplayName("Ora inizio")]
        [DataType(DataType.Time, ErrorMessage = "Formato ora non valido")]
        public string ora_i { get; set; }

        [DataMember]
        [Required(ErrorMessage = "L'ora di fine dell'evento è obbligatoria")]
        [DisplayName("Ora fine")]
        [DataType(DataType.Time, ErrorMessage = "Formato ora non valido")]
        public string ora_f { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La data d'inizio  dell'evento è obbligatoria")]
        [DisplayName("Data inizio")]
        [DataType(DataType.Date, ErrorMessage = "Formato data non valido")]
        public string data_i { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La data di fine dell'evento è obbligatoria")]
        [DisplayName("Data fine")]
        [DataType(DataType.Date, ErrorMessage = "Formato data non valido")]
        public string data_f { get; set; }
        [DataMember]
        [Required(ErrorMessage = "L'id del luogo è obbligatorio")]
        [DisplayName("IDluogo")]
        public int IdLuogo { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La città dell'evento è obbligatoria")]
        [DisplayName("Città")]
        public string citta { get; set; }
        [DataMember]
        [Required(ErrorMessage = "La via dell'evento è obbligatoria")]
        [DisplayName("Via")]
        public string via { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lo stato dove si svolge l'evento è obbligatoria")]
        [DisplayName("Stato")]
        public string stato { get; set; }


    }
}