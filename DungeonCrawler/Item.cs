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
        public Texture2D[] Textures;

        public int healthModifier;
        public int attackModifier;
        public double blockModifier;

        public Vector2 position;

        public SwordAbilitySet SAbilities;

        public Item(Texture2D Front, Texture2D Back, Texture2D Left, Texture2D Right, int HealthModifier, int AttackModifier, double BlockModifier)
        {
            Textures = new Texture2D[4];

            Textures[0] = Front;
            Textures[1] = Back;
            Textures[2] = Left;
            Textures[3] = Right;

            healthModifier = HealthModifier;
            attackModifier = AttackModifier;
            blockModifier = BlockModifier;
        }

        public Item(Texture2D Front, Texture2D Back, Texture2D Left, Texture2D Right, int HealthModifier, int AttackModifier, double BlockModifier, string WeaponType, SwordAbilitySet abilities)
        {
            Textures = new Texture2D[4];

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

        public Item()
        {

        }
    }
}
