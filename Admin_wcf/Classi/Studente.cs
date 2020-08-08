using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Admin_wcf.Classi
{
    [DataContract]
    public class Studente
    {
        
        public Studente(int IdStud, string nome, string cognome, string email, string tel, string data_n, string citta, string stato, string nazionalita, string password, string instagram="", string facebook="")
        {
            this.IdStud = IdStud;
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.tel = tel;
            this.data_n = data_n;
            this.citta = citta;
            this.stato = stato; 
            this.nazionalita = nazionalita;
            this.password = password;
            this.instagram = instagram;
            this.facebook = facebook;

        }
        [DataMember]
        public int IdStud { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string cognome { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string tel { get; set; }
        [DataMember]
        public string data_n { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string stato { get; set; }
        [DataMember]
        public string nazionalita { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string instagram { get; set; }
        [DataMember]
        public string facebook { get; set; }
    }
}
