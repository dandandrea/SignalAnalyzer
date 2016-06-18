using System;
using System.Collections.Generic;

namespace SignalAnalyzer.Ui
{
    public class AsciiRenderer : IRenderer
    {
        public void Render(List<List<bool>> bytes)
        {
            foreach (var byteBlock in bytes)
            {
                if (byteBlock.Count < 8)
                {
                    break;
                }

                // var bits = ChangeBit(byteBlock, 8, 0);
                // bits.Reverse();
                Console.Write(BitManipulator.ByteToChar(byteBlock));
            }

            Console.WriteLine();
        }
    }
}
