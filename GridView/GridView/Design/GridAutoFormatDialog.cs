using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Security.Permissions;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text;
using System.Drawing.Design;
using System.Security;
using System.ComponentModel.Design;
using System.Resources;
using System.Globalization;
using System.Xml;
using System.Windows.Forms.Design;
using System.IO;

namespace mControl.GridStyle.Design
{
	[SecurityPermission(SecurityAction.Demand)]
	internal class GridAutoFormatDialog : Form
	{
		// Methods
		public GridAutoFormatDialog(Grid dgrid)
		{
			this.dataSet = new DataSet();
			this.IMBusy = false;
			this.selectedIndex = -1;
			this.dgrid = dgrid;
			base.ShowInTaskbar = false;
			this.dataSet.Locale = CultureInfo.InvariantCulture;
			this.dataSet.ReadXmlSchema(new XmlTextReader(new StringReader("<xsd:schema id=\"pulica\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\"><xsd:element name=\"Scheme\"><xsd:complexType><xsd:all><xsd:element name=\"SchemeName\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"SchemePicture\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"FlatMode\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"Font\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"CaptionFont\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"HeaderFont\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"AlternatingBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"BackgroundColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"CaptionForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"CaptionBackColor\" minOccurs=\"0\" type=\"xsd:string\"/>"+
				"<xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"GridLineColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"GridLineStyle\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"HeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"HeaderForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"LinkColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"LinkHoverColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"ParentRowsBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"ParentRowsForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"SelectionForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"SelectionBackColor\" minOccurs=\"0\" type=\"xsd:string\"/></xsd:all></xsd:complexType></xsd:element></xsd:schema>")));
			this.dataSet.ReadXml(new StringReader("<pulica><Scheme><SchemeName>Default</SchemeName><SchemePicture>default.bmp</SchemePicture><BorderStyle></BorderStyle><FlatMode></FlatMode><CaptionFont></CaptionFont><Font></Font><HeaderFont></HeaderFont><AlternatingBackColor></AlternatingBackColor><BackColor></BackColor><CaptionForeColor></CaptionForeColor><CaptionBackColor></CaptionBackColor><ForeColor></ForeColor><GridLineColor></GridLineColor><GridLineStyle></GridLineStyle><HeaderBackColor></HeaderBackColor><HeaderForeColor></HeaderForeColor><LinkColor></LinkColor><LinkHoverColor></LinkHoverColor><ParentRowsBackColor></ParentRowsBackColor><ParentRowsForeColor></ParentRowsForeColor><SelectionForeColor></SelectionForeColor><SelectionBackColor></SelectionBackColor></Scheme><Scheme><SchemeName>Professional 1</SchemeName><SchemePicture>professional1.bmp</SchemePicture><CaptionFont>Verdana, 10pt</CaptionFont><AlternatingBackColor>LightGray</AlternatingBackColor><CaptionForeColor>Navy</CaptionForeColor><CaptionBackColor>White</CaptionBackColor><ForeColor>Black</ForeColor><BackColor>DarkGray</BackColor>"+
				"<GridLineColor>Black</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>Silver</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>Navy</LinkColor><LinkHoverColor>Blue</LinkHoverColor><ParentRowsBackColor>White</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Navy</SelectionBackColor></Scheme><Scheme><SchemeName>Professional 2</SchemeName><SchemePicture>professional2.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Tahoma, 8pt</CaptionFont><AlternatingBackColor>Gainsboro</AlternatingBackColor><BackColor>Silver</BackColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>DarkSlateBlue</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>White</GridLineColor><HeaderBackColor>DarkGray</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>RoyalBlue</LinkHoverColor><ParentRowsBackColor>Black</ParentRowsBackColor><ParentRowsForeColor>White</ParentRowsForeColor>"+
				"<SelectionForeColor>White</SelectionForeColor><SelectionBackColor>DarkSlateBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Professional 3</SchemeName><SchemePicture>professional3.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><Font>Tahoma, 8pt</Font><AlternatingBackColor>LightGray</AlternatingBackColor><BackColor>Gainsboro</BackColor><BackgroundColor>Silver</BackgroundColor><CaptionForeColor>MidnightBlue</CaptionForeColor><CaptionBackColor>LightSteelBlue</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>DimGray</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>MidnightBlue</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>MidnightBlue</LinkColor><LinkHoverColor>RoyalBlue</LinkHoverColor><ParentRowsBackColor>DarkGray</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>CadetBlue</SelectionBackColor></Scheme>"+
				"<Scheme><SchemeName>Professional 4</SchemeName><SchemePicture>professional4.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><Font>Tahoma, 8pt</Font><AlternatingBackColor>Lavender</AlternatingBackColor><BackColor>WhiteSmoke</BackColor><BackgroundColor>LightGray</BackgroundColor><CaptionForeColor>MidnightBlue</CaptionForeColor><CaptionBackColor>LightSteelBlue</CaptionBackColor><ForeColor>MidnightBlue</ForeColor><GridLineColor>Gainsboro</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>MidnightBlue</HeaderBackColor><HeaderForeColor>WhiteSmoke</HeaderForeColor><LinkColor>Teal</LinkColor><LinkHoverColor>DarkMagenta</LinkHoverColor><ParentRowsBackColor>Gainsboro</ParentRowsBackColor><ParentRowsForeColor>MidnightBlue</ParentRowsForeColor><SelectionForeColor>WhiteSmoke</SelectionForeColor><SelectionBackColor>CadetBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Classic</SchemeName><SchemePicture>classic.bmp</SchemePicture>"+
				"<BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Times New Roman, 9pt</Font><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><AlternatingBackColor>WhiteSmoke</AlternatingBackColor><BackColor>Gainsboro</BackColor><BackgroundColor>DarkGray</BackgroundColor><CaptionForeColor>Black</CaptionForeColor><CaptionBackColor>DarkKhaki</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Silver</GridLineColor><HeaderBackColor>Black</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>Firebrick</LinkHoverColor><ParentRowsForeColor>Black</ParentRowsForeColor><ParentRowsBackColor>LightGray</ParentRowsBackColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Firebrick</SelectionBackColor></Scheme><Scheme><SchemeName>Simple</SchemeName><SchemePicture>Simple.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Courier New, 9pt</Font><HeaderFont>Courier New, 10pt, style=1</HeaderFont>"+
				"<CaptionFont>Courier New, 10pt, style=1</CaptionFont><AlternatingBackColor>White</AlternatingBackColor><BackColor>White</BackColor><BackgroundColor>Gainsboro</BackgroundColor><CaptionForeColor>Black</CaptionForeColor><CaptionBackColor>Silver</CaptionBackColor><ForeColor>DarkSlateGray</ForeColor><GridLineColor>DarkGray</GridLineColor><HeaderBackColor>DarkGreen</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>DarkGreen</LinkColor><LinkHoverColor>Blue</LinkHoverColor><ParentRowsForeColor>Black</ParentRowsForeColor><ParentRowsBackColor>Gainsboro</ParentRowsBackColor><SelectionForeColor>Black</SelectionForeColor><SelectionBackColor>DarkSeaGreen</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 1</SchemeName><SchemePicture>colorful1.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 9pt, style=1</CaptionFont><HeaderFont>Tahoma, 9pt, style=1</HeaderFont><AlternatingBackColor>LightGoldenrodYellow</AlternatingBackColor><BackColor>White</BackColor>"+
				"<BackgroundColor>LightGoldenrodYellow</BackgroundColor><CaptionForeColor>DarkSlateBlue</CaptionForeColor><CaptionBackColor>LightGoldenrodYellow</CaptionBackColor><ForeColor>DarkSlateBlue</ForeColor><GridLineColor>Peru</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>Maroon</HeaderBackColor><HeaderForeColor>LightGoldenrodYellow</HeaderForeColor><LinkColor>Maroon</LinkColor><LinkHoverColor>SlateBlue</LinkHoverColor><ParentRowsBackColor>BurlyWood</ParentRowsBackColor><ParentRowsForeColor>DarkSlateBlue</ParentRowsForeColor><SelectionForeColor>GhostWhite</SelectionForeColor><SelectionBackColor>DarkSlateBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 2</SchemeName><SchemePicture>colorful2.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><AlternatingBackColor>GhostWhite</AlternatingBackColor><BackColor>GhostWhite</BackColor><BackgroundColor>Lavender</BackgroundColor>"+
				"<CaptionForeColor>White</CaptionForeColor><CaptionBackColor>RoyalBlue</CaptionBackColor><ForeColor>MidnightBlue</ForeColor><GridLineColor>RoyalBlue</GridLineColor><HeaderBackColor>MidnightBlue</HeaderBackColor><HeaderForeColor>Lavender</HeaderForeColor><LinkColor>Teal</LinkColor><LinkHoverColor>DodgerBlue</LinkHoverColor><ParentRowsBackColor>Lavender</ParentRowsBackColor><ParentRowsForeColor>MidnightBlue</ParentRowsForeColor><SelectionForeColor>PaleGreen</SelectionForeColor><SelectionBackColor>Teal</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 3</SchemeName><SchemePicture>colorful3.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><AlternatingBackColor>OldLace</AlternatingBackColor><BackColor>OldLace</BackColor><BackgroundColor>Tan</BackgroundColor><CaptionForeColor>OldLace</CaptionForeColor><CaptionBackColor>SaddleBrown</CaptionBackColor><ForeColor>DarkSlateGray</ForeColor>"+
				"<GridLineColor>Tan</GridLineColor><GridLineStyle>Solid</GridLineStyle><HeaderBackColor>Wheat</HeaderBackColor><HeaderForeColor>SaddleBrown</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>Teal</LinkHoverColor><ParentRowsBackColor>OldLace</ParentRowsBackColor><ParentRowsForeColor>DarkSlateGray</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>SlateGray</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 4</SchemeName><SchemePicture>colorful4.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><AlternatingBackColor>White</AlternatingBackColor><BackColor>White</BackColor><BackgroundColor>Ivory</BackgroundColor><CaptionForeColor>Lavender</CaptionForeColor><CaptionBackColor>DarkSlateBlue</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Wheat</GridLineColor><HeaderBackColor>CadetBlue</HeaderBackColor>"+
				"<HeaderForeColor>Black</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>LightSeaGreen</LinkHoverColor><ParentRowsBackColor>Ivory</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>DarkSlateBlue</SelectionForeColor><SelectionBackColor>Wheat</SelectionBackColor></Scheme><Scheme><SchemeName>256 Color 1</SchemeName><SchemePicture>256_1.bmp</SchemePicture><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8 pt</CaptionFont><HeaderFont>Tahoma, 8pt</HeaderFont><AlternatingBackColor>Silver</AlternatingBackColor><BackColor>White</BackColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>Maroon</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Silver</GridLineColor><HeaderBackColor>Silver</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>Maroon</LinkColor><LinkHoverColor>Red</LinkHoverColor><ParentRowsBackColor>Silver</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor>"+
				"<SelectionBackColor>Maroon</SelectionBackColor></Scheme><Scheme><SchemeName>256 Color 2</SchemeName><SchemePicture>256_2.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Microsoft Sans Serif, 10 pt, style=1</CaptionFont><Font>Tahoma, 8pt</Font><HeaderFont>Tahoma, 8pt</HeaderFont><AlternatingBackColor>White</AlternatingBackColor><BackColor>White</BackColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>Teal</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Silver</GridLineColor><HeaderBackColor>Black</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>Purple</LinkColor><LinkHoverColor>Fuchsia</LinkHoverColor><ParentRowsBackColor>Gray</ParentRowsBackColor><ParentRowsForeColor>White</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Maroon</SelectionBackColor></Scheme></pulica>"), XmlReadMode.IgnoreSchema);
			this.schemeTable = this.dataSet.Tables["Scheme"];
			this.IMBusy = true;
			this.InitializeComponent();
			this.AddDataToDataGrid();
			this.AddStyleSheetInformationToDataGrid();
			if (dgrid.Site != null)
			{
				IUIService service1 = (IUIService) dgrid.Site.GetService(typeof(IUIService));
				if (service1 != null)
				{
					Font font1 = (Font) service1.Styles["DialogFont"];
					if (font1 != null)
					{
						this.Font = font1;
					}
				}
			}
			base.Focus();
			this.IMBusy = false;
		}

		private void AddDataToDataGrid()
		{
			DataTable table1 = new DataTable("Table1");
			table1.Columns.Add(new DataColumn("First Name"));
			table1.Columns.Add(new DataColumn("Last Name"));
			DataRow row1 = table1.NewRow();
			row1["First Name"] = "Robert";
			row1["Last Name"] = "Brown";
			table1.Rows.Add(row1);
			row1 = table1.NewRow();
			row1["First Name"] = "Nate";
			row1["Last Name"] = "Sun";
			table1.Rows.Add(row1);
			row1 = table1.NewRow();
			row1["First Name"] = "Carole";
			row1["Last Name"] = "Poland";
			table1.Rows.Add(row1);
			this.dataGrid.SetDataBinding(table1, "");
		}

		private void AddStyleSheetInformationToDataGrid()
		{
			ResourceManager manager1 = new ResourceManager(typeof(GridAutoFormatDialog));
			GridTableStyle style1 = new GridTableStyle();
			style1.MappingName = "Table1";
			GridColumnStyle style2 = new GridTextBoxColumn();
			style2.MappingName = "First Name";
			style2.HeaderText = manager1.GetString("table.FirstColumn");
			GridColumnStyle style3 = new GridTextBoxColumn();
			style3.MappingName = "Last Name";
			style3.HeaderText = manager1.GetString("table.FirstColumn");
			style1.GridColumnStyles.Add(style2);
			style1.GridColumnStyles.Add(style3);
			DataRowCollection collection1 = this.dataSet.Tables["Scheme"].Rows;
			DataRow row1 = collection1[0];
			row1["SchemeName"] = manager1.GetString("schemeName.Default");
			row1 = collection1[1];
			row1["SchemeName"] = manager1.GetString("schemeName.Professional1");
			row1 = collection1[2];
			row1["SchemeName"] = manager1.GetString("schemeName.Professional2");
			row1 = collection1[3];
			row1["SchemeName"] = manager1.GetString("schemeName.Professional3");
			row1 = collection1[4];
			row1["SchemeName"] = manager1.GetString("schemeName.Professional4");
			row1 = collection1[5];
			row1["SchemeName"] = manager1.GetString("schemeName.Classic");
			row1 = collection1[6];
			row1["SchemeName"] = manager1.GetString("schemeName.Simple");
			row1 = collection1[7];
			row1["SchemeName"] = manager1.GetString("schemeName.Colorful1");
			row1 = collection1[8];
			row1["SchemeName"] = manager1.GetString("schemeName.Colorful2");
			row1 = collection1[9];
			row1["SchemeName"] = manager1.GetString("schemeName.Colorful3");
			row1 = collection1[10];
			row1["SchemeName"] = manager1.GetString("schemeName.Colorful4");
			row1 = collection1[11];
			row1["SchemeName"] = manager1.GetString("schemeName.256Color1");
			row1 = collection1[12];
			row1["SchemeName"] = manager1.GetString("schemeName.256Color2");
			this.dataGrid.TableStyles.Add(style1);
			this.tableStyle = style1;
		}

		private void AutoFormat_HelpRequested(object sender, HelpEventArgs e)
		{
			if ((this.dgrid != null) && (this.dgrid.Site != null))
			{
				IDesignerHost host1 = this.dgrid.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (host1 != null)
				{
					IHelpService service1 = host1.GetService(typeof(IHelpService)) as IHelpService;
					if (service1 != null)
					{
						service1.ShowHelpFromKeyword("vs.GridAutoFormatDialog");
					}
				}
			}
		}

		private void Button1_Clicked(object sender, EventArgs e)
		{
			this.selectedIndex = this.schemeName.SelectedIndex;
		}

 
		private void InitializeComponent()
		{
			ResourceManager manager1 = new ResourceManager(typeof(GridAutoFormatDialog));
			this.formats = new Label();
			this.schemeName = new ListBox();
			this.dataGrid = new GridAutoFormatDialog.AutoFormatDataGrid();
			this.preview = new Label();
			this.button1 = new Button();
			this.button2 = new Button();
			this.dataGrid.BeginInit();
			this.dataGrid.SuspendLayout();
			base.SuspendLayout();
			this.formats.AccessibleDescription = (string) manager1.GetObject("formats.AccessibleDescription");
			this.formats.AccessibleName = (string) manager1.GetObject("formats.AccessibleName");
			this.formats.Anchor = (AnchorStyles) manager1.GetObject("formats.Anchor");
			this.formats.AutoSize = (bool) manager1.GetObject("formats.AutoSize");
			this.formats.Cursor = (Cursor) manager1.GetObject("formats.Cursor");
			this.formats.Dock = (DockStyle) manager1.GetObject("formats.Dock");
			this.formats.Enabled = (bool) manager1.GetObject("formats.Enabled");
			this.formats.Font = (Font) manager1.GetObject("formats.Font");
			this.formats.Image = (Image) manager1.GetObject("formats.Image");
			this.formats.ImageAlign = (ContentAlignment) manager1.GetObject("formats.ImageAlign");
			this.formats.ImageIndex = (int) manager1.GetObject("formats.ImageIndex");
			this.formats.ImeMode = (ImeMode) manager1.GetObject("formats.ImeMode");
			this.formats.Location = (Point) manager1.GetObject("formats.Location");
			this.formats.Name = "formats";
			this.formats.RightToLeft = (RightToLeft) manager1.GetObject("formats.RightToLeft");
			this.formats.Size = (Size) manager1.GetObject("formats.Size");
			this.formats.TabIndex = (int) manager1.GetObject("formats.TabIndex");
			this.formats.Text = manager1.GetString("formats.Text");
			this.formats.TextAlign = (ContentAlignment) manager1.GetObject("formats.TextAlign");
			this.formats.Visible = (bool) manager1.GetObject("formats.Visible");
			this.schemeName.DataSource = this.schemeTable;
			this.schemeName.DisplayMember = "SchemeName";
			this.schemeName.SelectedIndexChanged += new EventHandler(this.SchemeName_SelectionChanged);
			this.schemeName.AccessibleDescription = (string) manager1.GetObject("schemeName.AccessibleDescription");
			this.schemeName.AccessibleName = (string) manager1.GetObject("schemeName.AccessibleName");
			this.schemeName.Anchor = (AnchorStyles) manager1.GetObject("schemeName.Anchor");
			this.schemeName.BackgroundImage = (Image) manager1.GetObject("schemeName.BackgroundImage");
			this.schemeName.ColumnWidth = (int) manager1.GetObject("schemeName.ColumnWidth");
			this.schemeName.Cursor = (Cursor) manager1.GetObject("schemeName.Cursor");
			this.schemeName.Dock = (DockStyle) manager1.GetObject("schemeName.Dock");
			this.schemeName.Enabled = (bool) manager1.GetObject("schemeName.Enabled");
			this.schemeName.Font = (Font) manager1.GetObject("schemeName.Font");
			this.schemeName.HorizontalExtent = (int) manager1.GetObject("schemeName.HorizontalExtent");
			this.schemeName.HorizontalScrollbar = (bool) manager1.GetObject("schemeName.HorizontalScrollbar");
			this.schemeName.ImeMode = (ImeMode) manager1.GetObject("schemeName.ImeMode");
			this.schemeName.IntegralHeight = (bool) manager1.GetObject("schemeName.IntegralHeight");
			this.schemeName.ItemHeight = (int) manager1.GetObject("schemeName.ItemHeight");
			this.schemeName.Location = (Point) manager1.GetObject("schemeName.Location");
			this.schemeName.Name = "schemeName";
			this.schemeName.RightToLeft = (RightToLeft) manager1.GetObject("schemeName.RightToLeft");
			this.schemeName.ScrollAlwaysVisible = (bool) manager1.GetObject("schemeName.ScrollAlwaysVisible");
			this.schemeName.Size = (Size) manager1.GetObject("schemeName.Size");
			this.schemeName.TabIndex = (int) manager1.GetObject("schemeName.TabIndex");
			this.schemeName.Visible = (bool) manager1.GetObject("schemeName.Visible");
			this.dataGrid.AccessibleDescription = (string) manager1.GetObject("dataGrid.AccessibleDescription");
			this.dataGrid.AccessibleName = (string) manager1.GetObject("dataGrid.AccessibleName");
			this.dataGrid.Anchor = (AnchorStyles) manager1.GetObject("dataGrid.Anchor");
			this.dataGrid.BackgroundImage = (Image) manager1.GetObject("dataGrid.BackgroundImage");
			this.dataGrid.CaptionFont = (Font) manager1.GetObject("dataGrid.CaptionFont");
			this.dataGrid.CaptionText = manager1.GetString("dataGrid.CaptionText");
			this.dataGrid.Cursor = (Cursor) manager1.GetObject("dataGrid.Cursor");
			this.dataGrid.DataMember = "";
			this.dataGrid.Dock = (DockStyle) manager1.GetObject("dataGrid.Dock");
			this.dataGrid.Enabled = (bool) manager1.GetObject("dataGrid.Enabled");
			this.dataGrid.Font = (Font) manager1.GetObject("dataGrid.Font");
			this.dataGrid.ImeMode = (ImeMode) manager1.GetObject("dataGrid.ImeMode");
			this.dataGrid.Location = (Point) manager1.GetObject("dataGrid.Location");
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.RightToLeft = (RightToLeft) manager1.GetObject("dataGrid.RightToLeft");
			this.dataGrid.Size = (Size) manager1.GetObject("dataGrid.Size");
			this.dataGrid.TabIndex = (int) manager1.GetObject("dataGrid.TabIndex");
			this.dataGrid.Visible = (bool) manager1.GetObject("dataGrid.Visible");
			this.preview.AccessibleDescription = (string) manager1.GetObject("preview.AccessibleDescription");
			this.preview.AccessibleName = (string) manager1.GetObject("preview.AccessibleName");
			this.preview.Anchor = (AnchorStyles) manager1.GetObject("preview.Anchor");
			this.preview.AutoSize = (bool) manager1.GetObject("preview.AutoSize");
			this.preview.Cursor = (Cursor) manager1.GetObject("preview.Cursor");
			this.preview.Dock = (DockStyle) manager1.GetObject("preview.Dock");
			this.preview.Enabled = (bool) manager1.GetObject("preview.Enabled");
			this.preview.Font = (Font) manager1.GetObject("preview.Font");
			this.preview.Image = (Image) manager1.GetObject("preview.Image");
			this.preview.ImageAlign = (ContentAlignment) manager1.GetObject("preview.ImageAlign");
			this.preview.ImageIndex = (int) manager1.GetObject("preview.ImageIndex");
			this.preview.ImeMode = (ImeMode) manager1.GetObject("preview.ImeMode");
			this.preview.Location = (Point) manager1.GetObject("preview.Location");
			this.preview.Name = "preview";
			this.preview.RightToLeft = (RightToLeft) manager1.GetObject("preview.RightToLeft");
			this.preview.Size = (Size) manager1.GetObject("preview.Size");
			this.preview.TabIndex = (int) manager1.GetObject("preview.TabIndex");
			this.preview.Text = manager1.GetString("preview.Text");
			this.preview.TextAlign = (ContentAlignment) manager1.GetObject("preview.TextAlign");
			this.preview.Visible = (bool) manager1.GetObject("preview.Visible");
			this.button1.DialogResult = DialogResult.OK;
			this.button1.Click += new EventHandler(this.Button1_Clicked);
			this.button1.AccessibleDescription = (string) manager1.GetObject("button1.AccessibleDescription");
			this.button1.AccessibleName = (string) manager1.GetObject("button1.AccessibleName");
			this.button1.Anchor = (AnchorStyles) manager1.GetObject("button1.Anchor");
			this.button1.BackgroundImage = (Image) manager1.GetObject("button1.BackgroundImage");
			this.button1.Cursor = (Cursor) manager1.GetObject("button1.Cursor");
			this.button1.Dock = (DockStyle) manager1.GetObject("button1.Dock");
			this.button1.Enabled = (bool) manager1.GetObject("button1.Enabled");
			this.button1.FlatStyle = (FlatStyle) manager1.GetObject("button1.FlatStyle");
			this.button1.Font = (Font) manager1.GetObject("button1.Font");
			this.button1.Image = (Image) manager1.GetObject("button1.Image");
			this.button1.ImageAlign = (ContentAlignment) manager1.GetObject("button1.ImageAlign");
			this.button1.ImageIndex = (int) manager1.GetObject("button1.ImageIndex");
			this.button1.ImeMode = (ImeMode) manager1.GetObject("button1.ImeMode");
			this.button1.Location = (Point) manager1.GetObject("button1.Location");
			this.button1.Name = "button1";
			this.button1.RightToLeft = (RightToLeft) manager1.GetObject("button1.RightToLeft");
			this.button1.Size = (Size) manager1.GetObject("button1.Size");
			this.button1.TabIndex = (int) manager1.GetObject("button1.TabIndex");
			this.button1.Text = manager1.GetString("button1.Text");
			this.button1.TextAlign = (ContentAlignment) manager1.GetObject("button1.TextAlign");
			this.button1.Visible = (bool) manager1.GetObject("button1.Visible");
			this.button2.DialogResult = DialogResult.Cancel;
			this.button2.AccessibleDescription = (string) manager1.GetObject("button2.AccessibleDescription");
			this.button2.AccessibleName = (string) manager1.GetObject("button2.AccessibleName");
			this.button2.Anchor = (AnchorStyles) manager1.GetObject("button2.Anchor");
			this.button2.BackgroundImage = (Image) manager1.GetObject("button2.BackgroundImage");
			this.button2.Cursor = (Cursor) manager1.GetObject("button2.Cursor");
			this.button2.Dock = (DockStyle) manager1.GetObject("button2.Dock");
			this.button2.Enabled = (bool) manager1.GetObject("button2.Enabled");
			this.button2.FlatStyle = (FlatStyle) manager1.GetObject("button2.FlatStyle");
			this.button2.Font = (Font) manager1.GetObject("button2.Font");
			this.button2.Image = (Image) manager1.GetObject("button2.Image");
			this.button2.ImageAlign = (ContentAlignment) manager1.GetObject("button2.ImageAlign");
			this.button2.ImageIndex = (int) manager1.GetObject("button2.ImageIndex");
			this.button2.ImeMode = (ImeMode) manager1.GetObject("button2.ImeMode");
			this.button2.Location = (Point) manager1.GetObject("button2.Location");
			this.button2.Name = "button2";
			this.button2.RightToLeft = (RightToLeft) manager1.GetObject("button2.RightToLeft");
			this.button2.Size = (Size) manager1.GetObject("button2.Size");
			this.button2.TabIndex = (int) manager1.GetObject("button2.TabIndex");
			this.button2.Text = manager1.GetString("button2.Text");
			this.button2.TextAlign = (ContentAlignment) manager1.GetObject("button2.TextAlign");
			this.button2.Visible = (bool) manager1.GetObject("button2.Visible");
			base.CancelButton = this.button2;
			base.AcceptButton = this.button1;
			base.HelpRequested += new HelpEventHandler(this.AutoFormat_HelpRequested);
			base.AccessibleDescription = (string) manager1.GetObject("$this.AccessibleDescription");
			base.AccessibleName = (string) manager1.GetObject("$this.AccessibleName");
			this.Anchor = (AnchorStyles) manager1.GetObject("$this.Anchor");
			this.AutoScaleBaseSize = (Size) manager1.GetObject("$this.AutoScaleBaseSize");
			this.AutoScroll = (bool) manager1.GetObject("$this.AutoScroll");
			base.AutoScrollMargin = (Size) manager1.GetObject("$this.AutoScrollMargin");
			base.AutoScrollMinSize = (Size) manager1.GetObject("$this.AutoScrollMinSize");
			this.BackgroundImage = (Image) manager1.GetObject("$this.BackgroundImage");
			base.ClientSize = (Size) manager1.GetObject("$this.ClientSize");
			base.Controls.AddRange(new Control[] { this.button2, this.button1, this.preview, this.dataGrid, this.schemeName, this.formats });
			this.Cursor = (Cursor) manager1.GetObject("$this.Cursor");
			this.Dock = (DockStyle) manager1.GetObject("$this.Dock");
			base.Enabled = (bool) manager1.GetObject("$this.Enabled");
			this.Font = (Font) manager1.GetObject("$this.Font");
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.ImeMode = (ImeMode) manager1.GetObject("$this.ImeMode");
			base.Location = (Point) manager1.GetObject("$this.Location");
			base.MaximizeBox = false;
			base.MaximumSize = (Size) manager1.GetObject("$this.MaximumSize");
			base.MinimizeBox = false;
			base.MinimumSize = (Size) manager1.GetObject("$this.MinimumSize");
			base.Name = "Win32Form1";
			this.RightToLeft = (RightToLeft) manager1.GetObject("$this.RightToLeft");
			base.StartPosition = (FormStartPosition) manager1.GetObject("$this.StartPosition");
			this.Text = manager1.GetString("$this.Text");
			base.Visible = (bool) manager1.GetObject("$this.Visible");
			this.dataGrid.EndInit();
			this.dataGrid.ResumeLayout(false);
			base.ResumeLayout(false);
		}

 
		private bool IsTableProperty(string propName)
		{
			if (propName.Equals("HeaderColor"))
			{
				return true;
			}
			if (propName.Equals("AlternatingBackColor"))
			{
				return true;
			}
			if (propName.Equals("BackColor"))
			{
				return true;
			}
			if (propName.Equals("ForeColor"))
			{
				return true;
			}
			if (propName.Equals("GridLineColor"))
			{
				return true;
			}
			if (propName.Equals("GridLineStyle"))
			{
				return true;
			}
			if (propName.Equals("HeaderBackColor"))
			{
				return true;
			}
			if (propName.Equals("HeaderForeColor"))
			{
				return true;
			}
			if (propName.Equals("LinkColor"))
			{
				return true;
			}
			if (propName.Equals("LinkHoverColor"))
			{
				return true;
			}
			if (propName.Equals("SelectionForeColor"))
			{
				return true;
			}
			if (propName.Equals("SelectionBackColor"))
			{
				return true;
			}
			if (propName.Equals("HeaderFont"))
			{
				return true;
			}
			return false;
		}

 
		private void SchemeName_SelectionChanged(object sender, EventArgs e)
		{
			if (!this.IMBusy)
			{
				DataRow row1 = ((DataRowView) this.schemeName.SelectedItem).Row;
				if (row1 != null)
				{
					PropertyDescriptorCollection collection1 = TypeDescriptor.GetProperties(typeof(Grid));
					PropertyDescriptorCollection collection2 = TypeDescriptor.GetProperties(typeof(GridTableStyle));
					foreach (DataColumn column1 in row1.Table.Columns)
					{
						PropertyDescriptor descriptor1;
						object obj2;
						object obj1 = row1[column1];
						if (this.IsTableProperty(column1.ColumnName))
						{
							descriptor1 = collection2[column1.ColumnName];
							obj2 = this.tableStyle;
						}
						else
						{
							descriptor1 = collection1[column1.ColumnName];
							obj2 = this.dataGrid;
						}
						if (descriptor1 != null)
						{
							if (Convert.IsDBNull(obj1) || (obj1.ToString().Length == 0))
							{
								descriptor1.ResetValue(obj2);
							}
							else
							{
								try
								{
									object obj3 = descriptor1.Converter.ConvertFromString(obj1.ToString());
									descriptor1.SetValue(obj2, obj3);
									continue;
								}
								catch (Exception)
								{
									continue;
								}
							}
						}
					}
				}
			}
		}


		// Properties
		public DataRow SelectedData
		{
			get
			{
				if (this.schemeName != null)
				{
					return ((DataRowView) this.schemeName.Items[this.selectedIndex]).Row;
				}
				return null;
			}
		}
	
		// Fields
		private Button button1;
		private Button button2;
		internal const string data = "<pulica><Scheme><SchemeName>Default</SchemeName><SchemePicture>default.bmp</SchemePicture><BorderStyle></BorderStyle><FlatMode></FlatMode><CaptionFont></CaptionFont><Font></Font><HeaderFont></HeaderFont><AlternatingBackColor></AlternatingBackColor><BackColor></BackColor><CaptionForeColor></CaptionForeColor><CaptionBackColor></CaptionBackColor>" +
			"<ForeColor></ForeColor><GridLineColor></GridLineColor><GridLineStyle></GridLineStyle><HeaderBackColor></HeaderBackColor><HeaderForeColor></HeaderForeColor><LinkColor></LinkColor><LinkHoverColor></LinkHoverColor><ParentRowsBackColor></ParentRowsBackColor><ParentRowsForeColor></ParentRowsForeColor><SelectionForeColor></SelectionForeColor>"+
			"<SelectionBackColor></SelectionBackColor></Scheme><Scheme><SchemeName>Professional 1</SchemeName><SchemePicture>professional1.bmp</SchemePicture><CaptionFont>Verdana, 10pt</CaptionFont><AlternatingBackColor>LightGray</AlternatingBackColor><CaptionForeColor>Navy</CaptionForeColor><CaptionBackColor>White</CaptionBackColor><ForeColor>Black</ForeColor>"+
			"<BackColor>DarkGray</BackColor><GridLineColor>Black</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>Silver</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>Navy</LinkColor><LinkHoverColor>Blue</LinkHoverColor><ParentRowsBackColor>White</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Navy</SelectionBackColor></Scheme><Scheme><SchemeName>Professional 2</SchemeName><SchemePicture>professional2.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Tahoma, 8pt</CaptionFont><AlternatingBackColor>Gainsboro</AlternatingBackColor><BackColor>Silver</BackColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>DarkSlateBlue</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>White</GridLineColor><HeaderBackColor>DarkGray</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>RoyalBlue</LinkHoverColor><ParentRowsBackColor>Black</ParentRowsBackColor><ParentRowsForeColor>White</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>DarkSlateBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Professional 3</SchemeName>"+
			"<SchemePicture>professional3.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><Font>Tahoma, 8pt</Font><AlternatingBackColor>LightGray</AlternatingBackColor>"+
			"<BackColor>Gainsboro</BackColor><BackgroundColor>Silver</BackgroundColor><CaptionForeColor>MidnightBlue</CaptionForeColor><CaptionBackColor>LightSteelBlue</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>DimGray</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>MidnightBlue</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>MidnightBlue</LinkColor><LinkHoverColor>RoyalBlue</LinkHoverColor><ParentRowsBackColor>DarkGray</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>CadetBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Professional 4</SchemeName><SchemePicture>professional4.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><Font>Tahoma, 8pt</Font><AlternatingBackColor>Lavender</AlternatingBackColor><BackColor>WhiteSmoke</BackColor><BackgroundColor>LightGray</BackgroundColor><CaptionForeColor>MidnightBlue</CaptionForeColor><CaptionBackColor>LightSteelBlue</CaptionBackColor><ForeColor>MidnightBlue</ForeColor><GridLineColor>Gainsboro</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>MidnightBlue</HeaderBackColor>"+
			"<HeaderForeColor>WhiteSmoke</HeaderForeColor><LinkColor>Teal</LinkColor><LinkHoverColor>DarkMagenta</LinkHoverColor><ParentRowsBackColor>Gainsboro</ParentRowsBackColor><ParentRowsForeColor>MidnightBlue</ParentRowsForeColor><SelectionForeColor>WhiteSmoke</SelectionForeColor><SelectionBackColor>CadetBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Classic</SchemeName><SchemePicture>classic.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Times New Roman, 9pt</Font><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><AlternatingBackColor>WhiteSmoke</AlternatingBackColor><BackColor>Gainsboro</BackColor><BackgroundColor>DarkGray</BackgroundColor><CaptionForeColor>Black</CaptionForeColor><CaptionBackColor>DarkKhaki</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Silver</GridLineColor><HeaderBackColor>Black</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>Firebrick</LinkHoverColor>" +
			"<ParentRowsForeColor>Black</ParentRowsForeColor><ParentRowsBackColor>LightGray</ParentRowsBackColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Firebrick</SelectionBackColor></Scheme><Scheme><SchemeName>Simple</SchemeName><SchemePicture>Simple.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Courier New, 9pt</Font><HeaderFont>Courier New, 10pt, style=1</HeaderFont><CaptionFont>Courier New, 10pt, style=1</CaptionFont><AlternatingBackColor>White</AlternatingBackColor><BackColor>White</BackColor><BackgroundColor>Gainsboro</BackgroundColor><CaptionForeColor>Black</CaptionForeColor><CaptionBackColor>Silver</CaptionBackColor><ForeColor>DarkSlateGray</ForeColor><GridLineColor>DarkGray</GridLineColor><HeaderBackColor>DarkGreen</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>DarkGreen</LinkColor><LinkHoverColor>Blue</LinkHoverColor><ParentRowsForeColor>Black</ParentRowsForeColor><ParentRowsBackColor>Gainsboro</ParentRowsBackColor>"+
			"<SelectionForeColor>Black</SelectionForeColor><SelectionBackColor>DarkSeaGreen</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 1</SchemeName><SchemePicture>colorful1.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 9pt, style=1</CaptionFont><HeaderFont>Tahoma, 9pt, style=1</HeaderFont><AlternatingBackColor>LightGoldenrodYellow</AlternatingBackColor><BackColor>White</BackColor><BackgroundColor>LightGoldenrodYellow</BackgroundColor><CaptionForeColor>DarkSlateBlue</CaptionForeColor><CaptionBackColor>LightGoldenrodYellow</CaptionBackColor><ForeColor>DarkSlateBlue</ForeColor><GridLineColor>Peru</GridLineColor><GridLineStyle>None</GridLineStyle><HeaderBackColor>Maroon</HeaderBackColor><HeaderForeColor>LightGoldenrodYellow</HeaderForeColor><LinkColor>Maroon</LinkColor><LinkHoverColor>SlateBlue</LinkHoverColor><ParentRowsBackColor>BurlyWood</ParentRowsBackColor><ParentRowsForeColor>DarkSlateBlue</ParentRowsForeColor><SelectionForeColor>GhostWhite</SelectionForeColor>"+
			"<SelectionBackColor>DarkSlateBlue</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 2</SchemeName><SchemePicture>colorful2.bmp</SchemePicture><BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><AlternatingBackColor>GhostWhite</AlternatingBackColor><BackColor>GhostWhite</BackColor><BackgroundColor>Lavender</BackgroundColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>RoyalBlue</CaptionBackColor><ForeColor>MidnightBlue</ForeColor><GridLineColor>RoyalBlue</GridLineColor><HeaderBackColor>MidnightBlue</HeaderBackColor><HeaderForeColor>Lavender</HeaderForeColor><LinkColor>Teal</LinkColor><LinkHoverColor>DodgerBlue</LinkHoverColor><ParentRowsBackColor>Lavender</ParentRowsBackColor><ParentRowsForeColor>MidnightBlue</ParentRowsForeColor><SelectionForeColor>PaleGreen</SelectionForeColor><SelectionBackColor>Teal</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 3</SchemeName><SchemePicture>colorful3.bmp</SchemePicture>"+
			"<BorderStyle>None</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8pt, style=1</CaptionFont><HeaderFont>Tahoma, 8pt, style=1</HeaderFont><AlternatingBackColor>OldLace</AlternatingBackColor><BackColor>OldLace</BackColor><BackgroundColor>Tan</BackgroundColor><CaptionForeColor>OldLace</CaptionForeColor><CaptionBackColor>SaddleBrown</CaptionBackColor><ForeColor>DarkSlateGray</ForeColor><GridLineColor>Tan</GridLineColor><GridLineStyle>Solid</GridLineStyle><HeaderBackColor>Wheat</HeaderBackColor><HeaderForeColor>SaddleBrown</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>Teal</LinkHoverColor><ParentRowsBackColor>OldLace</ParentRowsBackColor><ParentRowsForeColor>DarkSlateGray</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>SlateGray</SelectionBackColor></Scheme><Scheme><SchemeName>Colorful 4</SchemeName><SchemePicture>colorful4.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8pt, style=1</CaptionFont>"+
			"<HeaderFont>Tahoma, 8pt, style=1</HeaderFont><AlternatingBackColor>White</AlternatingBackColor><BackColor>White</BackColor><BackgroundColor>Ivory</BackgroundColor><CaptionForeColor>Lavender</CaptionForeColor><CaptionBackColor>DarkSlateBlue</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Wheat</GridLineColor><HeaderBackColor>CadetBlue</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>DarkSlateBlue</LinkColor><LinkHoverColor>LightSeaGreen</LinkHoverColor><ParentRowsBackColor>Ivory</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>DarkSlateBlue</SelectionForeColor><SelectionBackColor>Wheat</SelectionBackColor></Scheme><Scheme><SchemeName>256 Color 1</SchemeName>" +
			"<SchemePicture>256_1.bmp</SchemePicture><Font>Tahoma, 8pt</Font><CaptionFont>Tahoma, 8 pt</CaptionFont><HeaderFont>Tahoma, 8pt</HeaderFont><AlternatingBackColor>Silver</AlternatingBackColor><BackColor>White</BackColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>Maroon</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Silver</GridLineColor><HeaderBackColor>Silver</HeaderBackColor><HeaderForeColor>Black</HeaderForeColor><LinkColor>Maroon</LinkColor><LinkHoverColor>Red</LinkHoverColor><ParentRowsBackColor>Silver</ParentRowsBackColor><ParentRowsForeColor>Black</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Maroon</SelectionBackColor></Scheme><Scheme><SchemeName>256 Color 2</SchemeName>"+
			"<SchemePicture>256_2.bmp</SchemePicture><BorderStyle>FixedSingle</BorderStyle><FlatMode>True</FlatMode><CaptionFont>Microsoft Sans Serif, 10 pt, style=1</CaptionFont><Font>Tahoma, 8pt</Font><HeaderFont>Tahoma, 8pt</HeaderFont><AlternatingBackColor>White</AlternatingBackColor><BackColor>White</BackColor><CaptionForeColor>White</CaptionForeColor><CaptionBackColor>Teal</CaptionBackColor><ForeColor>Black</ForeColor><GridLineColor>Silver</GridLineColor><HeaderBackColor>Black</HeaderBackColor><HeaderForeColor>White</HeaderForeColor><LinkColor>Purple</LinkColor><LinkHoverColor>Fuchsia</LinkHoverColor><ParentRowsBackColor>Gray</ParentRowsBackColor><ParentRowsForeColor>White</ParentRowsForeColor><SelectionForeColor>White</SelectionForeColor><SelectionBackColor>Maroon</SelectionBackColor></Scheme></pulica>";
		private AutoFormatDataGrid dataGrid;
		private DataSet dataSet;
		private Grid dgrid;
		private Label formats;
		private bool IMBusy;
		private Label preview;
		internal const string scheme = "<xsd:schema id=\"pulica\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\"><xsd:element name=\"Scheme\"><xsd:complexType><xsd:all><xsd:element name=\"SchemeName\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"SchemePicture\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"BorderStyle\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"FlatMode\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"Font\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"CaptionFont\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"HeaderFont\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"AlternatingBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"BackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"BackgroundColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"CaptionForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"CaptionBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"ForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"GridLineColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"GridLineStyle\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"HeaderBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"HeaderForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"LinkColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"LinkHoverColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"ParentRowsBackColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"ParentRowsForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"SelectionForeColor\" minOccurs=\"0\" type=\"xsd:string\"/><xsd:element name=\"SelectionBackColor\" minOccurs=\"0\" type=\"xsd:string\"/></xsd:all></xsd:complexType></xsd:element></xsd:schema>";
		private ListBox schemeName;
		private DataTable schemeTable;
		private int selectedIndex;
		private GridTableStyle tableStyle;

		// Nested Types
		private class AutoFormatDataGrid : Grid
		{
			// Methods
				public AutoFormatDataGrid()
				{
				}

				protected override void OnMouseDown(MouseEventArgs e)
				{
				}

 
				protected override void OnMouseMove(MouseEventArgs e)
				{
				}

				protected override void OnMouseUp(MouseEventArgs e)
				{
				}

				protected override bool ProcessDialogKey(Keys keyData)
				{
					return false;
				}

				protected override bool ProcessKeyPreview(ref Message m)
				{
					return false;
				}

			}
		}
	}
