using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Gestione
    {
        public Gestione(int IdVolont, int IdEv)
        {
            this.IdVolont = IdVolont;
            this.IdEv = IdEv;
        }
        public int IdVolont { get; set; }
        public int IdEv{ get; set; }
    }
}
