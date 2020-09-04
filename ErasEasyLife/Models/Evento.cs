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
        [Required(ErrorMessage = "Required event number")]
        [DisplayName("Event number")]
        public int IdEv { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required event name")]
        [DisplayName("Name")]
        public string nome { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Insert the type")]
        [DisplayName("Type")]
        public string tipologia { get; set; }
        [DataMember]
        [Required(ErrorMessage ="Insert the minimum number of partecipants")]
        [DisplayName("Minimum number of partecipants")]
        public int min_p { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Insert the maximum number of partecipants")]
        [DisplayName("Maximum number of partecipants")]
        public int max_p { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Insert the minumum number of volunteers to assign")]
        [DisplayName("Minimum number of volunteers")]
        public int min_v { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Insert the maximum number of volunteers to assign")]
        [DisplayName("Maximum number of volunteers")]
        public int max_v { get; set; }
        [DataMember]
        [DisplayName("Cost")]
        public int costo { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Insert a description")]
        [DisplayName("Description")]
        public string descrizione { get; set; }
        
        [DataMember]
        [Required(ErrorMessage = "Required start time")]
        [DisplayName("Start time")]
        [DataType(DataType.Time, ErrorMessage = "Time format not valid")]
        public string ora_i { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Required end time")]
        [DisplayName("End time")]
        [DataType(DataType.Time, ErrorMessage = "Time format not valid")]
        public string ora_f { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required start date")]
        [DisplayName("Start date")]
        [DataType(DataType.Date, ErrorMessage = "Date format not valid")]
        public string data_i { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required end date")]
        [DisplayName("End date")]
        [DataType(DataType.Date, ErrorMessage = "Date format not valid")]
        public string data_f { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required place ID")]
        [DisplayName("IDluogo")]
        public int IdLuogo { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required city")]
        [DisplayName("City")]
        public string citta { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required address")]
        [DisplayName("Address")]
        public string via { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Required state")]
        [DisplayName("State")]
        public string stato { get; set; }


    }
}