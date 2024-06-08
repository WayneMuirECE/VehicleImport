namespace VehicleImport.Models
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            DateTimeAdded = DateTime.Now;
        }

        public string ID
        {
            get { return ColumnData.ContainsKey("ID") ? ColumnData["ID"] : string.Empty; }
            set { ColumnData["ID"] = value; }
        }
        public string VIN
        {
            get { return ColumnData.ContainsKey("VIN") ? ColumnData["VIN"] : string.Empty; }
            set { ColumnData["VIN"] = value; }
        }
        public string City
        {
            get { return ColumnData.ContainsKey("CITY") ? ColumnData["CITY"] : string.Empty; }
            set { ColumnData["CITY"] = value; }
        }
        public bool CityNameChanged { get; set; } = false;
        public DateTime DateTimeAdded { get; private set; }
        public string MakeModel
        {
            get { return ColumnData.ContainsKey("MAKE") && ColumnData.ContainsKey("MODEL") ? ColumnData["MAKE"] + " - " + ColumnData["MODEL"] : string.Empty; }
            set
            {
                if (value.Contains('-'))
                {
                    string[] makeModel = value.Split('-');
                    ColumnData["MAKE"] = makeModel[0].Trim();
                    ColumnData["MODEL"] = makeModel[1].Trim();
                }
                else
                {
                    throw new Exception("MakeModel can not be set without a hyphen '-' for value: " + value);
                }
            }
        }
        public string Make
        {
            get { return ColumnData.ContainsKey("MAKE") ? ColumnData["MAKE"] : string.Empty; }
            set { ColumnData["MAKE"] = value; }
        }
        public string Model
        {
            get { return ColumnData.ContainsKey("MODEL") ? ColumnData["MODEL"] : string.Empty; }
            set { ColumnData["MODEL"] = value; }
        }

        // Contains all the data from the file
        public Dictionary<string, string> ColumnData { get; set; } = new Dictionary<string, string>();
    }
}
