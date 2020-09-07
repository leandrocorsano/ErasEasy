//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Admin_wcf.Classi;

namespace Admin_wcf
{
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
        bool University_Registration(Frequentazione f);

        [OperationContract]
        List<Frequentazione> GetUniversity(Studente s);

        [OperationContract]
        List<Studente> Show_students(string cond = "");

        [OperationContract]
        bool BookEvent(int studente, int evento);

        [OperationContract]
        List<Svolgimento> Show_Event(int idstud);

        [OperationContract]
        bool CancelBooking(int studente, int evento);
        [OperationContract]
        bool Friendship_Request(int stud1, int stud2);
        [OperationContract]
        bool Friendship_State(int stud1, int stud2, string state);
        [OperationContract]
        List<Studente> Show_Friends(Studente stud, string state);

        [OperationContract]
        List<Studente> My_Friendship_Request(Studente stud);

        [OperationContract]
        bool Delete_Friendship(int stud1, int stud2);

        [OperationContract]
        int Generate_id();

        [OperationContract]
        int Generate_id_universita();


    }
}
