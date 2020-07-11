using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Evento
    {
        public Evento(int IdEv, string nome, string tipologia, int min_p, int max_p, int min_v, int max_v, int costo, string descrizione, Associazione IdAss)
        {
            this.IdEv = IdEv;
            this.nome = nome;
            this.tipologia = tipologia;
            this.min_p = min_p;
            this.max_p = max_p;
            this.min_v = min_v;
            this.max_v = max_v;
            this.costo = costo;
            this.descrizione = descrizione;
            this.IdAss = IdAss;
        }
        public int IdEv { get; set; }
        public string nome { get; set; }
        public string tipologia { get; set; }
        public int min_p { get; set; }
        public int max_p { get; set; }
        public int min_v { get; set; }
        public int max_v { get; set; }
        public int costo { get; set; }
        public string descrizione { get; set; }
        public Associazione IdAss { get; set; }

    }
}
