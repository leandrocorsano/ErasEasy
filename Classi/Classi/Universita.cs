using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Universita
    {
        public Universita(int IdUni, string nome, string citta, string stato)
        {
            this.IdUni = IdUni;
            this.nome = nome;
            this.citta = citta;
            this.stato = stato;
        }
        public int IdUni { get; set; }
        public string nome { get; set; }
        public string citta { get; set; }
        public string stato { get; set; }
    }
}
