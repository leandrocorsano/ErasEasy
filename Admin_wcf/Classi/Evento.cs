using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Admin_wcf.Classi
{
    [DataContract]
    public class Evento
    {
        public Evento(int IdEv, string nome, string tipologia, int min_p, int max_p, int min_v, int max_v, int costo, string descrizione, Associazione ass)
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
            this.ass = ass;
        }
        [DataMember]
        public int IdEv { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string tipologia { get; set; }
        [DataMember]
        public int min_p { get; set; }
        [DataMember]
        public int max_p { get; set; }
        [DataMember]
        public int min_v { get; set; }
        [DataMember]
        public int max_v { get; set; }
        [DataMember]
        public int costo { get; set; }
        [DataMember]
        public string descrizione { get; set; }
        [DataMember]
        public Associazione ass { get; set; }

    }
}
