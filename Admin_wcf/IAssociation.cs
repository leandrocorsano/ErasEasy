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
using MySql.Data.MySqlClient;

namespace Admin_wcf
{
    [ServiceContract]
    public interface IAssociation
    {

        [OperationContract]
        bool Registration(Associazione a);

        [OperationContract]
        Associazione Login(string email, string password);

        [OperationContract]
        Associazione Profile(int id);

        [OperationContract]
        bool UpdateProfile(Associazione a);

        [OperationContract]
        bool UpdatePassword(int id, string new_password);

        [OperationContract]
        bool Create_events(Svolgimento s);

        [OperationContract]
        List<string> GetCitta(string cond = "");

        [OperationContract]
        List<Associazione> Show_associations(string cond="");//la cond si usa nel caso vogliamo mostrare associazioni particolari
        
        [OperationContract]
        List<Svolgimento> Show_Event(int idass, string tipologia="");

        [OperationContract]
        bool Add_ruolo(int idvolont, string ruolo);
        [OperationContract]
        int Generate_id();



    }


}
