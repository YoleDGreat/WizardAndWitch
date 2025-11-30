using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WizardAndWitch.WizardAndWitch
{
    using System;
    
    
        public abstract class Character
        {
            public string Username { get; protected set; }
            public string HairColor { get; protected set; }
            public string EyeColor { get; protected set; }
            public string FaceShape { get; protected set; }
            public string SkinColor { get; protected set; }
            public string Hat { get; protected set; }
            public string Outfit { get; protected set; }
            public string Weapon { get; protected set; }
            public string CompanionType { get; protected set; }
            public string CompanionName { get; protected set; }
            public string ElementClass { get; protected set; }
            protected int[] statPoints;
            protected string[] statNames = { "Magic Damage", "Healing Magic","Potion Alchemy Mastery", "Magic Defense", "Intelligence" };
            
            public abstract void DisplayCharacterInfo();
            
            protected void DisplayBaseInfo()
            {
                Console.WriteLine("\nUsername: " + Username);
                Console.WriteLine("Element Class: " + ElementClass);
                Console.WriteLine("Hair color: " + HairColor);
                Console.WriteLine("Eye color: " + EyeColor);
                Console.WriteLine("Face shape: " + FaceShape);
                Console.WriteLine("Skin color: " + SkinColor);
                Console.WriteLine("Hat: " + Hat);
                Console.WriteLine("Outfit: " + Outfit);
                Console.WriteLine("Primary weapon: " + Weapon);
                Console.WriteLine("Companion: " + CompanionName + "(" + CompanionType + ")");
            }
            protected void DisplayStats()
            {
                Console.WriteLine("\n====== Character Stats ======");
                Console.WriteLine("HP: 100");
                Console.WriteLine("MANA: 50");
                for (int i = 0; i < statNames.Length; i++)
                {
                    Console.WriteLine($"{statNames[i]}: {statPoints[i]}");
                }
                Console.WriteLine("============================");
            }
            
            protected void SetStats(int[] stats)
            {
                if (stats == null || stats.Length != statNames.Length)
                {
                    
                    statPoints = new int[statNames.Length];
                }
                else
                {
                    statPoints = stats;
                }
            }
        }
    }


