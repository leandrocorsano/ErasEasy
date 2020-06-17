using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;


namespace DB_Manager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IDBManager" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IDBManager
    {
        [OperationContract]
        SqlConnection DBconnection();

        [OperationContract]
        bool DBinsert();
        bool DBdelate();
        SqlDataReader DBselect();
        bool DBupdate();

    }
}
