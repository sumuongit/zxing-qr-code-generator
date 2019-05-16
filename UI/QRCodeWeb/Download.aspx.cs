using System;
using System.IO;

namespace SecureQRCodeWeb
{
    public partial class Download : System.Web.UI.Page
    {
        FileInfo file = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            file = new FileInfo(Request.QueryString["myFile"]);               
        }    

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.TransmitFile(file.FullName);
            Response.End();
        }
    }
}