using Maxsys.Sample.AspNetExcelUpload.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Spire.Xls;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Maxsys.Sample.AspNetExcelUpload.MVC.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> ListarPessoas(IFormFile filexls)
    {
        // Checar se o arquivo é excel
        var isXlFile = Path.GetExtension(filexls.FileName).ToLower() switch
        {
            ".xls" or ".xlsx" or ".xlsm" => true,
            _ => false
        };

        if (!isXlFile)
        {
            // fazer algo.
        }
        using (var stream = new MemoryStream())
        {
            //TRATAR STREAM
            //
            Workbook workbook = new Workbook();
            try
            {
                await filexls.CopyToAsync(stream);
                workbook.LoadFromStream(stream);
            }
            catch
            {
                _logger.Log(LogLevel.Error, "Ocorreu um erro ao ler a stream.");
            }
            //TRATAR PLANILHA
            //
            var sheet = workbook.Worksheets[0];
            var SheetViewModel = new SheetViewModel(sheet.Name, GetColumns(sheet), GetRows(sheet));
            return View(SheetViewModel);
        }
    }

    public IEnumerable<IColumn> GetColumns(Worksheet sheet)
    {
        List<IColumn> columns = new();
        var columns_range = sheet.Range[sheet.FirstRow, sheet.FirstColumn, sheet.FirstRow, sheet.LastColumn];
        foreach (var c in columns_range)
        {
            var cell = new CellViewModel(c.Value2.ToString());
            var col = new ColumnViewModel(cell, Utils.GetRegex(c.Value2.ToString()));
            columns.Add(col);
        }
        return columns;
    }

    public IEnumerable<IRow> GetRows(Worksheet sheet)
    {
        IEnumerable<IColumn> columns = GetColumns(sheet);
        List<IRow> rows = new();
        for (int i = 1; i < sheet.Rows.Count(); i++)
        {
            Dictionary<string, ICell> cells = new();
            foreach (var r in sheet.Rows[i])
            {
                IColumn column = columns.ElementAt(cells.Count());
                cells.Add(columns.ElementAt(cells.Count()).Column.Content, new CellViewModel(r.Value2.ToString()));
            }
            rows.Add(new RowViewModel(cells));
        }
        return rows;
    }
}
