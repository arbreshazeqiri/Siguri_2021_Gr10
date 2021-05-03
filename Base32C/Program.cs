using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Base32C
{

    public static class Program

    {
        private static readonly char[] _digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();
        public static void Enkodimi(string s)
        {
            var textb = s.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')).ToList();


            while (textb.Count % 5 != 0)
            {
                textb.Add("xxxxxxxx");
            }

            string sc = String.Join("", textb.ToArray());

            var chunks = Split(sc, 5).ToList();

            List<string> modchunks = chunks.Select(FindPad).ToList();

            for (int i = 0; i < modchunks.Count; i++)
            {
                Console.WriteLine(modchunks[i]);
            }

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

        // public static void Dekodimi(string s)
        // {

        // }


        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Jepni fjalen per te enkoduar: ");
                string input = Console.ReadLine();
                string teksti = input.ToUpper();
                Enkodimi(teksti);
            }
        }
    }
}