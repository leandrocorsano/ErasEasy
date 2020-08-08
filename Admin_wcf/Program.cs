using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin_wcf.Classi;

namespace Admin_wcf
{
    class Program
    {
        static void Main(string[] args)
        {
            //var wcfclient = new DBManager.DBManagerClient();
            //Console.WriteLine("WCF CLIENT CREATO");
            /*PROVA REGISTRATION IASSOCIATION*/
            Studente s = new Studente(1, "Marco", "Neri", "f@gmail.com", "3474233955", "2000-05-22", "Napoli", "Italia", "Italiana", "1234");
            Student stud = new Student();
            bool r = stud.UpdateProfile(s);
            Studente s1 = stud.Profile(1); 
            Console.WriteLine(s1.nome+" "+s1.password);
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
