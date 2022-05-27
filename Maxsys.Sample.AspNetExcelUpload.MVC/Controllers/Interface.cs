using System.Text.RegularExpressions;

namespace Maxsys.Sample.AspNetExcelUpload.MVC.Controllers
{
    public interface ISheet
    {
        public IEnumerable<IColumn> Columns { get; set; }
        public IEnumerable<IRow> Rows { get; set; }
    }
    public interface IColumn
    {
        public ICell Column { get; set; }
        public string Validation { get; set; }
    }
    public interface IRow
    {
        public IDictionary<string, ICell> Cells { get; set; }
    }
    public interface ICell
    {
        public string Content { get; set; }
    }
}
