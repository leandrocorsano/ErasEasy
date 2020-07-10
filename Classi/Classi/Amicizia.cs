using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Amicizia
    {
        public Amicizia(int IdStud1, int IdStud2, bool confermata)
        {
            this.IdStud1 = IdStud1;
            this.IdStud2 = IdStud2;
            this.confermata = confermata;
        }
        public int IdStud1 { get; set; }
        public int IdStud2 { get; set; }
        public bool confermata { get; set; }
    }
}
