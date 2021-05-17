using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace Base32C
{
    static class Program
    {
       private static readonly char[] _digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

        public static string Enkodimi(string s)
        {
            var textb = s.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')).ToList();


            while (textb.Count % 5 != 0)
            {
                textb.Add("xxxxxxxx");
            }

            string sc = String.Join("", textb.ToArray());

            var chunks = Split(sc, 5).ToList();

            List<string> modchunks = chunks.Select(FindPad).ToList();
            modchunks = modchunks.Select(ToDec).ToList();
            string res = "";
            foreach (var chunk in modchunks)
            {
                if (chunk == "=")
                {
                    res += chunk;
                }
                else
                {
                    res += _digits[Int32.Parse(chunk)];
                }

            }
            return res;

        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
        }


        static string FindPad(string bin)
        {
            int nrcount = 0;
            int xcount = 0;
            for (int i = 0; i < bin.Length; i++)
            {
                if (bin[i] == '1' || bin[i] == '0')
                {
                    nrcount++;
                }
                if (bin[i] == 'x')
                {
                    xcount++;
                }
                if (nrcount >= 1 && xcount >= 1)
                {
                    return bin.Replace('x', '0');
                }
            }
            return bin;
        }

        static string FindPad2(string bin)
        {
            int nrcount = 0;
            int xcount = 0;
            for (int i = 0; i < bin.Length; i++)
            {
                if (bin[i] == '1' || bin[i] == '0')
                {
                    nrcount++;
                }
                if (bin[i] == 'x')
                {
                    xcount++;
                }
                if (nrcount >= 1 && xcount >= 1)
                {
                    return bin.Replace('0', 'x');
                }
            }
            return bin;
        }

        static string ToDec(string bin)
        {
            if (bin.Contains("x"))
            {
                return "=";
            }
            else
            {
                return Convert.ToInt32(bin, 2).ToString();
            }
        }
        static string ToBin(string str)
        {
            if (str.Contains("="))
            {
                return "xxxxx";
            }
            else
            {
                return Convert.ToString(Convert.ToInt32(str, 10), 2).PadLeft(5, '0').ToString();
            }
        }

        static List<string> RemovePadding(List<string> l)
        {
            for (int i = 0; i < l.Count; i++)
            {


                if (l[i] == "xxxxxxxx")
                {
                    l.Remove(l[i]);
                    i--;
                }
            }
            return l;
        }

        public static string Dekodimi(string s)
        {
            if(s.Length % 8 != 0){
                Console.WriteLine("Sigurohuni qe argumenti te jete ciphertext valid!");
                System.Environment.Exit(1);
            }

            List<string> textb = Split(s, 1).ToList();

            for(int i=0; i<textb.Count; i++){
            if(char.Parse(textb[i])!=61 && ((char.Parse(textb[i]) < 64 && char.Parse(textb[i]) > 57) | char.Parse(textb[i]) > 91 | char.Parse(textb[i])<50)){
               Console.WriteLine("Sigurohuni qe argumenti te jete ciphertext valid!");
                System.Environment.Exit(1);
              }
            if(char.Parse(textb[i])!=61 && (char.Parse(textb[i]) < 64 | char.Parse(textb[i]) > 91)){
               Console.WriteLine("Sigurohuni qe argumenti te jete ciphertext valid!");
                System.Environment.Exit(1);
              }
            }

            List<string> dec = new List<string>();

            foreach (var chunk in textb)
            {
                if (chunk == "=")
                {
                    dec.Add("=");
                }
                else
                {
                    int i = Array.IndexOf(_digits, char.Parse(chunk));
                    if(i == -1){
                     Console.WriteLine("Sigurohuni qe argumenti te jete ciphertext valid!");
                     System.Environment.Exit(1);
                    }
                    else{
                    dec.Add(i.ToString());
                    }
                }
            }
            var moddec = dec.Select(ToBin).ToList();
            string komplet = String.Join("", moddec.ToArray());
            moddec = Split(komplet, 8).ToList();
            List<string> moddec2 = moddec.Select(FindPad2).ToList();
            moddec2 = RemovePadding(moddec2);
            List<char> final = moddec2.Select(c => (char)Convert.ToInt32(c, 2)).ToList();
            string res = String.Join("", final.ToArray());
            return res;
        }


        
        static void Main(string[] args){
         if(args.Length == 2){
                if(args[0].ToUpper()=="ENCODE"){
                    Console.WriteLine(Enkodimi(args[1].ToUpper()));

                }
                else if(args[0].ToUpper()=="DECODE"){
                    try {
                      Console.WriteLine(Dekodimi(args[1].ToUpper()));
                    }
                    catch{
                    Console.WriteLine("Gabim ne perkthim. Sigurohuni qe ciphertexti juaj te jete valid.");
                    }
                }
                else{
                Console.WriteLine("Argument accepted: encode/decode");
                }
            }
            else{
                Console.WriteLine("Gabim ne perkthim. Sigurohuni qe ciphertexti juaj te jete valid.\n args[0] = encode/decode, args[1] = plaintext/ciphertext");
            }

        }
    }
}
