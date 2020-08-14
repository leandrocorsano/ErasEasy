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
                ServerAssociazione.Open();
                ServerVolontario.Open();
                ServerStudente.Open();
                Console.WriteLine("Server Associazione attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Volonatario attivo, premi un tasto per interrompere");
                Console.WriteLine("Server Studente attivo, premi un tasto per interrompere");
                //Association a = new Association();
                //Associazione ass = a.Profile(1);
                //Evento e = new Evento(1, "Disco inferno", "Party", 50, 250, 10, 50, 15, "sacnv", ass);
                //bool r = a.Create_events(e);
                
                //Student stud = new Student();
                //Studente s = stud.Profile(1);

                //Console.WriteLine(s.nome + " " + s.cognome);
                Console.ReadLine();
                ServerAssociazione.Close();
                ServerVolontario.Close();
                ServerStudente.Close();
                Console.WriteLine("Tutti e tre i server sono stati chiusi chiuso");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore" + ex.ToString());
            }
            
        }
    }
}
