using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WizardAndWitch.WizardAndWitch;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace WizAndWitch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            DB.GetConnection();
            

            string[] mainMenu = { "New Game", "Load Game", "Delete Character", "Campaign", "Credits", "Exit" };
            string[] elementClassOptions = { "Fire", "Earth", "Air", "Water", "Spirit" };
            string[] haircolorOptions = { "White", "Black", "Purple", "Red", "Green" };
            string[] eyecolorOptions = { "Gold", "Red", "Silver", "Grey", "Blue" };
            string[] faceshapeOptions = { "Sharp", "Soft", "Round", "Angular", "Heart" };
            string[] skincolorOptions = { "Brown", "White", "Green", "Pale", "Tan" };
            string[] wizardoutfitOptions = { "Arcane Scholar Robes", "GrandSorcerer Vestments", "Celestial Rune Cloak", "Celestial Magus", "Astral Conjurer" };
            string[] wizardweaponOptions = { "Staff", "Orb", "Book", "Wand", "Chalice" };
            string[] wizardcompanionOptions = { "Arcane Owl", "Dragon", "Spirit Raven", "Golem", "Elemental Spirit" };
            string[] wizardhatOptions = { "Classic Pointed Hat", "Wide Brimmed Hat", "Elemental Hat", "Hooded Wizard Hat", "Crown-Like Magical Hat" };
            string[] witchElementOptions = { "Shadow", "Blood Magic", "Spirit Magic", "Necromancy", "Fate Magic" };
            string[] witchOutfitOptions = { "Moon Shadow Witch Dress", "Night bloom Coven Robe", "Crimson Hexweaver Gown", "Twilight Sorceress Gown", "Eclipse Enchantress Robe" };
            string[] witchWeaponOptions = { "Book", "Wand", "Athame", "Chalice", "Boline" };
            string[] witchCompanionOptions = { "Night craw", "Scarlet hound", "Forest driad", "Lunar cat", "Undead raven" };
            string[] witchhatOptions = { "Classic Pointed Witch Hat", "Curved Tip Hat", "Decorated Witch Hat", "Hooded Witch Hat", "Mini Witch Hat" };
            string[] statNames = { "Magic Damage", "Healing Magic", "Potion Alchemy Mastery", "Magic Defense", "Intelligence" };

            Intro.Play();
            Console.ResetColor();
            bool running = true;
            while (running)
            {
                int selected = Utils.ArrowMenu("MAIN MENU", mainMenu);
                switch (selected)
                {
                    case 0: // New Game
                        NewGameFlow(elementClassOptions, haircolorOptions,
                        eyecolorOptions, faceshapeOptions, skincolorOptions,
                        wizardhatOptions, wizardoutfitOptions,
                        wizardweaponOptions, wizardcompanionOptions,
                        witchElementOptions, witchhatOptions,
                        witchOutfitOptions, witchWeaponOptions, witchCompanionOptions,
                        statNames);
                        break;
                    case 1: // Load Game
                        Console.Clear();
                        List<Character> characters = DB.LoadCharacter();

                        if (characters.Count == 0)
                        {
                            Console.WriteLine("No characters found!");
                            Thread.Sleep(2000);
                            break; // return changed to break para bumalik sa main menu
                        }

                        Console.WriteLine("Choose a character to view info (or 0 to go back):");
                        for (int i = 0; i < characters.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {characters[i].Username}");
                        }
                        Console.WriteLine("0) Back");

                        int choice = -1;
                        bool validInput = false;

                        while (!validInput)
                        {
                            Console.Write("\nEnter number: ");
                            string input = Console.ReadLine()!;

                            if (int.TryParse(input, out choice))
                            {
                                if (choice == 0)
                                {
                                    validInput = true;
                                    Console.WriteLine("Returning to main menu...");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else if (choice >= 1 && choice <= characters.Count)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input! Please enter a valid number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input! Please enter a number.");
                            }
                        }

                        if (choice == 0) break; // bumalik sa main menu

                        // Display the selected character info
                        characters[choice - 1].DisplayCharacterInfo();

                        // Wait before returning to menu
                        Thread.Sleep(5000);
                        Console.WriteLine("\nPress any key to return to menu...");
                        Console.ReadKey();
                        break;

                    case 2: // delete Character
                        Console.Clear();
                        List<Character> charactersToDelete = DB.LoadCharacter();

                        if (charactersToDelete.Count == 0)
                        {
                            Console.WriteLine("No characters found!");
                            Thread.Sleep(2000);
                            break; // instead of return para bumalik sa main menu
                        }

                        Console.WriteLine("Choose a character to delete (or 0 to go back):");
                        for (int i = 0; i < charactersToDelete.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {charactersToDelete[i].Username}");
                        }
                        Console.WriteLine("0) Back");

                        int deleteChoice = -1;
                        bool validDeleteInput = false;

                        while (!validDeleteInput)
                        {
                            Console.Write("\nEnter number: ");
                            string input = Console.ReadLine()!;

                            if (int.TryParse(input, out deleteChoice))
                            {
                                if (deleteChoice == 0)
                                {
                                    // user wants to go back
                                    validDeleteInput = true;
                                    Console.WriteLine("Returning to main menu...");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else if (deleteChoice >= 1 && deleteChoice <= charactersToDelete.Count)
                                {
                                    validDeleteInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input! Please enter a valid number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input! Please enter a number.");
                            }
                        }



                        if (deleteChoice == 0) break; // back to main menu

                        string usernameToDelete = charactersToDelete[deleteChoice - 1].Username;
                        DB.DeleteCharacter(usernameToDelete);

                        Console.WriteLine($"Character {usernameToDelete} has been deleted.");
                        Thread.Sleep(2000);
                        break;

                    case 3: // Campaign
                        ShowStory();
                        break;

                    

                    case 4: // Credits

                        ShowCredits();
                        break;

                    case 5: // Exit
                        running = false;
                        break;
                }
            }
            Console.WriteLine("Exiting... Press any key.");
            Console.ReadKey();
        }
        private static void NewGameFlow(string[] elementClassOptions, string[]
        haircolorOptions, string[] eyecolorOptions,
        string[] faceshapeOptions, string[] skincolorOptions, string[]
        wizardhatOptions, string[] wizardoutfitOptions,
        string[] wizardweaponOptions, string[] wizardcompanionOptions,
        string[] witchElementOptions, string[] witchhatOptions,
        string[] witchOutfitOptions, string[] witchWeaponOptions, string[]
        witchCompanionOptions, string[] statNames)
        {
            Console.Clear();
            string Uname;
            // username validation
            while (true)
            {
                try
                {
                    Console.Write("Create a Username: ");
                    Uname = Console.ReadLine().ToUpper();
                    if (Uname.Length < 8 || Uname.Length > 20 || !
                    Utils.IsAlphanumeric(Uname))
                        throw new InvalidOptionException("Username must be 8-20 characters and contain NO special characters.");
                    Console.WriteLine("Username Accepted");
                    break;
                }
                catch (InvalidOptionException ex)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("ERROR: " + ex.Message);
                    Console.ResetColor();
                    Thread.Sleep(1500);
                }

            }
            // Choose class (wizard/witch) using simple arrow menu
            string[] classOptions = { "Wizard", "Witch" };
            int classSelected = Utils.ArrowMenu("Choose Character Class",
            classOptions);
            if (classSelected == 0)
            {
                // Wizard creation
                Console.Clear();
                Console.WriteLine("====== WIZARD CREATION ======");
                string El = Utils.ChooseOptionByInput("ELEMENT CLASS",
                elementClassOptions);
                string hair = Utils.ChooseOptionByInput("HAIR COLOR",
                haircolorOptions);
                string Ceye = Utils.ChooseOptionByInput("EYE COLOR",
                eyecolorOptions);
                string face = Utils.ChooseOptionByInput("FACE SHAPE",
                faceshapeOptions);
                string scolor1 = Utils.ChooseOptionByInput("SKIN COLOR",
                skincolorOptions);
                int wizHatIdx = Utils.ArrowMenu("WIZARD HAT", wizardhatOptions);
                string wizHat1 = wizardhatOptions[wizHatIdx];
                int outfitIdx = Utils.ArrowMenu("WIZARD OUTFIT",
                wizardoutfitOptions);
                string outfitWiz1 = wizardoutfitOptions[outfitIdx];
                int weaponIdx = Utils.ArrowMenu("WIZARD WEAPON",
                wizardweaponOptions);
                string weaponWiz1 = wizardweaponOptions[weaponIdx];
                int companionIdx = Utils.ArrowMenu("WIZARD COMPANION",
                wizardcompanionOptions);
                string companionWiz1 = wizardcompanionOptions[companionIdx];
                string wizcompN = "";
                while (true)
                {
                    Console.Write("Enter your companion name: ");
                    wizcompN = Console.ReadLine().ToUpper();
                    if (wizcompN.Length >= 8 && wizcompN.Length <= 20 &&
                    Utils.IsAlphanumeric(wizcompN))
                    {
                        Console.WriteLine("Companion name accepted");
                        break;
                    }
                    else

                    {
                        Console.WriteLine("Invalid! Companion name must be 8-20 characters and contain NO special characters.\n");
                    }
                }
                int[] wizardStats = Utils.AllocateStatPoints(statNames);
                Console.WriteLine("Creating account...");
                Thread.Sleep(1000);
                Console.WriteLine("Account Successfully CREATED! ");
                Wizard wizard = new Wizard(
                    Uname,
                    El,
                    hair,
                    Ceye,
                    face,
                    scolor1,
                    wizHat1,
                    outfitWiz1,
                    weaponWiz1,
                    companionWiz1,
                    wizcompN,
                    wizardStats
                );
                wizard.UpdateAdvantage();


                // save character
                DB.SaveCharacter(wizard);

                wizard.DisplayCharacterInfo();
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
            else
            {
                // Witch creation
                Console.Clear();
                Console.WriteLine("\n=== WITCH CREATION ===");
                string witchEl = Utils.ChooseOptionByInput("ELEMENT CLASS",
                witchElementOptions);
                string witchHair = Utils.ChooseOptionByInput("HAIR COLOR",
                haircolorOptions);
                string witchEye = Utils.ChooseOptionByInput("EYE COLOR",
                eyecolorOptions);
                string witchFace = Utils.ChooseOptionByInput("FACE SHAPE",
                faceshapeOptions);
                string witchSkin = Utils.ChooseOptionByInput("SKIN COLOR",
                skincolorOptions);

                int hatIdx = Utils.ArrowMenu("WITCH HAT", witchhatOptions);
                string witchHat1 = witchhatOptions[hatIdx];
                int outIdx = Utils.ArrowMenu("WITCH OUTFIT",
                witchOutfitOptions);
                string witchOut1 = witchOutfitOptions[outIdx];
                int wpnIdx = Utils.ArrowMenu("WITCH WEAPON",
                witchWeaponOptions);
                string witchWeapon1 = witchWeaponOptions[wpnIdx];
                int compIdx = Utils.ArrowMenu("WITCH COMPANION",
                witchCompanionOptions);
                string witchCompanion1 = witchCompanionOptions[compIdx];
                string witchcompN = "";
                while (true)
                {
                    Console.Write("Enter your companion name: ");
                    witchcompN = Console.ReadLine().ToUpper();
                    if (witchcompN.Length >= 8 && witchcompN.Length <= 20 &&
                    Utils.IsAlphanumeric(witchcompN))
                    {
                        Console.WriteLine("Companion name accepted");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid! Companion name must be 8-20 characters and contain NO special characters.\n");
                    }
                }
                int[] witchStats = Utils.AllocateStatPoints(statNames);
                Console.WriteLine("Creating account...");
                Thread.Sleep(1000);
                Console.WriteLine("Account Successfully CREATED! ");
                Witch witch = new Witch(
                    Uname,
                    witchEl,
                    witchHair,
                    witchEye,
                    witchFace,
                    witchSkin,
                    witchHat1,
                    witchOut1,
                    witchWeapon1,
                    witchCompanion1,
                    witchcompN,

                    witchStats
                );
                witch.UpdateAdvantage();

                // save character
                DB.SaveCharacter(witch);

                witch.DisplayCharacterInfo();
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }

        static void ShowStory()
        {
            Console.Clear();
            string story = "Arcanum Eclipse: Rise of unity \r\nNoong unang panahon sa planetang Arcanum, iisa lang ang goal ng Simurian Wizards at Kalyan witches. Magkasama nilang binabantayan ang kanilang mundo. Ang mga Wizards ay merong unique staff na nagtataglay ng celestial power na nagbibigay kaayusan at kapayapaan, habang ang Witches naman ay may unique magic book na tinatawag na oathbreaker na kayang bumuhay ng patay at mga sacred magic na sila lang nakakagawa. Magkakasama silang nagtatayo ng mga academy kung saan dito matututo ng magic ang bawat isa, nagkakaroon ng ceremony para sa kanilang kapayapaan, at nagtatanggol laban sa mga nilalang na gusto silang sakupin. That time, ang Arcanum ay naging pinaka sacred at pinaka powerful sa kasaysayan.\r\n";


            string story2 = "\nNgunit sa isang tragedy nagbago ang lahat. Nang may nangyaring aksidente sa isang high officials ng witches at isinisi to sa mga wizard, ginawa ng wizard ang lahat ng kanilang makakaya pero hindi nila kayang mabalik ang buhay na nawala na tanging witch lang nakakagawa nito pero di nila to pwede gawin sa mga ka uri nila tanging sa wizard lang at sa ibang living creature, kaya wala silang nagawa kundi tanggapin nalang ito at ituring na kalaban ang mga wizard pero pareho nilang ayaw ng gulo kaya umalis nlaang ang mga wizard at nagpakalayo layo sa mga witches at sa paglipas ng panahon ganto sila mamuhay, magkahiwalay, may di pagkakaintindihan but in deep side wala sa kanila ang may kasalanan.\r\n";


            string story3 = "\nMatapos ang mahabang panahon, parang nagkaka totoo ang prophecy kung saan ikaw ang magbabalik ng pagkakaisa at pagtutulungan ng wizard and witches sayo nakasalalay ang magiging takbo ng kwento at ang kapalaran ng arcanum. Ikaw ang makapag babalik ng balanse ng mundong arcanum at magbabalik buhay sa mga nasayang na panahon . Kailangan mong wakasan ang sumpang walang ibang dulot kundi paghihirap, kalungkutan, pagdurusa. Kailangan mong magsacrifice, umunawa, pumili ng tamang landas na tatahakin dahil para makamit ang tunay na layunin na pag isahin muli ang wizard and witches ay hindi lang nakasalalay sa kapangyarihan kundi pati rin sa pagpapatawad, paglimot at maging handa sa mga posibleng mangyari sa hinaharap.";

            string fullStory = story + story2 + story3;

            Console.WriteLine("Press 'S' anytime to skip the story.\n");

            foreach (char c in fullStory)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.S)
                    {
                        Console.WriteLine("\n\nStory skipped!\n");
                        break; // exit the loop
                    }
                }

                Console.Write(c);
                Thread.Sleep(30);
            }

            Console.WriteLine("\nPress any key to go back! ");
            Console.ReadKey();
        }

       
        static void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("=====================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("SPECIAL THANKS TO OUR PROF AFAN, LORENZ CHRISTOPHER < 3");
            Console.ResetColor();
            Console.WriteLine("=====================================================");
            Console.WriteLine("\nA GAME BY TRINITY CS");
            Console.WriteLine("============================================");
            Console.WriteLine("\nCODER: \n- GEMLOYD TALADTAD \n- JOHN DAVID BLANCO \n - DANIELO CARRETERO ");
            Console.WriteLine("============================================");
            Console.WriteLine("STORY WRITER: \nDAVID BLANCO");
            Console.WriteLine("============================================");
            Console.WriteLine("LEAD PROGRAMMER: \nDANIELO CARRETERO ");
            Console.WriteLine("============================================");
            Console.WriteLine("PROGRAMMER: \nGEMLOYD TALADTAD \nJOHN DAVID BLANCO");
            Console.WriteLine("============================================");
            Console.WriteLine("FLOWCHART MAKER: \nGEMLOYD TALADTAD");
            Console.WriteLine("============================================");
            Console.WriteLine("\nPress any key to go back to main menu");
            Console.ReadKey();
        }

    }
}
