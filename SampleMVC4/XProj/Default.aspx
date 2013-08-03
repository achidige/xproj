<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XProj._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    Please select/enter
    Compound: <asp:DropDownList ID="DropDownList1"  runat="server"></asp:DropDownList>
    
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</asp:Content>
