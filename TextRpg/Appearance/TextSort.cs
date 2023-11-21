using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Appearance
{
    internal class TextSort
    {
        // 인벤토리, 상점 padding값
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; //  한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; //  나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            // 텍스트의 진짜 길이를 구함
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

        public static int GetPrintableLengthNum(int num)
        {
            int length = 0;
            string str = num.ToString();
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; //  한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; //  나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedNum(int num, int totalLength)
        {
            // 텍스트의 진짜 길이를 구함
            int currentLength = GetPrintableLengthNum(num);
            int padding = totalLength - currentLength;
            return num.ToString().PadRight(num.ToString().Length + padding);
        }

    }
}
