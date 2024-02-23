using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utemezes
{
    internal class Program
    {
        static List<Tabor> taborok = new List<Tabor>();
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader(path: @"..\..\src\taborok.txt", encoding: Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                    taborok.Add(new Tabor(sr.ReadLine()));
            }



            Console.WriteLine("2. feladat");
            Console.WriteLine($"Az adatsorok száma: {taborok.Count} ");
            Console.WriteLine($"Az először rögzített tábor témája: {taborok.First().Tema} ");
            Console.WriteLine($"Az utoljára rögzített tábor témája: {taborok.Last().Tema} ");



            Console.WriteLine("3. feladat");
            var zeneiTabor = taborok.Where(t => t.Tema == "zenei");
            if (zeneiTabor.Count() == 0)
            {
                Console.WriteLine("Nem volt zenei tábor.");
            }
            else
            {
                foreach (Tabor tabor in zeneiTabor)
                    Console.WriteLine($"Zenei tábor kezdődik {tabor.Kezdet.Month}. hó {tabor.Kezdet.Day}. napján.");
            }


            Console.WriteLine("4. feladat");
            Console.WriteLine("Legnépszerűbben");

            Tabor legnepszerubb = taborok.OrderBy(tabor => tabor.Tanulok.Count).Last();
            var legnepszerubbek = taborok.Where(t => t.Tanulok.Count == legnepszerubb.Tanulok.Count);

            foreach (Tabor leg in legnepszerubbek)
                Console.WriteLine($"{leg.Kezdet.Month} {leg.Kezdet.Day} {leg.Tema}");




            Console.WriteLine("6. feladat");
            Console.Write("hó: ");
            int ho = int.Parse(Console.ReadLine());
            Console.Write("nap: ");
            int nap = int.Parse(Console.ReadLine());
            Console.WriteLine($"Ekkor éppen {taborok.Count(t => sorszam(ho, nap) > sorszam(t.Kezdet.Month, t.Kezdet.Day) && sorszam(ho, nap) < sorszam(t.Vege.Month, t.Vege.Day))} tábor tart.");




            Console.WriteLine("7. feladat");
            Console.Write("Adja meg egy tanuló betűjelét: ");
            char tanulo = char.Parse(Console.ReadLine().ToUpper());
            var tanuloTaborok = taborok.Where(t => t.Tanulok.Contains(tanulo)).OrderBy(t => t.Kezdet);

            using (StreamWriter sw = new StreamWriter(path: @"..\..\src\egytanulo.txt", append: false, encoding: Encoding.UTF8))
            {
                foreach (var t in tanuloTaborok)
                    sw.WriteLine($"{t.Kezdet.Month}.{t.Kezdet.Day}-{t.Vege.Month}.{t.Vege.Day} {t.Tema}");
            }

            bool tudTaborbanLenni = true;
            foreach (var t1 in tanuloTaborok)
            {
                foreach(var t2 in tanuloTaborok)
                {
                    if (t1 != t2)
                    {
                        if (
                            sorszam(t1.Kezdet.Month, t1.Kezdet.Day) < sorszam(t2.Kezdet.Month, t2.Kezdet.Day) &&
                            sorszam(t1.Vege.Month, t1.Vege.Day) > sorszam(t2.Kezdet.Month, t2.Kezdet.Day)
                            )
                        {
                            tudTaborbanLenni = false;
                            break;
                        }
                    }
                    if (!tudTaborbanLenni) break;
                }
            }

            if (tudTaborbanLenni) Console.WriteLine("Elmehet mindegyik táborba");
            else Console.WriteLine("Nem mehet el mindegyik táborba.");

            Console.ReadKey();
        }

        private static int sorszam(int honap, int nap)
        {
            DateTime kezdet = new DateTime(2023, 6, 16);
            DateTime jelenlegi = new DateTime(2023, honap, nap);
            TimeSpan kulonbseg = jelenlegi - kezdet;

            return kulonbseg.Days;
        }
    }
}
