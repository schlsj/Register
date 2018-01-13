using System;
using System.Management;

namespace Jensen
{
    public class SoftReg
    {
        private string GetDiskVolumeSerialNumber()
        {
            //ManagementClass mc = new ManagementClass("win32_NetworkAdapterConfiguration");
            ManagementObject disk=new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        private string GetCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
            foreach (var myObject in myCpuCollection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
            }
            return strCpu;
        }

        public string GetMNum()
        {
            string strNum = GetCpu() + GetDiskVolumeSerialNumber();
            string strMNum = strNum.Substring(0, 24);
            return strMNum;
        }

        private int[] intCode = new int[127];
        private char[] charCode = new char[25];
        private int[] intNumber = new int[25];

        private void SetIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        public string GetRNum()
        {
            SetIntCode();
            string strMNum = GetMNum();
            for (int i = 1; i < charCode.Length; i++)
            {
                charCode[i] = Convert.ToChar(strMNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)
            {
                intNumber[j] = Convert.ToInt32(charCode[j]) + intCode[Convert.ToInt32(charCode[j])];
            }
            string strAsciiName = "";
            for (int k = 1; k < intNumber.Length; k++)
            {
                if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k] <= 90)
                    || (intNumber[k] >= 97 && intNumber[k] <= 122))
                {
                    strAsciiName += Convert.ToChar(intNumber[k]).ToString();
                }else if (intNumber[k] > 122)
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 10).ToString();
                }
                else
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 9).ToString();
                }
            }
            return strAsciiName;
        }
    }
}
