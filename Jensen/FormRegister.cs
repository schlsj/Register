using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Jensen
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        public static bool State = true;
        SoftReg softReg=new SoftReg();

        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLicence.Text == softReg.GetRNum())
                {
                    MessageBox.Show("注册成功！重启软件后生效", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RegistryKey retKey =
                        Registry.CurrentUser.OpenSubKey("Software", true)
                            .CreateSubKey("mySoftware")
                            .CreateSubKey("Register.INI")
                            .CreateSubKey(txtLicence.Text);
                    retKey.SetValue("UserName", "Rsoft");
                    Close();
                }
                else
                {
                    MessageBox.Show("注册码错误!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLicence.SelectAll();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (State)
            {
                Close();
            }
            else
            {
                Application.Exit();
            }
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            txtHardware.Text = softReg.GetMNum();
        }
    }
}
