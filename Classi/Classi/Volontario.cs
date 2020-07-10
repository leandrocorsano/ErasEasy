using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classi
{
    public class Volontario
    {
        public Volontario(int IdVolont, string nome, string cognome, int data_n, string email, int telefono, int data_iscr, string password, int IdAss)
        {
            this.IdVolont = IdVolont;
            this.nome = nome;
            this.cognome = cognome;
            this.data_n = data_n;
            this.email = email;
            this.telefono = telefono;
            this.data_iscr = data_iscr;
            this.password = password;
            this.IdAss = IdAss;

        }
        public int IdVolont { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public int data_n { get; set; }
        public string email { get; set; }
        public int telefono { get; set; }
        public int data_iscr { get; set; }
        public string password { get; set; }
        public int IdAss { get; set; }

    }
}
