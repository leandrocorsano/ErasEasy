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
    public class Frequentazione
    {
        public Frequentazione(Studente stud, Universita Uni, string tipo)
        {
            this.studente = stud;
            this.universita = Uni;
            this.tipo = tipo;

        }
        [DataMember]
        public Studente studente { get; set; }
        [DataMember]
        public Universita universita { get; set; }
        [DataMember]
        public string tipo { get; set; }
    }
}
