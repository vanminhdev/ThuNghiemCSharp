using Google.Authenticator;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TotpDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key = "123abc";

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", key, false, 3);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            // verify
            TwoFactorAuthenticator tfa2 = new TwoFactorAuthenticator();
            Console.Write("Nhap otp: ");
            bool result = tfa2.ValidateTwoFactorPIN(key, Console.ReadLine());
            Console.Write(result);
        }
    }
}
