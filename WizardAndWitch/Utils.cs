using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace WizAndWitch

{
public static class Utils
{
    // Arrow-key menu for nicer UI. Returns index of selected option.
    public static int ArrowMenu(string title, string[] options)
    {
        int idx = 0;
        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.WriteLine("=============================");
            Console.WriteLine("\n======== " + title + " ========\n");
            for (int i = 0; i < options.Length; i++)
            {
                if (i == idx)
                    Console.WriteLine($"> {options[i]}");
                else
                    Console.WriteLine($" {options[i]}");
            }
            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
            {
                idx = (idx - 1 + options.Length) % options.Length;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                idx = (idx + 1) % options.Length;
            }
        } while (key != ConsoleKey.Enter);
        return idx; // zero-based
    }
    // Original choose option: enter number or name (case-insensitive)
    public static string ChooseOptionByInput(string title, string[] options)
    {
        Console.WriteLine("=============================");
        Console.WriteLine("\n======== " + title + " ========");
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        while (true)
        {
            Console.Write("Enter number or name: ");
            
        string input = Console.ReadLine().Trim();
            if (int.TryParse(input, out int num))
            {
                if (num >= 1 && num <= options.Length)
                    return options[num - 1];
            }
            for (int i = 0; i < options.Length; i++)
            {
                if (string.Equals(input, options[i],
                StringComparison.OrdinalIgnoreCase))
                {
                    return options[i];
                }
            }
            Console.WriteLine("Invalid input! Please enter a valid number or name.");
        }
    }
    // Validate username/companion using regex (alphanumeric only)
    public static bool IsAlphanumeric(string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        return Regex.IsMatch(s, "^[a-zA-Z0-9]+$");
    }
    // Allocate stat points (keeps user interaction centralized)
    public static int[] AllocateStatPoints(string[] statNames, int
    totalPoints = 10)
    {
        int[] statPoints = new int[statNames.Length];
        while (totalPoints > 0)
        {
            Console.Clear();
            Console.WriteLine("You have " + totalPoints + " points to allocate to stats");
            Console.WriteLine("Current Stats:");
            for (int i = 0; i < statNames.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + statNames[i] + ": " +
                statPoints[i]);
            }
            
        Console.WriteLine("Points left: " + totalPoints);
            Console.Write("Choose a stat number to allocate points to: ");
            if (!int.TryParse(Console.ReadLine(), out int statChoice))
            {
                Console.WriteLine("Invalid number. Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            statChoice -= 1;
            if (statChoice < 0 || statChoice >= statNames.Length)
            {
                Console.WriteLine("Invalid stat choice! Try again.");
                Console.ReadKey();
                continue;
            }
            Console.Write("How many points do you want to assign to " +
            statNames[statChoice] + "? ");
            if (!int.TryParse(Console.ReadLine(), out int points))
            {
                Console.WriteLine("Invalid number. Press any key to try again...");
                Console.ReadKey();
                continue;
            }
            if (points < 0 || points > totalPoints)
            {
                Console.WriteLine("Invalid input! You have " + totalPoints
                + " points remaining.");
                Console.ReadKey();
                continue;
            }
            statPoints[statChoice] += points;
            totalPoints -= points;
            Console.WriteLine("==================================");
            Console.WriteLine(points + " points added to " +
            statNames[statChoice] + "!");
            Console.WriteLine("==================================");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        return statPoints;
        
    }
}
}