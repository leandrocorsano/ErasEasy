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
            //Associazione a = new Associazione(2, "ESN", "Roma", "Italia", "via palermo, 1", "3474233955", "esn@gmail.com", "1234");
            Association ass = new Association();
            //bool r = ass.Registration(a);
            Associazione a = ass.Login("esn@gmail.com", "ciao");
            if (a != null)
            {
                Console.WriteLine(a.citta);
            }
            //else
            //{
            //    Console.WriteLine("pippo");
            //}

            //bool r = ass.UpdateProfile(a);
        }
    }
}
