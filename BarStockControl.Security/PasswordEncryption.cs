using System.Text;

namespace BarStockControl.Security
{
    public static class PasswordEncryption
    {
        
        public static string EncryptPassword(string plain)
        {
            if (string.IsNullOrEmpty(plain))
                return string.Empty;

            byte[] bytes = Encoding.Unicode.GetBytes(plain);
            return Convert.ToBase64String(bytes);
        }

        public static string DecryptPassword(string encrypted)
        {
            if (string.IsNullOrEmpty(encrypted))
                return string.Empty;

            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(encrypted);
            }
            catch
            {
                throw new ArgumentException("El valor proporcionado no es una contraseña encriptada válida.");
            }

            return Encoding.Unicode.GetString(bytes);
        }
    }
}
