using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Utility
{
    public  class TypeConversion
    {
        /// <summary>
        /// This method converts parameter parameterType to the type specified as parameter typename
        /// </summary>
        /// <param name="parameterType">name of the parameter to be converted to the type specified in the type name</param>
        /// <param name="typeName">type to be converted to in a string</param>
        /// <returns></returns>
        public static dynamic ConvertToType(string parameterType, string typeName)
        {

            try
            {
                Type type = Type.GetType(typeName);
                TypeConverter converter = new TypeConverter();
                var returnAsType = TypeDescriptor.GetConverter(type).ConvertFromString(parameterType);
                //var returnAsType = converter.ConvertTo(parameterType, type);
                return returnAsType;
            }
            catch (Exception ex)
            {
                //log exception
                return null;
            }
        }
    }
}
