using FactoryStorage.Data.Data.Context;
using FactoryStorage.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace FactoryStorage.Data.Data
{
    public class DataFactoryStorageContext : DbContext, IDataFactoryStorageContext
    {
        public DataFactoryStorageContext() : base("FactoryStorageDB")
        {

        }

        public IDbSet<StorageDataModel> StorageDataModels { get; set; }

        public IDbSet<SchemeDataModel> SchemeDataModels { get; set; }

        public IDbSet<DataTransaction> DataTransactions { get; set; }

        public IDbSet<DataSchemeElement> DataSchemeElements { get; set; }

        public static DataFactoryStorageContext Create()
        {
            return new DataFactoryStorageContext();
        }
    }
}
