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
    public class Volontario
    {
        public Volontario(int IdVolont, string nome, string cognome, string data_n, string email, string telefono, string data_iscr, string password,  Associazione ass, string ruolo = "")
        {
            this.IdVolont = IdVolont;
            this.nome = nome;
            this.cognome = cognome;
            this.data_n = data_n;
            this.email = email;
            this.telefono = telefono;
            this.data_iscr = data_iscr;
            this.password = password;
            this.ruolo = ruolo;
            this.ass = ass; //associazione

        }
        [DataMember]
        public int IdVolont { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string cognome { get; set; }
        [DataMember]
        public string data_n { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string data_iscr { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string ruolo { get; set; }
        [DataMember]
        public Associazione ass { get; set; }

    }
}
