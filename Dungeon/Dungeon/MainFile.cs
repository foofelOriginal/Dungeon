using System;
using System.Collections.Generic; // for later inventory if no1 already modding it bruh
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Dungeon
{
    // Player class
    [Serializable]
    public class Player
    {
        public int pHealth { get; set; }
        public int pDamage { get; set; }
        public int coins { get; set; }
        public int activeItem { get; set; }
        public List<string> inventory = new List<string>();
        public List<string> weapons = new List<string>();
        public Player(int _pHealth, int _pDamage, int _coins)
        {
            pHealth = _pHealth;
            pDamage = _pDamage;
            coins = _coins;
        }
        public Player() { }
    }
    // Enemy class
    class Enemy
    {                                                               // haha funi
        private static readonly string[] names = { "Ogre", "Mage", /*"Sausage",*/ "Wolf", "Giant Bee", "Cyclope" };
        private static readonly Random rnd = new Random();

        public int eHealth;
        public int eDamage;
        public string eName;

        public Enemy(int _eHealth, int _eDamage, string _eName = null)
        {
            eHealth = _eHealth;
            eDamage = _eDamage;
            eName = eName ?? names[rnd.Next(0, names.Length)];
        }
    }
    // Main class
    class MainClass
    {
        // Le menu
        public static void Menu()
        {
            Console.Clear();

            Console.WriteLine("Main menu: ");
            Console.WriteLine("-----------------");
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Shop (WIP)");
            Console.WriteLine("3. Settings");
            Console.WriteLine("4. Misc");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
            /* <-- Remove this thingies for idk, show thing below?
            Console.WriteLine("99. Show debugging parameter"); */
        }
        static void Main()
        {
            Console.Clear();

            Console.Title = "Dungeon (github build)";

            BinaryFormatter binForm = new BinaryFormatter();

            string fileName = "save.bds";
            string sourcePath = Directory.GetCurrentDirectory();
            string targetSavePath = @"C:\DungeonSaveFiles";

            string sourceFile = Path.Combine(sourcePath, fileName);
            string destFile = Path.Combine(targetSavePath, fileName);

            if (!Directory.Exists(targetSavePath))
                Directory.CreateDirectory(targetSavePath);

            if (File.Exists(sourceFile))
                File.Copy(sourceFile, destFile, true);
            File.Delete(sourceFile);

            Player loadPlayer;

            void LoadData()
            {
                using (FileStream binStream = new FileStream(@"C:\DungeonSaveFiles\save.bds", FileMode.OpenOrCreate))
                {
                    loadPlayer = (Player)binForm.Deserialize(binStream);
                }
            }

            LoadData();
            // Player creation. Add more interaction like putting a name
            Player defPlayer = new Player(500, loadPlayer.pDamage, loadPlayer.coins);

            void SaveData()
            {
                using (FileStream binStream = new FileStream(@"C:\DungeonSaveFiles\save.bds", FileMode.OpenOrCreate))
                {
                    binForm.Serialize(binStream, defPlayer);
                }
            }

            // Cheat menu code generator
            Random cheatCodeGen = new Random();
            string cheatCode = Convert.ToString(cheatCodeGen.Next(1, 5001));

            Menu();

            /* <-- Remove this thingies to show cheatcode. duh.
            Console.WriteLine("Cheat code is: " + cheatCode); */

            string chs = Console.ReadLine();
            int chsChkRes;
            bool chsChk = Int32.TryParse(chs, out chsChkRes);

            if (chsChk)
            {
                if (chs == "1")
                {
                    Console.Clear();

                    // Creating then announcing enemy
                    Enemy enm1 = new Enemy(420, 50);

                    Console.WriteLine(enm1.eName + " is on the way!");
                    Console.WriteLine("type attack to attack");

                    while (enm1.eHealth > 0)
                    {
                        string command = Console.ReadLine();
                        if (defPlayer.pHealth > 0)
                        {
                            if (command == "attack")
                            {
                                // The battle screen
                                Console.Clear();
                                    
                                Random hitChance = new Random();
                                int enmHitChance = hitChance.Next(1, 101);
                                int plrHitChance = hitChance.Next(1, 101);

                                Console.WriteLine("Player does a swing!");

                                if (plrHitChance > 24)
                                {
                                    enm1.eHealth -= defPlayer.pDamage;
                                    if (enm1.eHealth < 0)
                                        enm1.eHealth = 0;
                                    Console.WriteLine("WHOOP! That attack did " + defPlayer.pDamage + " damage to the " + enm1.eName + ". Its hp is " + enm1.eHealth + "\n");
                                }
                                else
                                {
                                    Console.WriteLine("Player missed! :peepoSad: \n");
                                }

                                Console.WriteLine(enm1.eName + " does a swing!");

                                if (enmHitChance > 69)
                                {
                                    defPlayer.pHealth -= enm1.eDamage;
                                    Console.WriteLine("WHOOP! That attack id " + enm1.eDamage + " damage to the player. :peepoSad:" + "\n");
                                }
                                else
                                {
                                    Console.WriteLine(enm1.eName + " missed! :peepoHappy:" + "\n");
                                }

                                Console.WriteLine("Ur eichpee is " + defPlayer.pHealth);
                            }
                            else if (command == cheatCode)
                            {
                                Console.Clear();

                                // Here starts cheat menu
                                Console.WriteLine("Debug Menu: (WIP)");
                                Console.WriteLine("Instantly kill current enemy: ik");
                                Console.WriteLine("Get OP Weapon (lasts only for this fight): opwpn");
                                Console.WriteLine("Get 10k coins: moreCoins");
                                Console.WriteLine("Crash the game: crash");

                                void crash1 ()
                                {
                                    Console.WriteLine("bruh?");
                                    crash2();
                                }

                                void crash2()
                                {
                                    Console.WriteLine("bruh.");
                                    crash1();
                                }

                                string debug = Console.ReadLine();

                                if (debug == "ik")
                                {
                                    Console.Clear();
                                    Console.WriteLine($"{enm1.eName} has die.");
                                    enm1.eHealth -= enm1.eHealth;
                                }
                                else if (debug == "opwpn")
                                {
                                    Console.Clear();
                                    defPlayer.pDamage = enm1.eHealth;
                                    Console.WriteLine("Weapon got! type attack");
                                }
                                else if (debug == "moreCoins")
                                {
                                    Console.Clear();
                                    defPlayer.coins += 10000;
                                    Console.WriteLine("Coins given!");
                                    Console.WriteLine("You can continue the fight!");
                                }
                                else if (debug == "crash")
                                {
                                    crash1();
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect code. Returning to the fight.");
                                }
                            }
                            else if (command != cheatCode)
                            {
                                Console.WriteLine("Wrong command!!1!");
                            } 
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You died. Press enter to restart");

                            string urdedCmd = Console.ReadLine();

                            if (urdedCmd == string.Empty)
                            {
                                Main();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Press enter.");
                            }
                        }
                    }
                    enm1.eHealth = 0;
                    // After battle screen WIP
                    Random coinGen = new Random();
                    int earnedCoins = coinGen.Next(1, 51);
                    Console.WriteLine(enm1.eName + " has been defeated!");
                    defPlayer.pDamage = loadPlayer.pDamage;  // cuz opwpn u know

                    /* bruh item code xd (idk how to binary serialize lists (smh) thats why its not working)

                    string[] items = { "Stick", "Scrap", "Rotten Eggs", "Broken dildo", "Dull sword" };
                    Random itemIdGen = new Random();
                    int itemId = itemIdGen.Next(0, 3);
                    Random itemAmountGen = new Random();
                    int itemAmount = itemAmountGen.Next(1, 8);

                    defPlayer.inventory.Add(items[itemId] + " x" + itemAmount); */
                    defPlayer.coins += earnedCoins;
                    Console.WriteLine("Coins earned: " + earnedCoins);
                    Console.WriteLine("Current coins: " + defPlayer.coins);
                    SaveData();

                    Console.WriteLine("type menu to return");

                    string aftbCmd = Console.ReadLine();
                    if (aftbCmd == "menu")
                    {
                        Main();
                    }
                    else
                    {
                        Console.WriteLine("i said menu");
                    }
                }
                else if (chs == "2")
                {
                    // shop duh. work in progress
                    Console.Clear();

                    Console.WriteLine("Current balance: " + defPlayer.coins);
                    Console.WriteLine();
                    Console.WriteLine("Dull Sword. Cost 25 coins.");
                    Console.WriteLine("Damage: 69");

                    Console.WriteLine("1. Buy");
                    Console.WriteLine("2. Exit");

                    string shopCmd = Console.ReadLine();
                    int shpCmdRes;

                    if (Int32.TryParse(shopCmd, out shpCmdRes))
                    {
                        if (shopCmd == "1")
                        {
                            if (defPlayer.coins >= 5)
                            {
                                Console.WriteLine("Buy successful!");
                                defPlayer.coins -= 25;
                                defPlayer.pDamage = 69;
                                SaveData();
                                Main();
                            }
                            else
                            {
                                Console.WriteLine("Not enough coins!");
                                Console.ReadKey();
                                Main();
                            }
                        }
                        else if (shopCmd == "2")
                        {
                            Main();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid command.");
                    }

                    // Settings menu section (not working at all, very WIP) 
                }
                else if (chs == "5")
                {
                }
                else if (chs == "4")
                {
                    Console.Clear();
                    Console.WriteLine("1. Join discord server");
                    string miscCmd = Console.ReadLine();
                    int miscCmdChkRes;
                    bool miscCmdChk = Int32.TryParse(chs, out miscCmdChkRes);

                    if (miscCmdChk)
                    {
                        if (miscCmd == "1")
                        {
                            Process.Start("https://vk.cc/bYElqI");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect command!");
                        Console.ReadKey();
                        Main();
                    }
                }
                else if (chs == "99")
                {
                    // Here u can put debug or modding code like idk
                    MessageBox.Show("shit by foofel#3299", "bruh");
                    Main();
                }
                else if (chs == "3")
                {
                    Console.Clear();
                    Console.WriteLine("1. Load data from save");
                    string settingsCmd = Console.ReadLine();
                    int settingsCmdParseResult;
                    bool settingsCmdParse = Int32.TryParse(settingsCmd, out settingsCmdParseResult);

                    if (settingsCmdParse)
                    {
                        if (settingsCmd == "1")
                        {
                            LoadData();
                            Console.Clear();
                            Console.WriteLine("Load Success.");
                            Console.ReadKey();
                            Main();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Command. Command is not a number or not equals the ones specified in the list. Press enter to restart");
                var crash = Console.ReadKey();
                Main();
            }
        }
    }
}