using SecureQRCode.Interface;

namespace SecureQRCode
{
    public class Property : ISecureQRCode
    {
        private string p;
        // path Property implementation: 
        public string path
        {
            get
            {
                return p;
            }

            set
            {
                p = value;
            }
        }

        private string qrc;
        // barCode Property implementation: 
        public string qrCode
        {
            get
            {
                return qrc;
            }

            set
            {
                qrc = value;
            }
        }
    }
}
