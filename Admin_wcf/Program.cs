using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_wcf
{
    class Program
    {
        static void Main(string[] args)
        {
            var wcfclient = new DBManager.DBManagerClient();
            Console.WriteLine("WCF CLIENT CREATO");

        }
    }
}
