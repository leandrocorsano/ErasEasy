using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_wcf.Classi
{
    public class Svolgimento
    {
        public Svolgimento(Evento IdEv, int IdLuogo, int ora_i, int ora_f, int data_i, int data_f)
        {
            this.IdEv = IdEv;
            this.IdLuogo = IdLuogo;
            this.ora_i = ora_i;
            this.ora_f = ora_f;
            this.data_i = data_i;
            this.data_f = data_f;
        }
        public Evento IdEv { get; set; }
        public int IdLuogo { get; set; }
        public int ora_i { get; set; }
        public int ora_f { get; set; }
        public int data_i { get; set; }
        public int data_f{ get; set; }
    }
}
