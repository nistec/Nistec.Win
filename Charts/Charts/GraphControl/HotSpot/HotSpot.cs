namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.UI;
    using System.Collections;

    /// <summary>Implements the basic functionality common to all hot spot shapes.</summary>
    [TypeConverter(typeof(ExpandableObjectConverter)), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    [Serializable]
    public abstract class ChartHotSpot : IStateManager
    {
        private bool _isTrackingViewState;
        private ChartStateBag _viewState;
        //private bool marked;
        private bool isDirty;

        public bool IsDirty
        {
            get { return isDirty; }
        }

        /// <summary>Initializes a new instance of the <see cref="T:ChartHotSpot"></see> class.</summary>
        protected ChartHotSpot()
        {
        }

        /// <summary>When overridden in a derived class, returns a string that represents the coordinates of the <see cref="T:ChartHotSpot"></see> region.</summary>
        /// <returns>A string that represents the coordinates of the <see cref="T:ChartHotSpot"></see> region.</returns>
        public abstract string GetCoordinates();


        /// <summary>Restores the <see cref="T:ChartHotSpot"></see> object's previously saved view state to the object.</summary>
        /// <param name="savedState">An <see cref="T:System.Object"></see> that represents the <see cref="T:ChartHotSpot"></see> object to restore. </param>
        protected virtual void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                this.ViewState.LoadViewState(savedState);
            }
        }

        //internal void LoadViewState(object state)
        //{
        //    if (state != null)
        //    {
        //        ArrayList list = (ArrayList)state;
        //        for (int i = 0; i < list.Count; i += 2)
        //        {
        //            string key = ((IndexedString)list[i]).Value;
        //            object obj2 = list[i + 1];
        //            ViewState.Add(key, obj2);
        //        }
        //    }
        //}
        //internal object SaveViewStateInternal()
        //{
        //    ArrayList list = null;
        //    if (ViewState.Count != 0)
        //    {
        //        //IDictionaryEnumerator enumerator = ViewState.GetEnumerator();
        //        IEnumerator enumerator = ViewState.GetEnumerator();

        //        while (enumerator.MoveNext())
        //        {
        //            StateItem item = (StateItem)enumerator.Current;//.Value;
        //            if (item.IsDirty)
        //            {
        //                if (list == null)
        //                {
        //                    list = new ArrayList();
        //                }
        //                list.Add(new IndexedString((string)enumerator.Key));
        //                list.Add(item.Value);
        //            }
        //        }
        //    }
        //    return list;
        //}

        //internal void TrackViewStateInternal()
        //{
        //   // this.marked = true;
        //    _isTrackingViewState = true;
        //}

        //internal bool IsTrackingViewStateInternal
        //{
        //    get
        //    {
        //        return this.marked;
        //    }
        //}

        /// <summary>Saves the changes to the <see cref="T:ChartHotSpot"></see> object's view state since the time the page was posted back to the server.</summary>
        /// <returns>The <see cref="T:System.Object"></see> that contains the changes to the <see cref="T:ChartHotSpot"></see> object's view state. If no view state is associated with the object, this method returns null.</returns>
        protected virtual object SaveViewState()
        {
            //return SaveViewStateInternal();

            if (this._viewState != null)
            {
                return this._viewState.SaveViewState();
                //return SaveViewStateInternal();
            }
            return null;
        }

        internal void SetDirty()
        {
            if (this._viewState != null)
            {
                this._viewState.SetDirty(true);
            }
            isDirty = true;
        }

        void IStateManager.LoadViewState(object savedState)
        {
            this.LoadViewState(savedState);
        }

        object IStateManager.SaveViewState()
        {
            return this.SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            this.TrackViewState();
        }

        /// <summary>Returns the <see cref="T:System.String"></see> representation of this instance of a <see cref="T:ChartHotSpot"></see> object.</summary>
        /// <returns>A string that represents this <see cref="T:ChartHotSpot"></see> object.</returns>
        public override string ToString()
        {
            return base.GetType().Name;
        }

        /// <summary>Causes the <see cref="T:ChartHotSpot"></see> object to track changes to its view state so they can be stored in the object's <see cref="T:ChartStateBag"></see> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState"></see> property.</summary>
        protected virtual void TrackViewState()
        {
            this._isTrackingViewState = true;
            if (this._viewState != null)
            {
                this._viewState.TrackViewState();
                //TrackViewStateInternal();
            }
        }

        /// <summary>Gets or sets the access key that allows you to quickly navigate to the <see cref="T:ChartHotSpot"></see> region.</summary>
        /// <returns>The access key for quick navigation to the <see cref="T:ChartHotSpot"></see> region. The default value is <see cref="F:System.String.Empty"></see>, which indicates that this property is not set.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified access key is neither a null reference, an empty string (""), nor a single character string. </exception>
        [Category("Accessibility"), Description("HotSpot_AccessKey"), Localizable(true), DefaultValue("")]
        public virtual string AccessKey
        {
            get
            {
                string str = (string) this.ViewState["AccessKey"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                if ((value != null) && (value.Length > 1))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["AccessKey"] = value;
            }
        }

        /// <summary>Gets or sets the alternate text to display for a <see cref="T:ChartHotSpot"></see> object in an <see cref="T:ChartImageMap"></see> control when the image is unavailable or renders to a browser that does not support images.</summary>
        /// <returns>The text displayed in place of the <see cref="T:ChartHotSpot"></see> when the <see cref="T:ChartImageMap"></see> control's image is unavailable. The default value is an empty string ("").</returns>
        [NotifyParentProperty(true), Description("HotSpot_AlternateText"), Localizable(true), Bindable(true), Category("Behavior"), DefaultValue("")]
        public virtual string AlternateText
        {
            get
            {
                object obj2 = this.ViewState["AlternateText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["AlternateText"] = value;
            }
        }

        /// <summary>Gets or sets the behavior of a <see cref="T:ChartHotSpot"></see> object in an <see cref="T:ChartImageMap"></see> control when the <see cref="T:ChartHotSpot"></see> is clicked.</summary>
        /// <returns>One of the <see cref="T:ChartHotSpotMode"></see> enumeration values. The default is Default.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:ChartHotSpotMode"></see> enumeration values. </exception>
        [Category("Behavior"), NotifyParentProperty(true), DefaultValue(0), Description("HotSpot_HotSpotMode")]
        public virtual ChartHotSpotMode HotSpotMode
        {
            get
            {
                object obj2 = this.ViewState["HotSpotMode"];
                if (obj2 != null)
                {
                    return (ChartHotSpotMode)obj2;
                }
                return ChartHotSpotMode.NotSet;
            }
            set
            {
                if ((value < ChartHotSpotMode.NotSet) || (value > ChartHotSpotMode.Inactive))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["HotSpotMode"] = value;
            }
        }

        /// <summary>Gets a value indicating whether the <see cref="T:ChartHotSpot"></see> object is tracking its view-state changes.</summary>
        /// <returns>true if the <see cref="T:ChartHotSpot"></see> object is tracking its view-state changes; otherwise, false.</returns>
        protected virtual bool IsTrackingViewState
        {
            get
            {
                return this._isTrackingViewState;
            }
        }

        /// <summary>When overridden in a derived class, gets the string representation for the <see cref="T:ChartHotSpot"></see> object's shape.</summary>
        /// <returns>A string that represents the name of the <see cref="T:ChartHotSpot"></see> object's shape.</returns>
        protected internal abstract string MarkupName { get; }

        /// <summary>Gets or sets the URL to navigate to when a <see cref="T:ChartHotSpot"></see> object is clicked.</summary>
        /// <returns>The URL to navigate to when a <see cref="T:ChartHotSpot"></see> object is clicked. The default is an empty string ("").</returns>
        [Description("HotSpot_NavigateUrl"), NotifyParentProperty(true), Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Behavior"), DefaultValue(""), Bindable(true), UrlProperty]
        public string NavigateUrl
        {
            get
            {
                object obj2 = this.ViewState["NavigateUrl"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["NavigateUrl"] = value;
            }
        }

        /// <summary>Gets or sets the name of the <see cref="T:ChartHotSpot"></see> object to pass in the event data when the <see cref="T:ChartHotSpot"></see> is clicked.</summary>
        /// <returns>The name of the <see cref="T:ChartHotSpot"></see> object to pass in the event data when the <see cref="T:ChartHotSpot"></see> is clicked. The default is an empty string ("").</returns>
        [Bindable(true), NotifyParentProperty(true), Category("Behavior"), DefaultValue(""), Description("HotSpot_PostBackValue")]
        public string PostBackValue
        {
            get
            {
                object obj2 = this.ViewState["PostBackValue"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["PostBackValue"] = value;
            }
        }

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return this.IsTrackingViewState;
            }
        }

        /// <summary>Gets or sets the tab index of the <see cref="T:ChartHotSpot"></see> region.</summary>
        /// <returns>The tab index of the <see cref="T:ChartHotSpot"></see> region. The default is 0, which indicates that this property is not set.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified tab index is not between -32768 and 32767. </exception>
        [Description("HotSpot_TabIndex"), DefaultValue((short) 0), Category("Accessibility")]
        public virtual short TabIndex
        {
            get
            {
                object obj2 = this.ViewState["TabIndex"];
                if (obj2 != null)
                {
                    return (short) obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["TabIndex"] = value;
            }
        }

        /// <summary>Gets or sets the target window or frame in which to display the Web page content linked to when a <see cref="T:ChartHotSpot"></see> object that navigates to a URL is clicked.</summary>
        /// <returns>The target window or frame in which to load the Web page linked to when a <see cref="T:ChartHotSpot"></see> object that navigates to a URL is clicked. The default value is an empty string (""), which refreshes the window or frame with focus.</returns>
        [DefaultValue(""), NotifyParentProperty(true), Category("Behavior"), TypeConverter(typeof(System.Web.UI.WebControls.TargetConverter)), Description("HotSpot_Target")]
        public virtual string Target
        {
            get
            {
                object obj2 = this.ViewState["Target"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        /// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:ChartHotSpot"></see> object across multiple requests for the same page.</summary>
        /// <returns>An instance of the <see cref="T:ChartStateBag"></see> class that contains the <see cref="T:ChartHotSpot"></see> region's view-state information.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected ChartStateBag /*System.Web.SessionState.HttpSessionState*/ ViewState
        {
            get
            {
                if (this._viewState == null)
                {
                    this._viewState = new ChartStateBag(false);
                    if (this._isTrackingViewState)
                    {
                        ((IStateManager)this._viewState).TrackViewState();
                    }
                }
                return this._viewState;
                
                //return HttpContext.Current.Session;
            }
        }
    }
}

