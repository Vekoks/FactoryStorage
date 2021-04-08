using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Data.Models
{
    public class DataTransaction
    {
        public int Id { get; set; }

        public string Discription { get; set; }

        public DateTime Date { get; set; }

        public int StorageDataModelId { get; set; }

        public virtual StorageDataModel StorageDataModel { get; set; }
    }
}
