using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactoryStorage.Data.Models
{
    public class SchemeDataModel
    {
        private ICollection<DataSchemeElement> elements;

        public SchemeDataModel()
        {
            this.elements = new List<DataSchemeElement>();
        }

        public int Id { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public virtual ICollection<DataSchemeElement> DataSchemeElements
        {
            get { return elements; }
            set { elements = value; }
        }
    }
}
