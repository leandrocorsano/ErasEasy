//=============================================================================
// Authors: Francesca Rossi, Leandro Corsano
//=============================================================================
using Admin_wcf.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Admin_wcf
{
    [ServiceContract]
    public interface IVolunteer
    {
        [OperationContract]
        bool Registration(Volontario v);

        [OperationContract]
        Volontario Login(string email, string password);

        [OperationContract]
        Volontario Profile(int id);

        [OperationContract]
        bool UpdateProfile(Volontario v);

        [OperationContract]
        bool UpdatePassword(int id, string new_password);

        [OperationContract]
        Associazione GetAssociazione(int id);

        [OperationContract]
        List<Volontario> Show_volontari(string cond = "");

        [OperationContract]
        bool BookEvent(int volontario, int evento);

        [OperationContract]
        List<Svolgimento> Show_Event(int idvol, string tipologia = "");

        [OperationContract]
        bool CancelBooking(int volontario, int evento);

        [OperationContract]
        int Generate_id();


    }
}
