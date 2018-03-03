using System;
using System.ComponentModel.DataAnnotations;

namespace Lab03Mahur
{
    public class UserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string name = value.ToString();

            foreach (char ch in name)
            {
                if (ch != '-' && ch != '\'' && !Char.IsLetter(ch))
                    return false;
            }
            return true;
        }
    }
}
