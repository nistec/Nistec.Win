using System;

namespace Nistec.WinForms
{

	public delegate void NavBarUpdatedEventHandler(object sender, NavBarUpdatedEventArgs e);

	public delegate void NavBarUpdatingEventHandler(object sender, NavBarUpdatingEventArgs e);

	/// <summary>
	/// Summary description for NavBarEvent.
	/// </summary>
	public class NavBarUpdatedEventArgs:EventArgs
	{
		private NavStatus navStatus;
		private int navPosition;

		public NavBarUpdatedEventArgs(NavStatus status,int position)
		{
			navPosition=position;
			navStatus=status;
		}

		public NavStatus NavBarStatus 
		{
			get{return navStatus;} 
		}

		public int NavBarPosition 
		{
			get{return navPosition;} 
		}
	}

	/// <summary>
	/// Summary description for NavBarEvent.
	/// </summary>
	public class NavBarUpdatingEventArgs:NavBarUpdatedEventArgs
	{
		private bool cancel;

		public NavBarUpdatingEventArgs(NavStatus status,int position):base( status, position)
		{
		}

		public bool Cancel
		{
			get{return cancel;} 
			set{cancel=value;}
		}

	}

}
