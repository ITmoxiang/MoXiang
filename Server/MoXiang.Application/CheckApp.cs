using MoXiang.Infrastructure;
using MoXiang.Infrastructure.Cipher;
using MoXiang.Infrastructure.Thirdparty;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MoXiang.Application
{
    public class CheckApp
    {
        private ThirdpartyHelper _thirdpartyHelper;
        public CheckApp( ThirdpartyHelper thirdpartyHelper)
        {
            _thirdpartyHelper = thirdpartyHelper;
        }

        public async Task<string> Cipher() 
        {
            //var key = Encoding.UTF8.GetBytes("NEWARE");  //key转base64
            //var iv = Strings.ToByteArray("1234567895589784");      //模式为ECB时不支持初始化向量IV
            //var cipherStr =Convert.ToBase64String(Encryption.Encrypt(Strings.ToUtf8ByteArray("xinwei123"),key, iv, Algorithms.AES_CBC_PKCS7Padding));
            return "1";
        }
    }
}
