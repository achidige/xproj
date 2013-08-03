<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StandardDomains.aspx.cs" Inherits="ClinSpec.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="dsStandardDomain" runat="server" ConnectionString="name=SpecToolModelContext" DefaultContainerName="SpecToolModelContext" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="SpecDomains"></asp:EntityDataSource>
    
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="Id" DataSourceID="dsStandardDomain"  DefaultMode="Insert" >
        <EditItemTemplate>
            Id:
            <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
            <br />
            DomainId:
            <asp:TextBox ID="DomainIdTextBox" runat="server" Text='<%# Bind("DomainId") %>' />
            <br />
            SpecificationId:
            <asp:TextBox ID="SpecificationIdTextBox" runat="server" Text='<%# Bind("SpecificationId") %>' />
            <br />
            Domain:
            <asp:TextBox ID="DomainTextBox" runat="server" Text='<%# Bind("Domain") %>' />
            <br />
            Specification:
            <asp:TextBox ID="SpecificationTextBox" runat="server" Text='<%# Bind("Specification") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Id:
            <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>' />
            <br />
            DomainId:
            <asp:TextBox ID="DomainIdTextBox" runat="server" Text='<%# Bind("DomainId") %>' />
            <br />
            SpecificationId:
            <asp:TextBox ID="SpecificationIdTextBox" runat="server" Text='<%# Bind("SpecificationId") %>' />
            <br />
            Domain:
            <asp:TextBox ID="DomainTextBox" runat="server" Text='<%# Bind("Domain") %>' />
            <br />
            Specification:
            <asp:TextBox ID="SpecificationTextBox" runat="server" Text='<%# Bind("Specification") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            Id:
            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
            <br />
            DomainId:
            <asp:Label ID="DomainIdLabel" runat="server" Text='<%# Bind("DomainId") %>' />
            <br />
            SpecificationId:
            <asp:Label ID="SpecificationIdLabel" runat="server" Text='<%# Bind("SpecificationId") %>' />
            <br />
            Domain:
            <asp:Label ID="DomainLabel" runat="server" Text='<%# Bind("Domain") %>' />
            <br />
            Specification:
            <asp:Label ID="SpecificationLabel" runat="server" Text='<%# Bind("Specification") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
