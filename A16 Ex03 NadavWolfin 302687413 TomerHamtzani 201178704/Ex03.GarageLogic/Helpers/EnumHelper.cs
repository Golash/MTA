using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Helpers
{
    public static class EnumHelper
    {
        public static T ParseByName<T>(string i_ValueToParse)
        {
            if(!Enum.GetNames(typeof(T)).Contains(i_ValueToParse))
            {
                string errorMessage = string.Format("Failed to parse value: '{0}' to enum: {'{1}')", i_ValueToParse, typeof(T).Name);
                throw new FormatException(errorMessage);
            }

            return (T)Enum.Parse(typeof(T), i_ValueToParse);
        }
    }
}
