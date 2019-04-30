<%@ Page Title="QR CODE DECODER" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QRCodeDecoder.aspx.cs" Inherits="SecureQRCodeWeb.QRCodeDecoder" %>

<asp:Content ID="qrCodeReader" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="Content/jquery-ui.css">
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/jquery-ui.js"></script>
    <script src="Scripts/colpick.js" type="text/javascript"></script>
    <link rel="stylesheet" href="Content/colpick.css" type="text/css" />
    <link rel="stylesheet" href="Content/MyMenuStyle.css" type="text/css">
    <link rel="stylesheet" href="Content/MyStyle.css" />

    <script>
        var navIndex;
        jQuery(document).ready(function ($) {
            $('#nav > li > a').click(function () {
                navIndex = $('#nav > li > a').index(this);

                $('#nav li ul').slideUp();
                if ($(this).next().is(":visible")) {
                    $(this).next().slideUp();
                } else {
                    $(this).next().slideToggle();
                }
                $('#nav li a').removeClass('active');
                $(this).addClass('active');
            });

            $('#txtBackColor').colpick({
                colorScheme: 'dark',
                layout: 'rgbhex',
                color: 'ff8800',
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).css('background-color', '#' + hex);
                    $(el).val('#' + hex);
                    $(el).colpickHide();
                }
            });

            $('#txtForeColor').colpick({
                colorScheme: 'dark',
                layout: 'rgbhex',
                color: 'ff8800',
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).css('background-color', '#' + hex);
                    $(el).val('#' + hex);
                    $(el).colpickHide();
                }
            });
        });

    </script>
    <h2 class="Title">QR CODE DECODER</h2>

    <table class="QRCodeTable">

        <tr>

            <td class="QRCodeAppearance">

                <div class="DivStyle">
                    <h3>Step 1: UPLOAD GENERATED QR CODE</h3>

                    <table>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblEmbedLogo" runat="server" Text="Select QR Code: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">

                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="fuUploadQRCode" runat="server" CssClass="ButtonStyle_FU" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload QR Code" CssClass="ButtonStyle" OnClick="btnUploadQRCode_Click" />

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>

                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <asp:Label ID="lblErrorGenerate" runat="server"></asp:Label>
                                        </fieldset>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblResultingQRCode" runat="server" Text="Existing QR Code: "></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <asp:Image ID="imgQRCode" runat="server" CssClass="ImageStyle" />
                                        </fieldset>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>

            </td>
            <td class="QRCodeAppearance">
                <div class="DivStyle">
                    <h3>Step 2: DECODE UPLOADED QR CODE</h3>
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnDecode" runat="server" Text="Decode QR Code" CssClass="ButtonStyle_Read" OnClick="btnDecode_Click" />


                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="Label1" runat="server" Text="Original Information: "></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <asp:TextBox ID="txtDecodedOriginalInfo" runat="server" TextMode="MultiLine" Rows="20" Columns="40" CssClass="MultilineTextBoxStyle"></asp:TextBox>
                                        </fieldset>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnDecode" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <asp:Label ID="lblErrorDecode" runat="server"></asp:Label>
                                        </fieldset>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnDecode" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
