using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Siticone.Desktop.UI.WinForms;
using Memory;
using System.Threading;
using System.Diagnostics;
using AutoUpdaterDotNET;
using System.Runtime.InteropServices;
using System.IO;
namespace FUTPlus
{
    public partial class FUTPlus : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, UIntPtr lpAddress,
        uint dwSize, int dwFreeType);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        Mem m = new Mem();
        public bool FIFA22 = false;
        public bool FIFA22_Trial = false;
        public bool CARDS = false;
        public bool CARDS_Trial = false;
        public bool STARTWRITEFIFA22 = false;
        public bool STARTWRITEFIFA22_TRIAL = false;
        public bool SKIPSTOP = false;
        public bool INVISIBLESTOP = false;
        public bool NORMALSTOP = false;
        public long KEYBOARD = 0;
        public long CONTROLLER = 0;
        public long TELEPORT = 0;
        public long INSTANT = 0;
        public long KICKF5 = 0;
        public long KICKF4 = 0;
        public long UTEXISTS = 0;
        public long RATINGOPP = 0;
        public long CHEMISTRYOPP = 0;
        public long NAME = 0;
        public long BALLPOSITION = 0;
        public long SKIPSCAN = 0;
        public long DRAFTSPOOF = 0;
        byte[] RESTORETELEPORT = new byte[11];
        byte[] RESTOREINSTANT = new byte[7];
        byte[] RESTOREKICKF4 = new byte[7];
        byte[] RESTOREKICKF5 = new byte[7];
        byte[] RESTORENAME = new byte[24];
        UIntPtr CODECAVESKIP;
        public int CHEMISTRYSHOW;
        public int RATINGSHOW;
        public const int
        PAGE_READWRITE = 0x40,
        PROCESS_VM_OPERATION = 0x0008,
        PROCESS_VM_READ = 0x0010,
        PROCESS_VM_WRITE = 0x0020,
        MEM_COMMIT = 0x00001000,
        MEM_RESERVE = 0x00002000,
        MEM_DECOMMIT = 0x00004000,
        MEM_RELEASE = 0x00008000;
        public FUTPlus()
        {
            InitializeComponent();
            new SiticoneDragControl(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
            int oppHotkeyId = 5;
            int oppHotKeyKey = (int)Keys.Tab;
            Boolean TabRegistered = RegisterHotKey(
                this.Handle, oppHotkeyId, 0x0000, oppHotKeyKey
            );
            AutoUpdater.Start("yourwebsite");
        }
        private async void BGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (FIFA22 == false)
            {
                FIFA22 = m.OpenProcess("FIFA22");
                Thread.Sleep(200);
                if (FIFA22 == false)
                {
                    Process[] pname = Process.GetProcessesByName("FUTPlus");
                    Thread.Sleep(100);
                    Process[] pname1 = Process.GetProcessesByName("FIFA22");
                    Thread.Sleep(100);
                    Process[] pname2 = Process.GetProcessesByName("FIFA22_Trial");
                    Thread.Sleep(100);
                    if (pname.Length != 0 && pname1.Length == 0 || pname2.Length == 0)
                        MessageBox.Show("You need to open FIFA first, then open FUTPlus ! ");
                    foreach (var process in Process.GetProcessesByName("FUTPlus"))
                    {
                        process.Kill();
                    }
                }
                if (FIFA22 == true)
                {
                    bool TABWRITE = m.WriteMemory("  ", "bytes", "  ");
                    bool AFK = m.WriteMemory("0x00000", "bytes", "  ");
                    byte[] newBytes9 = { 0xC3, 0x90, 0x90, 0x90, 0x90 };//desync
                    UIntPtr codecavebase9 = m.CreateCodeCave("  ", newBytes9, 5, 2048);
                    Thread.Sleep(500);
                    KEYBOARD = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                    Thread.Sleep(500);
                    CONTROLLER = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                    Thread.Sleep(500);
                    TELEPORT = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTORETELEPORT = m.ReadBytes(TELEPORT.ToString("X"), 11);
                    Thread.Sleep(500);
                    INSTANT = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTOREINSTANT = m.ReadBytes(INSTANT.ToString("X"), 7);
                    Thread.Sleep(500);
                    KICKF4 = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTOREKICKF4 = m.ReadBytes(KICKF4.ToString("X"), 7);
                    Thread.Sleep(500);
                    KICKF5 = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTOREKICKF5 = m.ReadBytes(KICKF5.ToString("X"), 7);
                    Thread.Sleep(500);
                    if (CONTROLLER == 00 || KEYBOARD == 00)
                    {
                        MessageBox.Show("Failed to initialize ALWAYS WIN ! Restart your game and the CHEAT !");
                        return;
                    }
                    while (FIFA22 == true)
                    {
                        CARDS = m.OpenProcess("CardsDLL_Win64_retail");
                        Thread.Sleep(100);
                        UTEXISTS = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                        Thread.Sleep(100);
                        if (UTEXISTS != 0 && CONTROLLER != 0 && KEYBOARD != 0)
                        {
                            RATINGOPP = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                            Thread.Sleep(200);
                            CHEMISTRYOPP = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                            Thread.Sleep(200);
                            NAME = (await m.AoBScan(0x00000  , 0x00000, "  ", true, false)).FirstOrDefault();
                            RESTORENAME = m.ReadBytes(NAME.ToString("X"), 24);
                            Thread.Sleep(200);
                            if (RATINGOPP != 0 && CHEMISTRYOPP != 0 && CONTROLLER != 0 && KEYBOARD != 0)
                            {
                                MessageBox.Show("SUCCESFULLY LOADED !" + "\nIf you can see this message, it means you can start activating ALWAYS WIN and OPPONENT STATISTICS or any other option");
                                return;
                            }
                            else if (RATINGOPP == 00 || CHEMISTRYOPP == 00)
                            {
                                MessageBox.Show("Failed to initialize OPPONENT STATS ! Restart your game and the CHEAT !");
                                return;
                            }
                        }
                    }
                }
            }
        }
    
        private async void BGWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (FIFA22_Trial == false)
            {
                FIFA22_Trial = m.OpenProcess("FIFA22_Trial");
                Thread.Sleep(200);
                if (FIFA22_Trial == true)
                {
                    bool TABWRITE = m.WriteMemory("  ", "bytes", "  ");
                    bool AFK = m.WriteMemory("0x00000", "bytes", "  ");
                    byte[] newBytes9 = { 0xC3, 0x90, 0x90, 0x90, 0x90 };//desync
                    UIntPtr codecavebase9 = m.CreateCodeCave("  ", newBytes9, 5, 2048);
                    Thread.Sleep(500);
                    KEYBOARD = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                    Thread.Sleep(500);
                    CONTROLLER = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                    Thread.Sleep(500);
                    TELEPORT = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTORETELEPORT = m.ReadBytes(TELEPORT.ToString("X"), 11);
                    Thread.Sleep(500);
                    INSTANT = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTOREINSTANT = m.ReadBytes(INSTANT.ToString("X"), 7);
                    Thread.Sleep(500);
                    KICKF4 = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTOREKICKF4 = m.ReadBytes(KICKF4.ToString("X"), 7);
                    Thread.Sleep(500);
                    KICKF5 = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                    RESTOREKICKF5 = m.ReadBytes(KICKF5.ToString("X"), 7);
                    Thread.Sleep(500);
                    if (CONTROLLER == 00 || KEYBOARD == 00)
                    {
                        MessageBox.Show("Failed to initialize ALWAYS WIN ! Restart your game and the CHEAT !");
                        return;
                        
                    }
                    while (FIFA22_Trial == true)
                    {
                        CARDS = m.OpenProcess("CardsDLL_Win64_retail");
                        Thread.Sleep(100);
                        UTEXISTS = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                        Thread.Sleep(100);
                        if (UTEXISTS != 0 && CONTROLLER != 0 && KEYBOARD != 0)
                        {
                            RATINGOPP = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                            Thread.Sleep(200);
                            CHEMISTRYOPP = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                            Thread.Sleep(200);
                            NAME = (await m.AoBScan(0x00000, 0x00000, "  ", true, false)).FirstOrDefault();
                            RESTORENAME = m.ReadBytes(NAME.ToString("X"), 24);
                            Thread.Sleep(200);
                            if (RATINGOPP != 0 && CHEMISTRYOPP != 0 && CONTROLLER != 0 && KEYBOARD != 0)
                            {
                                MessageBox.Show("SUCCESFULLY LOADED !" + "\nIf you can see this message, it means you can start activating ALWAYS WIN and OPPONENT STATISTICS or any other option");
                                return;
                            }
                            else if (RATINGOPP == 00 || CHEMISTRYOPP == 00)
                            {
                                MessageBox.Show("Failed to initialize OPPONENT STATS ! Restart your game and the CHEAT !");
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void FUTPlus_Shown(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
            BGWorker2.RunWorkerAsync();

        }
        private async void SKIP_CheckedChanged(object sender, EventArgs e)
        {
            if (SKIP.Checked && SKIPSTOP == false)
            {
                SKIPSCAN = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                byte[] newBytes = { 0x00000 };
                CODECAVESKIP = m.CreateCodeCave(SKIPSCAN.ToString("X"), newBytes, 7, 2048);
                SKIPSTOP = true;
            }
            else
            {
                m.WriteMemory(SKIPSCAN.ToString("X"), "bytes", " ");
                VirtualFreeEx(m.mProc.Handle, CODECAVESKIP, 0, MEM_RELEASE);
                SKIPSTOP = false;
            }
        }
        private void INIVISBLENAME_CheckedChanged(object sender, EventArgs e)
        {
            if (INIVISBLENAME.Checked && INVISIBLESTOP == false)
            {

                m.WriteMemory(NAME.ToString("X"), "bytes", " ");
                INVISIBLESTOP = true;
            }
            else
            {
                m.WriteBytes(NAME.ToString("X"), RESTORENAME);
                INVISIBLESTOP = false;
            }
        }
        private async void DRAFTSPOOFER_SelectedValueChanged(object sender, EventArgs e)
        {
            if (DRAFTSPOOFER.SelectedIndex == 0)
            {
                m.WriteMemory("0x00000", "bytes", "    ");
                DRAFTSPOOF = (await m.AoBScan(0x00000, 0x12344, "  ", true, true)).FirstOrDefault();
                byte[] newBytes = { 0xC7, 0x41, 0x38, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x42, 0x3C };
                UIntPtr codecavebase = m.CreateCodeCave(DRAFTSPOOF.ToString("X"), newBytes, 6, 2048);
            }

            if (DRAFTSPOOFER.SelectedIndex == 1)
            {
                m.WriteMemory("0x00000", "bytes", "  ");
                DRAFTSPOOF = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                byte[] newBytes = { 0xC7, 0x41, 0x38, 0x01, 0x00, 0x00, 0x00, 0x8B, 0x42, 0x3C };
                UIntPtr codecavebase = m.CreateCodeCave(DRAFTSPOOF.ToString("X"), newBytes, 6, 2048);
            }
            if (DRAFTSPOOFER.SelectedIndex == 2)
            {
                m.WriteMemory("0x000003", "bytes", "   ");
                DRAFTSPOOF = (await m.AoBScan(0x00000, 0x00000, "  ", true, true)).FirstOrDefault();
                byte[] newBytes = { 0xC7, 0x41, 0x38, 0x02, 0x00, 0x00, 0x00, 0x8B, 0x42, 0x3C };
                UIntPtr codecavebase = m.CreateCodeCave(DRAFTSPOOF.ToString("X"), newBytes, 6, 2048);
            }

            if (DRAFTSPOOFER.SelectedIndex == 3)
            {
                m.WriteMemory("0x00000.dll+0x00000", "bytes", " ");
                DRAFTSPOOF = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                byte[] newBytes = { 0xC7, 0x41, 0x38, 0x03, 0x00, 0x00, 0x00, 0x8B, 0x42, 0x3C };
                UIntPtr codecavebase = m.CreateCodeCave(DRAFTSPOOF.ToString("X"), newBytes, 6, 2048);
            }
            if (DRAFTSPOOFER.SelectedIndex == 4)
            {
                m.WriteMemory("0x00000", "bytes", " ");
                DRAFTSPOOF = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                byte[] newBytes = { 0xC7, 0x41, 0x38, 0x04, 0x00, 0x00, 0x00, 0x8B, 0x42, 0x3C };
                UIntPtr codecavebase = m.CreateCodeCave(DRAFTSPOOF.ToString("X"), newBytes, 6, 2048);
            }
        }
        private async void DIVISIONSPOOFER_SelectedValueChanged(object sender, EventArgs e)
        {
            if (DIVISIONSPOOFER.SelectedIndex == 0)
            {
                long d1 = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                long d2 = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                long d3 = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                long d4 = (await m.AoBScan(0x00000, 0x00000, " ", true, true)).FirstOrDefault();
                byte[] newBytes1 = { };
                UIntPtr codecavebase1 = m.CreateCodeCave(d1.ToString("X"), newBytes1, 7, 2048);
                byte[] newBytes2 = {  };
                UIntPtr codecavebase2 = m.CreateCodeCave(d2.ToString("X"), newBytes2, 10, 2048);
                byte[] newBytes3 = {  };
                UIntPtr codecavebase3 = m.CreateCodeCave(d3.ToString("X"), newBytes3, 7, 2048);
                byte[] newBytes4 = {  };
                UIntPtr codecavebase4 = m.CreateCodeCave(d4.ToString("X"), newBytes4, 7, 2048);
            }
            if (DIVISIONSPOOFER.SelectedIndex == 1)
            {
               
            }
            if (DIVISIONSPOOFER.SelectedIndex == 2)
            {
               
            }
            if (DIVISIONSPOOFER.SelectedIndex == 3)
            {
               
            }
            if (DIVISIONSPOOFER.SelectedIndex == 4)
            {
              
            }
            if (DIVISIONSPOOFER.SelectedIndex == 5)
            {
              
            }
            if (DIVISIONSPOOFER.SelectedIndex == 6)
            {
            
            }
            if (DIVISIONSPOOFER.SelectedIndex == 7)
            {
         
            }
            if (DIVISIONSPOOFER.SelectedIndex == 8)
            {
             
            }

            if (DIVISIONSPOOFER.SelectedIndex == 9)
            {
         
            }
        }
        private async void Always_CheckedChanged(object sender, EventArgs e)
        {
            {
                while (Always.Checked)
                {
                    BALLPOSITION = (await m.AoBScan(0xA10FF794, 0xA3AFF794, " ", true, false)).FirstOrDefault();
                    int KB = m.ReadInt(KEYBOARD.ToString("X"));
                    int CT = m.ReadInt(CONTROLLER.ToString("X"));
                    if (BALLPOSITION != 0 && KB == 00)
                    {
                        byte[] newBytes = {  };
                        UIntPtr HOMETELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        byte[] newBytes1 = {  };
                        UIntPtr HOME2TELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes1, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        m.WriteMemory(TELEPORT.ToString("X"), "bytes", " ");
                        VirtualFreeEx(m.mProc.Handle, HOMETELEPORT, 0, MEM_RELEASE);
                        VirtualFreeEx(m.mProc.Handle, HOME2TELEPORT, 0, MEM_RELEASE);
                       
                    }
                    if (BALLPOSITION != 0 && KB == 01)
                    {
                        byte[] newBytes = {   };
                        UIntPtr AWAYTELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        byte[] newBytes1 = {   };
                        UIntPtr AWAY2TELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes1, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        m.WriteMemory(TELEPORT.ToString("X"), "bytes", " ");
                        VirtualFreeEx(m.mProc.Handle, AWAYTELEPORT, 0, MEM_RELEASE);
                        VirtualFreeEx(m.mProc.Handle, AWAY2TELEPORT, 0, MEM_RELEASE);
                    }
                    if (BALLPOSITION != 0 && CT == 00)
                    {
                        byte[] newBytes = {  };
                        UIntPtr HOMETELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        byte[] newBytes1 = {   };
                        UIntPtr HOME2TELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes1, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        m.WriteMemory(TELEPORT.ToString("X"), "bytes", " ");
                        VirtualFreeEx(m.mProc.Handle, HOMETELEPORT, 0, MEM_RELEASE);
                        VirtualFreeEx(m.mProc.Handle, HOME2TELEPORT, 0, MEM_RELEASE);
                    }
                     if(BALLPOSITION != 0 && CT == 01)
                    {
                        byte[] newBytes = {   };
                        UIntPtr AWAYTELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        byte[] newBytes1 = {   };
                        UIntPtr AWAY2TELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes1, 11, 4096);
                        System.Threading.Thread.Sleep(200);
                        m.WriteMemory(TELEPORT.ToString("X"), "bytes", " ");
                        VirtualFreeEx(m.mProc.Handle, AWAYTELEPORT, 0, MEM_RELEASE);
                        VirtualFreeEx(m.mProc.Handle, AWAY2TELEPORT, 0, MEM_RELEASE);
                    }                 
                }
            }
        }
        private void NORMALNAME_CheckedChanged(object sender, EventArgs e)
        {
            if (NORMALNAME.Checked && NORMALSTOP == false)
            {
                m.WriteMemory(NAME.ToString("X"), "bytes", " ");
                NORMALSTOP = true;
            }
            else
            {
                m.WriteBytes(NAME.ToString("X"), RESTORENAME);
                NORMALSTOP = false;
            }
        }
        private void OPP_Click(object sender, EventArgs e)
        {
            if (OPP.Checked)
            {
                MessageBox.Show("Press TAB to activate it when you found an opponent !");
            }
        }
        private void LOSTF5_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData;
            var converter = new KeysConverter();
            LOSTF5.Text = converter.ConvertToString(e.KeyData);
            int UniqueHotkeyId = 1;
            int HotKeyCode = (int)pressedKey;
            Boolean pressedKeyRegistered = RegisterHotKey(
                this.Handle, UniqueHotkeyId, 0x0000, HotKeyCode
            );
        }
        private void FORFEITF4_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData;
            var converter = new KeysConverter();
            FORFEITF4.Text = converter.ConvertToString(e.KeyData);
            int UniqueHotkeyId = 2;
            int HotKeyCode = (int)pressedKey;
            Boolean pressedKeyRegistered = RegisterHotKey(
                this.Handle, UniqueHotkeyId, 0x0000, HotKeyCode
            );
        }
        private void TELEPORTHOME_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData;
            var converter = new KeysConverter();
            TELEPORTHOME.Text = converter.ConvertToString(e.KeyData);
            int UniqueHotkeyId = 3;
            int HotKeyCode = (int)pressedKey;
            Boolean pressedKeyRegistered = RegisterHotKey(
                this.Handle, UniqueHotkeyId, 0x0000, HotKeyCode
            );
        }
        private void TELEPORTAWAY_KeyDown(object sender, KeyEventArgs e)
        {
            Keys modifierKeys = e.Modifiers;
            Keys pressedKey = e.KeyData;
            var converter = new KeysConverter();
            TELEPORTAWAY.Text = converter.ConvertToString(e.KeyData);
            int UniqueHotkeyId = 4;
            int HotKeyCode = (int)pressedKey;
            Boolean pressedKeyRegistered = RegisterHotKey(
                this.Handle, UniqueHotkeyId, 0x0000, HotKeyCode
            );
        }
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);
            if (message.Msg == 0x0312)
            {
                var id = message.WParam.ToInt32();
                if (id == 1)
                {
                    m.WriteMemory(KICKF5.ToString("X"), "bytes", " ");
                    System.Threading.Thread.Sleep(500);
                    m.WriteMemory(KICKF5.ToString("X"), "bytes", " ");
                }
                else if (id == 2)
                {
                    byte[] newBytes = {  };
                    UIntPtr F4KICK = m.CreateCodeCave(KICKF4.ToString("X"), newBytes, 7, 2048);
                    System.Threading.Thread.Sleep(400);
                    m.WriteMemory(KICKF4.ToString("X"), "bytes", " ");
                    VirtualFreeEx(m.mProc.Handle, F4KICK, 0, MEM_RELEASE);
                }
                else if (id == 3)
                {
                    byte[] newBytes = {   };
                    UIntPtr AWAYTELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes, 11, 4096);
                    System.Threading.Thread.Sleep(200);
                    byte[] newBytes1 = {   };
                    UIntPtr AWAY2TELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes1, 11, 4096);
                    System.Threading.Thread.Sleep(200);
                    m.WriteMemory(TELEPORT.ToString("X"), "bytes", " ");
                    VirtualFreeEx(m.mProc.Handle, AWAYTELEPORT, 0, MEM_RELEASE);
                    VirtualFreeEx(m.mProc.Handle, AWAY2TELEPORT, 0, MEM_RELEASE);
                }
                else if (id == 4)
                {
                    byte[] newBytes = {   };
                    UIntPtr HOMETELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes, 11, 4096);
                    System.Threading.Thread.Sleep(200);
                    byte[] newBytes1 = {   };
                    UIntPtr HOME2TELEPORT = m.CreateCodeCave(TELEPORT.ToString("X"), newBytes1, 11, 4096);
                    System.Threading.Thread.Sleep(200);
                    m.WriteMemory(TELEPORT.ToString("X"), "bytes", " ");
                    VirtualFreeEx(m.mProc.Handle, HOMETELEPORT, 0, MEM_RELEASE);
                    VirtualFreeEx(m.mProc.Handle, HOME2TELEPORT, 0, MEM_RELEASE);

                }
                else if (id == 5)
                {
                    RATINGSHOW = m.ReadInt(RATINGOPP.ToString("X"));
                    CHEMISTRYSHOW = m.ReadInt(CHEMISTRYOPP.ToString("X"));
                    label1.Text = RATINGSHOW.ToString();
                    label2.Text = CHEMISTRYSHOW.ToString();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread worker = new Thread(new ThreadStart(DoSomething));
            worker.Name = "Doer";
            worker.IsBackground = true;
            worker.Start();
        }
        private void DoSomething()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var canttouchProcess = Process.GetProcesses().Where(pr => pr.MainWindowTitle.StartsWith("cheat eng", StringComparison.OrdinalIgnoreCase)); // without '.exe'
            foreach (var process in canttouchProcess)
            {
                Process[] processess = Process.GetProcesses();//Get all the process in your system
                foreach (var process2 in processess)
                {
                    try
                    {
                        Console.WriteLine(process.ProcessName);
                        process2.PriorityClass = ProcessPriorityClass.BelowNormal; //sets all the process to below normal priority
                        process2.Kill();
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message + " :: [ " + process.ProcessName + " ] Could not be killed");
                    }
                }
            }
            var canttouchProcess1 = Process.GetProcesses().Where(pr => pr.MainWindowTitle.StartsWith("canttou", StringComparison.OrdinalIgnoreCase)); // without '.exe'
            foreach (var process in canttouchProcess1)
            {
                Process[] processess = Process.GetProcesses();//Get all the process in your system
                foreach (var process2 in processess)
                {
                    try
                    {
                        Console.WriteLine(process.ProcessName);
                        process2.PriorityClass = ProcessPriorityClass.BelowNormal; //sets all the process to below normal priority
                        process2.Kill();
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message + " :: [ " + process.ProcessName + " ] Could not be killed");
                    }
                }
            }
            var canttouchProcess3 = Process.GetProcesses().Where(pr => pr.MainWindowTitle.StartsWith("phonk", StringComparison.OrdinalIgnoreCase)); // without '.exe'
            foreach (var process in canttouchProcess3)
            {
                Process[] processess = Process.GetProcesses();//Get all the process in your system
                foreach (var process2 in processess)
                {
                    try
                    {
                        Console.WriteLine(process.ProcessName);
                        process2.PriorityClass = ProcessPriorityClass.BelowNormal; //sets all the process to below normal priority
                        process2.Kill();
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message + " :: [ " + process.ProcessName + " ] Could not be killed");
                    }
                }
            }
            var canttouchProcess5 = Process.GetProcesses().Where(pr => pr.MainWindowTitle.StartsWith("porno av", StringComparison.OrdinalIgnoreCase)); // without '.exe'
            foreach (var process in canttouchProcess5)
            {
                Process[] processess = Process.GetProcesses();//Get all the process in your system
                foreach (var process2 in processess)
                {
                    try
                    {
                        Console.WriteLine(process.ProcessName);
                        process2.PriorityClass = ProcessPriorityClass.BelowNormal; //sets all the process to below normal priority
                        process2.Kill();
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message + " :: [ " + process.ProcessName + " ] Could not be killed");
                    }
                }
            }
            FileInfo fis1 = new FileInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            if (fis1.Length < 14680064)
            {
                {
                    Process[] processess = Process.GetProcesses();//Get all the process in your system

                    foreach (var process in processess)
                    {
                        try
                        {
                            Console.WriteLine(process.ProcessName);
                            process.PriorityClass = ProcessPriorityClass.BelowNormal; //sets all the process to below normal priority
                            process.Kill();
                        }
                        catch (Exception E)
                        {
                            Console.WriteLine(E.Message + " :: [ " + process.ProcessName + " ] Could not be killed");
                        }
                    }
                }
            }
            sw.Stop();
        }
    }
}
      