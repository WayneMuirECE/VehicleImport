namespace VehicleImport.Models
{
    public class StringSplitter
    {
        public string[] SplitStringByDelimiter(string input)
        {
            // Define the delimiters
            char[] delimiters = new char[] { ',', '|' };

            // Split the string by the delimiters
            string[] result = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            return result;
        }
    }
}
