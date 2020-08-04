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
            Associazione a = new Associazione(1, "prova", "Parma", "Italia", "via palermo, 1", "3474233955", "p@gmail.com", "1234");
            Association ass = new Association();
   


        }
    }
}
