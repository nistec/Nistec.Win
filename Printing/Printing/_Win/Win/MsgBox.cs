using System;
using System.Windows.Forms;

namespace MControl.Win
{
	/// <summary>
	/// Summary description for MsgBox.
	/// </summary>
	public sealed class MsgBox
	{

		private const string Caption="MControl";

		#region  Icons Type

		public static DialogResult ShowInfo(string text )
		{
			return Show (text,Caption,MessageBoxButtons.OK ,MessageBoxIcon.Information );
		}

		public static DialogResult ShowInfo(string text,string caption )
		{
			return Show (text,caption,MessageBoxButtons.OK ,MessageBoxIcon.Information );
		}

		public static DialogResult ShowWarning(string text )
		{
			return Show (text,Caption,MessageBoxButtons.OK ,MessageBoxIcon.Warning  );
		}

		public static DialogResult ShowWarning(string text,string caption )
		{
			return Show (text,caption,MessageBoxButtons.OK ,MessageBoxIcon.Warning  );
		}

		public static DialogResult ShowWarning(string text,string caption ,MessageBoxButtons button)
		{
			return Show (text,caption,button ,MessageBoxIcon.Warning   );
		}

		public static DialogResult ShowQuestion(string text)
		{
			return Show (text,Caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question  );
		}

		public static DialogResult ShowQuestion(string text,string caption)
		{
			return Show (text,caption,MessageBoxButtons.YesNo,MessageBoxIcon.Question  );
		}

		public static DialogResult ShowQuestion(string text,string caption,MessageBoxButtons button)
		{
			return Show (text,caption,button,MessageBoxIcon.Question  );
		}

		public static DialogResult ShowQuestionYNC(string text,string caption)
		{
			return Show (text,caption,MessageBoxButtons.YesNoCancel ,MessageBoxIcon.Question  );
		}

		public static DialogResult ShowError(string text )
		{
			return Show (text,Caption,MessageBoxButtons.OK ,MessageBoxIcon.Error );
		}

		public static DialogResult ShowError(string text,string caption )
		{
			return Show (text,caption,MessageBoxButtons.OK ,MessageBoxIcon.Error );
		}

		public static DialogResult ShowError(string text,string caption ,MessageBoxButtons button)
		{
			return Show (text,caption,button ,MessageBoxIcon.Error );
		}

		#endregion

        public static DialogResult Show(string text, string caption, MessageBoxIcon icon)
        {
            return Show(text, caption, MessageBoxButtons.OK, icon);
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons button, MessageBoxIcon icon)
		{
			string Title="";
			
			if(caption=="")
				Title=Caption;
			else
				Title= caption;
           
			return MessageBox.Show (text,Title,button,icon);
		}
	
	}
}
