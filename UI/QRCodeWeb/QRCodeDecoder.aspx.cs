using System;
using System.Drawing;
using System.IO;
using SecureQRCode;
//ZXING LIBRARY REFERENCE
using com.google.zxing.qrcode;

namespace SecureQRCodeWeb
{
    public partial class QRCodeDecoder : System.Web.UI.Page
    {
        System.Drawing.Image image = null;
        Property property = new Property();
        QRCodeGenerator pro = new QRCodeGenerator();

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
            }
        }

        string qrCodePath = "";
        protected void btnUploadQRCode_Click(object sender, EventArgs e)
        {
            if (fuUploadQRCode.HasFile)
            {
                String fileName = fuUploadQRCode.PostedFile.FileName;
                qrCodePath = Server.MapPath("~/Images/QR_Codes/" + fileName);

                if(!File.Exists(qrCodePath))
                {
                    Bitmap bmp = new Bitmap(fuUploadQRCode.FileContent);
                    bmp.Save(Server.MapPath("~/Images/QR_Codes/" + "img_ext.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                    qrCodePath = Server.MapPath("~/Images/QR_Codes/" + "img_ext.jpg");
                }                

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
                        Session["isDefaultImage"] = "IsNotDefault";
                    }
                }
            }
            else
            {
                lblErrorGenerate.Text = "Fill Required Field First!";
                lblErrorGenerate.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void btnDecode_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Session["isDefaultImage"]) == "IsDefault")
            {
                lblErrorDecode.Text = "Upload QR Code First!";
                lblErrorDecode.ForeColor = System.Drawing.Color.Red;
                return;
            }
            property.path = Server.MapPath("~/Images/QR_Codes/" + "img.bmp");
            System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(property.path)));
            Bitmap bitMap = new Bitmap(image);

            try
            {
                com.google.zxing.LuminanceSource source = new RGBLuminanceSource(bitMap, bitMap.Width, bitMap.Height);
                var binarizer = new com.google.zxing.common.HybridBinarizer(source);
                var binBitmap = new com.google.zxing.BinaryBitmap(binarizer);
                QRCodeReader qrCodeReader = new QRCodeReader();
                com.google.zxing.Result str = qrCodeReader.decode(binBitmap);

                txtDecodedOriginalInfo.Text = str.ToString();

                lblErrorDecode.Text = "Successfully Decoded!";
                lblErrorDecode.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {

            }
        }
    }
}