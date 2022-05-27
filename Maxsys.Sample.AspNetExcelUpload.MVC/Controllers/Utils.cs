using System.Text.RegularExpressions;

namespace Maxsys.Sample.AspNetExcelUpload.MVC.Controllers
{
    static class Utils
    {
        static public bool Validate_Regex(string regex_string, string content/*string column*/)
        {
            var regex = new Regex( regex_string );
            if (regex != null)
            {
                if (regex.IsMatch(content)) return true;
            }
            return false;
        }
        static public string GetRegex(string column)
        {
            if (column == "#") return "^REQ#[0-9][0-9][0-9][0-9]$";
            if (column == "Data criação") return "^[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9] [0-9][0-9]:[0-9][0-9]$";
            if (column == "Dias") return "^[0-9]$";
            if (column == "Consumo") return "^[0-9]$";
            if (column == "Data sol. aprovação") return "^[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9] [0-9][0-9]:[0-9][0-9]$";
            return "";
        }
    }
}
