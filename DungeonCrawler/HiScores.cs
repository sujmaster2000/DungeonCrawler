using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DungeonCrawler
{
    public class HiScores
    {
        public void AddHiscore(int score, ref int position)
        {
            int[] hiScores = new int[10];

            bool isShifting = false;

            int prevValue = 0;
            int currValue = 0;

            for (int i = 0; i < 10; i++ )
            {
                hiScores[i] = 0;
            }
            for (int i = 0; i < File.ReadAllLines("HiScores.txt").Length; i++)
            {
                hiScores[i] = Convert.ToInt32(File.ReadLines("HiScores.txt").Skip(i).Take(1).First());
            }

            for (int i = 0; i < 10; i++)
            {
                if (!isShifting)
                {
                    if (score > hiScores[i])
                    {
                        isShifting = true;

                        currValue = hiScores[i];
                        hiScores[i] = score;

                        position = i + 1;
                    }
                }
                else
                {
                    prevValue = currValue;
                    currValue = hiScores[i];
                    hiScores[i] = prevValue;
                }
            }

            string toWrite = "";

            for (int i = 0; i < 10; i++)
            {
                toWrite += hiScores[i].ToString() + System.Environment.NewLine;
            }

            File.WriteAllText("HiScores.txt", toWrite);
        }

        public int[] GetHiScores()
        {
            int[] toReturn = new int[10];

            for (int i = 0; i < File.ReadAllLines("HiScores.txt").Length; i++)
            {
                toReturn[i] = Convert.ToInt32(File.ReadLines("HiScores.txt").Skip(i).Take(1).First());
            }

            return toReturn;
        }
    }
}
