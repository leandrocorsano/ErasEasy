using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Admin_wcf.Classi;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IEvent" nel codice e nel file di configurazione contemporaneamente.
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

    }
}
