<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sample4.aspx.cs" Inherits="Sample4" Title="Untitled Page" %>
<%@ Register Assembly="MControl.Charts" Namespace="MControl.Charts.Web" TagPrefix="Mc" %>
<%@ Register Assembly="MControl.Charts" Namespace="MControl.Charts" TagPrefix="Mc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ObjectDataSource ID="DataSource1" runat="server" TypeName="DataSource"  SelectMethod="Top10Orders"
        UpdateMethod="" DeleteMethod=""  >
     </asp:ObjectDataSource>

    &nbsp;
   
    <Mc:McGraph ID="mcWebChart" runat="server" ChartType="Line" AxisLabelFont="Arial, 12pt"
        BackColor="WhiteSmoke" ChartTitleFont="Verdana, 20pt" DataSourceID="DataSource1"
        ElementOpacity="200" Height="500px" FieldLabelFont="Arial, 8pt" KeyItemsFont="Arial, 8pt"
        Width="598px" ZValue="0.8" ElementSpacing="0.6" 
        ShouldSerializeState="true"
        GraphTitle="Top 10 Sales" 
        FieldLabel="CompanyName"  
        AxisLabelX="Company" AxisLabelY="Amount" >
        <DataItems>
            <Mc:DataItem Name="SaleAmount" />
        </DataItems>
        <ColorItems>
            <Mc:ColorItem Color="Blue" Name="Blue" />
        </ColorItems>
    </Mc:McGraph>

</asp:Content>

