using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Gestione
    {
        public Gestione(Volontario IdVolont, Evento IdEv)
        {
            this.IdVolont = IdVolont;
            this.IdEv = IdEv;
        }
        public Volontario IdVolont { get; set; }
        public Evento IdEv{ get; set; }
    }
}
