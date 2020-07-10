using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Partecipazione
    {
        public Partecipazione(int IdStud, int IdEv)
        {
            this.IdStud = IdStud;
            this.IdEv = IdEv;
        }
        public int IdStud { get; set; }
        public int IdEv { get; set; }
    }
}
