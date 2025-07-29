
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EduTrackServer.CapaBase
{
    public static class authorizeServices
    {


        public static byte[] IV = Encoding.UTF8.GetBytes("_@3x565_9012@@_6"); // 16 bytes for AES-128
        public static byte[] Key = Encoding.UTF8.GetBytes("_@3x565_9012@@_6"); // 16 bytes for AES-128

        public static string GetUserName(IHttpContextAccessor _context)
        {
            var getUser = _context.HttpContext?.User.FindFirst(ClaimTypes.Name);
            return getUser?.Value ?? "";
        }

        public static int GetRoleUser(IHttpContextAccessor _context)
        {
            var getRol = _context.HttpContext?.User.FindFirst(ClaimTypes.Role);
            return int.Parse(getRol?.Value ?? "-1");
        }

        public static int GetIdentityFk(IHttpContextAccessor _context)
        {
            var getRol = _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(getRol?.Value ?? "-1");
        }

        public static string GetIdHub(IHttpContextAccessor _context)
        {
            var idHub = _context.HttpContext?.User?.FindFirst("miHub");
            return idHub.Value.ToString() ?? "-1";
        }

        private static string Encrypt(string plainText)
        {

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

    }
}
