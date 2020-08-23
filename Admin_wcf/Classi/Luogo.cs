using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Admin_wcf.Classi
    
{
    [DataContract]
    public class Luogo
    {
        public Luogo(int IdLuogo, string citta, string via, string stato)
        {
            this.IdLuogo = IdLuogo;
            this.citta = citta;
            this.via = via;
            this.stato = stato;
        }
        [DataMember]
        public int IdLuogo { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string via { get; set; }
        [DataMember]
        public string stato { get; set; }
    }
}
