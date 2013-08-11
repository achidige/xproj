<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClinSpec.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row-fluid">
        <div class="span7">
            <div class="box">
                <div class="box-title">
                    <h3><i class="icon-bar-chart"></i>Clinical Study Tool Stats</h3>
                    <div class="box-tool">
                        <a data-action="collapse" href="#"><i class="icon-chevron-up"></i></a>
                        <a data-action="close" href="#"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">
                    <ul style="list-style:none">
                        <li>
                           <i class="icon-arrow-right"></i> Number of Compounds: <span class="number">5</span>                              
                        </li>
                        <li>
                           <i class="icon-arrow-right"></i> Number of Studies:  <span class="number">25</span>                              
                        </li>
                        <li>
                           <i class="icon-arrow-right"></i> Default MDR Version :  <span class="value">CDISC 1.1.2</span>                                                            
                        </li>
                    </ul>
                </div>
        </div>
            </div>
        <div class="span5">
            <div class="box">
                <div class="box-title">
                    <h3><i class="icon-tasks"></i>To Do</h3>
                    <div class="box-tool">
                        <a data-action="collapse" href="#"><i class="icon-chevron-up"></i></a>
                        <a data-action="close" href="#"><i class="icon-remove"></i></a>
                    </div>
                </div>
                <div class="box-content">                    
                    <ul class="things-to-do">
                    <li>
                        <p>
                            <i class="icon-user"></i>
                            <span class="value">1</span>
                            Manage Study XYZ...
                            <a class="btn btn-success" href="/ManageDomains.aspx?StudyId=1">Go</a>
                        </p>
                    </li>
                    <li>
                        <p>
                            <i class="icon-comments"></i>
                            <span class="value">2</span>
                            Review MDR Changes
                            <a class="btn btn-success" href="#">Go</a>
                        </p>
                    </li>
                    <li>
                        <p>
                            <i class="icon-shopping-cart blue"></i>
                            <span class="value">3</span>
                            Approve Code List changes
                            <a class="btn btn-success" href="#">Go</a>
                        </p>
                    </li>
                </ul>
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
