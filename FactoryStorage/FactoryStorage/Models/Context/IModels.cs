using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryStorage.Models
{
    public interface IModels
    {
        string Name { get; set; }

        int Number { get; set; }

        int CriticalNmber { get; set; }
    }
}
