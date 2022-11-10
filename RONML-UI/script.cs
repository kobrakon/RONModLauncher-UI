using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Reflection;

namespace RONML_UI
{
    public class IOScripts
    {
        public string[] Mevoli;
        public string[] VoList;
        public string[] BankList;
        public bool ModsLoaded = false;
        public Process GameProcess = new();

        public string Me
        { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("\\", "/"); } }

        public string ConfigPath
        { get => $"{Me}/config.xml"; }

        public string GamePaks
        { get { return $"{GamePath}/ReadyOrNot/Content/Paks"; } }

        public string Vo
        { get { return $"{GamePath}/ReadyOrNot/Content/VO"; } }

        public string Banks
        { get { return $"{GamePath}/ReadyOrNot/Content/FMOD/Desktop"; } }

        public string Paks
        { get { return $"{Me}/modcontent/PAKs/"; } }

        public string Modvo
        { get { return $"{Me}/modcontent/VO/"; } }

        public string Modbanks
        { get { return $"{Me}/modcontent/FMOD/"; } }

        public string Gamevotemp
        { get { return $"{Me}/modcontent/VO/GameTemp"; } }

        public string Gamebanktemp
        { get { return $"{Me}/modcontent/FMOD/GameTemp"; } }

        public string Importantfilepath
        { get { return $"{Me}/importantfile.txt"; } }

        public string BackupPath
        { get { return $"{Me}/backup"; } }

        public bool OverrideAll
        {
            get
            {
                return GetConfigValue("OverrideAll");
            }
            set
            {
                ChangeConfigValue("OverrideAll", value);
            }
        }

        public bool LoadVo
        {
            get
            {
                return GetConfigValue("LoadVo");
            }
            set
            {
                ChangeConfigValue("LoadVo", value);
            }
        }

        public bool LoadFmod
        {
            get
            {
                return GetConfigValue("LoadFMOD");
            }
            set
            {
                ChangeConfigValue("LoadFMOD", value);
            }
        }

        public bool LoadPak
        {
            get
            {
                return GetConfigValue("LoadPAK");
            }
            set
            {
                ChangeConfigValue("LoadPAK", value);
            }
        }
        
        public bool Loaddx12
        {
            get
            {
                return GetConfigValue("LoadDX12");
            }
            set
            {
                ChangeConfigValue("LoadDX12", value);
            }
        }

        public string GamePath
        {
            get
            {
                if (!File.Exists(Importantfilepath))
                {
                    var f = File.CreateText(Importantfilepath);
                    f.WriteLine("C:/Program Files (x86)/Steam/steamapps/common/Ready Or Not");
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

        public IOScripts()
        {
            if (!Directory.Exists($@"{Me}/modcontent"))
            {
                Directory.CreateDirectory(Modvo);
                Directory.CreateDirectory(Paks);
                Directory.CreateDirectory(Modbanks);
                Directory.CreateDirectory(Gamevotemp);
                Directory.CreateDirectory(Gamebanktemp);
            }

            if (!File.Exists(ConfigPath))
            {
                CreateConfig();
            }
        }
        
        public void SwitchIO()
        {
            try
            {
                VoList = (from f in Directory.GetDirectories(Modvo) where Directory.GetDirectories(Vo).Any(d => Path.GetFileName(d) == Path.GetFileName(f)) select Path.GetFileName(f)).ToArray();
                BankList = (from f in Directory.GetFiles(Banks) where Directory.GetFiles(Modbanks).Any(d => Path.GetFileName(d) == Path.GetFileName(f)) select Path.GetFileName(f)).ToArray();
            }
            catch (Exception e)
            {
                UpdateText("Couldn't read important file directories/files, check paths");
                return;
            }

            if (LoadVo)
            {
                Array.ForEach(VoList, (string f) =>
                {
                    Directory.CreateDirectory($"{Gamevotemp}/{f}");
                    Array.ForEach(Directory.GetFiles($"{Vo}/{f}"), (string b) =>
                    {
                        string file = Path.GetFileName(b);
                        if (OverrideAll) File.Move(b, $"{Gamevotemp}/{f}/{file}");
                        else File.Copy(b, $"{Gamevotemp}/{f}/{file}");
                    });
                     
                    Array.ForEach(Directory.GetFiles($"{Modvo}/{f}"), (string b) =>
                    {
                        string file = Path.GetFileName(b);
                        File.Copy(b, $"{Vo}/{f}/{file}", true);
                    });
                });
            }
            if (LoadFmod) Array.ForEach(BankList, (string f) => File.Replace($"{Modbanks}/{f}", $"{Banks}/{f}", $"{Gamebanktemp}/{f}"));
            if (LoadPak)
            {
                Array.ForEach(Directory.GetFiles(Paks), (string f) =>
                {
                    File.Copy(f, $"{GamePaks}/{Path.GetFileName(f)}", true);
                });
            }

            ModsLoaded = true;
            StartDaGame(false);
            return;
        }

        public async void StartDaGame(bool startVanilla)
        {
            try
            {
                UpdateText("Starting game...");
                var proc = new ProcessStartInfo()
                {
                    FileName = $"{GamePath}/ReadyOrNot.exe",
                    Arguments = Loaddx12 ? "dx12" : "dx11",
                    UseShellExecute = true
                };
                GameProcess = Process.Start(proc);
                await Task.Delay(1000);
                if (GameProcess.HasExited)
                {
                    UpdateText("Steam wasn't started so the process was killed immediately, trying again in 10 seconds...");
                    await Task.Delay(10000);
                    GameProcess = Process.Start(proc);
                }
                UpdateText("Game is running...");
                CleanupAsync(false);
                return;
            }
            catch (Exception e)
            {
                UpdateText("Couldn't start game, check game path");
                return;
            }
        }

        public async void CleanupAsync(bool skipAsync)
        {
            if (!skipAsync) await GameProcess.WaitForExitAsync();
            if (!ModsLoaded)
            {
                UpdateText("Game closed");
                return;
            }
            UpdateText("Cleaning up...");

            Array.ForEach(VoList, (string f) =>
            {
                Directory.Delete($"{Vo}/{f}", true);
                Directory.CreateDirectory($"{Vo}/{f}");
                Array.ForEach(Directory.GetFiles($"{Gamevotemp}/{f}"), (string b) =>
                {
                    string file = Path.GetFileName(b);
                    File.Move(b, $"{Vo}/{f}/{file}");
                });
                Directory.Delete($"{Gamevotemp}/{f}", true);
            });

            Array.ForEach(BankList, (string f) =>
            {
                File.Move($"{Gamebanktemp}/{f}", $"{Banks}/{f}", true);
            });

            Array.ForEach(Directory.GetFiles(Paks), (string f) =>
            {
                File.Delete($"{GamePaks}/{Path.GetFileName(f)}");
            });
            ModsLoaded = false;
            UpdateText("Cleanup finished");
        }

        public void Backup()
        {
            UpdateText("Backing up lossable game files, this will take a minute...");
            string[] Gamevoli = Directory.GetDirectories(Vo);
            string[] Gamebankli = Directory.GetFiles(Banks);
            if (!Directory.Exists($"{BackupPath}/VO")) Directory.CreateDirectory($"{BackupPath}/VO");
            if (!Directory.Exists($"{BackupPath}/FMOD")) Directory.CreateDirectory($"{BackupPath}/FMOD");

            Array.ForEach(Gamevoli, (string f) => 
            {
                Array.ForEach(Directory.GetFiles(f), (string b) =>
                {
                    string direc = Path.GetFileName(Path.GetDirectoryName(b));
                    string file = Path.GetFileName(b);
                    if (!Directory.Exists($"{BackupPath}/VO/{direc}")) Directory.CreateDirectory($"{BackupPath}/VO/{direc}");
                    File.Copy(b, $"{BackupPath}/VO/{direc}/{file}", true);
                });
            });

            Array.ForEach(Gamebankli, (string f) => 
            {
                string file = Path.GetFileName(f);
                File.Copy(f, $"{BackupPath}/FMOD/{file}", true);
            });
            UpdateText("Backup complete");
        }

        public void ImportBackup()
        {
            Program.Instance.label3.Text = "Importing backups";
            string[] VoBackups = Directory.GetDirectories($"{BackupPath}/VO");
            string[] BankBackups = Directory.GetFiles($"{BackupPath}/FMOD");

            if (VoBackups == null || VoBackups.Length == 0)
            {
                UpdateText("No backup found, or too much data was missing to be a valid backup.");
                return;
            }

            Array.ForEach(VoBackups, (string f) => 
            {
                Directory.Delete($"{Vo}/{Path.GetFileName(f)}", true);
                Array.ForEach(Directory.GetFiles(f), (string b) =>
                {
                    string direc = Path.GetFileName(Path.GetDirectoryName(b));
                    string file = Path.GetFileName(b);
                    if (!Directory.Exists($"{Vo}/{direc}")) Directory.CreateDirectory($"{Vo}/{direc}");
                    File.Copy(b, $"{Vo}/{direc}/{file}", true);
                });
            });

            Array.ForEach(BankBackups, (string f) => 
            {
                string file = Path.GetFileName(f);
                File.Copy(f, $"{Banks}/{file}", true);
            });

            UpdateText("Import complete");
        }

        void UpdateText(string message)
        {
            Program.Instance.label3.Invoke(() => Program.Instance.label3.Text = message);
        }

        void CreateConfig()
        {
            XElement ops = new XElement("config",
                new XElement("OverrideAll", true),
                new XElement("LoadVo", true),
                new XElement("LoadFMOD", true),
                new XElement("LoadPAK", true),
                new XElement("LoadDX12", true)
            );

            ops.Save(ConfigPath);
        }

        bool GetConfigValue(string node)
        {
            if (!File.Exists(ConfigPath)) CreateConfig();
            XmlDocument file = new();
            file.Load(ConfigPath);
            return bool.Parse(file.GetElementsByTagName(node)[0].InnerText);
        }

        void ChangeConfigValue(string node, bool val)
        {
            if (!File.Exists(ConfigPath)) CreateConfig();
            XmlDocument file = new();
            file.Load(ConfigPath);
            file.GetElementsByTagName(node)[0].InnerText = val.ToString();
            file.Save(ConfigPath);
        }
    }
}
