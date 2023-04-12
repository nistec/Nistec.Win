namespace Nistec.Printing.View
{
    using System;

    internal class mtd365
    {
        public void mtd366(ref string[] var0, ref ScriptLanguage var1, ref string var2)
        {
            ScriptEditor editor = new ScriptEditor();
            editor.mtd172(var0, ref var1, ref var2);
            editor.ShowDialog();
            if (editor.mtd4)
            {
                var2 = editor.rtfScript.Text;
                var1 = editor.scriptLanguage;//.mtd362;
            }
        }
    }
}

