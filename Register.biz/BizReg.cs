using Biz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BIZ
{
   public static class BizReg 
    {
        public static bool Validate(string UserID,string PassWord)
        {
            Dictionary<string, object> FieldValues = new Dictionary<string, object>();
            FieldValues.Add("NAME", UserID);
            FieldValues.Add("PASSWORD", PassWord);
            return Convert.ToInt32(BizCenter.DataAccessor.GetDataValue("ValidateReg", FieldValues, -1)) == 0 ? true : false;
        }     
    }
}
