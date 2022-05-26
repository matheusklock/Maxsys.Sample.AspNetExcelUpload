using System.Text.RegularExpressions;

namespace Maxsys.Sample.AspNetExcelUpload.MVC.Controllers
{
    public interface ISheet
    {
        public IEnumerable<IColumn> Columns { get; set; }
        public IEnumerable<ICell> Cells { get; set; }
    }
    public interface IColumn
    {
        public ICell Column { get; set; }
        public IEnumerable<ICell> Cells { get; set; }
    }
    public interface ICell
    {
        public string Content { get; set; }
        public Regex Validation { get; set; }
    }
}
