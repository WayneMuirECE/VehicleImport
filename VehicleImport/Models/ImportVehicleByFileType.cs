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
                    string columnName = headers[j];
                    if (columnName != null)
                    {
                        entry.ColumnData.Add(columnName, values[j]);
                    }
                }

                entries.Add(entry);
            }

            return entries;
        }

        public List<VehicleModel> LoadDataFromExcel(string filePath)
        {
            List<VehicleModel> entries = new List<VehicleModel>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming you're reading the first sheet

                // Read the header row (column names)
                ExcelRange headerRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column];
                List<string> headerNames = new List<string>();
                foreach (ExcelRangeBase cell in headerRow)
                {
                    headerNames.Add(cell.Text);
                }

                // Read data rows
                for (int rowNumber = 2; rowNumber <= worksheet.Dimension.End.Row; rowNumber++)
                {
                    VehicleModel entry = new VehicleModel();

                    for (int colNumber = 1; colNumber <= worksheet.Dimension.End.Column; colNumber++)
                    {
                        string columnName = headerNames[colNumber - 1];
                        string cellValue = !string.IsNullOrWhiteSpace(worksheet.Cells[rowNumber, colNumber].Text) ? worksheet.Cells[rowNumber, colNumber].Text : string.Empty;

                        // Add to the dictionary
                        entry.ColumnData.Add(columnName, cellValue);
                    }

                    entries.Add(entry);
                }
            }

            return entries;
        }
    }
}
