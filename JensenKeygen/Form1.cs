using System;
using System.Windows.Forms;

namespace JensenKeygen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SoftReg softReg=new SoftReg();

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string strHareware = txtHardware.Text;
                string strLicence = softReg.GetRNum(strHareware);
                txtLicence.Text = strLicence;
            }
            catch
            {
                MessageBox.Show("输入的机器码格式错误!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
