using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Admin_wcf.Classi
{
    [DataContract]
    public class Svolgimento
    {
        public Svolgimento(Evento IdEv, Luogo luogo, string ora_i, string ora_f, string data_i, string data_f)
        {
            this.evento = evento;
            this.luogo = luogo;
            this.ora_i = ora_i;
            this.ora_f = ora_f;
            this.data_i = data_i;
            this.data_f = data_f;
        }
        [DataMember]
        public Evento evento { get; set; }
        [DataMember]
        public Luogo  luogo { get; set; }
        [DataMember]
        public string ora_i { get; set; }
        [DataMember]
        public string ora_f { get; set; }
        [DataMember]
        public string data_i { get; set; }
        [DataMember]
        public string data_f{ get; set; }
    }
}
