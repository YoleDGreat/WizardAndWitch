using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardAndWitch.WizardAndWitch;
namespace WizAndWitch
{
    public class Wizard : Character
    {
        // constructor
        public Wizard(string username, string elementClass, string hairColor,
        string eyeColor,
        string faceShape, string skinColor, string hat, string
        outfit,
        string weapon, string companionType, string companionName,
        int[] stats)
        {
            this.Username = username;
            this.ElementClass = elementClass;
            this.HairColor = hairColor;
            this.EyeColor = eyeColor;
            this.FaceShape = faceShape;
            this.SkinColor = skinColor;
            this.Hat = hat;
            this.Outfit = outfit;
            this.Weapon = weapon;
            this.CompanionType = companionType;
            this.CompanionName = companionName;
            this.StatPoints = stats;

            if (Weapon == "Staff" || Weapon == "Orb")
            {
                HasMagicAdvantage = true;
            }
            else if (Weapon == "Book" || Weapon == "Wand")
            {
                HasMeleeAdvantage = true;
            }

        }
        public override void DisplayCharacterInfo()
        {
            Console.WriteLine("\n===== WIZARD CHARACTER INFORMATION =====");
            DisplayBaseInfo();
            DisplayStats();
        }
    }
}
    
