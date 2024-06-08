using VehicleImport.Models;

namespace VehicleImport
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            // Set the filter to allow CSV, PSV, TXT, XLS, and XLSX file types
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|PSV files (*.psv)|*.psv|TXT files (*.txt)|*.txt|Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = false; // Set to true if you want to allow multiple file selections

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the path of specified file
                string filePath = openFileDialog1.FileName;

                // Call the LoadData method
                ImportVehicleByFileType fileReader = new ImportVehicleByFileType();
                List<string> errorMessages = new List<string>();
                List<VehicleModel> vehicleList = fileReader.LoadData(filePath, errorMessages);
                vehicleList = vehicleList.OrderByDescending(v => v.ID).ToList();

                consoleTextBox.Text = "";

                Console.WriteLine("Rows Imported: " + vehicleList.Count);
                consoleTextBox.AppendText("Rows Imported: " + vehicleList.Count + Environment.NewLine);

                foreach (VehicleModel vehicle in vehicleList)
                {
                    if (vehicle.CityNameChanged)
                    {
                        Console.WriteLine("Modified ID: " + vehicle.ID);
                        consoleTextBox.AppendText("Modified ID: " + vehicle.ID + Environment.NewLine);
                    }
                }

                for (int i = 0; i < vehicleList.Count; i++)
                {
                    if (i > 2)
                    {
                        break;
                    }
                    Console.WriteLine(vehicleList[i].ID + "," + vehicleList[i].MakeModel + "," + "Date and time used: " + vehicleList[i].DateTimeAdded);
                    consoleTextBox.AppendText(vehicleList[i].ID + "," + vehicleList[i].MakeModel + "," + "Date and time used: " + vehicleList[i].DateTimeAdded + Environment.NewLine);
                }
            }
        }
    }
}
