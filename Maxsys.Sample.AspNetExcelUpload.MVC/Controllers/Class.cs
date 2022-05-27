using System.Text.RegularExpressions;

namespace Maxsys.Sample.AspNetExcelUpload.MVC.Controllers
{
    public class SheetViewModel : ISheet
    {
        public SheetViewModel(IEnumerable<IColumn> columns, IEnumerable<IRow> rows)
        {
            Columns = columns;
            Rows = rows;
        }
        public IEnumerable<IColumn> Columns { get; set; }
        public IEnumerable<IRow> Rows { get; set; }
    }

    public class ColumnViewModel : IColumn
    {
        public ColumnViewModel(ICell column, string validation)
        {
            Column = column;
            Validation = validation;
        }
        public ICell Column { get; set; }
        public string Validation { get; set; }
    }

    public class RowViewModel : IRow
    {
        public RowViewModel(IDictionary<string, ICell> cells)
        {
            Cells = cells;
        }
        public IDictionary<string, ICell> Cells { get; set; }
    }

    public class CellViewModel : ICell
    {
        public CellViewModel(string content)
        {
            Content = content;
        }
        public string Content { get; set; }
    }
}
