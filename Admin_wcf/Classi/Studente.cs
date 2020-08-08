using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_wcf.Classi
{
    public class Studente
    {
        public Studente(int IdStud, string nome, string cognome, string email, string tel, string data_n, string citta, string stato, string nazionalita, string password, string instagram="", string facebook="")
        {
            this.IdStud = IdStud;
            this.nome = nome;
            this.cognome = cognome;
            this.email = email;
            this.tel = tel;
            this.data_n = data_n;
            this.citta = citta;
            this.stato = stato; 
            this.nazionalita = nazionalita;
            this.password = password;
            this.instagram = instagram;
            this.facebook = facebook;

        }
        public int IdStud { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string data_n { get; set; }
        public string citta { get; set; }
        public string stato { get; set; }
        public string nazionalita { get; set; }
        public string password { get; set; }
        public string instagram { get; set; }
        public string facebook { get; set; }
    }
}
