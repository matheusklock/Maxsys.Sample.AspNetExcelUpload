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
        //
        var SheetViewModel = new SheetViewModel();
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
            SheetViewModel.Columns = GetColumns(sheet);
            SheetViewModel.Cells = GetCells(sheet);

            ;        }
        return View(SheetViewModel);
    }

    public IEnumerable<IColumn> GetColumns(Worksheet sheet)
    {
        List<IColumn> columns = new();
        List<ICell> column_cells = new();
        var columns_range = sheet.Range[sheet.FirstRow, sheet.FirstColumn, sheet.FirstRow, sheet.LastColumn];
        foreach (var c in columns_range)
        {
            var cell = new CellViewModel(c.Value2.ToString(), new Regex(""));
            var col = new ColumnViewModel { Column = cell };
            columns.Add(col);
        }
        return columns;
    }

    public IEnumerable<ICell> GetCells(Worksheet sheet)
    {
        List<ICell> cells = new();
        var range = sheet.Range[2, sheet.FirstColumn, sheet.LastRow, 1];
        foreach (var cell in range)
        {
            cells.Add(new CellViewModel(cell.Value2.ToString() ?? "", new Regex("")));
        }
        return cells;
    }
}
