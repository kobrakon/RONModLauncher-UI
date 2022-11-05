using System.Diagnostics;
using System.Reflection;

namespace RONML_UI
{
    public partial class Form1 : Form
    {
        bool LoadVo = true;
        bool LoadFmod = true;
        bool LoadPak = true;
        bool OverrideAll = true;
        bool Loaddx12 = true;
        bool ModsLoaded = false;

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
        string Paks
        {
            get
            {
                return $@"{Me}\modcontent\PAKs\";
            }
        }
        string Modvo
        {
            get
            {
                return $@"{Me}\modcontent\VO\";
            }
        }
        string Modbanks
        {
            get
            {
                return $@"{Me}\modcontent\FMOD\";
            }
        }
        string Gamevotemp
        {
            get
            {
                return $@"{Me}\modcontent\VO\GameTemp";
            }
        }
        string Gamebanktemp
        {
            get
            {
                return $@"{Me}\modcontent\FMOD\GameTemp";
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
                }
                else return File.ReadAllText(Importantfilepath).Replace("\n", "").Replace("\r", "");
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
            Application.ApplicationExit += new EventHandler(this.ExitEventHandler);
            if (!Directory.Exists($@"{Me}\modcontent"))
            {
                Directory.CreateDirectory(Modvo);
                Directory.CreateDirectory(Paks);
                Directory.CreateDirectory(Modbanks);
                Directory.CreateDirectory(Gamevotemp);
                Directory.CreateDirectory(Gamebanktemp);
            }
        }

        async void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Starting file transfers";
            await Task.Delay(500);
            SwitchIO();
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadVo = !LoadVo;
        }

        void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            LoadFmod = !LoadFmod;
        }

        void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            LoadPak = !LoadPak;
        }

        void label1_Click(object sender, EventArgs e)
        {
            return;
        }

        void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            GamePath = richTextBox1.Text;
        }

        void label3_Click(object sender, EventArgs e)
        {
            return;
        }

        void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Loaddx12 = !Loaddx12;
        }

        List<string> RandomText = new()
        {
            "Still better than mod.io",
            "See you in 2 minutes after you get insta-headshotted by a crackhead",
            "I knew you'd come back",
            "Miss me already?",
            "Shotguns are still ass btw",
            "Still better than Nexus Mods Launcher",
            "ur mom lmao",
            "Civilians are just obstacles with health bars",
            "When kicking a door, remember there is a chance of blowing up",
            "Reminder: Going non-lethal is more of a pain for you than the enemy",
            "Haha pepper spray goes PSSSSSSSHHHH",
            "AHHH FUCK HE HAS A KNIF-",
            "Battering ram goes brrrrrrr",
            "JHP vs AP is what matters on what is kept in the target and what goes through the target",
            "That moment when you go to arrest Voll and accidently pull out your .45 instead of a taser",
            "Five Guys ARs and Nines",
            "I wanna use the MP5/10mm, but the bolt release thing is cursed",
            "Remember: Evil is never dead enough",
            "All fun and games till the host dies and restarts the game"
        };

        void label2_Click(object sender, EventArgs e)
        {
            return;
        }

        void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            OverrideAll = !OverrideAll;
        }

        void button2_Click(object sender, EventArgs e)
        {
            StartDaGame(true);
        }

        void SwitchIO()
        {
            try
            {
                Gamevoli = Directory.GetDirectories(Vo).ToList();
                Gamebankli = Directory.GetFiles(Banks).ToList();
                Mevoli = Directory.GetDirectories(Modvo).ToList();
                Mebankli = Directory.GetFiles(Modbanks).ToList();
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
                                    File.Move(b, $@"{Gamevotemp}\{direc}\{file}");
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
                                    File.Copy($@"{Modvo}\{direc}\{file}", $@"{Vo}\{direc}\{file}", true);
                                }
                            });
                        });
                    }
                    else
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
                                    File.Copy(b, $@"{Gamevotemp}\{direc}\{file}");
                                }
                            });
                        });
                        Mevoli.ForEach((string f) =>
                        {
                            Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                            {
                                string[] filei = b.Split(@"\");
                                string filef = filei[^2];
                                string fileo = filei.Last();
                                Directory.CreateDirectory($@"{Gamevotemp}\{filef}");
                                if (File.Exists($@"{Vo}\{filef}\{fileo}"))
                                {
                                    File.Copy($@"{Vo}\{filef}\{fileo}", $@"{Gamevotemp}\{filef}\{fileo}", true);
                                    File.Copy(b, $@"{Vo}\{filef}\{fileo}", true);
                                }
                            });
                        });
                    }
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
                        File.Copy($@"{Banks}\{filei}", $@"{Gamebanktemp}\{filei}", true);
                        File.Copy(f, $@"{Banks}\{filei}", true);
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
                        File.Copy(f, $@"{GamePaks}\{f.Split(@"\").Last()}", true);
                    });
                    break;
            }
            ModsLoaded = true;
            StartDaGame(false);
            return;
        }

        void StartDaGame(bool startVanilla)
        {
            try
            {
                label3.Text = "Starting game...";
                var proc = new ProcessStartInfo()
                {
                    FileName = $@"{GamePath}\ReadyOrNot.exe",
                    Arguments = Loaddx12 ? "dx12" : "dx11",
                    UseShellExecute = true
                };
                GameProcess = Process.Start(proc);
                if (!startVanilla) CleanupAsync(false);
                return;
            }
            catch (Exception e)
            {
                label3.Text = "Couldn't start game, check game path";
                return;
            }
        }

        async void CleanupAsync(bool skipAsync)
        {
            if (!skipAsync) await GameProcess.WaitForExitAsync();
            if (!ModsLoaded) return;
            label3.Text = "Cleaning up...";
            await Task.Delay(500);
            Gamevoli = Directory.GetDirectories(Gamevotemp).ToList();
            Mevoli = Directory.GetDirectories(Vo).ToList();

            Mevoli.ForEach((string f) =>
            {
                string direc = f.Split(@"\").Last();
                if (Directory.Exists($@"{Modvo}\{direc}")) Directory.Delete($@"{Vo}\{direc}", true);
            });
            Gamevoli.ForEach((string f) =>
            {
                Directory.EnumerateFiles(f).ToList().ForEach((string b) =>
                {
                    string[] filei = b.Split(@"\");
                    string direc = filei[^2];
                    string file = filei.Last();
                    if (!Directory.Exists($@"{Vo}\{direc}")) Directory.CreateDirectory($@"{Vo}\{direc}");
                    File.Copy(b, $@"{Vo}\{direc}\{file}", true);
                });
                Directory.Delete(f, true);
            });

            Directory.EnumerateFiles(Gamebanktemp).ToList().ForEach((string s) =>
            {
                string f = s.Split(@"\").Last();
                File.Copy(s, $@"{Banks}\{f}", true);
            });

            Directory.EnumerateFiles(Paks).ToList().ForEach((string s) =>
            {
                File.Delete($@"{GamePaks}\{s.Split(@"\").Last()}");
            });
            ModsLoaded = false;
            label3.Text = "Cleanup finished";
        }

        void ExitEventHandler(object? sender, EventArgs e) => CleanupAsync(true);

        private void button3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.InitialDirectory = @"C:\";
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    richTextBox1.Text = dialog.SelectedPath;
                }
            }
        }
    }
}