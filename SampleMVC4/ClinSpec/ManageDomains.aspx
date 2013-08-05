<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDomains.aspx.cs" Inherits="ClinSpec.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row-fluid">

        <div class="controls controls-row">
            <span class="span5">Availabe
            </span>
            <span class="span2">
            </span>
            <span class="span5">Sellected
            </span>
        </div>
        <div class="controls controls-row">
            <span class="span5"> <asp:TreeView ID="treeAvailable" runat="server" ></asp:TreeView>
            </span>
            <span class="span2" style="vertical-align:central">
                
                <button type="submit" class="btn btn-primary" runat="server" id="btnAdd" onserverclick="btnAdd_ServerClick"> Add  <i class="icon-forward"></i></button>
                <br />
                <br /> 
                <button type="submit" class="btn btn-primary" runat="server" id="btnRemove" onserverclick="btnRemove_ServerClick"> <i class="icon-backward"></i>  Remove </button>

                 

            </span>
            <span class="span5"> <asp:TreeView ID="treeSelected" runat="server" ></asp:TreeView>
            </span>
        </div>

       

    </div>

</asp:Content>
