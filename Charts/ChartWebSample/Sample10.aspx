<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sample10.aspx.cs" Inherits="Sample10" Title="Untitled Page" %>
<%@ Register Assembly="MControl.Charts" Namespace="MControl.Charts.Web" TagPrefix="Mc" %>
<%@ Register Assembly="MControl.Charts" Namespace="MControl.Charts" TagPrefix="Mc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ObjectDataSource ID="DataSource1" runat="server" TypeName="DataSource"  SelectMethod="EmployeeSalesByQuarter"
        UpdateMethod="" DeleteMethod=""  >
     </asp:ObjectDataSource>

    &nbsp;
   
    <Mc:McGraph ID="mcWebChart" runat="server" ChartType="PieExpanded3D" AxisLabelFont="Arial, 12pt"
        BackColor="WhiteSmoke" ChartTitleFont="Verdana, 20pt" DataSourceID="DataSource1"
        ElementOpacity="200" Height="500px" FieldLabelFont="Arial, 8pt" KeyItemsFont="Arial, 8pt"
        Width="598px" ZValue="0.8" ElementSpacing="0.6"  ChartBackColor="white"
        ShouldSerializeState="true"
        GraphTitle="Employee Sales By Quarter" 
        FieldLabel="LastName"  
        AxisLabelX="Employee" AxisLabelY="Amount" >

        
 
        <DataItems>
            <Mc:DataItem Name="Quarter1" />
            <Mc:DataItem Name="Quarter2" />
            <Mc:DataItem Name="Quarter3" />
            <Mc:DataItem Name="Quarter4" />
        </DataItems>
        <ColorItems>
            <Mc:ColorItem Color="Blue" Name="Red" />
            <Mc:ColorItem Color="Blue" Name="Blue" />
            <Mc:ColorItem Color="Blue" Name="Gold" />
            <Mc:ColorItem Color="Blue" Name="Green" />
            <Mc:ColorItem Color="Blue" Name="Violet" />
     </ColorItems>
        <KeyItems>
        <Mc:KeyItem  Name="Quarter1"/>
        <Mc:KeyItem  Name="Quarter2"/>
       <Mc:KeyItem  Name="Quarter3"/>
       <Mc:KeyItem  Name="Quarter4"/>
       </KeyItems>
    </Mc:McGraph>

</asp:Content>

