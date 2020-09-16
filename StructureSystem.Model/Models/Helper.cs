using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
   public static class Helper
    {


        public static void SetPropertyValue(ref Object recipientObj, string propertyName, object propertyValue)
        {
            try
            {
                PropertyInfo prop = recipientObj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                if (prop != null)
                {
                    prop.SetValue(recipientObj, propertyValue);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }//end of class
}//end of namespace
