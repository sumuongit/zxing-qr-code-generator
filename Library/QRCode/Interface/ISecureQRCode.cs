using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureQRCode.Interface
{
    public interface ISecureQRCode
    {
        string path { get; set; }
        string qrCode { get; set; }
    }
}
