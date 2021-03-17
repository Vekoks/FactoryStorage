using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Models.Context
{
    public interface ISchemeModel
    {
        string Topic { get; set; }

        string Description { get; set; }

        List<StorageModel> Elements { get; set; }
    }
}
