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
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServer_Admin" nel codice e nel file di configurazione contemporaneamente.
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
        /*funzioni che mancano*/
        //Show_Event(idassoc) (lista che mostra tutti gli eventi creati da una particolare associazione)
        //Delete_Event(idevent) (boleano che conferma la cancellazione di un evento)
        //Modify_event(Evento) (bool che conferma la modifica dell'evento)
       
    }


}
