using System.Diagnostics;
using System.Reflection;

namespace RONML_UI
{
    internal class IOScripts
    {
        internal static bool LoadVo = true;
        internal static bool LoadFmod = true;
        internal static bool LoadPak = true;
        internal static bool OverrideAll = true;
        internal static bool Loaddx12 = true;
        internal static bool ModsLoaded = false;

        internal static string GamePaks
        {
            get
            {
                return $@"{GamePath}\ReadyOrNot\Content\Paks";
            }
        }
        internal static string Vo
        {
            get
            {
                return $@"{GamePath}\ReadyOrNot\Content\VO";
            }
        }
        internal static string Banks
        {
            get
            {
                return $@"{GamePath}\ReadyOrNot\Content\FMOD\Desktop";
            }
        }
        internal static string Me
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location.TrimEnd(@"RONML-UI.dll".ToCharArray()).Replace("\n", "").Replace("\r", ""); // gotta replace shit or line terminators fuck with I/O
            }
        }
        internal static string Paks
        {
            get
            {
                return $@"{Me}\modcontent\PAKs\";
            }
        }
        internal static string Modvo
        {
            get
            {
                return $@"{Me}\modcontent\VO\";
            }
        }
        internal static string Modbanks
        {
            get
            {
                return $@"{Me}\modcontent\FMOD\";
            }
        }
        internal static string Gamevotemp
        {
            get
            {
                return $@"{Me}\modcontent\VO\GameTemp";
            }
        }
        internal static string Gamebanktemp
        {
            get
            {
                return $@"{Me}\modcontent\FMOD\GameTemp";
            }
        }
        internal static string Importantfilepath
        {
            get
            {
                return $@"{Me}\importantfile.txt";
            }
        }
        internal static string GamePath
        {
            get
            {
                if (!File.Exists(Importantfilepath))
                {
                    var f = File.CreateText(Importantfilepath);
                    f.WriteLine(@"C:\Program Files (x86)\Steam\steamapps\common\Ready Or Not");
                    f.Close();
                }
                return File.ReadAllText(Importantfilepath).Replace("\n", "").Replace("\r", "");
            }
            set
            {
                var f = File.CreateText(Importantfilepath);
                f.WriteLine(value);
                f.Close();
            }
        }

        internal static string[] Gamevoli;
        internal static string[] Mevoli;
        internal static string[] Gamebankli;
        internal static string[] Mebankli;
        internal static Process GameProcess = new();
        internal static void SwitchIO()
        {
            try
            {
                Gamevoli = Directory.GetDirectories(Vo);
                Gamebankli = Directory.GetFiles(Banks);
                Mevoli = Directory.GetDirectories(Modvo);
                Mebankli = Directory.GetFiles(Modbanks);
            }
            catch (Exception e)
            {
                Program.Instance.label3.Text = "Couldn't read important file directories/files, check paths";
                return;
            }

            switch (LoadVo)
            {
                case false:
                    break;
                case true:
                    if (OverrideAll)
                    {
                        Array.ForEach(Gamevoli, (string f) =>
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
                        Array.ForEach(Mevoli, (string f) =>
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
                        Array.ForEach(Gamevoli, (string f) =>
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
                        Array.ForEach(Mevoli, (string f) =>
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
                    Array.ForEach(Mebankli, (string f) =>
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

        internal static void StartDaGame(bool startVanilla)
        {
            try
            {
                Program.Instance.label3.Text = "Starting game...";
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
                Program.Instance.label3.Text = "Couldn't start game, check game path";
                return;
            }
        }

        internal static async void CleanupAsync(bool skipAsync)
        {
            if (!skipAsync) await GameProcess.WaitForExitAsync();
            if (!ModsLoaded) return;
            Program.Instance.label3.Text = "Cleaning up...";
            await Task.Delay(500);
            Gamevoli = Directory.GetDirectories(Gamevotemp);
            Mevoli = Directory.GetDirectories(Vo);

            Array.ForEach(Mevoli, (string f) =>
            {
                string direc = f.Split(@"\").Last();
                if (Directory.Exists($@"{Modvo}\{direc}")) Directory.Delete($@"{Vo}\{direc}", true);
            });
            Array.ForEach(Gamevoli, (string f) =>
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
            Program.Instance.label3.Text = "Cleanup finished";
        }
    }
}
