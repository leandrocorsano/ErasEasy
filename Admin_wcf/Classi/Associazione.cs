//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Admin_wcf.Classi
{
    [DataContract]
    public class Associazione
    {
        
        public Associazione(int IdAss, string nome, string citta, string stato, string via,  string tel, string email, string password)
        {
            this.IdAss = IdAss;
            this.nome = nome;
            this.citta = citta;
            this.stato = stato;
            this.via = via;
            this.tel = tel;
            this.email = email;
            this.password = password;
        }
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
