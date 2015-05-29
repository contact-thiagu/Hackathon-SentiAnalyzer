using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Property
    {
        string propertyName=string.Empty;
        string propertValues=string.Empty;
        string propertyDesc = string.Empty;
        int propertyId=0;

        public string PropertyName
        {
            get
            {
                return propertyName;
            }

            set
            {
                propertyName = value;
            }
        }

        public string PropertValues
        {
            get
            {
                return propertValues;
            }

            set
            {
                propertValues = value;
            }
        }

        public string PropertyDesc
        {
            get
            {
                return propertyDesc;
            }

            set
            {
                propertyDesc = value;
            }
        }

        public int PropertyId
        {
            get
            {
                return propertyId;
            }

            set
            {
                propertyId = value;
            }
        }
    }
}
