namespace Nistec.Charts
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Security.Permissions;
    using System.Web;
    using System.Web.UI;

    /// <summary>Creates a control that displays an image on a page. When a hot spot region defined within the <see cref="T:ChartImageMap"></see> control is clicked, the control either generates a postback to the server or navigates to a specified URL.</summary>
    [SupportsEventValidation, ParseChildren(true, "HotSpots"), DefaultProperty("HotSpots"), DefaultEvent("Click"), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public class ChartImageMap : System.Web.UI.WebControls.Image, IPostBackEventHandler
    {
        private bool _hasHotSpots;
        private ChartHotSpotCollection _hotSpots;
        private static readonly object EventClick = new object();

        /// <summary>Occurs when a <see cref="T:ChartHotSpot"></see> object in an <see cref="T:ChartImageMap"></see> control is clicked.</summary>
        [Category("Action"), Description("ImageMap_Click")]
        public event ChartImageMapEventHandler Click
        {
            add
            {
                base.Events.AddHandler(EventClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventClick, value);
            }
        }

        /// <summary>Adds the HTML attributes and styles of an <see cref="T:ChartImageMap"></see> control to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter"></see>.</summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client. </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (this._hasHotSpots)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Usemap, "#ImageMap" + this.ClientID, false);
            }
        }

        /// <summary>Restores view-state information for the <see cref="T:ChartImageMap"></see> control from a previous page request that was saved by the <see cref="M:ChartImageMap.SaveViewState"></see> method.</summary>
        /// <param name="savedState">An <see cref="T:System.Object"></see> that represents the <see cref="T:ChartImageMap"></see> control to restore. </param>
        protected override void LoadViewState(object savedState)
        {
            object obj2 = null;
            object[] objArray = null;
            if (savedState != null)
            {
                objArray = (object[]) savedState;
                if (objArray.Length != 2)
                {
                    throw new ArgumentException("ViewState_InvalidViewState");
                }
                obj2 = objArray[0];
            }
            base.LoadViewState(obj2);
            if ((objArray != null) && (objArray[1] != null))
            {
                ((IStateManager) this.HotSpots).LoadViewState(objArray[1]);
            }
        }

        /// <summary>Raises the <see cref="E:ChartImageMap.Click"></see> event for the <see cref="T:ChartImageMap"></see> control.</summary>
        /// <param name="e">An argument of type <see cref="T:ChartImageMapEventArgs"></see> that contains the event data. </param>
        protected virtual void OnClick(ChartImageMapEventArgs e)
        {
            ChartImageMapEventHandler handler = (ChartImageMapEventHandler)base.Events[EventClick];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>Raises events for the <see cref="T:ChartImageMap"></see> control when a form is posted back to the server.</summary>
        /// <param name="eventArgument">The argument for the event.</param>
        protected virtual void RaisePostBackEvent(string eventArgument)
        {
            //base.ValidateEvent(this.UniqueID, eventArgument);
            string postBackValue = null;
            if ((eventArgument != null) && (this._hotSpots != null))
            {
                int num = int.Parse(eventArgument, CultureInfo.InvariantCulture);
                if ((num >= 0) && (num < this._hotSpots.Count))
                {
                    ChartHotSpot spot = this._hotSpots[num];
                    ChartHotSpotMode hotSpotMode = spot.HotSpotMode;
                    switch (hotSpotMode)
                    {
                        case ChartHotSpotMode.NotSet:
                            hotSpotMode = this.HotSpotMode;
                            break;

                        case ChartHotSpotMode.PostBack:
                            postBackValue = spot.PostBackValue;
                            goto Label_0061;
                    }
                }
            }
        Label_0061:
            if (postBackValue != null)
            {
                this.OnClick(new ChartImageMapEventArgs(postBackValue));
            }
        }

        /// <summary>Sends the <see cref="T:ChartImageMap"></see> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter"></see> object, which writes the content to render on the client.</summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"></see> object that receives the <see cref="T:ChartImageMap"></see> control content. </param>
        protected  override void Render(HtmlTextWriter writer)
        {
            if (this.Enabled && !base.IsEnabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            }
            this._hasHotSpots = (this._hotSpots != null) && (this._hotSpots.Count > 0);
            base.Render(writer);
            if (this._hasHotSpots)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, "ImageMap" + this.ClientID);
                writer.RenderBeginTag(HtmlTextWriterTag.Map);
                ChartHotSpotMode hotSpotMode = this.HotSpotMode;
                if (hotSpotMode == ChartHotSpotMode.NotSet)
                {
                    hotSpotMode = ChartHotSpotMode.Navigate;
                }
                int num = 0;
                string target = this.Target;
                foreach (ChartHotSpot spot in this._hotSpots)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Shape, spot.MarkupName, false);
                    writer.AddAttribute(HtmlTextWriterAttribute.Coords, spot.GetCoordinates());
                    ChartHotSpotMode mode2 = spot.HotSpotMode;
                    switch (mode2)
                    {
                        case ChartHotSpotMode.NotSet:
                            mode2 = hotSpotMode;
                            break;

                        case ChartHotSpotMode.PostBack:
                        {
                            if (this.Page != null)
                            {
                                this.Page.VerifyRenderingInServerForm(this);
                            }
                            string argument = num.ToString(CultureInfo.InvariantCulture);
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, argument, true));
                            goto Label_016F;
                        }
                        case ChartHotSpotMode.Navigate:
                        {
                            string str3 = base.ResolveClientUrl(spot.NavigateUrl);
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, str3);
                            string str4 = spot.Target;
                            if (str4.Length == 0)
                            {
                                str4 = target;
                            }
                            if (str4.Length > 0)
                            {
                                writer.AddAttribute(HtmlTextWriterAttribute.Target, str4);
                            }
                            goto Label_016F;
                        }
                        case ChartHotSpotMode.Inactive:
                            writer.AddAttribute("nohref", "true");
                            goto Label_016F;
                    }
                Label_016F:
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, spot.AlternateText);
                    writer.AddAttribute(HtmlTextWriterAttribute.Alt, spot.AlternateText);
                    string accessKey = spot.AccessKey;
                    if (accessKey.Length > 0)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
                    }
                    int tabIndex = spot.TabIndex;
                    if (tabIndex != 0)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString(NumberFormatInfo.InvariantInfo));
                    }
                    writer.RenderBeginTag(HtmlTextWriterTag.Area);
                    writer.RenderEndTag();
                    num++;
                }
                writer.RenderEndTag();
            }
        }

        /// <summary>Saves any changes to an <see cref="T:ChartImageMap"></see> control's view-state that have occurred since the time the page was posted back to the server.</summary>
        /// <returns>Returns the <see cref="T:ChartImageMap"></see> control's current view state. If there is no view state associated with the control, this method returns null.</returns>
        protected override object SaveViewState()
        {
            object obj2 = base.SaveViewState();
            object obj3 = null;
            if ((this._hotSpots != null) && (this._hotSpots.Count > 0))
            {
                obj3 = ((IStateManager) this._hotSpots).SaveViewState();
            }
            if ((obj2 == null) && (obj3 == null))
            {
                return null;
            }
            return new object[] { obj2, obj3 };
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.RaisePostBackEvent(eventArgument);
        }

        /// <summary>Tracks view-state changes to the <see cref="T:ChartImageMap"></see> control so they can be stored in the control's <see cref="T:ChartStateBag"></see> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState"></see> property.</summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();
            if (this._hotSpots != null)
            {
                ((IStateManager) this._hotSpots).TrackViewState();
            }
        }

        /// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
        /// <returns>true if the control is to respond to user clicks; otherwise, false.</returns>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        /// <summary>Gets or sets the default behavior for the <see cref="T:ChartHotSpot"></see> objects of an <see cref="T:ChartImageMap"></see> control when the <see cref="T:ChartHotSpot"></see> objects are clicked.</summary>
        /// <returns>One of the <see cref="T:ChartHotSpotMode"></see> enumeration values. The default is NotSet.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:ChartHotSpotMode"></see> enumeration values. </exception>
        [DefaultValue(0), Category("Behavior"), Description("HotSpot_HotSpotMode")]
        public virtual ChartHotSpotMode HotSpotMode
        {
            get
            {
                object obj2 = this.ViewState["HotSpotMode"];
                if (obj2 != null)
                {
                    return (ChartHotSpotMode) obj2;
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

        /// <summary>Gets a collection of <see cref="T:ChartHotSpot"></see> objects that represents the defined hot spot regions in an <see cref="T:ChartImageMap"></see> control.</summary>
        /// <returns>A <see cref="T:ChartHotSpotCollection"></see> object that represents the defined hot spot regions in an <see cref="T:ChartImageMap"></see> control.</returns>
        [Category("Behavior"), Description("ImageMap_HotSpots"), PersistenceMode(PersistenceMode.InnerDefaultProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        public ChartHotSpotCollection HotSpots
        {
            get
            {
                if (this._hotSpots == null)
                {
                    this._hotSpots = new ChartHotSpotCollection();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager) this._hotSpots).TrackViewState();
                    }
                }
                return this._hotSpots;
            }
        }

        /// <summary>Gets or sets the target window or frame that displays the Web page content linked to when the <see cref="T:ChartImageMap"></see> control is clicked.</summary>
        /// <returns>The target window or frame that displays the specified Web page when the <see cref="T:ChartImageMap"></see> control is clicked. Values must begin with a letter in the range of A through Z (case-insensitive), except for the following special values, which begin with an underscore: _blank Renders the content in a new window without frames. _parent Renders the content in the immediate frameset parent. _searchRenders the content in the search pane._self Renders the content in the frame with focus. _top Renders the content in the full window without frames. Check your browser documentation to determine if the _search value is supported.  For example, Microsoft Internet Explorer 5.0 and later support the _search target value.The default value is an empty string ("").</returns>
        [Description("HotSpot_Target"), Category("Behavior"), DefaultValue("")]
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
    }
}

