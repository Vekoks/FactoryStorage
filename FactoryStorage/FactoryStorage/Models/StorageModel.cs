using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryStorage.Models
{
    public class StorageModel : IModels
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }
}
