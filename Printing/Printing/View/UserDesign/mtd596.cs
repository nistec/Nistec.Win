namespace MControl.Printing.View.Design.UserDesigner
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;

    internal class mtd596 : INameCreationService
    {
        internal mtd596()
        {
        }

        public string CreateName(IContainer var0, Type var1)
        {
            int num = 0;
            string name = var1.Name;
            do
            {
                num++;
            }
            while (var0.Components[name + num] != null);
            return (name + num);
        }

        public bool IsValidName(string var2)
        {
            if ((var2 == null) || (var2.Length == 0))
            {
                return false;
            }
            if (!char.IsLetter(var2, 0))
            {
                return false;
            }
            if (var2.StartsWith("_"))
            {
                return false;
            }
            foreach (char ch in var2)
            {
                if (!char.IsLetterOrDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public void ValidateName(string var2)
        {
            if (!this.IsValidName(var2))
            {
                throw new ArgumentException("Invalid Name : " + var2);
            }
        }
    }
}

