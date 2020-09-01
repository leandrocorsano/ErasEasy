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

        [OperationContract]
        List<Studente> Show_students(string cond = "");

        [OperationContract]
        bool BookEvent(int studente, int evento);

        [OperationContract]
        List<Svolgimento> Show_Event(int idstud);

        [OperationContract]
        bool CancelBooking(int studente, int evento);

        /*funzioni che mancano*/

        //Disdici evento
        //richiedi amicizia
        //mostra amici
        //conferma amicizia
    }
}
