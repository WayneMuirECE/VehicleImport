using OfficeOpenXml;

namespace VehicleImport.Models
{
    public class ImportVehicleByFileType
    {
        public static List<VehicleModel> LoadDataFromCsvOrPsv(string filePath, List<string> errorMessages)
        {
            List<VehicleModel> entries = new List<VehicleModel>();
            string[] lines = File.ReadAllLines(filePath);
            StringSplitter splitter = new StringSplitter();
            string[] headers = splitter.SplitStringByDelimiter(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = splitter.SplitStringByDelimiter(lines[i]);
                if (values[i].Length != headers.Length)
                {
                    errorMessages.Add("This line does not contain all the columns for the header: " + lines[i]);
                    continue;
                }

                VehicleModel entry = new VehicleModel();

                for (int j = 0; j < headers.Length; j++)
                {
                    string property = headers[j];
                    if (property != null)
                    {
                        entry.ColumnData.Add(property, values[j]);
                    }
                }

                entries.Add(entry);
            }

            return entries;
        }

        public Dictionary<string, string> LoadDataFromExcel(string filePath)
        {
            var result = new Dictionary<string, string>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming you're reading the first sheet

                // Read the header row (column names)
                var headerRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column];
                var columnNames = new List<string>();
                foreach (var cell in headerRow)
                {
                    columnNames.Add(cell.Text);
                }

                // Read data rows
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        var columnName = columnNames[col - 1];
                        var cellValue = worksheet.Cells[row, col].Text;

                        // Add to the dictionary
                        result[columnName] = cellValue;
                    }
                }
            }

            return result;
        }
    }
}
