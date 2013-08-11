<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDomains.aspx.cs" Inherits="ClinSpec.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row-fluid">
        <div class="controls controls-row">
            <span class="span5 box">
                     <div class="box-title">
                        Domains from MDR/Other Studies
                    </div>
                    <div class="box-content">
                        <asp:TreeView ID="treeAvailable" runat="server"></asp:TreeView>
                    </div>
            </span>
            <span class="span2 box">
                <br />
                <br />
                <br />
                    
                <button type="submit" class="btn btn-primary" runat="server" id="btnAdd" onserverclick="btnAdd_ServerClick">Add  <i class="icon-forward"></i></button>
                <br />
                <br />
                <br />
                <button type="submit" class="btn btn-primary" runat="server" id="btnRemove" onserverclick="btnRemove_ServerClick" onclick="if(!confirm('All changes made to the selected domain(s) will be lost. Are you sure you want to continue?')) { return false;}"><i class="icon-backward"></i>Remove </button>
            </span>
            <span class="span5 box">
                    <div class="box-title">
                        Study Domains
                    </div>
                    <div class="box-content">
                        <asp:TreeView ID="treeSelected" runat="server"></asp:TreeView>
                    </div>
            </span>
        </div>

        <div class="form-actions">
            <span class="span5"></span>
            <span class="span7">

                <asp:Label ID="lblMessages" runat="server" Text="" Style="color: red" CssClass="input-error"></asp:Label>
                <br />
                <button type="button" class="btn btn-primary button-next" runat="server" id="btnAddNewDomain" onserverclick="btnAddNewDomain_ServerClick">Add New Domain</button> &nbsp;&nbsp;&nbsp;
                <button type="submit" class="btn btn-primary button-next" runat="server" id="btnNext" onserverclick="btnNext_ServerClick">Next</button>
            </span>

        </div>
    </div>

</asp:Content>
