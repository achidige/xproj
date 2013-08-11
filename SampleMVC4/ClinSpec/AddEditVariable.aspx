<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEditVariable.aspx.cs" Inherits="ClinSpec.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row-fluid">
        <div class="controls-row">
            <span class="span3">
                Name&nbsp;
            </span>
            <span class="span9">
                <asp:TextBox ID="txtName" runat="server" CssClass="input-xlarge" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="txtName" ErrorMessage="Name is required."></asp:RequiredFieldValidator>                
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" runat="server"  CssClass="text-error" ControlToValidate="txtName" ErrorMessage="Name must contain only 2 to 8 capitol letters and digits. Name cannot start with a digit."  ValidationExpression="[A-Z][A-Z0-9]{1,8}"></asp:RegularExpressionValidator>                
                <asp:CustomValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" CssClass="text-error" OnServerValidate="RegularExpressionValidator3_ServerValidate"  ControlToValidate="txtName" ErrorMessage="Variable already exists in the domain. Please enter another name."  ValidationExpression="[A-Z]{2}"></asp:CustomValidator>                
            </span>
        </div>

        <div class="controls-row">
            <span class="span3">
                Label Text
            </span>
            <span class="span9">
                <asp:TextBox ID="txtLabelText" runat="server"  CssClass="input-xlarge" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" CssClass="text-error" ControlToValidate="txtLabelText" ErrorMessage="Label Text is required."></asp:RequiredFieldValidator>
            </span>
        </div>

        <div class="controls-row">
            <span class="span3">
                Core
            </span>
            <span class="span9">
                <asp:DropDownList ID="ddlCore" runat="server"  CssClass="input-xlarge">                    
                </asp:DropDownList>
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator2" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="ddlCore" 
                    InitialValue="0"
                    ErrorMessage="Core is required."></asp:RequiredFieldValidator>                
            </span>
        </div>

         <div class="controls-row">
            <span class="span3">
                Origin
            </span>
            <span class="span9">
                <asp:DropDownList ID="ddlOrigin" runat="server"  CssClass="input-xlarge">
                </asp:DropDownList>
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator5" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="ddlOrigin" 
                    InitialValue="0"
                    ErrorMessage="Origin is required."></asp:RequiredFieldValidator>                
            </span>
        </div>


          <div class="controls-row">
            <span class="span3">
                Role
            </span>
            <span class="span9">
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="input-xlarge">
                </asp:DropDownList>
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator3" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="ddlRole" 
                    InitialValue="0"
                    ErrorMessage="Role is required."></asp:RequiredFieldValidator>                
            </span>
        </div>

        <div class="controls-row">
            <span class="span3">
                Data Type
            </span>
            <span class="span9">
    <asp:DropDownList ID="ddlDataType" runat="server" CssClass="input-xlarge">
                </asp:DropDownList>
                <asp:RequiredFieldValidator  ID="RequiredFieldValidator4" Display="Dynamic" runat="server" CssClass="text-error"  ControlToValidate="ddlDataType" 
                    InitialValue="0"
                    ErrorMessage="DataType is required."></asp:RequiredFieldValidator>                            </span>
        </div>

        
        <div class="controls-row">
            <span class="span3">
                Length/Significant Digits
            </span>
            <span class="span9">
                <asp:TextBox ID="txtLength" runat="server"  CssClass="input-small" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server"  ControlToValidate="txtLength" ErrorMessage="Length is required."></asp:RequiredFieldValidator>                
                , <asp:TextBox ID="txtSigDigits" runat="server"  CssClass="input-small" ></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ControlToValidate="txtLength" ErrorMessage="Length is required."></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="txtLength" Operator="DataTypeCheck"  Type="Integer" ErrorMessage="Length must be integer value."></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" Display="Dynamic" ControlToValidate="txtSigDigits" Operator="DataTypeCheck"  Type="Integer" ErrorMessage="Significant Digits must be integer value."></asp:CompareValidator>
            </span>
        </div>

        

           <div class="controls-row">
            <span class="span3">
                Code List
            </span>
            <span class="span9">
                <asp:DropDownList ID="ddlCodeLists" runat="server" CssClass="input-xlarge"></asp:DropDownList>
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
                <button type="button" class="btn btn-primary button-next" runat="server" id="btnSave" onserverclick="btnSave_ServerClick">Save</button> &nbsp;&nbsp;&nbsp;
                <button type="button" class="btn btn-primary button-close" runat="server" id="btnClose" causesvalidation="false" onserverclick="btnClose_ServerClick">Close</button> &nbsp;&nbsp;&nbsp;

            </span>

        </div>
    </div>
</asp:Content>
