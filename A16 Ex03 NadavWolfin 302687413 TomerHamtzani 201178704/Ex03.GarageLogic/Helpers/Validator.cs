using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Helpers
{
    internal static class Validator
    {
        public static void ValidateNotNullOrWhiteSpace(string i_Value, string i_FieldName)
        {
            if (string.IsNullOrWhiteSpace(i_Value))
            {
                throw new FormatException(string.Format("The field:'{0}' can't be null or white space", i_FieldName));
            }
        }

        public static void ValidateValueInRange(string fieldName, float i_Value, float i_MinValue, float i_MaxValue)
        {
            if (!(i_Value >= i_MinValue && i_Value <= i_MaxValue))
            {
                throw new ValueOutOfRangeException(i_MinValue, i_MaxValue, fieldName);
            }
        }
        public static void ValidateIsNotNull(object i_Obj, string i_Name)
        {
            if (i_Obj == null)
            {
                throw new NullReferenceException(string.Format("The {0} can't be null", i_Name));
            }
        }
    }
}
