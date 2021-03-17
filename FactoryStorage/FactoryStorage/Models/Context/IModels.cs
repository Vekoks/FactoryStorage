using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Models.Context
{
    public interface IModels
    {
        string Name { get; set; }

        int Number { get; set; }

        int CriticalNmber { get; set; }
    }
}
