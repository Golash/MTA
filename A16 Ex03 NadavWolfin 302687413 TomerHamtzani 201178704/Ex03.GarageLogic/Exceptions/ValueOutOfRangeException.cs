using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_FieldName) 
            : base(string.Format("Value out of range, the value should be between {0} to {1}", i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
            m_FieldName = i_FieldName;
        }

        public ValueOutOfRangeException(float i_MivValue, string i_FieldName)
            : base(string.Format("Value out of range, the value should be greater than {0}", i_MivValue))
        {

        }

        public float MaxValue
        {
            get
            {
                return m_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return m_MinValue;
            }
        }

        public string FieldName
        {
            get
            {
                return m_FieldName;
            }
        }

        private string m_FieldName;
        private float m_MinValue;
        private float m_MaxValue;
    }
}
