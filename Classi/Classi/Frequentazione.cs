using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Frequentazione
    {
        public Frequentazione(int IdStud, int IdUni, string tipo)
        {
            this.IdStud = IdStud;
            this.IdUni = IdUni;
            this.tipo = tipo;

        }
        public int IdStud { get; set; }
        public int IdUni { get; set; }
        public string tipo { get; set; }
    }
}
