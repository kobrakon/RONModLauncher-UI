#pragma warning disable IDE1006
namespace RONML_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Random random = new();
            label3.Text = RandomText[random.Next(RandomText.Count)];
            Application.ApplicationExit += new EventHandler(ExitEventHandler);
            richTextBox1.Text = IOScripts.GamePath;
            if (!Directory.Exists($@"{IOScripts.Me}\modcontent"))
            {
                Directory.CreateDirectory(IOScripts.Modvo);
                Directory.CreateDirectory(IOScripts.Paks);
                Directory.CreateDirectory(IOScripts.Modbanks);
                Directory.CreateDirectory(IOScripts.Gamevotemp);
                Directory.CreateDirectory(IOScripts.Gamebanktemp);
            }
        }

        async void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Starting file transfers";
            await Task.Run(() => IOScripts.SwitchIO());
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IOScripts.LoadVo = !IOScripts.LoadVo;
        }

        void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            IOScripts.LoadFmod = !IOScripts.LoadFmod;
        }

        void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            IOScripts.LoadPak = !IOScripts.LoadPak;
        }

        void label1_Click(object sender, EventArgs e)
        {
            return;
        }

        void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            IOScripts.GamePath = richTextBox1.Text;
        }

        void label3_Click(object sender, EventArgs e)
        {
            return;
        }

        void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            IOScripts.Loaddx12 = !IOScripts.Loaddx12;
        }

        readonly List<string> RandomText = new()
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
            IOScripts.OverrideAll = !IOScripts.OverrideAll;
        }

        async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => IOScripts.StartDaGame(true));
        }

        void ExitEventHandler(object? sender, EventArgs e) => Task.Run(() => IOScripts.CleanupAsync(true));

        private void button3_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            dialog.InitialDirectory = @"C:\";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                richTextBox1.Text = dialog.SelectedPath;
            }
        }
    }
}