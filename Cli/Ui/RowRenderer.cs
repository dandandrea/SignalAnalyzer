using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalAnalyzer.Ui
{
    public class RowRenderer : IRenderer
    {
        private int _blocksPerRow;

        public RowRenderer(int blocksPerRow = 8)
        {
            _blocksPerRow = blocksPerRow;
        }

        public void Render(List<List<bool>> bytes)
        {
            foreach (var byteBlock in bytes.Select((value, index) => new { value, index }))
            {
                if ((byteBlock.index) % _blocksPerRow == 0)
                {
                    Console.Write($"{byteBlock.index + 1,3}:  ");
                }

                foreach (var bit in byteBlock.value.Select((value, index) => new { value, index }))
                {
                    Console.Write(bit.value == true ? 1 : 0);

                    if ((bit.index + 1) % 4 == 0)
                    {
                        Console.Write(" ");
                    }

                    if ((bit.index + 1) % 8 == 0)
                    {
                        Console.Write(" ");
                    }
                }

                if ((byteBlock.index + 1) % (_blocksPerRow) == 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
