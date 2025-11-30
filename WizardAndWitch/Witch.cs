using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WizardAndWitch.WizardAndWitch;
namespace WizAndWitch
{
    public class Witch : Character
    {
        public Witch(string username, string elementClass, string hairColor,
        string eyeColor,
        string faceShape, string skinColor, string hat, string outfit,
        string weapon, string companionType, string companionName, int[]
        stats)
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
            SetStats(stats);
        }
        public override void DisplayCharacterInfo()
        {
            Console.WriteLine("\n===== WITCH CHARACTER INFORMATION =====");
            DisplayBaseInfo();
            DisplayStats();
        }
    }
}

