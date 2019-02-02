/*
     Copyright 2014 Sedat Kapanoglu

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Globalization;

namespace TurkishIdGen
{
    internal class Program
    {
        private const string usage =
            "Generates random and valid Turkish Republic citizen ID Number) <ssg@sourtimes.org>\r\n" +
            "\r\n" +
            "Usage: TurkishIdGen count";

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(usage);
                Environment.Exit(1);
            }
            int count = int.Parse(args[0], CultureInfo.InvariantCulture);
            var rnd = new Random();
            for (int n = 0; n < count; n++)
            {
                string no = generateRandomId(rnd);
                Console.WriteLine(no);
            }
        }

        private static string generateRandomId(Random rnd)
        {
            int value = rnd.Next(100_000_000, 1_000_000_000);
            return generateIdFromValue(value);
        }

        private static string generateIdFromValue(int x)
        {
            int d1 = x / 100_000_000;
            int d2 = (x / 10_000_000) % 10;
            int d3 = (x / 1_000_000) % 10;
            int d4 = (x / 100_000) % 10;
            int d5 = (x / 10_000) % 10;
            int d6 = (x / 1000) % 10;
            int d7 = (x / 100) % 10;
            int d8 = (x / 10) % 10;
            int d9 = x % 10;
            int oddSum = d1 + d3 + d5 + d7 + d9;
            int evenSum = d2 + d4 + d6 + d8;
            int firstChecksum = ((oddSum * 7) - evenSum) % 10;
            if (firstChecksum < 0)
            {
                firstChecksum += 10;
            }
            int secondChecksum = (oddSum + evenSum + firstChecksum) % 10;
            return $"{x}{firstChecksum}{secondChecksum}";
        }
    }
}