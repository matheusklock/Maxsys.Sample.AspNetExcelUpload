using System.Text.RegularExpressions;

namespace Maxsys.Sample.AspNetExcelUpload.MVC.Controllers
{
    public class SheetViewModel : ISheet
    {
        public IEnumerable<IColumn> Columns { get; set; }
        public IEnumerable<ICell> Cells { get; set; }
    }

    public class ColumnViewModel : IColumn
    {
        public ICell Column { get; set; }
        public IEnumerable<ICell> Cells { get; set; }
    }

    public class CellViewModel : ICell
    {
        public CellViewModel(string content, Regex validation)
        {
            Content = content;
            Validation = validation;
        }
        public string Content { get; set; }
        public Regex Validation { get; set; }
    }
}
