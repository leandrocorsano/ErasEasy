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
                
               // Association ass = new Association();
                ServiceHost ServerAssociazione = new ServiceHost(typeof(Association));
                ServiceHost ServerVolontario = new ServiceHost(typeof(Volunteer));
                ServiceHost ServerStudente = new ServiceHost(typeof(Student));
                ServiceHost ServerEvento = new ServiceHost(typeof(Event));
                ServerAssociazione.Open();
                ServerVolontario.Open();
                ServerStudente.Open();
                ServerEvento.Open();
                Console.WriteLine("Server Associazione attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Volonatario attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Studente attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Evento attivo, premi un tasto per interrompere");
 
                Console.ReadLine();
                ServerAssociazione.Close();
                ServerVolontario.Close();
                ServerStudente.Close();
                ServerEvento.Close();
                Console.WriteLine("Tutti  i server sono stati chiusi chiuso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore" + ex.ToString());
            }
            
        }
    }
}
