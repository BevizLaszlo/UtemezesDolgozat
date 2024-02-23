using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utemezes
{
    internal class Tabor
    {
        public DateTime Kezdet {  get; set; }
        public DateTime Vege {  get; set; }
        public List<char> Tanulok {  get; set; }
        public string Tema {  get; set; }

        public Tabor(string line)
        {
            string[] data = line.Split('\t');

            Kezdet = new DateTime(2023, int.Parse(data[0]), int.Parse(data[1]));
            Vege = new DateTime(2023, int.Parse(data[2]), int.Parse(data[3]));
            Tanulok = new List<char>();
            for (int i = 0; i < data[4].Length; i++)
                Tanulok.Add(data[4][i]);
            Tema = data[5];
        }

        public bool VanTanulo(char tanulo)
        {
            foreach (char t in Tanulok)
            {
                if (t == tanulo) return true;
            }
            return false;
        }
    }
}
