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
    class Item
    {
        public Texture2D[] Textures;

        public int healthModifier;
        public int attackModifier;

        public SwordAbilitySet SAbilities;

        public Item(Texture2D Front, Texture2D Back, Texture2D Left, Texture2D Right, int HealthModifier, int AttackModifier)
        {
            Textures = new Texture2D[4];

            Textures[0] = Front;
            Textures[1] = Back;
            Textures[2] = Left;
            Textures[3] = Right;

            healthModifier = HealthModifier;
            attackModifier = AttackModifier;
        }

        public Item(Texture2D Front, Texture2D Back, Texture2D Left, Texture2D Right, int HealthModifier, int AttackModifier, string WeaponType, SwordAbilitySet abilities)
        {
            Textures = new Texture2D[4];

            Textures[0] = Front;
            Textures[1] = Back;
            Textures[2] = Left;
            Textures[3] = Right;

            healthModifier = HealthModifier;
            attackModifier = AttackModifier;

            switch (WeaponType)
            {
                case "sword":
                    {
                        SAbilities = abilities;
                        break;
                    }
            }
        }
    }
}
