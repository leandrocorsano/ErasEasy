using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Amicizia
    {
        public Amicizia(Studente IdStud1, Studente IdStud2, bool confermata)
        {
            this.IdStud1 = IdStud1;
            this.IdStud2 = IdStud2;
            this.confermata = confermata;
        }
        public Studente IdStud1 { get; set; }
        public Studente IdStud2 { get; set; }
        public bool confermata { get; set; }
    }
}
