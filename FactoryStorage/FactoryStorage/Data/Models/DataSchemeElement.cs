using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Data.Models
{
    public class DataSchemeElement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public int SchemeDataModelId { get; set; }

        public virtual SchemeDataModel SchemeDataModel { get; set; }
    }
}
