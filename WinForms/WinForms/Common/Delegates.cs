using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;



namespace Nistec.WinForms
{
    public delegate void AsyncDelegate();
    public delegate void AsyncDelegateArgs(object[] args);



	#region ButtonClickEvents

    public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);

	public class ButtonClickEventArgs: EventArgs
	{
		//private System.Windows.Forms.Button  mSender;
		private object mValue;

        public ButtonClickEventArgs(object Value)
		{
            //mSender = sender;// (System.Windows.Forms.Button)sender;
			mValue=Value;
		}

		public object Value
		{
			get{return mValue;}
			//set{mValue=value;}
		}
	
	}

	#endregion

    #region ToolButtonClickEvents

    public delegate void ToolButtonClickEventHandler(object sender, ToolButtonClickEventArgs e);

    public class ToolButtonClickEventArgs : EventArgs
    {
        private McToolButton button;

        public ToolButtonClickEventArgs(McToolButton Value)
        {
            button = Value;
        }

        public IMcToolButton Button
        {
            get { return button as IMcToolButton; }
        }

    }

    #endregion

	#region ItemClickEventArgs

//	public delegate void ItemClickEventHandler(object sender,ItemClickEventArgs e);
//
//	public class ItemClickEventArgs : EventArgs
//	{
//		private ButtonMenuItem m_SelectedValue;
//		private int m_SelectedIndex;
//	
//		#region Constructors
//			
//		public ItemClickEventArgs(ButtonMenuItem selectedValue,int selectedIndex)
//		{
//			m_SelectedValue = selectedValue;
//			m_SelectedIndex = selectedIndex;
//		}
//	
//		#endregion
//	
//		#region Properties
//	
//		public ButtonMenuItem Item
//		{
//			get{ return m_SelectedValue; }
//		}
//	
//		public int ItemIndex
//		{
//			get{ return m_SelectedIndex; }
//		}
//	
//		#endregion
//	}

	#endregion

	#region ItemInsertEvent

	public delegate void ItemInsertEventHandler(object sender,ItemInsertEvent e);
	
	public class ItemInsertEvent: EventArgs
	{
		private Nistec.WinForms.McListBox  mSender;
		private object mValue;
	
		public  ItemInsertEvent(object sender,object Value)
		{
			mSender=(Nistec.WinForms.McListBox)  sender;
			mValue=Value;
		}

		public object Value
		{
			get{return mValue;}
			//set{mValue=value;}
		}

		public int index
		{
			get{return mSender.SelectedIndex;}
			//set{mIndex=value;}
		}

		public bool Msg(string s)
		{
			string str="Add new Item ?";
			if(! s.Equals(string.Empty ))
				str=s;
			return  System.Windows.Forms.MessageBox.Show (str,"Nistec",System.Windows.Forms.MessageBoxButtons.YesNo , System.Windows.Forms.MessageBoxIcon.Question )==System.Windows.Forms.DialogResult.Yes  ;
		}

		public void ItemInsert()
		{
			if(mValue !=null)
			{
				mSender.Items.Add (mValue);
				mSender.Update ();
			}
				
		}

		public void ItemInsert(string sMsg)
		{
			if(mValue !=null)
			{
				if (! Msg(sMsg))
					return;
				mSender.Items.Add (mValue);
				mSender.Update ();
			}
				
		}
	
	}
	#endregion

	#region ItemUpdateEvent

	public delegate void ItemUpdateEventHandler(object sender,ItemUpdateEvent  e);

	public class ItemUpdateEvent: EventArgs
	{
		private Nistec.WinForms.McListBox  mSender;
		private object mValue;
	
		public  ItemUpdateEvent(object sender,object Value)
		{
			mSender=(Nistec.WinForms.McListBox)  sender;
			mValue=Value;
		}

		public object Value
		{
			get{return mValue;}
			//set{mValue=value;}
		}

		public int index
		{
			get{return mSender.SelectedIndex;}
			//set{mIndex=value;}
		}

		public bool Msg(string s)
		{
			string str="Update Item ?";
			if(! s.Equals(string.Empty ))
				str=s;
			return  System.Windows.Forms.MessageBox.Show (str,"Nistec",System.Windows.Forms.MessageBoxButtons.YesNo , System.Windows.Forms.MessageBoxIcon.Question )==System.Windows.Forms.DialogResult.Yes  ;
		}

		public void ItemUpdate()
		{
			if(mValue !=null && index >=0)
			{
				if (! Msg("") )
					return;
				mSender.Items [mSender.SelectedIndex] =mValue.ToString ();   
				mSender.Update (); 
			}
				
		}

		public void ItemUpdate(string sMsg)
		{
			if(mValue !=null && index >=0)
			{
				if (! Msg(sMsg) )
					return;
				mSender.Items [mSender.SelectedIndex] =mValue.ToString ();   
				mSender.Update (); 
			}
				
		}

	}

	#endregion

	#region ItemRemoveEvent

	public delegate void ItemRemoveEventHandler(object sender,ItemRemoveEvent  e);

	public class ItemRemoveEvent: EventArgs
	{
		private Nistec.WinForms.McListBox  mSender;
		private int mIndex;
		private object mValue;
	
		public  ItemRemoveEvent(object sender,int index,object value)
		{
			mSender=(Nistec.WinForms.McListBox )  sender;
			mIndex=index;
			mValue=value;
		}

		public int index
		{
			get{return mIndex;}
		}

		public object Value
		{
			get{return mValue;}
		}

		public bool Msg(string s)
		{
			string str="Remove Item ?";
			if(! s.Equals(string.Empty ))
				str=s;
			return  System.Windows.Forms.MessageBox.Show (str,"Nistec",System.Windows.Forms.MessageBoxButtons.YesNo , System.Windows.Forms.MessageBoxIcon.Question )==System.Windows.Forms.DialogResult.Yes ;
		}

		public void ItemRemove()
		{
			if(index >=0)
			{
				mSender.Items.RemoveAt(index);  
				mSender.Update(); 
			}
				
		}


		public void ItemRemove(string sMsg)
		{
			if(index >=0)
			{
				if (! Msg(sMsg))
					return;
				mSender.Items.RemoveAt(index);  
				mSender.Update(); 
			}
		}

	}

	#endregion

	#region ItemsSelected

	public class SelectedIndexChangedEvent: EventArgs
	{
		private int mIndex;
	
		public  SelectedIndexChangedEvent(int index)
		{
			mIndex=index;
		}

		public int Index {get{return mIndex;}}
	
	}

	public class SelectedValueChangedEvent: EventArgs
	{
		private object mValue;
	
		public  SelectedValueChangedEvent(object Value)
		{
			mValue=Value;
		}

		public object Value{get{return mValue;}}
	
	}

	public delegate void SelectedItemChangedEventHandler(object sender,SelectedItemChangedEvent e);

	public class SelectedItemChangedEvent: EventArgs
	{
		private object mValue;
		private int mIndex;
	
		public  SelectedItemChangedEvent(object Value,int index)
		{
			mValue=Value;
			mIndex=index;
		}

		public object Value{get{return mValue;}}
		public int Index {get{return mIndex;}}

	}

    public delegate void SelectedPopUpItemEventHandler(object sender, SelectedPopUpItemEvent e);

    public class SelectedPopUpItemEvent : EventArgs
    {
        private PopUpItem mItem;
        private int mIndex;

        public SelectedPopUpItemEvent(PopUpItem item, int index)
        {
            mItem = item;
            mIndex = index;
        }

        public PopUpItem Item { get { return mItem; } }
        public int Index { get { return mIndex; } }

    }
	#endregion

	#region NumberOcurredEventArgs

	public delegate void NumberOcurredEventHandler(object sender,NumberOcurredEventArgs e);

	public class NumberOcurredEventArgs : EventArgs
	{
		private Color m_NegativeColor = Color.Red;
		private Color m_PositiveColor = Color.Black;

		#region Constructors

		public NumberOcurredEventArgs()
		{
		}

		public NumberOcurredEventArgs(Color negativeColor, Color positiveColor)
		{
			m_NegativeColor = negativeColor;
			m_PositiveColor = positiveColor;
		}

		#endregion

		#region Properties

		public Color NegativeNumberColor
		{
			get {return m_NegativeColor;}
			set {m_NegativeColor = value;}
		}

		public Color PositiveNumberColor
		{
			get {return m_PositiveColor;}
			set {m_PositiveColor = value;}
		}

		#endregion
	}

	#endregion

	#region ErrorOcurredEventArgs
    //move to frameork
    //public delegate void ErrorOcurredEventHandler(object sender,ErrorOcurredEventArgs e);

    //public class ErrorOcurredEventArgs : EventArgs
    //{
    //    private string m_Message = "";

    //    #region Constructors

    //    public ErrorOcurredEventArgs(string message)
    //    {
    //        if(message==null)
    //            message="";
    //        m_Message = message;
    //    }

    //    #endregion

    //    #region Properties

    //    public string Message
    //    {
    //        get {return m_Message;}
    //        set 
    //        {  
    //            if(value==null)
    //                value="";
    //            m_Message = value;
    //        }
    //    }

    //    #endregion
    //}

	#endregion

	#region ColorStyleChangedEventArgs

	public delegate void ColorStyleChangedEventHandler(object sender,ColorStyleChangedEventArgs e);

	public class ColorStyleChangedEventArgs : EventArgs
	{
		private Styles m_NewStyle;

		#region Constructors

		public ColorStyleChangedEventArgs(Styles style)
		{
			m_NewStyle = style;
		}

		#endregion

		#region Properties

		public Styles NewStyle
		{
			get {return m_NewStyle;}
			set {m_NewStyle = value;}
		}

		#endregion
	}

	#endregion

	#region ComboSelChangedEventArgs

	public delegate void SelectionChangedEventHandler(object sender,SelectionChangedEventArgs e);

	public class SelectionChangedEventArgs : EventArgs
	{
		private object m_SelectedValue;

		#region Constructors
		
		public SelectionChangedEventArgs(object selectedValue)
		{
			m_SelectedValue = selectedValue;
		}

		#endregion

		#region Properties

		public object Value
		{
			get{ return m_SelectedValue; }
		}

		#endregion
	}

	#endregion

	#region StyleEventArgs

	public delegate void StyleChangedEventHandler(object sender,StyleEventArgs e);

	public class StyleEventArgs : EventArgs
	{
		private string m_PropertyName  = "";
		private object m_PropertyValue = null;

		public StyleEventArgs(string propertyName,object popertyValue)
		{
			m_PropertyName  = propertyName;
			m_PropertyValue = popertyValue;
		}

		public string PropertyName
		{
			get{ return m_PropertyName; }
		}

		public object PropertyValue
		{
			get{ return m_PropertyValue; }
		}
	}

	#endregion

	#region DateChangedEventArgs

	public delegate void DateSelectionChangedEventHandler(object sender,DateChangedEventArgs e);

	public class DateChangedEventArgs : EventArgs
	{
		private DateTime m_SelectedDate;

		public DateChangedEventArgs(DateTime selectedDate)
		{
			m_SelectedDate = selectedDate;
		}

		#region Properties Implementation
		
		public DateTime Date
		{
			get{ return m_SelectedDate; }
		}

		#endregion

	}

	#endregion

	#region ValueChangedEventArgs

	public delegate void ValueChangedEventHandler(object sender,ValueChangedEventArgs e);

	public class ValueChangedEventArgs : EventArgs
	{
		private object m_Value;

		public ValueChangedEventArgs(object value)
		{
			m_Value = value;
		}

		#region Properties Implementation
		
		public object Value
		{
			get{ return m_Value; }
		}

		#endregion

	}

	#endregion

	#region MessageEventsArgs

	public delegate void MessageEventHandler(object sender, MessageEventArgs e);

	public class MessageEventArgs : EventArgs
	{
		private Message men;
		private bool result = false;

		public MessageEventArgs(Message message, bool result)
		{
			this.men = message;
			this.result = result;
		}

		#region Properties Implementation
		
		public Message WindowsMessage
		{
			get {return this.men;}
		}

		public bool Result
		{
			get {return this.result;}
			set {this.result = value;}
		}

		#endregion

	}

	#endregion

	#region ValidatingEventArgs

	public delegate void ValidatingEventHandler(object sender,ValidatingEventArgs e);

	public class ValidatingEventArgs : System.ComponentModel.CancelEventArgs
	{
		private object m_NewValue = null;
		public ValidatingEventArgs(object newValue):base(false)
		{
			m_NewValue = newValue;
		}
		public object NewValue
		{
			get{return m_NewValue;}
			set{m_NewValue = value;}
		}
	}

	#endregion

	#region RowEventArgs

	public class RowEventArgs : EventArgs
	{
		protected int m_Row;
		public int Row
		{
			get{return m_Row;}
			set{m_Row = value;}
		}
		public RowEventArgs(int pRow)
		{
			m_Row = pRow;
		}
	}

	#endregion

	#region ColumnEventArgs

	public class ColumnEventArgs : EventArgs
	{
		protected int m_Column;
		public int Column
		{
			get{return m_Column;}
			set{m_Column = value;}
		}
		public ColumnEventArgs(int pColumn)
		{
			m_Column = pColumn;
		}
	}

	#endregion*/

	#region Cells
/*
	#region CellEventArgs

	public class CellEventArgs : EventArgs
	{
		protected int m_Row;
		protected int m_Column;
		//protected DataGridCell m_Cell;
		protected ControlWare.Windows.Controls.Cell  m_Cell;

		public ControlWare.Windows.Controls.Cell Cell
		{
			get{return m_Cell;}
			set{m_Cell = value;}
		}

		public int Column
		{
			get{return m_Column;}
			set{m_Column = value;}
		}
		public int Row
		{
			get{return m_Row;}
			set{m_Row = value;}
		}
		public CellEventArgs(ControlWare.Windows.Controls.Cell pCell)
		{
			m_Cell = pCell;
		}
		public CellEventArgs(int pRow,int pColumn)
		{
			m_Row = pRow;
			m_Column = pColumn;
		}
	}

	#endregion

	#region CellArrayEventArgs

	public class CellArrayEventArgs : EventArgs
	{
		protected object[] m_Cells;

		public object[] Cells
		{
			get{return m_Cells;}
			set{m_Cells = value;}
		}

		public CellArrayEventArgs(object[] cells)
		{
			m_Cells = cells;
		}
	}

	#endregion

	#region ScrollPositionChangedEventArgs

	public class ScrollPositionChangedEventArgs : EventArgs
	{
		protected int m_Delta;
		//protected int m_HScrollValue;
		//protected int m_OldHScrollValue;

		public int Delta
		{
			get{return m_Delta;}
			set{m_Delta = value;}
		}

		public ScrollPositionChangedEventArgs(int HScrollValue,int OldHScrollValue)
		{
			m_Delta = HScrollValue - OldHScrollValue;
		}
	}

	#endregion
*/
	#endregion

	#region LinkClickEvent

	public delegate void LinkClickEventHandler(object sender,Nistec.WinForms.LinkClickEvent e);

	public class LinkClickEvent : EventArgs 
	{

		private string m_Data;
        private string m_Name;

		// New 
		public LinkClickEvent(string data,string name) 
		{
			m_Data=data;
            m_Name = name;
		}

		public string Target 
		{
			get{return m_Data;}
		}

        public string Name
        {
            get { return m_Name; }
        }
		public void Link()
		{
			// Determine which link was clicked within the McLinkLabel.
			//this.mLink.Links[mLink.Links.IndexOf(e.Link)].Visited = true;
			try
			{
				
				if(null != m_Data)// && target.StartsWith("www"))
				{
					System.Diagnostics.Process.Start(m_Data);
				}
			}
	
			catch(Exception ex)
			{
				MessageBox.Show (ex.Message.ToString (),"Nistec"); 
			}
		}

	}
	#endregion

	#region ContextEventArgs

	public delegate void ContextEventHandler(object sender, ContextEventArgs e);

	public class ContextEventArgs : EventArgs
	{
		private Point m_screenPos;

		#region Constructors

		public ContextEventArgs()
		{
		}

		public ContextEventArgs(Point screenPos)
		{
			this.m_screenPos = screenPos;
		}

		#endregion

		#region Properties

		public Point ScreenPos
		{
			get {return m_screenPos;}
			set {m_screenPos = value;}
		}

		#endregion
	}

	#endregion

	#region ContextMenuEvent

	/// <summary>
	/// Summary description for ContextMenuEvent.
	/// </summary>
	public class ContextMenuEvent :EventArgs
	{
		public ContextMenuEvent()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ContextMenuEvent(System.Windows.Forms.MenuItem mnuItem)
		{
			mItem = mnuItem;
			mRow = -1;
			mCol = -1;
			mValue = null;
		}
		
		public ContextMenuEvent(System.Windows.Forms.MenuItem mnuItem, int rowNum ,int colNum , object iValue)
		{
			mItem = mnuItem;
			mRow = rowNum;
			mCol = colNum;
			mValue = iValue;
		}

		protected System.Windows.Forms.MenuItem mItem;
		protected CurrencyManager mCManger;
		protected int mRow;
		protected int mCol;
		protected object mValue;

		public  System.Windows.Forms.MenuItem MnuItem 
		{
			get{return mItem;}
		}
		public int  Row
		{
			get{return mRow;}
		}

		public int Col
		{
			get{return mCol;}
		}
		public object Value
		{
			get {return mValue;}
		}

		public CurrencyManager CM
		{
			get{return mCManger;}
		}

	}

	#endregion

	public class TreeValueChangedEventArgs : EventArgs
	{

		public TreeValueChangedEventArgs(McTreeView treeView, string text)
		{
			this.McTreeView = treeView;
			this.Text = text;
		}

		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}
 
		public McTreeView McTreeView
		{
			get
			{
				return this.treeView;
			}
			set
			{
				this.treeView = value;
			}
		}
 
		// Fields
		private string text;
		private McTreeView treeView;
	}

	public class TreeNodesFillEventArgs : EventArgs
	{
		public TreeNodesFillEventArgs(TreeNodeCollection nodes, McTreeView treeView)
		{
			this.Nodes = nodes;
			this.McTreeView = treeView;
		}


		// Fields
		public TreeNodeCollection Nodes;
		public McTreeView McTreeView;
	}

	public delegate void TreeNodesFillEventHandler(object sender,TreeNodesFillEventArgs e);
	public delegate void TreeValueChangedEventHandler(object sender, TreeValueChangedEventArgs e);


	//public delegate void CellEventHandler(object sender,CellEventArgs e);
	//public delegate void CellArrayEventHandler(object sender,CellArrayEventArgs e);
	//public delegate void ScrollPositionChangedEventHandler(object sender,ScrollPositionChangedEventArgs e);
	
}

namespace Nistec.Collections
{
	#region CollectionChangeEventArgs

	public class CollectionChangeEventArgs : EventArgs
	{
		private int index = -1;

		public CollectionChangeEventArgs(int index)
		{
			this.index = index;
		}

		public int Index
		{
			get {return this.index;}
		}
	}

	#endregion
		
	public delegate void CollectionClearEventHandler(object sender, EventArgs e);
	public delegate void CollectionChangeEventHandler(object sender, Nistec.Collections.CollectionChangeEventArgs e);
}



