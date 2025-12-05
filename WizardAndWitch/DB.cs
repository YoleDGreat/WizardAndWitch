using Microsoft.Data.Sqlite;
using WizAndWitch;

namespace WizardAndWitch.WizardAndWitch
{
    public class DB
    {
        // config of db
        private static string dbPath = Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName,
            "db.db"
        );

        private static string connectionString = $"Data Source={dbPath}";

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
        //save character
        public static void SaveCharacter(Character character)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO Characters 
                            (Role, Username, HairColor, EyeColor, FaceShape, SkinColor, Hat, Outfit, Weapon, CompanionType, CompanionName, ElementClass, MagicDamage, HealingMagic, PotionAlchemyMastery, MagicDefense, Intelligence)
                            VALUES 
                            (@Role, @Username, @HairColor, @EyeColor, @FaceShape, @SkinColor, @Hat, @Outfit, @Weapon, @CompanionType, @CompanionName, @ElementClass, @MagicDamage, @HealingMagic, @PotionAlchemyMastery, @MagicDefense, @Intelligence)
                        ";
                        cmd.Parameters.AddWithValue("@Role", character is Wizard ? "Wizard" : "Witch");
                        cmd.Parameters.AddWithValue("@Username", character.Username);
                        cmd.Parameters.AddWithValue("@HairColor", character.HairColor);
                        cmd.Parameters.AddWithValue("@EyeColor", character.EyeColor);
                        cmd.Parameters.AddWithValue("@FaceShape", character.FaceShape);
                        cmd.Parameters.AddWithValue("@SkinColor", character.SkinColor);
                        cmd.Parameters.AddWithValue("@Hat", character.Hat);
                        cmd.Parameters.AddWithValue("@Outfit", character.Outfit);
                        cmd.Parameters.AddWithValue("@Weapon", character.Weapon);
                        cmd.Parameters.AddWithValue("@CompanionType", character.CompanionType);
                        cmd.Parameters.AddWithValue("@CompanionName", character.CompanionName);
                        cmd.Parameters.AddWithValue("@ElementClass", character.ElementClass);
                        cmd.Parameters.AddWithValue("@MagicDamage", character.StatPoints[0]);
                        cmd.Parameters.AddWithValue("@HealingMagic", character.StatPoints[1]);
                        cmd.Parameters.AddWithValue("@PotionAlchemyMastery", character.StatPoints[2]);
                        cmd.Parameters.AddWithValue("@MagicDefense", character.StatPoints[3]);
                        cmd.Parameters.AddWithValue("@Intelligence", character.StatPoints[4]);

                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Character Created!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in saving character: " + ex.Message);
            }
        }

        public static List<Character> LoadCharacter()
        {
            List<Character> characters = new List<Character>();

            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Characters;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int[] stats = new int[]
                                {
                                    reader.GetInt32(reader.GetOrdinal("MagicDamage")),
                                    reader.GetInt32(reader.GetOrdinal("HealingMagic")),
                                    reader.GetInt32(reader.GetOrdinal("PotionAlchemyMastery")),
                                    reader.GetInt32(reader.GetOrdinal("MagicDefense")),
                                    reader.GetInt32(reader.GetOrdinal("Intelligence"))
                                };

                                string role = reader.GetString(reader.GetOrdinal("Role")); // 'Wizard' or 'Witch'

                                Character character;

                                if (role == "Wizard")
                                {
                                    character = new Wizard(
                                        reader.GetString(reader.GetOrdinal("Username")),
                                        reader.GetString(reader.GetOrdinal("ElementClass")),
                                        reader.GetString(reader.GetOrdinal("HairColor")),
                                        reader.GetString(reader.GetOrdinal("EyeColor")),
                                        reader.GetString(reader.GetOrdinal("FaceShape")),
                                        reader.GetString(reader.GetOrdinal("SkinColor")),
                                        reader.GetString(reader.GetOrdinal("Hat")),
                                        reader.GetString(reader.GetOrdinal("Outfit")),
                                        reader.GetString(reader.GetOrdinal("Weapon")),
                                        reader.GetString(reader.GetOrdinal("CompanionType")),
                                        reader.GetString(reader.GetOrdinal("CompanionName")),
                                        stats
                                    );
                                }
                                else // Witch
                                {
                                    character = new Witch(
                                        reader.GetString(reader.GetOrdinal("Username")),
                                        reader.GetString(reader.GetOrdinal("ElementClass")),
                                        reader.GetString(reader.GetOrdinal("HairColor")),
                                        reader.GetString(reader.GetOrdinal("EyeColor")),
                                        reader.GetString(reader.GetOrdinal("FaceShape")),
                                        reader.GetString(reader.GetOrdinal("SkinColor")),
                                        reader.GetString(reader.GetOrdinal("Hat")),
                                        reader.GetString(reader.GetOrdinal("Outfit")),
                                        reader.GetString(reader.GetOrdinal("Weapon")),
                                        reader.GetString(reader.GetOrdinal("CompanionType")),
                                        reader.GetString(reader.GetOrdinal("CompanionName")),
                                        stats
                                    );
                                }

                                characters.Add(character); 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in loading characters: " + ex.Message);
            }

            return characters;
        }
        //Delete character
        public static void DeleteCharacter(string username)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Characters WHERE Username = @Username;";
                        cmd.Parameters.AddWithValue("@Username", username);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Character '{username}' successfully deleted!");
                        }
                        else
                        {
                            Console.WriteLine($"No character found with username '{username}'.");
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in deleting character: " + ex.Message);
            }
        }
    }
}
