using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace DungeonCrawler
{
    public class Score
    {
        public int totalScore = 0;

        public int EnemyScore = 0;

        public int position;

        public void Total(Player p, Game1 g)
        {
            totalScore = 0;

            totalScore += EnemyScore;

            totalScore += g.floorNum;

            foreach (Item i in p.Equiped)
            {
                if (i.color == Color.Green)
                {
                    totalScore += i.FloorNum;
                }
                else if (i.color == Color.Blue)
                {
                    totalScore += i.FloorNum * 2;
                }
                else if (i.color == Color.Red)
                {
                    totalScore += i.FloorNum * 4;
                }
            }
            totalScore += p.maxHealth;
            totalScore += p.HealthPotions * 10;
            totalScore += p.Attack;
            totalScore += Convert.ToInt32(p.Block);
        }
    }
}
