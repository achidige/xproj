<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewDomain.aspx.cs" Inherits="ClinSpec.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="controls-row">
            <span class="span3">
               Name
            </span>
            <span class="span9">
                <asp:TextBox ID="txtName" runat="server" CssClass="input-xlarge" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="txtName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server"  CssClass="text-error" ControlToValidate="txtName" ErrorMessage="Name must be a 2 capitol letters word."  ValidationExpression="[A-Z]{2}"></asp:RegularExpressionValidator>                
                <asp:CustomValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" CssClass="text-error" OnServerValidate="RegularExpressionValidator3_ServerValidate"  ControlToValidate="txtName" ErrorMessage="Name already exists in the domain. Pleas enter another name."  ValidationExpression="[A-Z]{2}"></asp:CustomValidator>                
            </span>
        </div>

        <div class="controls-row">
            <span class="span3">
                Description 
            </span>
            <span class="span9">
                <asp:TextBox ID="txtDomainDescription" runat="server"  CssClass="input-xlarge" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" CssClass="text-error" ControlToValidate="txtDomainDescription" ErrorMessage="Description is required."></asp:RequiredFieldValidator>
            </span>
        </div>

        <div class="controls-row">
            <span class="span3">
                Classification
            </span>
            <span class="span9">
                <asp:DropDownList ID="ddlClassification" runat="server" OnSelectedIndexChanged="ddlClassification_SelectedIndexChanged" AutoPostBack="true"   CssClass="input-xlarge">
                    <asp:ListItem Value="None">-- Select Classification --</asp:ListItem>
                    <asp:ListItem Value="EVENTS">EVENTS</asp:ListItem>
                    <asp:ListItem Value="INTERVENTIONS">INTERVENTIONS</asp:ListItem>
                    <asp:ListItem Value="FINDINGS">FINDINGS</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="ddlClassification" 
                    InitialValue="0"
                    ErrorMessage="Classification is required."></asp:RequiredFieldValidator>                
            </span>
        </div>

        <div class="controls-row">
            <span class="span3">
                Structure Description
            </span>
            <span class="span9">
                <asp:TextBox ID="txtStructureDescription" runat="server"  CssClass="input-xxlarge" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server"  ControlToValidate="txtStructureDescription" ErrorMessage="Structure Description is required."></asp:RequiredFieldValidator>                
            </span>
        </div>

         <div class="controls-row">
            <span class="span3">
                Comments
            </span>
            <span class="span9">
                <asp:TextBox ID="txtComments"  MaxLength="1000" TextMode="MultiLine"  runat="server"  CssClass="input-xxlarge" ></asp:TextBox>
            </span>
        </div>

        <div class="form-actions">
            <span class="span3"></span>
            <span class="span7">
                <asp:Label ID="lblMessages" runat="server" Text=""  Style="color: red" CssClass="input-error"></asp:Label>                
                <button type="button" class="btn btn-primary button-next" runat="server" id="btnSave" onserverclick="btnSave_ServerClick">Save & Close</button> &nbsp;&nbsp;&nbsp;
       
            </span>

        </div>
    </div>
</asp:Content>
