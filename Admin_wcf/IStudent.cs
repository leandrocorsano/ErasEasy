using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Admin_wcf.Classi;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IStudent" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IStudent
    {
        [OperationContract]
        bool Registration(Studente s);

        [OperationContract]
        Studente Login(string email, string password);

        [OperationContract]
        Studente Profile(int id);

        [OperationContract]
        bool UpdateProfile(Studente a);

        [OperationContract]
        bool UpdatePassword(int id, string new_password);
    }
}
