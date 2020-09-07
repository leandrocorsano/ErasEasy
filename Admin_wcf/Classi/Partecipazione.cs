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
    public class Partecipazione
    {
        public Partecipazione(Studente IdStud, Evento IdEv)
        {
            this.IdStud = IdStud;
            this.IdEv = IdEv;
        }
        [DataMember]
        public Studente IdStud { get; set; }
        [DataMember]
        public Evento IdEv { get; set; }
    }
}
