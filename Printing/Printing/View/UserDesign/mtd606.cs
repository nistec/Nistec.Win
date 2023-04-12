namespace MControl.Printing.View.Design.UserDesigner
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;

    internal class mtd606 : IMenuCommandService
    {
        private ArrayList _var0 = new ArrayList();
        private DesignerVerbCollection _var1 = new DesignerVerbCollection();

        internal mtd606()
        {
        }

        public void AddCommand(MenuCommand var2)
        {
            this._var0.Add(var2);
        }

        public void AddVerb(DesignerVerb var3)
        {
            this._var1.Add(var3);
        }

        public MenuCommand FindCommand(CommandID var4)
        {
            foreach (object obj2 in this._var0)
            {
                MenuCommand command = (MenuCommand) obj2;
                if (command.CommandID == var4)
                {
                    return command;
                }
            }
            foreach (object obj3 in this._var1)
            {
                DesignerVerb verb = (DesignerVerb) obj3;
                if (verb.CommandID == var4)
                {
                    return verb;
                }
            }
            return null;
        }

        public bool GlobalInvoke(CommandID var4)
        {
            MenuCommand command = this.FindCommand(var4);
            if (command != null)
            {
                command.Invoke();
                return true;
            }
            return false;
        }

        public void RemoveCommand(MenuCommand var2)
        {
            this._var0.Remove(var2);
        }

        public void RemoveVerb(DesignerVerb var3)
        {
            this._var1.Remove(var3);
        }

        public void ShowContextMenu(CommandID var5, int var6, int var7)
        {
        }

        public DesignerVerbCollection Verbs
        {
            get
            {
                return this._var1;
            }
        }
    }
}

