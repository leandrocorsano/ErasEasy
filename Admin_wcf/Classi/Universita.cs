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
    public class Universita
    {
        
        public Universita(int IdUni, string nome, string citta, string stato)
        {
            this.IdUni = IdUni;
            this.nome = nome;
            this.citta = citta;
            this.stato = stato;
        }
        [DataMember]
        public int IdUni { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string stato { get; set; }
    }
}
