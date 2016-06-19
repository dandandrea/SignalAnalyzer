using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Core.BinaryData
{
    public class BitManipulator
    {
        private Dictionary<char, List<bool>> _asciiTable;

        public BitManipulator()
        {
            // TODO: Is there a better way to convert from chars to arrays of bools?
            _asciiTable = GetAsciiTable();
        }

        public static ICollection<bool> RemoveLeftBits(ICollection<bool> bits, int numberOfBits)
        {
            if (bits == null)
            {
                throw new ArgumentNullException(nameof(bits));
            }

            var newBits = new List<bool>();

            for (var i = 0; i < bits.Count(); i++)
            {
                if (i + 1 <= numberOfBits)
                {
                    continue;
                }

                newBits.Add(bits.ToList()[i]);
            }

            return newBits;
        }

        public static ICollection<bool> RemoveStartStopBits(ICollection<bool> bits)
        {
            if (bits == null)
            {
                throw new ArgumentNullException(nameof(bits));
            }

            var newBits = new List<bool>();

            var bitNumber = 0;
            for (var i = 0; i < bits.Count; i++)
            {
                bitNumber++;

                if (bitNumber == 1)
                {
                    continue;
                }

                if (bitNumber == 10)
                {
                    bitNumber = 0;
                    continue;
                }

                newBits.Add(bits.ToList()[i]);
            }

            return newBits;
        }

        public static List<List<bool>> BitsToBytes(ICollection<bool> bits)
        {
            if (bits == null)
            {
                throw new ArgumentNullException(nameof(bits));
            }

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
            if (incomingBits == null)
            {
                throw new ArgumentNullException(nameof(incomingBits));
            }

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
            if (bits == null)
            {
                throw new ArgumentNullException(nameof(bits));
            }

            bits[bitNumber - 1] = (newValue == 1 ? true : false);
            return bits;
        }

        public static char ByteToChar(List<bool> bits)
        {
            if (bits == null)
            {
                throw new ArgumentNullException(nameof(bits));
            }

            byte val = 0;
            foreach (bool bit in bits)
            {
                val <<= 1;
                if (bit) val |= 1;
            }
            return Convert.ToChar(val);
        }

        public static ICollection<bool> BitStringToBits(string bitString)
        {
            if (bitString == null)
            {
                throw new ArgumentNullException(nameof(bitString));
            }

            var bits = new List<bool>();

            for (var i = 0; i < bitString.Length; i++)
            {
                if (bitString[i] != '0' && bitString[i] != '1')
                {
                    throw new ArgumentOutOfRangeException(nameof(bitString), "String must only contain 0s and 1s");
                }

                if (bitString[i] == '0')
                {
                    bits.Add(false);
                }
                else
                {
                    bits.Add(true);
                }
            }

            return bits;
        }

        public ICollection<bool> StringToBits(string inputString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }

            var bits = new List<bool>();

            for (var i = 0; i < inputString.Length; i++)
            {
                if (! _asciiTable.ContainsKey(inputString[i]))
                {
                    Debug.WriteLine($"WARN: Could not find ASCII bits for character [{inputString[i]}] at index {i}");
                    continue;
                }

                _asciiTable[inputString[i]].ForEach(bit => bits.Add(bit));
            }

            return bits;
        }

        public static string BitsToString(ICollection<bool> bits)
        {
            var outgoingBytes = BitsToBytes(bits);
            var outgoingString = new StringBuilder();

            foreach (var outgoingByte in outgoingBytes)
            {
                outgoingString.Append(ByteToChar(outgoingByte));
            }

            return outgoingString.ToString();
        }

        private Dictionary<char, List<bool>> GetAsciiTable()
        {
            return new Dictionary<char, List<bool>>
            {
                {' ',new List<bool>{false,false,true,false,false,false,false,false}},
                {'!',new List<bool>{false,false,true,false,false,false,false,true}},
                {'"',new List<bool>{false,false,true,false,false,false,true,false}},
                {'#',new List<bool>{false,false,true,false,false,false,true,true}},
                {'$',new List<bool>{false,false,true,false,false,true,false,false}},
                {'%',new List<bool>{false,false,true,false,false,true,false,true}},
                {'&',new List<bool>{false,false,true,false,false,true,true,false}},
                {'\'',new List<bool>{false,false,true,false,false,true,true,true}},
                {'(',new List<bool>{false,false,true,false,true,false,false,false}},
                {')',new List<bool>{false,false,true,false,true,false,false,true}},
                {'*',new List<bool>{false,false,true,false,true,false,true,false}},
                {'+',new List<bool>{false,false,true,false,true,false,true,true}},
                {',',new List<bool>{false,false,true,false,true,true,false,false}},
                {'-',new List<bool>{false,false,true,false,true,true,false,true}},
                {'.',new List<bool>{false,false,true,false,true,true,true,false}},
                {'/',new List<bool>{false,false,true,false,true,true,true,true}},
                {'0',new List<bool>{false,false,true,true,false,false,false,false}},
                {'1',new List<bool>{false,false,true,true,false,false,false,true}},
                {'2',new List<bool>{false,false,true,true,false,false,true,false}},
                {'3',new List<bool>{false,false,true,true,false,false,true,true}},
                {'4',new List<bool>{false,false,true,true,false,true,false,false}},
                {'5',new List<bool>{false,false,true,true,false,true,false,true}},
                {'6',new List<bool>{false,false,true,true,false,true,true,false}},
                {'7',new List<bool>{false,false,true,true,false,true,true,true}},
                {'8',new List<bool>{false,false,true,true,true,false,false,false}},
                {'9',new List<bool>{false,false,true,true,true,false,false,true}},
                {':',new List<bool>{false,false,true,true,true,false,true,false}},
                {';',new List<bool>{false,false,true,true,true,false,true,true}},
                {'<',new List<bool>{false,false,true,true,true,true,false,false}},
                {'=',new List<bool>{false,false,true,true,true,true,false,true}},
                {'>',new List<bool>{false,false,true,true,true,true,true,false}},
                {'?',new List<bool>{false,false,true,true,true,true,true,true}},
                {'@',new List<bool>{false,true,false,false,false,false,false,false}},
                {'A',new List<bool>{false,true,false,false,false,false,false,true}},
                {'B',new List<bool>{false,true,false,false,false,false,true,false}},
                {'C',new List<bool>{false,true,false,false,false,false,true,true}},
                {'D',new List<bool>{false,true,false,false,false,true,false,false}},
                {'E',new List<bool>{false,true,false,false,false,true,false,true}},
                {'F',new List<bool>{false,true,false,false,false,true,true,false}},
                {'G',new List<bool>{false,true,false,false,false,true,true,true}},
                {'H',new List<bool>{false,true,false,false,true,false,false,false}},
                {'I',new List<bool>{false,true,false,false,true,false,false,true}},
                {'J',new List<bool>{false,true,false,false,true,false,true,false}},
                {'K',new List<bool>{false,true,false,false,true,false,true,true}},
                {'L',new List<bool>{false,true,false,false,true,true,false,false}},
                {'M',new List<bool>{false,true,false,false,true,true,false,true}},
                {'N',new List<bool>{false,true,false,false,true,true,true,false}},
                {'O',new List<bool>{false,true,false,false,true,true,true,true}},
                {'P',new List<bool>{false,true,false,true,false,false,false,false}},
                {'Q',new List<bool>{false,true,false,true,false,false,false,true}},
                {'R',new List<bool>{false,true,false,true,false,false,true,false}},
                {'S',new List<bool>{false,true,false,true,false,false,true,true}},
                {'T',new List<bool>{false,true,false,true,false,true,false,false}},
                {'U',new List<bool>{false,true,false,true,false,true,false,true}},
                {'V',new List<bool>{false,true,false,true,false,true,true,false}},
                {'W',new List<bool>{false,true,false,true,false,true,true,true}},
                {'X',new List<bool>{false,true,false,true,true,false,false,false}},
                {'Y',new List<bool>{false,true,false,true,true,false,false,true}},
                {'Z',new List<bool>{false,true,false,true,true,false,true,false}},
                {'[',new List<bool>{false,true,false,true,true,false,true,true}},
                {'\\',new List<bool>{false,true,false,true,true,true,false,false}},
                {']',new List<bool>{false,true,false,true,true,true,false,true}},
                {'a',new List<bool>{false,true,true,false,false,false,false,true}},
                {'b',new List<bool>{false,true,true,false,false,false,true,false}},
                {'c',new List<bool>{false,true,true,false,false,false,true,true}},
                {'d',new List<bool>{false,true,true,false,false,true,false,false}},
                {'e',new List<bool>{false,true,true,false,false,true,false,true}},
                {'f',new List<bool>{false,true,true,false,false,true,true,false}},
                {'g',new List<bool>{false,true,true,false,false,true,true,true}},
                {'h',new List<bool>{false,true,true,false,true,false,false,false}},
                {'i',new List<bool>{false,true,true,false,true,false,false,true}},
                {'j',new List<bool>{false,true,true,false,true,false,true,false}},
                {'k',new List<bool>{false,true,true,false,true,false,true,true}},
                {'l',new List<bool>{false,true,true,false,true,true,false,false}},
                {'m',new List<bool>{false,true,true,false,true,true,false,true}},
                {'n',new List<bool>{false,true,true,false,true,true,true,false}},
                {'o',new List<bool>{false,true,true,false,true,true,true,true}},
                {'p',new List<bool>{false,true,true,true,false,false,false,false}},
                {'q',new List<bool>{false,true,true,true,false,false,false,true}},
                {'r',new List<bool>{false,true,true,true,false,false,true,false}},
                {'s',new List<bool>{false,true,true,true,false,false,true,true}},
                {'t',new List<bool>{false,true,true,true,false,true,false,false}},
                {'u',new List<bool>{false,true,true,true,false,true,false,true}},
                {'v',new List<bool>{false,true,true,true,false,true,true,false}},
                {'w',new List<bool>{false,true,true,true,false,true,true,true}},
                {'x',new List<bool>{false,true,true,true,true,false,false,false}},
                {'y',new List<bool>{false,true,true,true,true,false,false,true}},
                {'z',new List<bool>{false,true,true,true,true,false,true,false}},
                {'{',new List<bool>{false,true,true,true,true,false,true,true}},
                {'}',new List<bool>{false,true,true,true,true,true,false,true}},
                {'|',new List<bool>{false,true,true,true,true,true,true,false}}
            };
        }
    }
}
