using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gui
{
    public class CsvExporter
    {
        public static string ExportToCsv(DataGridView dataGridView)
        {
            var csvStringBuilder = new StringBuilder();

            var headers = dataGridView.Columns.Cast<DataGridViewColumn>();
            csvStringBuilder.AppendLine(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"").ToArray()));

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                csvStringBuilder.AppendLine(string.Join(",", cells.Select(cell => "\"" + cell.Value.ToString().Replace("\"", "") + "\"").ToArray()));
            }

            return csvStringBuilder.ToString();
        }
    }
}
