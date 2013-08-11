<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateDomain.aspx.cs" Inherits="ClinSpec.UpdateDomain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row-fluid">
        <div class="controls controls-row">
            <span class="span3">Select a domain to manage
            </span>
            <span class="span9">
                <asp:DropDownList ID="lstDomains" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lstDomains_SelectedIndexChanged"></asp:DropDownList>
            </span>

        </div>
        <div class="form-actions">
            <span class="span3"></span>
            <span class="span9">
                <asp:Label ID="Label3" runat="server" Text="" Style="color: red" CssClass="input-error"></asp:Label>
                <br />
                <button type="submit" class="btn btn-primary" runat="server" id="btnAddNew" onserverclick="btnAddNew_ServerClick">Add New Variable</button>
                &nbsp;&nbsp;&nbsp;
                <button type="submit" class="btn btn-primary" runat="server" id="Button1" onserverclick="btnSave_ServerClick">Save</button>
                &nbsp;&nbsp;&nbsp;
                <button type="submit" class="btn btn-primary" runat="server" id="btnNext2" onserverclick="btnNext_ServerClick">Download Specification</button>

            </span>
        </div>
        <div class="controls controls-row" id="plnDomain">
            <span class="span12">
                <div runat="server" id="lblMsg" visible="false">
                </div>
                <asp:GridView ID="grdDomainData" runat="server" OnSelectedIndexChanged="grdDomainData_SelectedIndexChanged" AutoGenerateColumns="false" OnRowDataBound="grdDomainData_RowDataBound">
                    <Columns>                       
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Name
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVarName" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.Name") %>'></asp:Label>
                                <asp:Label ID="lblCodeListCode" runat="server" Visible='<%# Eval("RowType")=="CodeList" %>' Text='<%# Eval("CodeListCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Description
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVarLabelText" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.LableText") %>'></asp:Label>
                                <asp:Label ID="lblCodeListName" CssClass="codelist-namestyle" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("CodeListName") %>'></asp:Label>
                                <asp:Label ID="lblCodeListValue" runat="server" Visible='<%# Eval("RowType")=="CodeList" %>' Text='<%# Eval("CodeListValue") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Core
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCore" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.Core") %>'></asp:Label>
</ItemTemplate>
                            </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Role
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRole" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.Role").ToString().Replace("_"," ") %>'></asp:Label>
</ItemTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                Data Type
                            </HeaderTemplate>
                            <ItemTemplate>
                                   <asp:Label ID="lblDataType" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.DataType") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>


                        <asp:TemplateField>
                            <HeaderTemplate>
                                Length
                            </HeaderTemplate>
                            <ItemTemplate>
                                   <asp:Label ID="lblLen" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.Length") %> '></asp:Label> <asp:Label ID="lblSigDigit" runat="server" Visible='<%# Eval("RowType")=="Variable" %>' Text='<%# Eval("VarObj.SignificantDigits") %> '></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>


                        <asp:TemplateField>
                            <HeaderTemplate>
                                Exclude From Study 
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="lblChecked" runat="server" Visible='<%#  Eval("AllowExclusion") %>' Checked='<%# Eval("IsExcluded") %>'></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField>
                            <HeaderTemplate>
                                Edit
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a  href='AddEditVariable.aspx?Mode=Edit&StudyId=<%: Request.QueryString["StudyId"] %>&DomainId=<%: this.DomainId %>&VariableId=<%# Eval("VariableId") %>' title="Edit" class="icon-edit-sign"></a>
                                <asp:Label ID="lblRowtype" runat="server" ForeColor="Black" Visible='false'
                                    Text='<%# Eval("Rowtype") %>'></asp:Label>

                                <asp:Label ID="lblVariableId" runat="server" ForeColor="Red" Visible='false'
                                    Text='<%# Eval("VariableId") %>'></asp:Label>
                                <asp:Label ID="lblCodeListId" runat="server" Visible='false' ForeColor="Green" Text='<%# Eval("CodeListId") %>'></asp:Label>
                                <asp:Label ID="lblCodeListValueId" runat="server" Visible='false' ForeColor="Blue" Text='<%# Eval("CodeListValueId") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </span>
            
        </div>

        <div class="form-actions">
            <span class="span3"></span>
            <span class="span9">
                <asp:Label ID="lblMessages" runat="server" Text="" Style="color: red" CssClass="input-error"></asp:Label>
                <br />
                <button type="submit" class="btn btn-primary" runat="server" id="Button2" onserverclick="btnAddNew_ServerClick">Add New Variable</button>
                &nbsp;&nbsp;&nbsp;
                <button type="submit" class="btn btn-primary" runat="server" id="Button3" onserverclick="btnSave_ServerClick">Save</button>
                &nbsp;&nbsp;&nbsp;
                <button type="submit" class="btn btn-primary" runat="server" id="Button4" onserverclick="btnNext_ServerClick">Download Specification</button>


            </span>

        </div>
    </div>

</asp:Content>
