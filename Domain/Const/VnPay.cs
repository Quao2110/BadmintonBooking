using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Const
{
    public static class VnPay
    {
        public static string TmnCode = "UWGK65VP";
        public static string HashSecret = "H07GLVJXOTQ2JOZDD7M4K8Y9SUQ37R6L";
        public static string BaseUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public static string ReturnUrl = "myapp://payment-result";
        public static string Version = "2.1.0";
        public static string Command = "pay";
        public static string Locale = "vn";
        public static string CurrencyCode = "VND";
        public static string DateFormat = "yyyyMMddHHmmss";

        public static string ComputeHash(string data)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(HashSecret)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string BuildPaymentUrl(string orderId, decimal amount, string? returnUrl = null, string? ipAddress = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "vnp_Version", Version },
                { "vnp_Command", Command },
                { "vnp_TmnCode", TmnCode },
                { "vnp_Amount", ((int)(amount * 100)).ToString() },
                { "vnp_CurrCode", CurrencyCode },
                { "vnp_TxnRef", orderId },
                { "vnp_ReturnUrl", string.IsNullOrWhiteSpace(returnUrl) ? ReturnUrl : returnUrl },
                { "vnp_Locale", Locale },
                { "vnp_OrderInfo", $"Thanh toan don hang {orderId}" },
                { "vnp_OrderType", "other" },
                { "vnp_CreateDate", DateTime.UtcNow.ToString(DateFormat)},
                { "vnp_ExpireDate", DateTime.UtcNow.AddMinutes(15).ToString(DateFormat)}
            };

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                queryParams.Add("vnp_IpAddr", ipAddress);
            }

            var sortedParams = queryParams.OrderBy(kvp => kvp.Key);
            var queryString = string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            var hashData = string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var secureHash = ComputeHash(hashData);
            return $"{BaseUrl}?{queryString}&vnp_SecureHash={secureHash}";
        }

        public static bool VerifySignature(IReadOnlyDictionary<string, string> queryParams)
        {
            if (!queryParams.TryGetValue("vnp_SecureHash", out var secureHash) || string.IsNullOrWhiteSpace(secureHash))
            {
                return false;
            }

            var signData = BuildSignData(queryParams);
            var calculatedHash = ComputeHash(signData);

            return string.Equals(calculatedHash, secureHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string BuildSignData(IReadOnlyDictionary<string, string> queryParams)
        {
            var sortedParams = queryParams
                .Where(kvp => kvp.Key.StartsWith("vnp_", StringComparison.OrdinalIgnoreCase)
                              && !string.IsNullOrWhiteSpace(kvp.Value)
                              && !string.Equals(kvp.Key, "vnp_SecureHash", StringComparison.OrdinalIgnoreCase)
                              && !string.Equals(kvp.Key, "vnp_SecureHashType", StringComparison.OrdinalIgnoreCase))
                .OrderBy(kvp => kvp.Key, StringComparer.Ordinal);

            return string.Join("&", sortedParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
    }
}
