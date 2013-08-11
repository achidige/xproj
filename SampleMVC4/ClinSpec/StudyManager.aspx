<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudyManager.aspx.cs" Inherits="ClinSpec._Default" %>

<asp:Content runat="server" ID="HeadConent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div class="form-horizontal form-row-separated">
        <div class="control-group">
            <label for="textfield3" class="control-label">Compound: </label>
            <div class="controls">
                <asp:DropDownList class="input-xxlarge" id="lstComponent" onserverchange="lstComponent_ServerChange" runat="server" tabindex="1" AutoPostBack="True" OnSelectedIndexChanged="lstComponent_SelectedIndexChanged" >
                </asp:DropDownList>
                 <br />
                Or Enter A New Compound
                <br />
                <asp:TextBox  class="input-xxlarge" ID="txtComponent" runat="server"></asp:TextBox>
            </div>
        </div>

                <div class="control-group">
            <label for="textfield3" class="control-label">Study: </label>
            <div class="controls">
                <asp:DropDownList class="input-xxlarge" id="lstStudy" runat="server" onserverchange="lstStudy_ServerChange" tabindex="1" AutoPostBack="False" OnSelectedIndexChanged="lstStudy_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                Or Enter A New Study
                <br />
                <asp:TextBox  class="input-xxlarge" ID="txtNewStudy" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="form-actions">

            <asp:Label ID="lblMessages" runat="server" Text="Label" CssClass="text-error"></asp:Label> <br />
            <button type="submit" class="btn btn-primary button-next" runat="server" id="btnNext" onserverclick="btnNext_ServerClick">Next</button>
          
        </div>
    </div>


</asp:Content>
