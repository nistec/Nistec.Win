using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using mControl.Util;
using System.Threading;
using mControl.Threading;
using mControl.Data;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.Design;
using System.Globalization;

namespace mControl.GridView
{
    public partial class GridColumnsDialog : Form
    {


    // Methods
    internal GridColumnsDialog()
    {
        this.InitializeComponent();
        this.dataGridViewPrivateCopy = new Grid();
        this.columnsPrivateCopy = this.dataGridViewPrivateCopy.Columns;
        this.columnsPrivateCopy.CollectionChanged += new CollectionChangeEventHandler(this.columnsPrivateCopy_CollectionChanged);
    }

    private void addButton_Click(object sender, EventArgs e)
    {
        int count;
        if (this.selectedColumns.SelectedIndex == -1)
        {
            count = this.selectedColumns.Items.Count;
        }
        else
        {
            count = this.selectedColumns.SelectedIndex + 1;
        }
        if (this.addColumnDialog == null)
        {
            this.addColumnDialog = new DataGridViewAddColumnDialog(this.columnsPrivateCopy, this.liveDataGridView);
            this.addColumnDialog.StartPosition = FormStartPosition.CenterParent;
        }
        this.addColumnDialog.Start(count, false);
        this.addColumnDialog.ShowDialog(this);
    }

    private void columnsPrivateCopy_CollectionChanged(object sender, CollectionChangeEventArgs e)
    {
        if (!this.columnCollectionChanging)
        {
            this.PopulateSelectedColumns();
            if (e.Action == CollectionChangeAction.Add)
            {
                this.selectedColumns.SelectedIndex = this.columnsPrivateCopy.IndexOf((DataGridViewColumn) e.Element);
                ListBoxItem selectedItem = this.selectedColumns.SelectedItem as ListBoxItem;
                this.userAddedColumns[selectedItem.DataGridViewColumn] = true;
                this.columnsNames[selectedItem.DataGridViewColumn] = selectedItem.DataGridViewColumn.Name;
            }
            this.formIsDirty = true;
        }
    }

    private void ColumnTypeChanged(ListBoxItem item, Type newType)
    {
        DataGridViewColumn dataGridViewColumn = item.DataGridViewColumn;
        DataGridViewColumn destColumn = Activator.CreateInstance(newType) as DataGridViewColumn;
        ITypeResolutionService tr = this.liveDataGridView.Site.GetService(iTypeResolutionServiceType) as ITypeResolutionService;
        ComponentDesigner componentDesignerForType = DataGridViewAddColumnDialog.GetComponentDesignerForType(tr, newType);
        CopyDataGridViewColumnProperties(dataGridViewColumn, destColumn);
        CopyDataGridViewColumnState(dataGridViewColumn, destColumn);
        this.columnCollectionChanging = true;
        int selectedIndex = this.selectedColumns.SelectedIndex;
        this.selectedColumns.Focus();
        base.ActiveControl = this.selectedColumns;
        try
        {
            ListBoxItem selectedItem = (ListBoxItem) this.selectedColumns.SelectedItem;
            bool flag = (bool) this.userAddedColumns[selectedItem.DataGridViewColumn];
            string str = string.Empty;
            if (this.columnsNames.Contains(selectedItem.DataGridViewColumn))
            {
                str = (string) this.columnsNames[selectedItem.DataGridViewColumn];
                this.columnsNames.Remove(selectedItem.DataGridViewColumn);
            }
            if (this.userAddedColumns.Contains(selectedItem.DataGridViewColumn))
            {
                this.userAddedColumns.Remove(selectedItem.DataGridViewColumn);
            }
            if (selectedItem.DataGridViewColumnDesigner != null)
            {
                TypeDescriptor.RemoveAssociation(selectedItem.DataGridViewColumn, selectedItem.DataGridViewColumnDesigner);
            }
            this.selectedColumns.Items.RemoveAt(selectedIndex);
            this.selectedColumns.Items.Insert(selectedIndex, new ListBoxItem(destColumn, this, componentDesignerForType));
            this.columnsPrivateCopy.RemoveAt(selectedIndex);
            destColumn.DisplayIndex = -1;
            this.columnsPrivateCopy.Insert(selectedIndex, destColumn);
            if (!string.IsNullOrEmpty(str))
            {
                this.columnsNames[destColumn] = str;
            }
            this.userAddedColumns[destColumn] = flag;
            this.FixColumnCollectionDisplayIndices();
            this.selectedColumns.SelectedIndex = selectedIndex;
            this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
        }
        finally
        {
            this.columnCollectionChanging = false;
        }
    }

    private void CommitChanges()
    {
        if (this.formIsDirty)
        {
            try
            {
                IComponentChangeService service = (IComponentChangeService) this.liveDataGridView.Site.GetService(iComponentChangeServiceType);
                PropertyDescriptor member = TypeDescriptor.GetProperties(this.liveDataGridView)["Columns"];
                IContainer container = (this.liveDataGridView.Site != null) ? this.liveDataGridView.Site.Container : null;
                DataGridViewColumn[] array = new DataGridViewColumn[this.liveDataGridView.Columns.Count];
                this.liveDataGridView.Columns.CopyTo(array, 0);
                service.OnComponentChanging(this.liveDataGridView, member);
                this.liveDataGridView.Columns.Clear();
                service.OnComponentChanged(this.liveDataGridView, member, null, null);
                if (container != null)
                {
                    for (int m = 0; m < array.Length; m++)
                    {
                        container.Remove(array[m]);
                    }
                }
                DataGridViewColumn[] columnArray2 = new DataGridViewColumn[this.columnsPrivateCopy.Count];
                bool[] flagArray = new bool[this.columnsPrivateCopy.Count];
                string[] strArray = new string[this.columnsPrivateCopy.Count];
                for (int i = 0; i < this.columnsPrivateCopy.Count; i++)
                {
                    DataGridViewColumn column = (DataGridViewColumn) this.columnsPrivateCopy[i].Clone();
                    column.ContextMenuStrip = this.columnsPrivateCopy[i].ContextMenuStrip;
                    columnArray2[i] = column;
                    flagArray[i] = (bool) this.userAddedColumns[this.columnsPrivateCopy[i]];
                    strArray[i] = (string) this.columnsNames[this.columnsPrivateCopy[i]];
                }
                if (container != null)
                {
                    for (int n = 0; n < columnArray2.Length; n++)
                    {
                        if (!string.IsNullOrEmpty(strArray[n]) && ValidateName(container, strArray[n], columnArray2[n]))
                        {
                            container.Add(columnArray2[n], strArray[n]);
                        }
                        else
                        {
                            container.Add(columnArray2[n]);
                        }
                    }
                }
                service.OnComponentChanging(this.liveDataGridView, member);
                for (int j = 0; j < columnArray2.Length; j++)
                {
                    PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(columnArray2[j])["DisplayIndex"];
                    if (descriptor2 != null)
                    {
                        descriptor2.SetValue(columnArray2[j], -1);
                    }
                    this.liveDataGridView.Columns.Add(columnArray2[j]);
                }
                service.OnComponentChanged(this.liveDataGridView, member, null, null);
                for (int k = 0; k < flagArray.Length; k++)
                {
                    PropertyDescriptor descriptor3 = TypeDescriptor.GetProperties(columnArray2[k])["UserAddedColumn"];
                    if (descriptor3 != null)
                    {
                        descriptor3.SetValue(columnArray2[k], flagArray[k]);
                    }
                }
            }
            catch (InvalidOperationException exception)
            {
                IUIService uiService = (IUIService) this.liveDataGridView.Site.GetService(typeof(IUIService));
                DataGridViewDesigner.ShowErrorDialog(uiService, exception, this.liveDataGridView);
                base.DialogResult = DialogResult.Cancel;
            }
        }
    }

    private void componentChanged(object sender, ComponentChangedEventArgs e)
    {
        if ((e.Component is ListBoxItem) && this.selectedColumns.Items.Contains(e.Component))
        {
            this.formIsDirty = true;
        }
    }

    private static void CopyDataGridViewColumnProperties(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
    {
        destColumn.AutoSizeMode = srcColumn.AutoSizeMode;
        destColumn.ContextMenuStrip = srcColumn.ContextMenuStrip;
        destColumn.DataPropertyName = srcColumn.DataPropertyName;
        if (srcColumn.HasDefaultCellStyle)
        {
            CopyDefaultCellStyle(srcColumn, destColumn);
        }
        destColumn.DividerWidth = srcColumn.DividerWidth;
        destColumn.HeaderText = srcColumn.HeaderText;
        destColumn.MinimumWidth = srcColumn.MinimumWidth;
        destColumn.Name = srcColumn.Name;
        destColumn.SortMode = srcColumn.SortMode;
        destColumn.Tag = srcColumn.Tag;
        destColumn.ToolTipText = srcColumn.ToolTipText;
        destColumn.Width = srcColumn.Width;
        destColumn.FillWeight = srcColumn.FillWeight;
    }

    private static void CopyDataGridViewColumnState(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
    {
        destColumn.Frozen = srcColumn.Frozen;
        destColumn.Visible = srcColumn.Visible;
        destColumn.ReadOnly = srcColumn.ReadOnly;
        destColumn.Resizable = srcColumn.Resizable;
    }

    private static void CopyDefaultCellStyle(DataGridViewColumn srcColumn, DataGridViewColumn destColumn)
    {
        Type c = srcColumn.GetType();
        Type type = destColumn.GetType();
        if (c.IsAssignableFrom(type) || type.IsAssignableFrom(c))
        {
            destColumn.DefaultCellStyle = srcColumn.DefaultCellStyle;
        }
        else
        {
            DataGridViewColumn column = null;
            try
            {
                column = Activator.CreateInstance(c) as DataGridViewColumn;
            }
            catch (Exception exception)
            {
                if (ClientUtils.IsCriticalException(exception))
                {
                    throw;
                }
                column = null;
            }
            catch
            {
                column = null;
            }
            if ((column == null) || (column.DefaultCellStyle.Alignment != srcColumn.DefaultCellStyle.Alignment))
            {
                destColumn.DefaultCellStyle.Alignment = srcColumn.DefaultCellStyle.Alignment;
            }
            if ((column == null) || !column.DefaultCellStyle.BackColor.Equals(srcColumn.DefaultCellStyle.BackColor))
            {
                destColumn.DefaultCellStyle.BackColor = srcColumn.DefaultCellStyle.BackColor;
            }
            if (((column != null) && (srcColumn.DefaultCellStyle.Font != null)) && !srcColumn.DefaultCellStyle.Font.Equals(column.DefaultCellStyle.Font))
            {
                destColumn.DefaultCellStyle.Font = srcColumn.DefaultCellStyle.Font;
            }
            if ((column == null) || !column.DefaultCellStyle.ForeColor.Equals(srcColumn.DefaultCellStyle.ForeColor))
            {
                destColumn.DefaultCellStyle.ForeColor = srcColumn.DefaultCellStyle.ForeColor;
            }
            if ((column == null) || !column.DefaultCellStyle.Format.Equals(srcColumn.DefaultCellStyle.Format))
            {
                destColumn.DefaultCellStyle.Format = srcColumn.DefaultCellStyle.Format;
            }
            if ((column == null) || (column.DefaultCellStyle.Padding != srcColumn.DefaultCellStyle.Padding))
            {
                destColumn.DefaultCellStyle.Padding = srcColumn.DefaultCellStyle.Padding;
            }
            if ((column == null) || !column.DefaultCellStyle.SelectionBackColor.Equals(srcColumn.DefaultCellStyle.SelectionBackColor))
            {
                destColumn.DefaultCellStyle.SelectionBackColor = srcColumn.DefaultCellStyle.SelectionBackColor;
            }
            if ((column == null) || !column.DefaultCellStyle.SelectionForeColor.Equals(srcColumn.DefaultCellStyle.SelectionForeColor))
            {
                destColumn.DefaultCellStyle.SelectionForeColor = srcColumn.DefaultCellStyle.SelectionForeColor;
            }
            if ((column == null) || (column.DefaultCellStyle.WrapMode != srcColumn.DefaultCellStyle.WrapMode))
            {
                destColumn.DefaultCellStyle.WrapMode = srcColumn.DefaultCellStyle.WrapMode;
            }
            if (!srcColumn.DefaultCellStyle.IsNullValueDefault)
            {
                object nullValue = srcColumn.DefaultCellStyle.NullValue;
                object obj3 = destColumn.DefaultCellStyle.NullValue;
                if (((nullValue != null) && (obj3 != null)) && (nullValue.GetType() == obj3.GetType()))
                {
                    destColumn.DefaultCellStyle.NullValue = nullValue;
                }
            }
        }
    }

    private void DataGridViewColumnCollectionDialog_Closed(object sender, EventArgs e)
    {
        for (int i = 0; i < this.selectedColumns.Items.Count; i++)
        {
            ListBoxItem item = this.selectedColumns.Items[i] as ListBoxItem;
            if (item.DataGridViewColumnDesigner != null)
            {
                TypeDescriptor.RemoveAssociation(item.DataGridViewColumn, item.DataGridViewColumnDesigner);
            }
        }
        this.columnsNames = null;
        this.userAddedColumns = null;
    }

    private void DataGridViewColumnCollectionDialog_HelpButtonClicked(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
        this.DataGridViewColumnCollectionDialog_HelpRequestHandled();
    }

    private void DataGridViewColumnCollectionDialog_HelpRequested(object sender, HelpEventArgs e)
    {
        this.DataGridViewColumnCollectionDialog_HelpRequestHandled();
        e.Handled = true;
    }

    private void DataGridViewColumnCollectionDialog_HelpRequestHandled()
    {
        IHelpService service = this.liveDataGridView.Site.GetService(iHelpServiceType) as IHelpService;
        if (service != null)
        {
            service.ShowHelpFromKeyword("vs.DataGridViewColumnCollectionDialog");
        }
    }

    private void DataGridViewColumnCollectionDialog_Load(object sender, EventArgs e)
    {
        Font defaultFont = Control.DefaultFont;
        IUIService service = (IUIService) this.liveDataGridView.Site.GetService(iUIServiceType);
        if (service != null)
        {
            defaultFont = (Font) service.Styles["DialogFont"];
        }
        this.Font = defaultFont;
        this.selectedColumns.SelectedIndex = Math.Min(0, this.selectedColumns.Items.Count - 1);
        this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
        this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
        this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != -1);
        this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
        this.selectedColumns.ItemHeight = this.Font.Height + 4;
        base.ActiveControl = this.selectedColumns;
        this.SetSelectedColumnsHorizontalExtent();
        this.selectedColumns.Focus();
        this.formIsDirty = false;
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.selectedColumns.SelectedIndex;
        this.columnsNames.Remove(this.columnsPrivateCopy[selectedIndex]);
        this.userAddedColumns.Remove(this.columnsPrivateCopy[selectedIndex]);
        this.columnsPrivateCopy.RemoveAt(selectedIndex);
        this.selectedColumns.SelectedIndex = Math.Min(this.selectedColumns.Items.Count - 1, selectedIndex);
        this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
        this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
        this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != -1);
        this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
    }

     private void FixColumnCollectionDisplayIndices()
    {
        for (int i = 0; i < this.columnsPrivateCopy.Count; i++)
        {
            this.columnsPrivateCopy[i].DisplayIndex = i;
        }
    }

    private void HookComponentChangedEventHandler(IComponentChangeService componentChangeService)
    {
        if (componentChangeService != null)
        {
            componentChangeService.ComponentChanged += new ComponentChangedEventHandler(this.componentChanged);
        }
    }

 
    private static bool IsColumnAddedByUser(DataGridViewColumn col)
    {
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(col)["UserAddedColumn"];
        if (descriptor != null)
        {
            return (bool) descriptor.GetValue(col);
        }
        return false;
    }

    private void moveDown_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.selectedColumns.SelectedIndex;
        this.columnCollectionChanging = true;
        try
        {
            ListBoxItem selectedItem = (ListBoxItem) this.selectedColumns.SelectedItem;
            this.selectedColumns.Items.RemoveAt(selectedIndex);
            this.selectedColumns.Items.Insert(selectedIndex + 1, selectedItem);
            this.columnsPrivateCopy.RemoveAt(selectedIndex);
            if (selectedItem.DataGridViewColumn.Frozen)
            {
                this.columnsPrivateCopy[selectedIndex].Frozen = true;
            }
            selectedItem.DataGridViewColumn.DisplayIndex = -1;
            this.columnsPrivateCopy.Insert(selectedIndex + 1, selectedItem.DataGridViewColumn);
            this.FixColumnCollectionDisplayIndices();
        }
        finally
        {
            this.columnCollectionChanging = false;
        }
        this.formIsDirty = true;
        this.selectedColumns.SelectedIndex = selectedIndex + 1;
        this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
        this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
    }

    private void moveUp_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.selectedColumns.SelectedIndex;
        this.columnCollectionChanging = true;
        try
        {
            ListBoxItem item = (ListBoxItem) this.selectedColumns.Items[selectedIndex - 1];
            this.selectedColumns.Items.RemoveAt(selectedIndex - 1);
            this.selectedColumns.Items.Insert(selectedIndex, item);
            this.columnsPrivateCopy.RemoveAt(selectedIndex - 1);
            if (item.DataGridViewColumn.Frozen && !this.columnsPrivateCopy[selectedIndex - 1].Frozen)
            {
                item.DataGridViewColumn.Frozen = false;
            }
            item.DataGridViewColumn.DisplayIndex = -1;
            this.columnsPrivateCopy.Insert(selectedIndex, item.DataGridViewColumn);
            this.FixColumnCollectionDisplayIndices();
        }
        finally
        {
            this.columnCollectionChanging = false;
        }
        this.formIsDirty = true;
        this.selectedColumns.SelectedIndex = selectedIndex - 1;
        this.moveUp.Enabled = this.selectedColumns.SelectedIndex > 0;
        this.moveDown.Enabled = this.selectedColumns.SelectedIndex < (this.selectedColumns.Items.Count - 1);
        if ((this.selectedColumns.SelectedIndex != -1) && (this.selectedColumns.TopIndex > this.selectedColumns.SelectedIndex))
        {
            this.selectedColumns.TopIndex = this.selectedColumns.SelectedIndex;
        }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
        this.CommitChanges();
    }

    private void PopulateSelectedColumns()
    {
        int selectedIndex = this.selectedColumns.SelectedIndex;
        for (int i = 0; i < this.selectedColumns.Items.Count; i++)
        {
            ListBoxItem item = this.selectedColumns.Items[i] as ListBoxItem;
            if (item.DataGridViewColumnDesigner != null)
            {
                TypeDescriptor.RemoveAssociation(item.DataGridViewColumn, item.DataGridViewColumnDesigner);
            }
        }
        this.selectedColumns.Items.Clear();
        ITypeResolutionService tr = (ITypeResolutionService) this.liveDataGridView.Site.GetService(iTypeResolutionServiceType);
        for (int j = 0; j < this.columnsPrivateCopy.Count; j++)
        {
            ComponentDesigner componentDesignerForType = DataGridViewAddColumnDialog.GetComponentDesignerForType(tr, this.columnsPrivateCopy[j].GetType());
            this.selectedColumns.Items.Add(new ListBoxItem(this.columnsPrivateCopy[j], this, componentDesignerForType));
        }
        this.selectedColumns.SelectedIndex = Math.Min(selectedIndex, this.selectedColumns.Items.Count - 1);
        this.SetSelectedColumnsHorizontalExtent();
        if (this.selectedColumns.Items.Count == 0)
        {
            this.propertyGridLabel.Text = "DataGridViewProperties";
        }
    }

    private void propertyGrid1_PropertyValueChanged(object sender, System.Windows.Forms.PropertyValueChangedEventArgs e)
    {
        if (!this.columnCollectionChanging)
        {
            this.formIsDirty = true;
            if (e.ChangedItem.PropertyDescriptor.Name.Equals("HeaderText"))
            {
                int selectedIndex = this.selectedColumns.SelectedIndex;
                Rectangle rc = new Rectangle(0, selectedIndex * this.selectedColumns.ItemHeight, this.selectedColumns.Width, this.selectedColumns.ItemHeight);
                this.columnCollectionChanging = true;
                try
                {
                    this.selectedColumns.Items[selectedIndex] = this.selectedColumns.Items[selectedIndex];
                }
                finally
                {
                    this.columnCollectionChanging = false;
                }
                this.selectedColumns.Invalidate(rc);
                this.SetSelectedColumnsHorizontalExtent();
            }
            else if (e.ChangedItem.PropertyDescriptor.Name.Equals("DataPropertyName"))
            {
                if (string.IsNullOrEmpty(((ListBoxItem) this.selectedColumns.SelectedItem).DataGridViewColumn.DataPropertyName))
                {
                    this.propertyGridLabel.Text = SR.GetString("DataGridViewUnboundColumnProperties");
                }
                else
                {
                    this.propertyGridLabel.Text = SR.GetString("DataGridViewBoundColumnProperties");
                }
            }
            else if (e.ChangedItem.PropertyDescriptor.Name.Equals("Name"))
            {
                DataGridViewColumn dataGridViewColumn = ((ListBoxItem) this.selectedColumns.SelectedItem).DataGridViewColumn;
                this.columnsNames[dataGridViewColumn] = dataGridViewColumn.Name;
            }
        }
    }

    private void selectedColumns_DrawItem(object sender, DrawItemEventArgs e)
    {
        if (e.Index >= 0)
        {
            ListBoxItem item = this.selectedColumns.Items[e.Index] as ListBoxItem;
            e.Graphics.DrawImage(item.ToolboxBitmap, e.Bounds.X + 2, e.Bounds.Y + 2, item.ToolboxBitmap.Width, item.ToolboxBitmap.Height);
            Rectangle bounds = e.Bounds;
            bounds.Width -= item.ToolboxBitmap.Width + 4;
            bounds.X += item.ToolboxBitmap.Width + 4;
            bounds.Y += 2;
            bounds.Height -= 4;
            Brush brush = new SolidBrush(e.BackColor);
            Brush brush2 = new SolidBrush(e.ForeColor);
            Brush brush3 = new SolidBrush(this.selectedColumns.BackColor);
            string text = ((ListBoxItem) this.selectedColumns.Items[e.Index]).ToString();
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                int width = Size.Ceiling(e.Graphics.MeasureString(text, e.Font, new SizeF((float) bounds.Width, (float) bounds.Height))).Width;
                Rectangle rect = new Rectangle(bounds.X, e.Bounds.Y + 1, width + 3, e.Bounds.Height - 2);
                e.Graphics.FillRectangle(brush, rect);
                rect.Inflate(-1, -1);
                e.Graphics.DrawString(text, e.Font, brush2, rect);
                rect.Inflate(1, 1);
                if (this.selectedColumns.Focused)
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, rect, e.ForeColor, e.BackColor);
                }
                e.Graphics.FillRectangle(brush3, new Rectangle(rect.Right + 1, e.Bounds.Y, (e.Bounds.Width - rect.Right) - 1, e.Bounds.Height));
            }
            else
            {
                e.Graphics.FillRectangle(brush3, new Rectangle(bounds.X, e.Bounds.Y, e.Bounds.Width - bounds.X, e.Bounds.Height));
                e.Graphics.DrawString(text, e.Font, brush2, bounds);
            }
            brush.Dispose();
            brush3.Dispose();
            brush2.Dispose();
        }
    }

    private void selectedColumns_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ((Control.ModifierKeys & Keys.Control) != Keys.None)
        {
            e.Handled = true;
        }
    }

    private void selectedColumns_KeyUp(object sender, KeyEventArgs e)
    {
        if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.F4))
        {
            this.propertyGrid1.Focus();
            e.Handled = true;
        }
    }

    private void selectedColumns_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.columnCollectionChanging)
        {
            this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
            this.moveDown.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != (this.selectedColumns.Items.Count - 1));
            this.moveUp.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex > 0);
            this.deleteButton.Enabled = (this.selectedColumns.Items.Count > 0) && (this.selectedColumns.SelectedIndex != -1);
            if (this.selectedColumns.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(((ListBoxItem) this.selectedColumns.SelectedItem).DataGridViewColumn.DataPropertyName))
                {
                    this.propertyGridLabel.Text = SR.GetString("DataGridViewUnboundColumnProperties");
                }
                else
                {
                    this.propertyGridLabel.Text = SR.GetString("DataGridViewBoundColumnProperties");
                }
            }
            else
            {
                this.propertyGridLabel.Text = SR.GetString("DataGridViewProperties");
            }
        }
    }

    internal void SetLiveDataGridView(DataGridView dataGridView)
    {
        IComponentChangeService service = null;
        if (dataGridView.Site != null)
        {
            service = (IComponentChangeService) dataGridView.Site.GetService(iComponentChangeServiceType);
        }
        if (service != this.compChangeService)
        {
            this.UnhookComponentChangedEventHandler(this.compChangeService);
            this.compChangeService = service;
            this.HookComponentChangedEventHandler(this.compChangeService);
        }
        this.liveDataGridView = dataGridView;
        this.dataGridViewPrivateCopy.Site = dataGridView.Site;
        this.dataGridViewPrivateCopy.AutoSizeColumnsMode = dataGridView.AutoSizeColumnsMode;
        this.dataGridViewPrivateCopy.DataSource = dataGridView.DataSource;
        this.dataGridViewPrivateCopy.DataMember = dataGridView.DataMember;
        this.columnsNames = new Hashtable(this.columnsPrivateCopy.Count);
        this.columnsPrivateCopy.Clear();
        this.userAddedColumns = new Hashtable(this.liveDataGridView.Columns.Count);
        this.columnCollectionChanging = true;
        try
        {
            for (int i = 0; i < this.liveDataGridView.Columns.Count; i++)
            {
                DataGridViewColumn column = this.liveDataGridView.Columns[i];
                DataGridViewColumn dataGridViewColumn = (DataGridViewColumn) column.Clone();
                dataGridViewColumn.ContextMenuStrip = this.liveDataGridView.Columns[i].ContextMenuStrip;
                dataGridViewColumn.DisplayIndex = -1;
                this.columnsPrivateCopy.Add(dataGridViewColumn);
                if (column.Site != null)
                {
                    this.columnsNames[dataGridViewColumn] = column.Site.Name;
                }
                this.userAddedColumns[dataGridViewColumn] = IsColumnAddedByUser(this.liveDataGridView.Columns[i]);
            }
        }
        finally
        {
            this.columnCollectionChanging = false;
        }
        this.PopulateSelectedColumns();
        this.propertyGrid1.Site = new DataGridViewComponentPropertyGridSite(this.liveDataGridView.Site, this.liveDataGridView);
        this.propertyGrid1.SelectedObject = this.selectedColumns.SelectedItem;
    }

    private void SetSelectedColumnsHorizontalExtent()
    {
        int num = 0;
        for (int i = 0; i < this.selectedColumns.Items.Count; i++)
        {
            int width = TextRenderer.MeasureText(this.selectedColumns.Items[i].ToString(), this.selectedColumns.Font).Width;
            num = Math.Max(num, width);
        }
        this.selectedColumns.HorizontalExtent = ((this.SelectedColumnsItemBitmap.Width + 4) + num) + 3;
    }

    private void UnhookComponentChangedEventHandler(IComponentChangeService componentChangeService)
    {
        if (componentChangeService != null)
        {
            componentChangeService.ComponentChanged -= new ComponentChangedEventHandler(this.componentChanged);
        }
    }

    private static bool ValidateName(IContainer container, string siteName, IComponent component)
    {
        ComponentCollection components = container.Components;
        if (components != null)
        {
            for (int i = 0; i < components.Count; i++)
            {
                IComponent component2 = components[i];
                if ((component2 != null) && (component2.Site != null))
                {
                    ISite site = component2.Site;
                    if (((site != null) && (site.Name != null)) && (string.Equals(site.Name, siteName, StringComparison.OrdinalIgnoreCase) && (site.Component != component)))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    // Properties
    private Bitmap SelectedColumnsItemBitmap
    {
        get
        {
            if (selectedColumnsItemBitmap == null)
            {
                selectedColumnsItemBitmap = new Bitmap(typeof(DataGridViewColumnCollectionDialog), "DataGridViewColumnsDialog.selectedColumns.bmp");
                selectedColumnsItemBitmap.MakeTransparent(Color.Red);
            }
            return selectedColumnsItemBitmap;
        }
    }

    // Nested Types
    private class ColumnTypePropertyDescriptor : PropertyDescriptor
    {
        // Methods
        public ColumnTypePropertyDescriptor() : base("ColumnType", null)
        {
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            if (component == null)
            {
                return null;
            }
            DataGridViewColumnCollectionDialog.ListBoxItem item = (DataGridViewColumnCollectionDialog.ListBoxItem) component;
            return item.DataGridViewColumn.GetType().Name;
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            DataGridViewColumnCollectionDialog.ListBoxItem item = (DataGridViewColumnCollectionDialog.ListBoxItem) component;
            Type newType = value as Type;
            if (item.DataGridViewColumn.GetType() != newType)
            {
                item.Owner.ColumnTypeChanged(item, newType);
                this.OnValueChanged(component, EventArgs.Empty);
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        // Properties
        public override AttributeCollection Attributes
        {
            get
            {
                EditorAttribute attribute = new EditorAttribute("System.Windows.Forms.Design.DataGridViewColumnTypeEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor));
                DescriptionAttribute attribute2 = new DescriptionAttribute(SR.GetString("DataGridViewColumnTypePropertyDescription"));
                CategoryAttribute design = CategoryAttribute.Design;
                return new AttributeCollection(new Attribute[] { attribute, attribute2, design });
            }
        }

        public override Type ComponentType
        {
            get
            {
                return typeof(DataGridViewColumnCollectionDialog.ListBoxItem);
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return typeof(Type);
            }
        }
    }

    internal class ListBoxItem : ICustomTypeDescriptor, IComponent, IDisposable
    {
        // Fields
        private DataGridViewColumn column;
        private ComponentDesigner compDesigner;
        private GridColumnsDialog owner;
        private Image toolboxBitmap;

        // Events
        event EventHandler IComponent.Disposed
        {
            add
            {
            }
            remove
            {
            }
        }

        // Methods
        public ListBoxItem(GridColumnStyle column, GridColumnsDialog owner, ComponentDesigner compDesigner)
        {
            this.column = column;
            this.owner = owner;
            this.compDesigner = compDesigner;
            if (this.compDesigner != null)
            {
                this.compDesigner.Initialize(column);
                TypeDescriptor.CreateAssociation(this.column, this.compDesigner);
            }
            ToolboxBitmapAttribute attribute = TypeDescriptor.GetAttributes(column)[DataGridViewColumnCollectionDialog.toolboxBitmapAttributeType] as ToolboxBitmapAttribute;
            if (attribute != null)
            {
                this.toolboxBitmap = attribute.GetImage(column, false);
            }
            else
            {
                this.toolboxBitmap = this.owner.SelectedColumnsItemBitmap;
            }
            DataGridViewColumnDesigner designer = compDesigner as DataGridViewColumnDesigner;
            if (designer != null)
            {
                designer.LiveDataGridView = this.owner.liveDataGridView;
            }
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this.column);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName(this.column);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this.column);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this.column);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this.column);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this.column);
        }

        object ICustomTypeDescriptor.GetEditor(Type type)
        {
            return TypeDescriptor.GetEditor(this.column, type);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this.column);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
        {
            return TypeDescriptor.GetEvents(this.column, attrs);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return this.GetProperties(null);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrs)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.column);
            PropertyDescriptor[] array = null;
            if (this.compDesigner != null)
            {
                Hashtable hashtable = new Hashtable();
                for (int i = 0; i < properties.Count; i++)
                {
                    hashtable.Add(properties[i].Name, properties[i]);
                }
                ((IDesignerFilter) this.compDesigner).PreFilterProperties(hashtable);
                array = new PropertyDescriptor[hashtable.Count + 1];
                hashtable.Values.CopyTo(array, 0);
            }
            else
            {
                array = new PropertyDescriptor[properties.Count + 1];
                properties.CopyTo(array, 0);
            }
            array[array.Length - 1] = new DataGridViewColumnCollectionDialog.ColumnTypePropertyDescriptor();
            return new PropertyDescriptorCollection(array);
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            if ((pd != null) && (pd is DataGridViewColumnCollectionDialog.ColumnTypePropertyDescriptor))
            {
                return this;
            }
            return this.column;
        }

        void IDisposable.Dispose()
        {
        }

        public override string ToString()
        {
            return this.column.HeaderText;
        }

        // Properties
        public DataGridViewColumn DataGridViewColumn
        {
            get
            {
                return this.column;
            }
        }

        public ComponentDesigner DataGridViewColumnDesigner
        {
            get
            {
                return this.compDesigner;
            }
        }

        public GridColumnsDialog Owner
        {
            get
            {
                return this.owner;
            }
        }

        ISite IComponent.Site
        {
            get
            {
                return this.owner.liveDataGridView.Site;
            }
            set
            {
            }
        }

        public Image ToolboxBitmap
        {
            get
            {
                return this.toolboxBitmap;
            }
        }
    }
}


}