using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_wcf.Classi
{
    public class Partecipazione
    {
        public Partecipazione(Studente IdStud, Evento IdEv)
        {
            this.IdStud = IdStud;
            this.IdEv = IdEv;
        }
        public Studente IdStud { get; set; }
        public Evento IdEv { get; set; }
    }
}
