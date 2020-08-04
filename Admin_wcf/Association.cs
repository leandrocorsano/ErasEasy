using Admin_wcf.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Server_Admin" nel codice e nel file di configurazione contemporaneamente.
   
    public class Association : IAssociation
    {

        
        Associazione IAssociation.Login(string email, string password)
        {
            throw new NotImplementedException();
        }

   
        Associazione IAssociation.Profile(int id)
        {
            throw new NotImplementedException();
        }

        public bool IAssociation.Registration(Associazione a)
        {
            var wcfclient = new DBManager.DBManagerClient(); //mi connetto al server
            string valori = ""+a.IdAss+",'"+a.nome+"','" + a.citta + "', '"+ a.stato+"','" + a.via+"','" + a.tel+"','" + a.email+ "','" + a.password+"'";
            bool risultato;
            try
            {
                risultato=wcfclient.DBinsert("ASSOCIAZIONE",valori,"`idass`, `nome`, `citta`, `stato`, `via`, `tel`, `email`, `password`");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return risultato;

        }

        bool IAssociation.UpdatePassword(Associazione a, string new_password)
        {
            throw new NotImplementedException();
        }

        bool IAssociation.UpdateProfile(Associazione a)
        {
            throw new NotImplementedException();
        }
    }
}
