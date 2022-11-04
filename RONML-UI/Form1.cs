using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RONML_UI
{
    public partial class Form1 : Form
    {
        bool LoadVo = true;
        bool LoadFmod = true;
        bool LoadPak = true;
        bool OverrideAll = true;
        bool Loaddx12 = true;
        string Paks
        {
            get
            {
                return $@"{GamePath}\modcontent\PAKs\";
            }
        }
        string GamePaks
        {
            get
            {
                return $@"{GamePath}\ReadyOrNot\Content\Paks";
            }
        }
        string Vo
        {
            get
            {
                return $@"{GamePath}\ReadyOrNot\Content\VO";
            }
        }
        string Banks
        {
            get
            {
                return $@"{GamePath}\ReadyOrNot\Content\FMOD\Desktop";
            }
        }
        string Me
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location.TrimEnd(@"RONML-UI.dll".ToCharArray()).Replace("\n", "").Replace("\r", ""); // gotta replace shit or line terminators fuck with I/O
            }
        }
        string Modvo
        {
            get
            {
                return $@"{GamePath}\modcontent\VO\";
            }
        }
        string Modbanks
        {
            get
            {
                return $@"{GamePath}\modcontent\FMOD\";
            }
        }
        string Gamevotemp
        {
            get
            {
                return $@"{GamePath}\modcontent\VO\GameTemp";
            }
        }
        string Gamebanktemp
        {
            get
            {
                return $@"{GamePath}\modcontent\FMOD\GameTemp";
            }
        }
        string Importantfilepath
        {
            get
            {
                return $@"{Me}\importantfile.txt";
            }
        }
        string GamePath
        {
            get
            {
                if (!File.Exists(Importantfilepath))
                {
                    var f = File.CreateText(Importantfilepath);
                    f.WriteLine(richTextBox1.Text);
                    f.Close();
                    return richTextBox1.Text.Replace("\n", "").Replace("\r", "");
                } else return File.ReadAllText(Importantfilepath).Replace("\n", "").Replace("\r", "");
            }
            set
            {
                var f = File.CreateText(Importantfilepath);
                f.WriteLine(value);
                f.Close();
            }
        }

        List<string> Gamevoli = new();
        List<string> Mevoli = new();
        List<string> Gamebankli = new();
        List<string> Mebankli = new();
        Process GameProcess = new();

        public Form1()
        {
            InitializeComponent();
            Random random = new Random();
            label3.Text = RandomText[random.Next(RandomText.Count())];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SwitchIO();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadVo = !LoadVo;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            LoadFmod = !LoadFmod;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            LoadPak = !LoadPak;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            return;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            GamePath = richTextBox1.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            return;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Loaddx12 = !Loaddx12;
        }

        private List<string> RandomText = new()
        {
            "Still better than mod.io",
            "See you in 2 minutes after you get insta-headshotted by a crackhead",
            "I knew you'd come back",
            "Miss me already?",
            "Shotguns are still ass btw",
            "Still better than Nexus Mods Launcher",
            "ur mom lmao",
            "Civilians are extra obstacles with health bars",
            "When kicking a door, remember there is a chance of blowing up",
            "Going non-lethal is more of a pain for you than the enemy",
            "Haha pepper spray goes PSSSSSSSHHHH",
            "AHHH FUCK HE HAS A KNIF-",
            "Battering ram goes brrrrrrr",
            "HP vs AP is what matters on what is kept in the target and what goes through the target"
        };

        private void label2_Click(object sender, EventArgs e)
        {
            return;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            OverrideAll = !OverrideAll;
        }

        private void SwitchIO()
        {
            try
            {
                Gamevoli = Directory.GetDirectories(Vo).ToList();
                Gamebankli = Directory.GetFiles(Banks).ToList();
                Mevoli = Directory.GetDirectories(Modvo).ToList();
                Mebankli = Directory.GetFiles(Modbanks).ToList();
                Console.WriteLine("I/O => Moving files...");
            }
            catch (Exception e)
            {
                label3.Text = "Couldn't read important file directories/files, check paths";
                return;
            }

            switch (LoadVo)
            {
                case false:
                    break;
                case true:
                    if (OverrideAll)
                    {
                        Gamevoli.ForEach((string f) =>
                        {
                            Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                            {
                                string[] e = b.Split(@"\");
                                string direc = e[^2];
                                string file = e.Last();
                                if (Directory.Exists($@"{Modvo}\{direc}"))
                                {
                                    Directory.CreateDirectory($@"{Gamevotemp}\{direc}");

                                    Directory.Move(b, $@"{Gamevotemp}\{direc}\{file}");
                                }
                            });
                        });
                        Mevoli.ForEach((string f) =>
                        {
                            Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                            {
                                string[] e = b.Split(@"\");
                                string direc = e[^2];
                                string file = e.Last();
                                if (Directory.Exists($@"{Vo}\{direc}"))
                                {
                                    Directory.Move($@"{Modvo}\{direc}\{file}", $@"{Vo}\{direc}\{file}");
                                }
                            });
                        });
                        break;
                    }
                    Mevoli.ForEach((string f) =>
                    {
                        Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                        {
                            string[] filei = b.Split(@"\");
                            string filef = filei[^2];
                            string fileo = filei.Last();
                            Directory.CreateDirectory($@"{Gamevotemp}\{filef}");
                            Directory.Move($@"{Vo}\{filef}\{fileo}", $@"{Gamevotemp}\{filef}\{fileo}");
                            Directory.Move(b, $@"{Vo}\{filef}\{fileo}");
                        });
                    });
                    break;
            }

            switch (LoadFmod)
            {
                case false:
                    break;
                case true:
                    Mebankli.ForEach((string f) =>
                    {
                        string filei = f.Split(@"\").Last();

                        Directory.Move($@"{Banks}\{filei}", $@"{Gamebanktemp}\{filei}");
                        Directory.Move(f, $@"{Banks}\{filei}");
                    });
                    break;
            }

            switch (LoadPak)
            {
                case false:
                    break;
                case true:
                    Directory.EnumerateFiles(Paks).ToList().ForEach((string f) =>
                    {
                        Directory.Move(f, $@"{GamePaks}\{f.Split(@"\").Last()}");
                        Console.WriteLine($@"I/O => Moved PAK {f.Split(@"\").Last()}");
                    });
                    break;
            }
            StartDaGame(false);
            return;
        }

        void StartDaGame(bool startVanilla)
        {
            try
            {
                var proc = new ProcessStartInfo()
                {
                    FileName = $@"{GamePath}\ReadyOrNot.exe",
                    Arguments = Loaddx12 ? "dx12" : "dx11",
                    UseShellExecute = true
                };
                GameProcess = Process.Start(proc);
                if (!startVanilla)CleanupAsync();
                return;
            }
            catch (Exception e)
            {
                label3.Text = "Couldn't start game, check game path";
                return;
            }
        }

        async void CleanupAsync()
        {
            await GameProcess.WaitForExitAsync();
            Gamevoli = Directory.GetDirectories(Gamevotemp).ToList();
            Mevoli = Directory.GetDirectories(Vo).ToList();

            if (!OverrideAll)
            {
                Mevoli.ForEach((string f) =>
                {
                    Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                    {
                        string[] filei = b.Split(@"\");
                        string direc = filei[^2];
                        string file = filei.Last();
                        if (File.Exists($@"{Modvo}\{direc}\{file}")) Directory.Move(b, $@"{Modvo}\{direc}\{file}");
                        if (File.Exists($@"{Gamebanktemp}\{direc}\{file}")) Directory.Move($@"{Gamebanktemp}\{direc}\{file}", $@"{Vo}\{direc}\{file}");
                        Console.WriteLine($@"I/O => Returned VO {direc}\{file}");
                    });
                });
                Gamevoli.ForEach((string f) => Directory.Delete(f));
            }
            else
            {
                Mevoli.ForEach((string f) =>
                {
                    Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                    {
                        string[] filei = b.Split(@"\");
                        string direc = filei[^2];
                        string file = filei.Last();
                        if (Directory.Exists($@"{Gamevotemp}\{direc}"))
                        {
                            Directory.Move(b, $@"{Modvo}\{direc}\{file}");
                            Console.WriteLine($@"I/O => Returned VO {direc}\{file}");
                        }
                    });
                });
                Gamevoli.ForEach((string f) =>
                {
                    Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                    {
                        string[] filei = b.Split(@"\");
                        string direc = filei[^2];
                        string file = filei.Last();
                        Directory.Move(b, $@"{Vo}\{direc}\{file}");
                    });
                    Directory.Delete(f);
                });
            }


            Directory.EnumerateFiles(Gamebanktemp).ToList().ForEach((string s) =>
            {
                string f = s.Split(@"\").Last();
                Directory.Move($@"{Banks}\{f}", $@"{Modbanks}\{f}");
                Directory.Move(s, $@"{Banks}\{f}");
                Console.WriteLine($"I/O => Returned FMOD bank {f}");
            });

            Directory.EnumerateFiles(GamePaks).ToList().ForEach((string s) =>
            {
                if (s.Contains("pakchunk99") || s.Contains("pakchunk-99"))
                {
                    Directory.Move(s, $@"{Paks}\{s.Split(@"\").Last()}");
                    Console.WriteLine($@"I/O => Returned PAK {s.Split(@"\").Last()}");
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartDaGame(true);
        }
    }
}