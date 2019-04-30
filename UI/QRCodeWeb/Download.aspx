<%@ Page Language="C#" Title="QR CODE GENERATOR" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Download.aspx.cs" Inherits="SecureQRCodeWeb.Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="Content/MyStyle.css" />

    <h4 class="Title" style="text-align: center;">DOWNLOAD QR CODE</h4>
    <table class="DownloadTable">
        <tr>
            <td class="DownloadAppearance">

                <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="ButtonStyle" OnClick="btnDownload_Click" />

            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrorDownload" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
