using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Admin_wcf.Classi;

namespace Admin_wcf
{
    class Program
    {
        static void Main(string[] args)
        { //Student stud = new Student();
            try
            {
                ServiceHost ServerAssociazione = new ServiceHost(typeof(Association));
                ServiceHost ServerVolontario = new ServiceHost(typeof(Volunteer));
                ServiceHost ServerStudente = new ServiceHost(typeof(Student));
                ServerAssociazione.Open();
                ServerVolontario.Open();
                ServerStudente.Open();
                Console.WriteLine("Server Associazione attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Volonatario attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Studente attivo, premi un tasto per interrompere");
                //Studente s = stud.Profile(1);
                //Console.WriteLine(s.nome + " " + s.cognome);
                Console.ReadLine();
                ServerAssociazione.Close();
                ServerVolontario.Close();
                //ServerStudente.Close();
                Console.WriteLine("Tutti e tre i server sono stati chiusi chiuso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore" + ex.ToString());
            }
            //var wcfclient = new DBManager.DBManagerClient();
            //Console.WriteLine("WCF CLIENT CREATO");
            /*PROVA REGISTRATION IASSOCIATION*/
            //Association ass = new Association();
            //Associazione a = new Associazione(3, "esn", "Parigi", "Francia", "Rue Napoleon, 15", "054125956", "p@gmail.com", "1111");
            //Console.WriteLine(a.nome);
            //bool r = ass.Registration(a);
            //Volontario v = new Volontario(3,"Maria", "Neri", "2000-01-02", "fra@gmail.com", "3412566954", "2020-08-08", "1000", a);
            //Volunteer vol = new Volunteer();

            //bool r = vol.UpdatePassword(3,"1234");
            //Studente s1 = stud.Profile(1); 
            //Console.WriteLine(v.nome+" "+v.ass.citta);
            /*Associazione a = ass.Login("esn@gmail.com", "ciao");
            if (a != null)
            {
                Console.WriteLine(a.citta);
            }
            //else
            //{
            //    Console.WriteLine("pippo");
            //}

            //bool r = ass.UpdateProfile(a);
            */

        }
    }
}
