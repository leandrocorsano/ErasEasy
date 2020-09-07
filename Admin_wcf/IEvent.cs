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
    public interface IEvent
    {
        [OperationContract]
        List<Svolgimento> Show_events(string cond = "");

        [OperationContract]
        Svolgimento Get_event_by_id(int id);

        [OperationContract]
        List<Studente> Event_partecipations(Svolgimento e);

        [OperationContract]
        List<Volontario> Event_volunteers(Svolgimento e);

        [OperationContract]
        bool Edit_Event(Svolgimento s);

        [OperationContract]
        bool Delete_Event(Svolgimento s);

        [OperationContract]
        void Send_Email(string nome, string email, string body_message, string sub);

        [OperationContract]
        int Generate_id_Luogo();

        [OperationContract]
        int Generate_id();
    }
}
