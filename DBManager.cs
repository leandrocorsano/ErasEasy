using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DB_Manager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "DBManager" nel codice e nel file di configurazione contemporaneamente.
    public class DBManager : IDBManager
    {
        public SqlConnection DBconnection()
        {
            throw new NotImplementedException();
        }

        public bool DBdelate()
        {
            throw new NotImplementedException();
        }

        public bool DBinsert()
        {
            throw new NotImplementedException();
        }

        public SqlDataReader DBselect()
        {
            throw new NotImplementedException();
        }

        public bool DBupdate()
        {
            throw new NotImplementedException();
        }
    }
}
