<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AlertBox.ascx.cs" Inherits="ClinSpec.UserControls.AlertBox" %>
<div id="modal-1" class="modal hide" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h3 id="myModalLabel">Error </h3>
        </div>
        <div class="modal-body">
            <p runat="server" id="lblMessageBox"></p>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" data-dismiss="modal">OK</button>
        </div>
    </div>