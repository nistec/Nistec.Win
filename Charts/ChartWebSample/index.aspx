<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <form id="Form1" runat="server">
    To Run this samples you need to change the ConnectionString
    from Web.config to Northwind database.&nbsp;<br />
    <br />
    <br />
    <asp:DataList ID="DataList1" runat="server" HorizontalAlign="Center" RepeatColumns="1"
        Width="100%" OnItemCommand="DataList1_ItemCommand" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Both">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="<%# Bind('number') %>"
                ImageUrl="<%# Bind('imagindx') %>" />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" BackColor="White" ForeColor="#330099" />
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
    </asp:DataList>
    </form>
</asp:Content>

