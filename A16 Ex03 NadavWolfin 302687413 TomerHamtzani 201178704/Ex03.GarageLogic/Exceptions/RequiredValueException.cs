using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class RequiredValueException : ArgumentException
    {
        public RequiredValueException(string i_RequiredValue, string i_InvalidValie, string i_FieldName) :
            base(string.Format("The field: '{0}' required value is:'{1}' but '{1}' was given",i_FieldName, i_RequiredValue, i_InvalidValie),i_FieldName)
        {
            m_RequiredValue = i_RequiredValue;
            m_InvalidValue = i_InvalidValie;
        }

        public string RequiredValue
        {
            get
            {
                return m_RequiredValue;
            }
        }

        public string InvalidValue
        {
            get
            {
                return m_InvalidValue;
            }
        }

        private readonly string m_RequiredValue;
        private readonly string m_InvalidValue;
    }
}
