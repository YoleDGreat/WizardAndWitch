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
        public string role { get; protected set; }
        public bool HasMagicAdvantage { get; set; }
        public bool HasMeleeAdvantage { get; set; }




        public int[] StatPoints { get; protected set; }
        public string[] StatNames { get; protected set; } = { "Magic Damage", "Healing Magic", "Potion Alchemy Mastery", "Magic Defense", "Intelligence" };


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
            Console.WriteLine($"Magic Advantage: {HasMagicAdvantage}");
            Console.WriteLine($"Melee Advantage: {HasMeleeAdvantage}");
        }
        public void UpdateAdvantage()
        {
            if (Weapon == "Book" || Weapon == "Chalice")
            {
                HasMagicAdvantage = true;
                HasMeleeAdvantage = false;
            }
            else if (Weapon == "Athame" || Weapon == "Boline")
            {
                HasMeleeAdvantage = true;
                HasMagicAdvantage = false;
            }
            else
            {
                HasMagicAdvantage = false;
                HasMeleeAdvantage = false;
            }
        }
        protected void DisplayStats()
        {
            Console.WriteLine("\n====== Character Stats ======");
            Console.WriteLine("HP: 100");
            Console.WriteLine("MANA: 50");
            for (int i = 0; i < StatNames.Length; i++)
            {
                Console.WriteLine($"{StatNames[i]}: {StatPoints[i]}");
            }
            Console.WriteLine("============================");
        }
    }
}


