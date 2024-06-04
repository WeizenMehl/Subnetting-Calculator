using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Subnetting_Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string solution = string.Empty;
            string sub = string.Empty;

            Console.WriteLine("**||Subnetting Calculator||**");

            Console.Write("Bitte geben Sie die IpAdresse ein:");
            string ip = Console.ReadLine();

            string[] array = ip.Split('/');

            if(array.Length == 1) 
            {
                Console.Write("Bitte geben Sie jetzt auch die Subnetz maske ein:");
                sub = Console.ReadLine();

                solution = Calculation(ip, sub);
            }

            else
            {
                int suffix = Convert.ToInt32(array[1]);

                sub = toABCD((int)(0xFFFFFFFFU << (32 - suffix)));

                solution = Calculation(array[0], sub);
            }
            
            Console.WriteLine(solution);
        }

        static int to32(string x)
        {
            string[] sarray = x.Split('.');
            int[] iarray = new int[sarray.Length];
            int i = 0;

            foreach (string s in sarray)
            {
                iarray[i] = Convert.ToInt32(s);
                i++;
            }

            int result = (iarray[0] << 24) | (iarray[1] << 16) | (iarray[2] << 8) | iarray[3];

            return result;
        }

        static string toABCD(int i) 
        {
            int i1 = (i >> 24) & 255;
            int i2 = (i >> 16) & 255;
            int i3 = (i >> 8) & 255;
            int i4 = i & 255;



            return $"{i1}.{i2}.{i3}.{i4}";
        }

        static string Calculation(string ip, string sub)
        {
            string net = string.Empty;
            string brod = string.Empty;
            string firstAdr = string.Empty;
            string secondAdr = string.Empty;

            int i = to32(sub) & to32(ip);
            net = toABCD(i);

            firstAdr = toABCD(i + 1);

            i = ~to32(sub);
            i = i | to32(ip);
            brod = toABCD(i);

            secondAdr = toABCD(i - 1);

            return $"Netzwerkadresse:\t{net}\n" +
                $"Brodcastadresse:\t{brod}\n" +
                $"1.Adresse:\t\t{firstAdr}\n" +
                $"Letze Adresse:\t\t{secondAdr}\n";
        }
    }
}
