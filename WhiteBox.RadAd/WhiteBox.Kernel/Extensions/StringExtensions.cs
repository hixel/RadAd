namespace WhiteBox.Kernel.Extensions
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        public static string GetMd5(this string value)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var originalBytes = new UTF8Encoding().GetBytes(value);
            var encodedBytes = md5.ComputeHash(originalBytes);

            // Преобразуем зашифрованные байты обратно в строку
            return BitConverter.ToString(encodedBytes).Replace("-", String.Empty);
        }

        public static bool IsEqual(this string valueA, string valueB)
        {
            return String.Compare(valueA, valueB, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}