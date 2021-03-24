<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="up_down._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>File Upload and Download with a ListBox!</h1>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"/>
    <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
    <br />
    <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True"></asp:ListBox>
</asp:Content>
