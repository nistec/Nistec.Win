<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sample8.aspx.cs" Inherits="Sample8" Title="Untitled Page" %>
<%@ Register Assembly="MControl.Charts" Namespace="MControl.Charts.Web" TagPrefix="Mc" %>
<%@ Register Assembly="MControl.Charts" Namespace="MControl.Charts" TagPrefix="Mc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ObjectDataSource ID="DataSource1" runat="server" TypeName="DataSource"  SelectMethod="SalesByCategory"
        UpdateMethod="" DeleteMethod=""  >
     </asp:ObjectDataSource>

    &nbsp;
   
    <Mc:McGraph ID="mcWebChart" runat="server" ChartType="Surface3D" AxisLabelFont="Arial, 12pt"
        BackColor="WhiteSmoke" ChartTitleFont="Verdana, 20pt" DataSourceID="DataSource1"
        ElementOpacity="200" Height="500px" FieldLabelFont="Arial, 8pt" KeyItemsFont="Arial, 8pt"
        Width="598px" ZValue="0.8" ElementSpacing="0.6" 
        ShouldSerializeState="true"
        GraphTitle="Sales By Category" 
        FieldLabel="CategoryName"  
        AxisLabelX="Category" AxisLabelY="Product Sales" >
        
        <DataItems>
            <Mc:DataItem Name="ProductSales" />
        </DataItems>
        <ColorItems>
            <Mc:ColorItem Color="Red" Name="Red" />
        </ColorItems>
    </Mc:McGraph>
</asp:Content>

