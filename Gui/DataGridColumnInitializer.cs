using System.Windows.Forms;

namespace Gui
{
    public class DataGridColumnInitializer
    {
        public static void InitializeColumns(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.ColumnCount = 9;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var columnNumber = 0;

            dataGridView.Columns[columnNumber].Name = "Baud rate";
            dataGridView.Columns[columnNumber].HeaderText = "Baud rate";
            dataGridView.Columns[columnNumber].DataPropertyName = "BaudRate";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Number of symbols (typically bits) per second";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "Boost (Hz)";
            dataGridView.Columns[columnNumber].HeaderText = "Boost (Hz)";
            dataGridView.Columns[columnNumber].DataPropertyName = "BoostFrequencyAmount";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Amount (in Hz) that the original space and mark frequencies were increased by";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "Avg diff (Hz)";
            dataGridView.Columns[columnNumber].HeaderText = "Avg diff (Hz)";
            dataGridView.Columns[columnNumber].DataPropertyName = "AverageFrequencyDifference";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Format = "0.0";
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Average difference (in Hz) of expected space or mark frequency and actual detected frequency";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "Min diff (Hz)";
            dataGridView.Columns[columnNumber].HeaderText = "Min diff (Hz)";
            dataGridView.Columns[columnNumber].DataPropertyName = "MinimumFrequencyDifference";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Format = "0.0";
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Minimum difference (in Hz) of expected space or mark frequency and actual detected frequency";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "Max diff (Hz)";
            dataGridView.Columns[columnNumber].HeaderText = "Max diff (Hz)";
            dataGridView.Columns[columnNumber].DataPropertyName = "MaximumFrequencyDifference";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Format = "0.0";
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Maximum difference (in Hz) of expected space or mark frequency and actual detected frequency";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "# > tolerance";
            dataGridView.Columns[columnNumber].HeaderText = "# > tolerance";
            dataGridView.Columns[columnNumber].DataPropertyName = "NumberOfFrequencyDifferences";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Number of times that the detected frequency was outside of the supplied frequency deviation tolerance";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "# zero freq";
            dataGridView.Columns[columnNumber].HeaderText = "# zero freq";
            dataGridView.Columns[columnNumber].DataPropertyName = "NumberOfZeroFrequencies";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Number of times that the detected frequency was zero";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "Output";
            dataGridView.Columns[columnNumber].HeaderText = "Output";
            dataGridView.Columns[columnNumber].DataPropertyName = "ResultingString";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "The string that resulted from decoding the encoded signal";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
            columnNumber++;

            dataGridView.Columns[columnNumber].Name = "Match?";
            dataGridView.Columns[columnNumber].HeaderText = "Match?";
            dataGridView.Columns[columnNumber].DataPropertyName = "Matched";
            dataGridView.Columns[columnNumber].Frozen = false;
            dataGridView.Columns[columnNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[columnNumber].ToolTipText = "Whether or not the decoded signal matched the encoded signal";
            dataGridView.Columns[columnNumber].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
