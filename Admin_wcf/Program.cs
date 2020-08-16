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
                //List<Associazione> ass = a.Show_associations();
                //ass.ForEach(x => { Console.WriteLine(x.nome); });
                
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
