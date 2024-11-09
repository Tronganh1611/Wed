using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPM_QLQuanAo.Helpers
{
    public static class NumberToWordsConverter
    {
        private static readonly string[] Units = { "", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private static readonly string[] Tens = { "", "mười", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };

        public static string ConvertToWords(int number)
        {
            if (number == 0) return "không";
            if (number < 0) return "âm " + ConvertToWords(-number);

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertToWords(number / 1000000) + " triệu ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertToWords(number / 1000) + " ngàn ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertToWords(number / 100) + " trăm ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "lẻ ";

                if (number < 10)
                    words += Units[number];
                else if (number < 20)
                    words += "mười " + Units[number - 10];
                else
                {
                    words += Tens[number / 10];
                    if ((number % 10) > 0)
                        words += " " + Units[number % 10];
                }
            }

            return words.Trim();
        }
    }
}