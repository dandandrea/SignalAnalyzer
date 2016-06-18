using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalAnalyzer.Ui
{
    public class BitManipulator
    {
        public static List<List<bool>> BitsToBytes(ICollection<bool> bits)
        {
            var bytes = new List<List<bool>>();

            foreach (var bit in bits.Select((value, index) => new { value, index }))
            {
                if (bit.index == 0 || bit.index % 8 == 0)
                {
                    bytes.Add(new List<bool>());
                }

                bytes.ElementAt(bytes.Count - 1).Add(bit.value);
            }

            return bytes;
        }

        public static ICollection<bool> RemoveBit(ICollection<bool> incomingBits, int bitNumber)
        {
            var bits = new List<bool>();

            var n = 0;
            foreach (var bit in incomingBits)
            {
                n++;

                if (n == bitNumber)
                {
                    continue;
                }

                bits.Add(bit);
            }

            return bits;
        }

        public static List<bool> ChangeBit(List<bool> bits, int bitNumber, int newValue)
        {
            bits[bitNumber - 1] = (newValue == 1 ? true : false);
            return bits;
        }

        public static char ByteToChar(List<bool> bits)
        {
            byte val = 0;
            foreach (bool bit in bits)
            {
                val <<= 1;
                if (bit) val |= 1;
            }
            return Convert.ToChar(val);
        }
    }
}
