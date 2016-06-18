using System;
using System.Collections.Generic;

namespace SignalAnalyzer.Ui
{
    public class UnformattedRenderer : IRenderer
    {
        public void Render(List<List<bool>> bytes)
        {
            bytes.ForEach(myBytes => myBytes.ForEach(bit => Console.Write(bit == true ? 1 : 0)));
            Console.WriteLine();
        }
    }
}
