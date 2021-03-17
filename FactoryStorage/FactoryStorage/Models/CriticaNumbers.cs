using FactoryStorage.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Models
{
    public class CriticaNumbers : IModels
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public int CriticalNmber { get; set; }
    }
}
