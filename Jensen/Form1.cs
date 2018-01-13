using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Jensen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SoftReg softReg = new SoftReg();

        private void button1_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(softReg.GetRNum());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey retkey =
                Registry.CurrentUser.OpenSubKey("SOFTWARE", true)
                    .CreateSubKey("mySoftWare")
                    .CreateSubKey("Register.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == softReg.GetRNum())
                {
                    labRegInfo.Text = "此软件已注册";
                    btnReg.Enabled = false;
                    return;
                }
            }
            labRegInfo.Text = "此软件尚未注册";
            btnReg.Enabled = true;
            MessageBox.Show("您现在使用的是试用版，可以免费使用30次!", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Int32 tLong;
            try
            {
                tLong = (int) Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\mySoftware", "UseTimes", 0);
                MessageBox.Show("您已经使用了" + tLong + "次!", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("欢迎使用本软件", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\mySoftWare", "UseTimes", 0,RegistryValueKind.DWord);
            }
            tLong = (int) Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\mySoftWare", "UseTimes", 0);
            if (tLong < 5)
            {
                int tTimes = tLong + 1;
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\mySoftWare", "UseTimes", tTimes);
            }
            else
            {
                DialogResult result = MessageBox.Show("使用次数已到，您是否需要注册?", "信息", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    //todo
                    FormRegister.State = false;
                    btnReg_Click(sender, e);
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            FormRegister frmRegister=new FormRegister();
            frmRegister.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
