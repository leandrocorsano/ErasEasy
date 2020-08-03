using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_wcf.Classi
{
    public class Luogo
    {
        public Luogo(int IdLuogo, string citta, string via, string stato)
        {
            this.IdLuogo = IdLuogo;
            this.citta = citta;
            this.via = via;
            this.stato = stato;
        }
        public int IdLuogo { get; set; }
        public string citta { get; set; }
        public string via { get; set; }
        public string stato { get; set; }
    }
}
