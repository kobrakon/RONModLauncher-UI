#pragma warning disable IDE1006
namespace RONML_UI
{
    public partial class Form1 : Form
    {
        IOScripts IO = new();

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
            "All fun and games till the host dies and restarts the game",
            "Do you feel like a hero yet?",
            "That moment when a suspect shoots you 15 times but you still get penalized for unauthorized use of force",
            "That moment when a suspect with a tiny .38 penetrates your full suit of AR500 steel armor and kills you",
            "You should always have more firepower than the suspects, go ahead and bring the grenade launcher",
            "Frag grenades when????? Void pls",
            "The quicker you shoot, the quicker they can't",
            "Beanbag shotguns are useless against the body, just ignore the warnings and aim for the head",
            "amogus",
            "Set your NVGs to white phosphor. Do it",
            "why tf can't we sprint but we can jump???\n'it ain't realistic' mf i got a full suit of gear and can run fine in it"
        };

        public Form1()
        {
            InitializeComponent();
            Random random = new();
            label3.Text = RandomText[random.Next(RandomText.Count)];
            Application.ApplicationExit += new EventHandler(ExitEventHandler);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
            richTextBox1.Text = IO.GamePath;
            checkBox1.Checked = IO.LoadVo;
            checkBox2.Checked = IO.LoadPak;
            checkBox3.Checked = IO.LoadFmod;
            checkBox4.Checked = IO.Loaddx12;
            checkBox5.Checked = IO.OverrideAll;
        }

        async void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Transferring files...";
            await Task.Run(IO.SwitchIO);
        }

        async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() => IO.StartDaGame(true));
        }

        void button3_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog dialog = new();
            dialog.InitialDirectory = @"C:\";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                richTextBox1.Text = dialog.SelectedPath;
            }
        }

        async void button4_Click(object sender, EventArgs e)
        {
            await Task.Run(IO.Backup);
        }

        async void button5_Click(object sender, EventArgs e)
        {
            await Task.Run(IO.ImportBackup);
        }

        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IO.LoadVo = !IO.LoadVo;
        }

        void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            IO.LoadPak = !IO.LoadPak;
        }

        void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            IO.LoadFmod = !IO.LoadFmod;
        }

        void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            IO.Loaddx12 = !IO.Loaddx12;
        }

        void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            IO.OverrideAll = !IO.OverrideAll;
        }

        void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            IO.GamePath = richTextBox1.Text;
        }

        void ExitEventHandler(object? sender, EventArgs e) => IO.CleanupAsync(true);

        void ExceptionHandler(object? sender, UnhandledExceptionEventArgs args)
        {
            label3.ForeColor = Color.DarkRed;
            Exception e = (Exception)args.ExceptionObject;
            label3.Text = $"An attempted operation raised an unhandled exception => \n{e.Message}\n{e.StackTrace}";
        }
    }
}
