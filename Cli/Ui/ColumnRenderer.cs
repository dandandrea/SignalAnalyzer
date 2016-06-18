using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalAnalyzer.Ui
{
    public class ColumnRenderer : IRenderer
    {
        private int _rowsBeforePause;

        public ColumnRenderer(int rowsBeforePause = 20)
        {
            _rowsBeforePause = rowsBeforePause;
        }

        public void Render(List<List<bool>> bytes)
        {
            foreach (var byteBlock in bytes.Select((value, index) => new { value, index }))
            {
                Console.Write($"{byteBlock.index + 1,3}: ");

                foreach (var bit in byteBlock.value.Select((value, index) => new { value, index }))
                {
                    Console.Write(bit.value == true ? 1 : 0);

                    if (bit.index == 3)
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();

                if ((byteBlock.index + 1) % _rowsBeforePause == 0)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}