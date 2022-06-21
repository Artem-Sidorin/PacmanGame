using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Pc_Man_Game_MOO_ICT
{
    internal class Save
    {
        public Save()
        {
        }

        public void WriteScore(int score)
        {
            try
            {
                StreamWriter sw = new StreamWriter("Test.txt", true);
                sw.WriteLine(score);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        public List<int> ReadScore()
        {
            String line;
            List<int> scoreRecord = new List<int>();
            try
            {
                StreamReader sr = new StreamReader("Test.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    scoreRecord.Add(Int32.Parse(line));
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            
            scoreRecord.Sort();
            scoreRecord.Reverse();
            return scoreRecord;
        }
    }
}
