using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Data.Models
{
    public class StorageDataModel
    {
        private ICollection<DataTransaction> transactions;

        public StorageDataModel()
        {
            this.transactions = new List<DataTransaction>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public int CriticalNumber { get; set; }

        public virtual ICollection<DataTransaction> Transactions
        {
            get { return transactions; }
            set { transactions = value; }
        }
    }
}
