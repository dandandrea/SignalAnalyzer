using System.Collections.Generic;

namespace SignalAnalyzer.Ui
{
    public interface IRenderer
    {
        void Render(List<List<bool>> bytes);
    }
}
