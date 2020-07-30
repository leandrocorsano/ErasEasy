using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Frequentazione
    {
        public Frequentazione(Studente IdStud, Universita IdUni, string tipo)
        {
            this.IdStud = IdStud;
            this.IdUni = IdUni;
            this.tipo = tipo;

        }
        public Studente IdStud { get; set; }
        public Universita IdUni { get; set; }
        public string tipo { get; set; }
    }
}
