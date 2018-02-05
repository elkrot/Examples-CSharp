using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel;

namespace ValidateCertificate
{
    class Program
    {
        static void Main(string[] args)
        {
            var cert = X509.LocalMachine.My.SubjectDistinguishedName.Find("CN=web.local", false).First();

            //ValidateUsingChain(cert);
            ValidateUsingValidator(cert);
        }

        private static void ValidateUsingChain(X509Certificate2 cert)
        {
            var chain = new X509Chain();
            var policy = new X509ChainPolicy
            {
                RevocationFlag = X509RevocationFlag.EntireChain,
                RevocationMode = X509RevocationMode.Online
            };

            chain.ChainPolicy = policy;

            if (!chain.Build(cert))
            {
                foreach (var element in chain.ChainElements)
                {
                    foreach (var status in element.ChainElementStatus)
                    {
                        Console.WriteLine(status.StatusInformation);
                    }
                }
            }
            else
            {
                Console.WriteLine("cert is trusted");
            }
        }

        private static void ValidateUsingValidator(X509Certificate2 cert)
        {
            var validator = X509CertificateValidator.ChainTrust;
            validator.Validate(cert);
        }
    }
}