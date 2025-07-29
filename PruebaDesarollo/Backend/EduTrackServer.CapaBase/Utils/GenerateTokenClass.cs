using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaBase.Utils
{
    public static class GenerateClassActive
    {

        private static IConfiguration _configuration;
        public static string KEY_JWT = "";
        public static string KEY_AES = "";
        public static string KEY_IV = "";

        public static string GenerateToken(string roleCode = null, string usernameFk = null,string? identityFk = null)
        {

            string typeName = !string.IsNullOrEmpty(usernameFk) ? ClaimTypes.Name : "";
            string typeRol = !string.IsNullOrEmpty(roleCode) ? ClaimTypes.Role : "";
            string typeIdentityFk = !string.IsNullOrEmpty(identityFk) ? ClaimTypes.NameIdentifier : "";
 

            var claims = new[]
            {
                new Claim(typeName,usernameFk ?? ""),
                new Claim(typeRol,roleCode ?? ""),
                new Claim(typeIdentityFk,identityFk ?? ""),
            };


            var strlKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY_JWT));
            var strlPswd = new SigningCredentials(strlKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: strlPswd
            );

            string rstToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rstToken;
        }

        public static string ConvertToSha256(string sendTo)
        {
            // Calcular el hash SHA-256 de los datos ordenados
            byte[] bytes = Encoding.UTF8.GetBytes(sendTo);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(bytes);
                // Convertir el hash a una cadena hexadecimal para obtener el resultado cifrado
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashString;
            }
        }

        public static string Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(KEY_AES);
            aes.IV = Encoding.UTF8.GetBytes(KEY_IV);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new();
            using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
            using (StreamWriter sw = new(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(KEY_AES);
            aes.IV = Encoding.UTF8.GetBytes(KEY_IV);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] buffer = Convert.FromBase64String(cipherText);

            using MemoryStream ms = new(buffer);
            using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new(cs);

            return sr.ReadToEnd();
        }

    }
}
