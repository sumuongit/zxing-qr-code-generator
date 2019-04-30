<%@ Page Title="QR CODE GENERATOR" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QRCodeGenerator.aspx.cs" Inherits="SecureQRCodeWeb.QRCodeGenerator" %>

<asp:Content ID="qrCodeGenerator" ContentPlaceHolderID="MainContent" runat="server">

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

        function validateForm() {

            if (navIndex == 0) {
                var text;
                text = document.getElementById("<%=txtText.ClientID%>").value.length;

          if (text == 0) {
              alert("Text is required!");
              return false;
          }
      }
  }

    </script>
    <h2 class="Title">QR CODE GENERATOR</h2>

    <table class="QRCodeTable">

        <tr>
            <td class="QRCodeAppearance">

                <div class="CollapsibleContainerDivStyle">
                    <h3>Step 1: QR CODE CONTENT TYPES</h3>

                    <ul id="nav">

                        <li><a href="#">Text</a>
                            <ul>
                                <li>
                                    <div class="CollapsibleDivStyle">
                                        <table>
                                            <tr>
                                                <td class="QRCodeContentLeft">
                                                    <asp:Label ID="lblText" runat="server" Text="Text: "></asp:Label>
                                                </td>
                                                <td class="QRCodeContentRight">
                                                    <asp:TextBox ID="txtText" runat="server" TextMode="MultiLine" Rows="4" Columns="35" CssClass="MultilineTextBoxStyle"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </li>
                            </ul>
                        </li>


                    </ul>
                </div>

            </td>
            <td></td>
            <td class="QRCodeAppearance">

                <div class="DivStyle">
                    <h3>2: QR CODE APPEARANCE</h3>

                    <table>
                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblErrorCorrection" runat="server" Text="Error Correction: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">

                                <asp:DropDownList ID="ddlErrorCorrection" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="Low (7%)" Value="Low" />
                                    <asp:ListItem Text="Medium (15%)" Value="Medium" />
                                    <asp:ListItem Text="Quartile (25%)" Value="Quartile" />
                                    <asp:ListItem Text="High (30%)" Value="High" />
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblEncoding" runat="server" Text="Encoding: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">

                                <asp:DropDownList ID="ddlEncoding" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="Alpha-Numeric" Value="Alpha-Numeric" />
                                    <asp:ListItem Text="Byte" Value="Byte" />
                                    <asp:ListItem Text="Numeric" Value="Numeric" />
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblVersion" runat="server" Text="Version: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">

                                <asp:DropDownList ID="ddlVersion" runat="server" CssClass="DropDownListStyle">
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblBlockSize" runat="server" Text="Block Size: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">
                                <asp:DropDownList ID="ddlBlockSize" runat="server" CssClass="DropDownListStyle">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblForeColor" runat="server" Text="Foreground Color: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">
                                <asp:TextBox ID="txtForeColor" runat="server" CssClass="TextBoxStyle" ClientIDMode="Static"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblBackColor" runat="server" Text="Background Color: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">
                                <asp:TextBox ID="txtBackColor" runat="server" CssClass="TextBoxStyle" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblEmbedLogo" runat="server" Text="Embed Logo: "></asp:Label>
                            </td>
                            <td class="QRCodeContentRight">

                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:FileUpload ID="fuEmbedLogo" runat="server" CssClass="ButtonStyle_FU" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnGenerate" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnGenerate" runat="server" Text="Generate QR Code" CssClass="ButtonStyle" OnClick="btnGenerate_Click" OnClientClick="return validateForm();" />
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
                                        <asp:PostBackTrigger ControlID="btnGenerate" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblResultingQRCode" runat="server" Text="Resulting QR Code: "></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <asp:Image ID="imgQRCode" runat="server" CssClass="ImageStyle" />
                                        </fieldset>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnGenerate" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="QRCodeContentLeft">
                                <asp:Label ID="lblFormat" runat="server" Text="Format: "></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFormat" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="JPG" Value="JPG" />
                                    <asp:ListItem Text="PNG" Value="PNG" />
                                    <asp:ListItem Text="GIF" Value="GIF" />
                                    <asp:ListItem Text="BMP" Value="BMP" />
                                </asp:DropDownList>


                            </td>

                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnDownload" runat="server" Text="Download QR Code" CssClass="ButtonStyle" OnClick="btnDownload_Click" />

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <asp:Label ID="lblErrorDownload" runat="server"></asp:Label>
                                        </fieldset>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnDownload" EventName="Click" />
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
