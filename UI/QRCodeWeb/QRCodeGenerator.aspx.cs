using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Collections;
using SecureQRCode;
//ZXING LIBRARY REFERENCE
using com.google.zxing.qrcode;
using com.google.zxing.common;
using com.google.zxing.qrcode.decoder;

namespace SecureQRCodeWeb
{   
    public partial class QRCodeGenerator : System.Web.UI.Page
    {
        Mode mode;
        ErrorCorrectionLevel errorCorrectionLevel;
        System.Drawing.Image image = null;
        StringBuilder sb = new StringBuilder();
        Property property = new Property();
        int version, scale;
        SolidBrush backColor;
        SolidBrush foreColor;       
      
        byte[] plaintext;
        byte[] encryptedtext; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (imgQRCode.ImageUrl == "")
                {
                    string qrCodePath = Server.MapPath("~/Images/QR_Codes/" + "default_image.png");

                    image = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(qrCodePath)));

                    using (Bitmap bitMap = new Bitmap(image))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            bitMap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                            Byte[] bytes = new Byte[memoryStream.Length];
                            memoryStream.Position = 0;
                            memoryStream.Read(bytes, 0, (int)bytes.Length);
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            imgQRCode.ImageUrl = "data:image/png;base64," + base64String;
                            Session["isDefaultImage"] = "IsDefault";
                        }
                    }
                }

                int mxSize = 21; int increment = 0;
                for (int i = 1; i <= 40; i++)
                {
                    mxSize = mxSize + increment;
                    ddlVersion.Items.Add(new ListItem(i.ToString() + "     [" + mxSize + "X" + mxSize + "]", i.ToString()));
                    increment = 4;
                }
                for (int i = 1; i <= 150; i++)
                {
                    ddlBlockSize.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                txtForeColor.BackColor = System.Drawing.Color.Black;
                txtForeColor.ForeColor = System.Drawing.Color.White;
                txtForeColor.Text = "#000000";

                txtBackColor.BackColor = System.Drawing.Color.White;
                txtBackColor.ForeColor = System.Drawing.Color.Black;
                txtBackColor.Text = "#ffffff";
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtForeColor.Text != String.Empty && txtBackColor.Text != String.Empty)
            {
                backColor = new SolidBrush(System.Drawing.ColorTranslator.FromHtml(txtBackColor.Text));
                foreColor = new SolidBrush(System.Drawing.ColorTranslator.FromHtml(txtForeColor.Text));
            }
            else
            {
                lblErrorGenerate.Text = "Fill Required Field First!";
                lblErrorGenerate.ForeColor = System.Drawing.Color.Red;
                return;
            }

            String encoding = ddlEncoding.Text;
            if (encoding == "Byte")
            {
                mode = Mode.BYTE;
            }
            else if (encoding == "Alpha-Numeric")
            {
                mode = Mode.ALPHANUMERIC;
            }
            else if (encoding == "Numeric")
            {
                mode = Mode.NUMERIC;
            }
            try
            {
                scale = Convert.ToInt16(ddlBlockSize.Text);
            }
            catch (Exception ex)
            {
                return;
            }
            try
            {
                version = Convert.ToInt16(ddlVersion.Text);
            }
            catch
            {

            }

            string errorCorrect = ddlErrorCorrection.Text;
            if (errorCorrect == "Low")                
                 errorCorrectionLevel = ErrorCorrectionLevel.L;
            else if (errorCorrect == "Medium")               
                errorCorrectionLevel = ErrorCorrectionLevel.M;
            else if (errorCorrect == "Quartile")              
                 errorCorrectionLevel =ErrorCorrectionLevel.Q;
            else if (errorCorrect == "High")                
            errorCorrectionLevel = ErrorCorrectionLevel.H;           

            if (txtText.Text.Trim() != String.Empty)
            {
                sb = new StringBuilder();
                sb.Append("Text: ");
                sb.Append(txtText.Text.Trim());

                property.qrCode = sb.ToString();
            } 

             string txt = property.qrCode;
            
            if (sb.Length == 0)
            {
                lblErrorGenerate.Text = "Fill Required Field First!";
                lblErrorGenerate.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblErrorGenerate.Text = "Successfully Generated!";
                lblErrorGenerate.ForeColor = System.Drawing.Color.Green;
            }

            QRCodeWriter writer = new QRCodeWriter();
            Hashtable hints = new Hashtable();

            hints.Add(com.google.zxing.EncodeHintType.ERROR_CORRECTION, errorCorrectionLevel);
            hints.Add("Version", version);
            hints.Add("Mode", mode);
            hints.Add(com.google.zxing.EncodeHintType.CHARACTER_SET, "utf-8");
            ByteMatrix byteIMGNew = writer.encode(txt, com.google.zxing.BarcodeFormat.QR_CODE, 400, 400, hints);
            sbyte[][] imgNew = byteIMGNew.Array;
            Bitmap bmp1 = new Bitmap(byteIMGNew.Width, byteIMGNew.Height);
            Graphics g1 = Graphics.FromImage(bmp1);
            g1.Clear(Color.White);
            for (int i = 0; i <= imgNew.Length - 1; i++)
            {
                for (int j = 0; j <= imgNew[i].Length - 1; j++)
                {
                    if (imgNew[j][i] == 0)
                    {
                        g1.FillRectangle(foreColor, i, j, scale, scale);
                    }
                    else
                    {
                        g1.FillRectangle(backColor, i, j, scale, scale);
                    }
                }
            }
           
            bmp1.Save(Server.MapPath("~/Images/QR_Codes/" + "img.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp1.Save(Server.MapPath("~/Images/QR_Codes/" + "img.png"), System.Drawing.Imaging.ImageFormat.Png);
            bmp1.Save(Server.MapPath("~/Images/QR_Codes/" + "img.gif"), System.Drawing.Imaging.ImageFormat.Gif);
            bmp1.Save(Server.MapPath("~/Images/QR_Codes/" + "img.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
                       
            property.path = Server.MapPath("~/Images/QR_Codes/" + "img.jpg");
            string logoPath = "";

            image = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(property.path)));

            if (fuEmbedLogo.HasFile)
            {
                String fileName = fuEmbedLogo.PostedFile.FileName;
                fuEmbedLogo.SaveAs(Server.MapPath("~/Images/Logos/" + fileName));
                logoPath = Server.MapPath("~/Images/Logos/" + fileName);
               
                System.Drawing.Image logo = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(logoPath)));

                /* logo resizing begins*/
                int maxLogoWidth = 50, left, top;
                Bitmap resizedLogo = null;

                if (logo.Width > maxLogoWidth)
                {
                    int newLogoHeight = (int)(logo.Height * ((float)maxLogoWidth / (float)logo.Width));
                    resizedLogo = new Bitmap(maxLogoWidth, newLogoHeight);
                    Graphics gr = Graphics.FromImage(resizedLogo);
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(logo, 0, 0, maxLogoWidth, newLogoHeight);

                    left = (image.Width / 2) - (resizedLogo.Width / 2);
                    top = (image.Height / 2) - (resizedLogo.Height / 2);

                    Graphics g = Graphics.FromImage(image);
                    g.DrawImage(resizedLogo, new Point(left, top));
                }
                else
                {
                    left = (image.Width / 2) - (logo.Width / 2);
                    top = (image.Height / 2) - (logo.Height / 2);

                    Graphics g = Graphics.FromImage(image);
                    g.DrawImage(logo, new Point(left, top));
                }
                /* logo resizing ends*/

                image.Save(Server.MapPath("~/Images/QR_Codes/" + "img.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                image.Save(Server.MapPath("~/Images/QR_Codes/" + "img.png"), System.Drawing.Imaging.ImageFormat.Png);
                image.Save(Server.MapPath("~/Images/QR_Codes/" + "img.gif"), System.Drawing.Imaging.ImageFormat.Gif);
                image.Save(Server.MapPath("~/Images/QR_Codes/" + "img.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
            }
            else
            {
                
            }
            
            using (Bitmap bitMap = new Bitmap(image))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    bitMap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    Byte[] bytes = new Byte[memoryStream.Length];
                    memoryStream.Position = 0;
                    memoryStream.Read(bytes, 0, (int)bytes.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    imgQRCode.ImageUrl = "data:image/png;base64," + base64String;
                    Session["isDefaultImage"] = "IsNotDefault";
                }
            }

            image.Dispose();
        }       

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["isDefaultImage"]) == "IsDefault")
            {
                lblErrorDownload.Text = "Generate QR Code First!";
                lblErrorDownload.ForeColor = System.Drawing.Color.Red;
                return;
            }

            property.path = Server.MapPath("~/Images/QR_Codes/" + "img.jpg");
            FileInfo myfile = new FileInfo(property.path);
            string pathExtensionChanged = String.Empty;

            if (myfile.Exists)
            {
                if (ddlFormat.Text == "PNG")
                {
                    pathExtensionChanged = Path.ChangeExtension(property.path, ".png");
                    myfile = new FileInfo(pathExtensionChanged);
                }

                else if (ddlFormat.Text == "GIF")
                {
                    pathExtensionChanged = Path.ChangeExtension(property.path, ".gif");
                    myfile = new FileInfo(pathExtensionChanged);
                }

                else if (ddlFormat.Text == "BMP")
                {
                    pathExtensionChanged = Path.ChangeExtension(property.path, ".bmp");
                    myfile = new FileInfo(pathExtensionChanged);
                }

                else
                {
                    myfile = new FileInfo(property.path);
                }
            }

            if (myfile.Exists)
            {               
                Response.Redirect("Download.aspx?myFile=" + myfile);                
            }
        }
    }
}