using FactoryStorage.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Models
{
    public class SchemeModel : ISchemeModel
    {
        public string Topic { get; set; }

        public string Description { get; set; }

        public List<StorageModel> Elements { get; set; }
    }
}
