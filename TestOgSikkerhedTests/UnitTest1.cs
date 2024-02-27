using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace TestOgSikkerhedTests
{
    public class UnitTest1
    {
        [Fact]
        public void CesarEncrypt()
        {
            var strings = new Dictionary<int, string[]>()
            {
                {7, ["Aab vinder guld igen", "HHI CPUKLY NBSK PNLU"] },
                {16, ["Programmering er sjovt", "FHEWHQCCUHYDW UH IZELJ"]},
                {4, ["H5PD rejser sig, i det Per Kommer ind i lokalet", "LTH VINWIV WMK M HIX TIV OSQQIV MRH M PSOEPIX"]},
                {26, ["H5PD rejser sig, i det Per Kommer ind i lokalet", "HPD REJSER SIG I DET PER KOMMER IND I LOKALET"]},
            };

            Encrypting encrypting = new Encrypting();

            foreach(var key in strings)
            {
                string encryptedValue = encrypting.Encrypt(key.Value[0], key.Key);
                Assert.Equal(key.Value[1].ToUpper(), encryptedValue);
            }
        }

        [Fact]
        public void CesarDecrypt()
        {
            var strings = new Dictionary<int, string[]>()
            {
                {7, ["Aab vinder guld igen", "HHI CPUKLY NBSK PNLU"] },
                {16, ["Programmering er sjovt", "FHEWHQCCUHYDW UH IZELJ"]},
                {4, ["HPD REJSER SIG I DET PER KOMMER IND I LOKALET", "LTH VINWIV WMK M HIX TIV OSQQIV MRH M PSOEPIX"]},
                {26, ["HPD REJSER SIG I DET PER KOMMER IND I LOKALET", "HPD REJSER SIG I DET PER KOMMER IND I LOKALET"]},
            };

            Encrypting encrypting = new Encrypting();

            foreach (var key in strings)
            {
                string encryptedValue = encrypting.Decrypt(key.Value[1], key.Key);
                Assert.Equal(key.Value[0].ToUpper(), encryptedValue);
            }
        }

        [Fact]
        public void VigenereEncrypt()
        {
            var strings = new Dictionary<string, string[]>()
            {
                {"kevin", ["Jeg elsker kage!", "Tib mycozz xkkz!"] },
                {"rasmus", ["Jeg elsker kage!", "Aey qfkbej wuyv!"] },
                {"jacob", ["Jeg elsker kage!", "Sei smbkgf ljgg!"] },
                {"niels", ["Jeg elsker kage!", "Wmk pdfsic cnoi!"] },
            };

            Encrypting vigenere = new Encrypting();

            foreach(var key in strings)
            {
                string encryptedString = vigenere.VigenereEncipher(key.Value[0], key.Key);
                Assert.Equal(key.Value[1], encryptedString);
            }
        }

        [Fact]
        public void VigenereDecrypt()
        {
            var strings = new Dictionary<string, string[]>()
            {
                {"kevin", ["Jeg elsker kage!", "Tib mycozz xkkz!"] },
                {"rasmus", ["Jeg elsker kage!", "Aey qfkbej wuyv!"] },
                {"jacob", ["Jeg elsker kage!", "Sei smbkgf ljgg!"] },
                {"niels", ["Jeg elsker kage!", "Wmk pdfsic cnoi!"] },
            };

            Encrypting vigenere = new Encrypting();

            foreach (var key in strings)
            {
                string decryptedString = vigenere.VigenereDecipher(key.Value[1], key.Key);
                Assert.Equal(key.Value[0], decryptedString);
            }
        }
    }
}