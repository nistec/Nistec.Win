using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Messaging;



namespace MControl.Win
{
  
    #region ErrorOcurredEventArgs

    public delegate void ErrorOcurredEventHandler(object sender, ErrorOcurredEventArgs e);

    public class ErrorOcurredEventArgs : EventArgs
    {
        private string m_Message = "";

        #region Constructors

        public ErrorOcurredEventArgs(string message)
        {
            if (message == null)
                message = "";
            m_Message = message;
        }

        #endregion

        #region Properties

        public string Message
        {
            get { return m_Message; }
            set
            {
                if (value == null)
                    value = "";
                m_Message = value;
            }
        }

        #endregion
    }

    #endregion

    #region PropertyItemChangedEventArgs

    public delegate void PropertyItemChangedEventHandler(object sender, PropertyItemChangedEventArgs e);

    public class PropertyItemChangedEventArgs : EventArgs
    {
        private string m_PropertyName = "";
        private object m_PropertyValue = null;
        public PropertyItemChangedEventArgs(string propertyName, object popertyValue)
        {
            m_PropertyName = propertyName;
            m_PropertyValue = popertyValue;
        }

        public string PropertyName
        {
            get { return m_PropertyName; }
        }

        public object PropertyValue
        {
            get { return m_PropertyValue; }
        }
    }

    #endregion

    #region DateChangedEventArgs

    public delegate void DateChangedEventHandler(object sender, DateChangedEventArgs e);

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
            get { return m_SelectedDate; }
        }

        #endregion

    }

    #endregion

    #region ValueChangedEventArgs

    public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

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
            get { return m_Value; }
        }

        #endregion

    }

    #endregion

    #region ValidatingEventArgs

    public delegate void ValidatingEventHandler(object sender, ValidatingEventArgs e);

    public class ValidatingEventArgs : System.ComponentModel.CancelEventArgs
    {
        private object m_NewValue = null;
        public ValidatingEventArgs(object newValue)
            : base(false)
        {
            m_NewValue = newValue;
        }
        public object NewValue
        {
            get { return m_NewValue; }
            set { m_NewValue = value; }
        }
    }

    #endregion
  
}


