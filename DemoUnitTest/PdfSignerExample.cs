using iText.Commons.Bouncycastle.Cert;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;

namespace DemoUnitTest
{
    public class PdfSignerExample
    {
        public static X509Certificate2 GenerateCertificate()
        {
            //var keyGenParams = new KeyGenerationParameters(new SecureRandom(), 2048);
            //var keyPairGen = new RsaKeyPairGenerator();
            //keyPairGen.Init(keyGenParams);
            //var keyPair = keyPairGen.GenerateKeyPair();

            //var certGen = new Org.BouncyCastle.X509.X509V3CertificateGenerator();
            //var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(long.MaxValue), new SecureRandom());
            //certGen.SetSerialNumber(serialNumber);
            //certGen.SetIssuerDN(new Org.BouncyCastle.Asn1.X509.X509Name("CN=Test CA"));
            //certGen.SetNotBefore(DateTime.UtcNow.Date);
            //certGen.SetNotAfter(DateTime.UtcNow.Date.AddYears(2));
            //certGen.SetSubjectDN(new Org.BouncyCastle.Asn1.X509.X509Name("CN=Test CA"));
            //certGen.SetPublicKey(keyPair.Public);
            //certGen.SetSignatureAlgorithm("SHA256WithRSA");

            //var certificate = certGen.Generate(keyPair.Private);
            //var certPem = new StringWriter();
            //var pemWriter = new PemWriter(certPem);
            //pemWriter.WriteObject(certificate);
            //pemWriter.Writer.Flush();

            //var keyPem = new StringWriter();
            //pemWriter = new PemWriter(keyPem);
            //pemWriter.WriteObject(keyPair.Private);
            //pemWriter.Writer.Flush();

            //var x509 = new X509Certificate2(DotNetUtilities.ToX509Certificate(certificate));
            //x509.PrivateKey = DotNetUtilities.ToRSA(keyPair.Private as Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters);
            //return x509;
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="cert"></param>
        public static void SignPdf(string src, string dest, X509Certificate cert)
        {
            PdfReader reader = new PdfReader(src);
            PdfSigner signer = new PdfSigner(reader, new FileStream(dest, FileMode.Create), new StampingProperties());

            // Create the signature appearance
            PdfSignatureAppearance appearance = signer.GetSignatureAppearance()
                .SetReason("Test Signing")
                .SetLocation("Test Location")
                .SetPageRect(new iText.Kernel.Geom.Rectangle(36, 648, 200, 100))
                .SetPageNumber(1);
            signer.SetFieldName("Signature1");
            var bouncyCastleCert = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(cert);
            //IExternalSignature pks = new AsymmetricAlgorithmSignature(null, DigestAlgorithms.SHA256);
            //signer.SignDetached(pks, new X509Certificate2[] { }, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
        }

        public static void Test()
        {
            //X509Certificate2 cert = GenerateCertificate();
            //string src = "input.pdf"; // Input PDF file path
            //string dest = "signed.pdf"; // Output signed PDF file path
            //SignPdf(src, dest, cert);
        }
    }
}
