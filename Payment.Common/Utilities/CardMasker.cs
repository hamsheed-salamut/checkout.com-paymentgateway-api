using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Common.Utilities
{
    public class CardMasker
    {
        public static string Mask(string cardNumber)
        {
            var firstDigits = cardNumber.Substring(0, 6);
            var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

            var requiredMask = new String('X', cardNumber.Length - firstDigits.Length - lastDigits.Length);

            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);

            return maskedString;
        }
    }
}
