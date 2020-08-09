using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ErasEasyLife.Models
{
    [Serializable]
    [DataContract]
    public class Associazione
    {
        [DataMember]
        public int IdAss { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string stato { get; set; }
        [DataMember]
        public string via { get; set; }
        [DataMember]
        public string tel { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }
    }
}