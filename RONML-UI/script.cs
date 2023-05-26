using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;

#nullable disable
#pragma warning disable CA1825 // arrays are zero-length initialized cause they're gonna be allocated to later anyway
#pragma warning disable CA1822
namespace RONML_UI
{
    public class IOScripts
    {
        public string[] Mevoli = new string[0];
        public string[] VoList = new string[0];
        public string[] BankList = new string[0];
        public bool ModsLoaded = false;
        public Process GameProcess = new();

        public string Me
        { get => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("\\", "/"); }

        public string ConfigPath
        { get => Me + "/config.xml"; }

        public string GamePaks
        { get => GamePath + "/ReadyOrNot/Content/Paks"; }

        public string Vo
        { get => GamePath + "/ReadyOrNot/Content/VO"; }

        public string Banks
        { get => GamePath + "/ReadyOrNot/Content/FMOD/Desktop"; }

        public string Paks
        { get => Me + "/modcontent/PAKs/"; }

        public string Modvo
        { get => Me + "/modcontent/VO/"; }

        public string Modbanks
        { get => Me + "/modcontent/FMOD/"; }

        public string Gamevotemp
        { get => Me + "/modcontent/VO/GameTemp"; }

        public string Gamebanktemp
        { get => Me + "/modcontent/FMOD/GameTemp"; }

        public string Importantfilepath
        { get => Me + "/importantfile.txt"; }

        public string BackupPath
        { get => Me + "/backup"; }

        public bool OverrideAll
        {
            get => GetConfigValue("OverrideAll");
            set => ChangeConfigValue("OverrideAll", value);
        }

        public bool LoadVo
        {
            get => GetConfigValue("LoadVo");
            set => ChangeConfigValue("LoadVo", value);
        }

        public bool LoadFmod
        {
            get => GetConfigValue("LoadFMOD");
            set => ChangeConfigValue("LoadFMOD", value);
        }

        public bool LoadPak
        {
            get => GetConfigValue("LoadPAK");
            set => ChangeConfigValue("LoadPAK", value);
        }
        
        public bool Loaddx12
        {
            get => GetConfigValue("LoadDX12");
            set => ChangeConfigValue("LoadDX12", value);
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
            if (!Directory.Exists($"{Me}/modcontent"))
            {
                Directory.CreateDirectory(Modvo);
                Directory.CreateDirectory(Paks);
                Directory.CreateDirectory(Modbanks);
                Directory.CreateDirectory(Gamevotemp);
                Directory.CreateDirectory(Gamebanktemp);
            }

            if (!File.Exists(ConfigPath))
                CreateConfig();
        }

        public void SwitchIO()
        {
            try
            {
                VoList = (from f in Directory.GetDirectories(Modvo) where Directory.GetDirectories(Vo).Any(d => Path.GetFileName(d) == Path.GetFileName(f)) select Path.GetFileName(f)).ToArray();
                BankList = (from f in Directory.GetFiles(Banks) where Directory.GetFiles(Modbanks).Any(d => Path.GetFileName(d) == Path.GetFileName(f)) select Path.GetFileName(f)).ToArray();
            }
            catch (Exception)
            {
                ChangeText("Couldn't read important file directories/files, check paths");
                return;
            }

            ChangeText("Transferring files...");

            if (LoadVo)
            {
                foreach (string f in VoList)
                {
                    Directory.CreateDirectory($"{Gamevotemp}/{f}");

                    foreach (string b in Directory.GetFiles($"{Vo}/{f}"))
                    {
                        if (OverrideAll)
                            File.Move(b, $"{Gamevotemp}/{f}/{Path.GetFileName(b)}", true);
                        else File.Copy(b, $"{Gamevotemp}/{f}/{Path.GetFileName(b)}", true);
                    }

                    foreach (string b in Directory.GetFiles($"{Modvo}/{f}"))
                        File.Copy(b, $"{Vo}/{f}/{Path.GetFileName(b)}", true);
                }
            }
            if (LoadFmod) foreach (string f in BankList) File.Replace($"{Modbanks}/{f}", $"{Banks}/{f}", $"{Gamebanktemp}/{f}");
            if (LoadPak)
            {
                foreach(string f in Directory.GetFiles(Paks))
                    File.Copy(f, $"{GamePaks}/{Path.GetFileName(f)}", true);
            }

            ModsLoaded = true;
            StartDaGame();
            return;
        }

        public void StartDaGame()
        {
            try
            {
                ChangeText("Starting game...");
                var proc = new ProcessStartInfo()
                {
                    FileName = $"{GamePath}/ReadyOrNot.exe",
                    Arguments = Loaddx12 ? "dx12" : "dx11",
                    UseShellExecute = true
                };

                GameProcess = Process.Start(proc);

                ChangeText("Game is running...");
                CleanupAsync();
                return;
            }
            catch (Exception)
            {
                ChangeText("Couldn't start game, check game path");
                return;
            }
        }

        public async void CleanupAsync(bool exitEvent = false)
        {
            if (!exitEvent) await GameProcess.WaitForExitAsync();
            if (!ModsLoaded)
            {
                ChangeText("Game closed");
                return;
            }
            ChangeText("Cleaning up...");

            foreach (string f in VoList)
            {
                Directory.Delete($"{Vo}/{f}", true);
                Directory.CreateDirectory($"{Vo}/{f}");
                
                foreach (string b in Directory.GetFiles($"{Gamevotemp}/{f}"))
                    File.Move(b, $"{Vo}/{f}/{Path.GetFileName(b)}");

                Directory.Delete($"{Gamevotemp}/{f}", true);
            }

            foreach (string f in BankList)
                File.Move($"{Gamebanktemp}/{f}", $"{Banks}/{f}", true);

            foreach (string f in Directory.GetFiles(Paks))
                File.Delete($"{GamePaks}/{Path.GetFileName(f)}");

            ModsLoaded = false;
            ChangeText("Cleanup finished");
        }

        public void Terminate()
        {
            if (GameProcess.HasExited)
                ChangeText("No game process to terminate");
            else
                GameProcess.Kill();
        }

        public void Backup()
        {
            ChangeText("Backing up lossable game files, this will take a minute...");
            string[] Gamevoli = Directory.GetDirectories(Vo);
            string[] Gamebankli = Directory.GetFiles(Banks);
            if (!Directory.Exists($"{BackupPath}/VO")) Directory.CreateDirectory($"{BackupPath}/VO");
            if (!Directory.Exists($"{BackupPath}/FMOD")) Directory.CreateDirectory($"{BackupPath}/FMOD");

            foreach (string f in Gamevoli)
            {
                foreach (string b in Directory.GetFiles(f))
                {
                    string direc = Path.GetFileName(Path.GetDirectoryName(b));
                    string file = Path.GetFileName(b);
                    if (!Directory.Exists($"{BackupPath}/VO/{direc}")) Directory.CreateDirectory($"{BackupPath}/VO/{direc}");
                    File.Copy(b, $"{BackupPath}/VO/{direc}/{file}", true);
                }
            }

            foreach (string f in Gamebankli)
            {
                string file = Path.GetFileName(f);
                File.Copy(f, $"{BackupPath}/FMOD/{file}", true);
            }
            Program.Instance.label3.Text = "Backup complete";
        }

        public void ImportBackup()
        {
            ChangeText("Importing backups");
            string[] VoBackups = Directory.GetDirectories($"{BackupPath}/VO");
            string[] BankBackups = Directory.GetFiles($"{BackupPath}/FMOD");

            if (VoBackups == null || VoBackups.Length == 0)
            {
                ChangeText("No backup found, or too much data was missing to be a valid backup.");
                return;
            }

            foreach (string f in VoBackups)
            {
                Directory.Delete($"{Vo}/{Path.GetFileName(f)}", true);
                foreach (string b in Directory.GetFiles(f))
                {
                    string direc = Path.GetFileName(Path.GetDirectoryName(b));
                    string file = Path.GetFileName(b);
                    if (!Directory.Exists($"{Vo}/{direc}")) Directory.CreateDirectory($"{Vo}/{direc}");
                    File.Copy(b, $"{Vo}/{direc}/{file}", true);
                }
            }

            foreach (string f in BankBackups)
            {
                string file = Path.GetFileName(f);
                File.Copy(f, $"{Banks}/{file}", true);
            }

            ChangeText("Import complete");
        }

        // microsoft is being delusional and requiring me to do this again even though the regular way works just fine
        // not me literally pissed out of my mind because it's saying 'cross-thread not valid' when I literally
        // have it marked volatile and I can see that it changed the text just fine in debugging window but broke anyway
        void ChangeText(string message) => Program.Instance.Invoke(() => Program.Instance.label3.Text = message);

        void CreateConfig()
        {
            XElement ops = new("config",
                new XElement("OverrideAll", true),
                new XElement("LoadVo", true),
                new XElement("LoadFMOD", true),
                new XElement("LoadPAK", true),
                new XElement("LoadDX12", true),
                new XElement("PreloadMods", false)
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
