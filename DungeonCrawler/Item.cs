using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Xml.Serialization;

namespace DungeonCrawler
{
    public class Item
    {
        public Color color;

        public Texture2D[] Textures = new Texture2D[0];

        public int healthModifier;
        public int attackModifier;
        public double blockModifier;

        public int FloorNum;

        public Vector2 position;

        public string Type;

        public SwordAbilitySet SAbilities;

        public Item(Texture2D Front, Texture2D Back, Texture2D Left, Texture2D Right, int HealthModifier, int AttackModifier, double BlockModifier, string Rarity)
        {
            switch (Rarity)
            {
                case "common":
                    color = Color.Blue;
                    break;

                case "rare":
                    color = Color.Green;
                    break;

                case "legendary":
                    color = Color.Purple;
                    break;
            }

            Textures = new Texture2D[4];

            Textures[0] = Front;
            Textures[1] = Back;
            Textures[2] = Left;
            Textures[3] = Right;

            healthModifier = HealthModifier;
            attackModifier = AttackModifier;
            blockModifier = BlockModifier;
        }

        public Item(Texture2D Front, Texture2D Back, Texture2D Left, Texture2D Right, int HealthModifier, int AttackModifier, double BlockModifier, string WeaponType, string Rarity, SwordAbilitySet abilities)
        {
            Textures = new Texture2D[4];

            switch (Rarity)
            {
                case "common":
                    color = Color.Blue;
                    break;

                case "rare":
                    color = Color.Green;
                    break;

                case "legendary":
                    color = Color.Purple;
                    break;
            }

            Textures[0] = Front;
            Textures[1] = Back;
            Textures[2] = Left;
            Textures[3] = Right;

            healthModifier = HealthModifier;
            attackModifier = AttackModifier;
            blockModifier = BlockModifier;

            switch (WeaponType)
            {
                case "sword":
                    {
                        SAbilities = abilities;
                        break;
                    }
            }
        }

        public void Draw(SpriteBatch s)
        {
            s.Draw(Textures[0], new Vector2(position.X * 32, position.Y * 32), color);
        }

        public Item()
        {

        }
    }
}
