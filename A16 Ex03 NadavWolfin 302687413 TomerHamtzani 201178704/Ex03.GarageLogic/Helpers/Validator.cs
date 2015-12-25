using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Helpers
{
    internal static class Validator
    {
        public static void IsNotNullOrWhiteSpace(string i_Value, string i_FieldName)
        {
            if (string.IsNullOrWhiteSpace(i_Value))
            {
                throw new ArgumentException(string.Format("The field:'{0}' can't be null or white space", i_FieldName), i_FieldName);
            }
        }

        public static void IsInRange(float i_Value, float i_MinValue, float i_MaxValue)
        {
            if (!(i_Value >= i_MinValue && i_Value <= i_MaxValue))
            {
                throw new ValueOutOfRangeException(null, i_MinValue, i_MaxValue);
            }
        }
    }
}
