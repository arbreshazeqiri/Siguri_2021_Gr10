using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Base32C
{

    public static class Program

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
         public static void Dekodimi(string s)
         {
             List<string> textb = Split(s, 1).ToList();
            List<string> dec = new List<string>();
            foreach (var chunk in textb)
            {
                if (chunk == "=")
                {
                    dec.Add("=");
                }
                else
                {
                    dec.Add(Array.IndexOf(_digits, char.Parse(chunk)).ToString());
                }
            }

            var moddec = dec.Select(ToBin).ToList();

            for(int i=0;i<moddec.Count;i++){
                Console.WriteLine(moddec[i]);
            }
         }


        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Jepni fjalen per te enkoduar: ");
                string input = Console.ReadLine();
                string teksti = input.ToUpper();
                string encoded = Enkodimi(teksti);
                Console.WriteLine("Teksti i enkoduar: " + encoded);
                Dekodimi(encoded);

            }
        }
    }
}