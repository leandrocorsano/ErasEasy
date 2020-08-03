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

        bool IAssociation.Registration(Associazione a)
        {
            throw new NotImplementedException();
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
