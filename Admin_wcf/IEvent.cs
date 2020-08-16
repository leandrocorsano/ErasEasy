using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Admin_wcf
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IEvent" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IEvent
    {
        [OperationContract]
        void DoWork();
        /*funzioni che mancano*/
        //get_event_by_id(id)
        //Show_event( datainzio, datafine, citta) (lista che mostra tutti gli eventi di un determinato periodo)
        //All_event(citta) (lista che mostra tutti gli eventi in una determinata città)
        //All_riunioni(citta)
        //Show_riunioni(datainzio, datafine, citta)
    }
}
