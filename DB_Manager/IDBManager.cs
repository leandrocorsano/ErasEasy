using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace DB_Manager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IDBManager" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IDBManager
    {


        
        

        [OperationContract]
        bool DBinsert(string table, string values, string field = "");

        //[OperationContract]
        //bool DBdelete(string table, string condition);

        [OperationContract]
        MySqlDataReader DBselect( string campi, string tabella, string condizione="");
        
        //[OperationContract]
        //bool DBupdate(string table, string setter, string condition);

    }
}
