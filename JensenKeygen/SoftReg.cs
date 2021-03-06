﻿using System;

namespace JensenKeygen
{
    public class SoftReg
    {
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

        public string GetRNum(string strMNum)
        {
            SetIntCode();
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
                }
                else if (intNumber[k] > 122)
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
