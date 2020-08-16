using Admin_wcf.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
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
        /*funzioni che mancano*/
        //show_volontari (lista di volontari)
        //bookevent
        //disdici event
        //Show_Event(idstud) (lista che mostra tutti gli eventi a cui partecipa un volontario)

    }
}
