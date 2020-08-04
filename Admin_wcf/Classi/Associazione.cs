using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_wcf.Classi
{
    public class Associazione
    {
        public Associazione(int IdAss, string nome, string citta, string stato, string via,  string tel, string email, string password)
        {
            this.IdAss = IdAss;
            this.nome = nome;
            this.citta = citta;
            this.stato = stato;
            this.via = via;
            this.tel = tel;
            this.email = email;
            this.password = password;
        }
        public int IdAss { get; set; }
        public string nome { get; set; }
        public string citta { get; set; }
        public string stato { get; set; }
        public string via { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
