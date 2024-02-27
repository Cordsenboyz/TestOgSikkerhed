
using System.Text.RegularExpressions;

public class Encrypting()
{
    public static void Main(string[] args)
    {
        Encrypting encrypting = new Encrypting();

        Console.WriteLine("input encrypted string:");
        string encryptedString = Console.ReadLine();
        encrypting.VigenereHackermans(encryptedString);


        /*
                string? UserString = "Do you remember, 21st night of September?\r\nLove was changing the mind of pretenders\r\nWhile chasing the clouds away\r\nOur hearts were ringing\r\nIn the key that our souls were singing\r\nAs we danced in the night\r\nRemember, how the stars stole the night away, yeah yeah yeah\r\nHey hey hey\r\nBa de ya, say do you remember?\r\nBa de ya, dancing in September\r\nBa de ya, never was a cloudy day\r\nBa duda, ba duda, ba duda, badu\r\nBa duda, badu, ba duda, badu\r\nBa duda, badu, ba duda, yeah";

                int key = 10;

                // string encryptedText = encrypting.Encrypt(UserString.ToUpper(), key);


                string encryptedText = "LQNGPG LQNGPG LQNGPG LQNGPG\r\nQJ K'O DGIIKPI QH AQW RNGCUG FQP'V VCMG OA OCP\r\nLQNGPG LQNGPG LQNGPG LQNGPG\r\nRNGCUG FQP'V VCMG JKO GXGP VJQWIJ AQW ECP\r\n \r\nAQWT DGCWVA KU DGAQPF EQORCTG\r\nYKVJ HNCOKPI NQEMU QH CWDWTP JCKT\r\nYKVJ KXQTA UMKP CPF GAGU QH GOGTCNF ITGGP\r\n \r\nAQWT UOKNG KU NKMG C DTGCVJ QH URTKPI\r\nAQWT UMKP KU UQHV NKMG UWOOGT TCKP\r\nCPF K ECP PQV EQORGVG YKVJ AQW LQNGPG\r\n \r\nCPF K ECP GCUKNA WPFGTUVCPF\r\nJQY AQW EQWNF GCUKNA VCMG OA OCP\r\nDWV AQW FQP'V MPQY YJCV JG OGCPU VQ OG LQNGPG\r\n \r\nJG VCNMU CDQWV AQW KP JKU UNGGR\r\nVJGTG'U PQVJKPI K ECP FQ VQ MGGR\r\nHTQO ETAKPI YJGP JG ECNNU AQWT PCOG LQNGPG LQNGPG";
                Console.WriteLine(encryptedText);

                Console.WriteLine("\n");
                encrypting.HackerMans(encryptedText);

                Console.ReadKey();
        */

        /*
                string keyInput = Console.ReadLine();
                string textInput = Console.ReadLine();
                string cipherText = encrypting.VigenereEncipher(textInput, keyInput);
                string decipher = encrypting.VigenereDecipher(cipherText, keyInput);
                Console.WriteLine(cipherText);
                Console.WriteLine("\n");
                Console.WriteLine("**************************************");
                Console.WriteLine("\n");
                Console.WriteLine(decipher);
                Console.ReadKey();
        */
    }
    public static char Cipher(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {
            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)(((ch + key - d) % 26) + d);
    }

    public string Encrypt(string input, int key)
    {
        if (input is null) return "Input cannot be null";
        string output = "";
        foreach (char ch in input) output += Cipher(ch, key);

        output = Regex.Replace(output, "[^a-zA-Z\r\n ]+", "");
        return output.ToUpper();
    }
    public string Decrypt(string input, int key)
    {
        return Encrypt(input, 26 - key);
    }

    public string HackerMans(string input)
    {
        var freq_norm = new Dictionary<string, double>()
        {
            { "A", 0.64297 },
            { "B", 0.11746 },
            { "C", 0.21902 },
            { "D", 0.33483 },
            { "E", 1.00000 },
            { "F", 0.17541 },
            { "G", 0.15864 },
            { "H", 0.47977 },
            { "I", 0.54842 },
            { "J", 0.01205 },
            { "K", 0.06078 },
            { "L", 0.31688 },
            { "M", 0.18942 },
            { "N", 0.53133 },
            { "O", 0.59101 },
            { "P", 0.15187 },
            { "Q", 0.00748 },
            { "R", 0.47134 },
            { "S", 0.49811 },
            { "T", 0.71296 },
            { "U", 0.21713 },
            { "V", 0.07700 },
            { "W", 0.18580 },
            { "X", 0.01181 },
            { "Y", 0.15541 },
            { "Z", 0.00583 }
        };

        var sumDict = new Dictionary<int, double>();

        for(int i = 0; i <= 26; i++)
        {
            double sum = 0.0;

            string decryptedString = Encrypt(input, 26 - i);

            var Words = decryptedString.Split(" ");

            foreach(var word in Words)
            {
                char[] letters = word.ToCharArray();

                foreach(char letter in letters)
                {
                    if (char.IsLetter(letter))
                    {
                        sum += freq_norm[letter.ToString()];
                    }
                }
            }
            {  sumDict.Add(i, sum); }
        }

        var max = sumDict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        string actualDecryptedString = Decrypt(input, max);

        Console.WriteLine(actualDecryptedString);
        Console.WriteLine($"Key: {max}");

        return "";
    }

    private static int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }

    public string VigenereCipher(string input, string key, bool encipher)
    {
        for (int i = 0; i < key.Length; ++i)
            if (!char.IsLetter(key[i]))
                return null;

        string output = "";
        int notCharCount = 0;

        for (int i = 0; i < input.Length; ++i)
        {
            if (!char.IsLetter(input[i]))
            {
                output += input[i];
                ++notCharCount;
                continue;
            }

            bool cIsUpper = char.IsUpper(input[i]);
            char offset = cIsUpper ? 'A' : 'a';
            int keyIndex = (i - notCharCount) % key.Length;
            int k = (cIsUpper ? char.ToUpper(key[keyIndex]) : char.ToLower(key[keyIndex])) - offset;
            k = encipher ? k : -k;
            char ch = (char)(Mod(input[i] + k - offset, 26) + offset);
            output += ch;
        }

        return output;
    }

    public string VigenereEncipher(string input, string key)
    {
        return VigenereCipher(input, key, true);
    }

    public string VigenereDecipher(string input, string key)
    {
        return VigenereCipher(input, key, false);
    }

    public string VigenereHackermans(string input)
    {
        var freq_norm = new Dictionary<string, double>()
        {
            { "A", 0.64297 },
            { "B", 0.11746 },
            { "C", 0.21902 },
            { "D", 0.33483 },
            { "E", 1.00000 },
            { "F", 0.17541 },
            { "G", 0.15864 },
            { "H", 0.47977 },
            { "I", 0.54842 },
            { "J", 0.01205 },
            { "K", 0.06078 },
            { "L", 0.31688 },
            { "M", 0.18942 },
            { "N", 0.53133 },
            { "O", 0.59101 },
            { "P", 0.15187 },
            { "Q", 0.00748 },
            { "R", 0.47134 },
            { "S", 0.49811 },
            { "T", 0.71296 },
            { "U", 0.21713 },
            { "V", 0.07700 },
            { "W", 0.18580 },
            { "X", 0.01181 },
            { "Y", 0.15541 },
            { "Z", 0.00583 }
        };

        IEnumerable<string> letters = new[]{
                "A","B","C","D","E","F",
                "G","H","I","J","K","L",
                "M","N","O","P","Q","R","S",
                "T","U","V","W","X","Y","Z"};

        var result = Enumerable.Range(0, 3)
                        .Aggregate(letters, (curr, i) => curr.SelectMany(s => letters, (s, c) => s + c));

        var sumDict = new Dictionary<string, double>();

        foreach (var val in result)
        {
            double sum = 0.0;
            var decryptedString = VigenereCipher(input, val, false);
            var Words = decryptedString.Split(" ");

            foreach (var word in Words)
            {
                char[] lettersInWord = word.ToCharArray();

                foreach (char letter in lettersInWord)
                {
                    if (char.IsLetter(letter))
                    {
                        sum += freq_norm[letter.ToString().ToUpper()];
                    }
                }
            }
            { sumDict.Add(val, sum); }
        }
        var max = sumDict.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        var sumValue = sumDict.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;

        var actualDecryptedString = VigenereCipher(input, max, false);
        Console.WriteLine($"String: {actualDecryptedString}");
        Console.WriteLine($"Key: {max}");
        Console.WriteLine($"Sum: {sumValue}");

        return "";
    }
}



