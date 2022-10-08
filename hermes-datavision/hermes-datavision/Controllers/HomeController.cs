using ChartJSCore.Helpers;
using ChartJSCore.Models;
using hermes_datavision.DAL;
using hermes_datavision.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace hermes_datavision.Controllers
{
    public class HomeController : Controller
    {
        private readonly HermesDbContext Context;

        public HomeController(HermesDbContext context)
        {
            Context = context;
        }

        public IActionResult Index(int? remoraId)
        {
            var dives = Context.Remoras.OrderByDescending(r => r.CreationDate).ToList();

            ViewData["dives"] = dives;


            if (remoraId.HasValue)
            {

                Chart chart = new Chart();
                chart.Type = Enums.ChartType.Line;

                var records = Context.RemoraRecords.Where(r => r.RemoraId == remoraId).ToList();
                var data = new Data();
                data.Labels = new List<string>();
                foreach (var record in records)
                {
                    data.Labels.Add(record.CreationDate.ToString());
                }

                LineDataset dataset = new LineDataset()
                {
                    Label = "Degrees",
                    Data = new List<double?>(),
                    Fill = "false",
                    //LineTension = 0.1,
                    //BackgroundColor = ChartColor.FromRgba(75, 192, 192, 0.4),
                    //BorderColor = ChartColor.FromRgb(75, 192, 192),
                    BorderCapStyle = "butt",
                    BorderDash = new List<int> { },
                    BorderDashOffset = 0.0,
                    BorderJoinStyle = "miter",
                    PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                    PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                    PointBorderWidth = new List<int> { 1 },
                    PointHoverRadius = new List<int> { 5 },
                    PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
                    PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220, 220, 220) },
                    PointHoverBorderWidth = new List<int> { 2 },
                    PointRadius = new List<int> { 1 },
                    PointHitRadius = new List<int> { 10 },
                    SpanGaps = false
                };

                foreach (var record in records)
                {
                    dataset.Data.Add(record.Degrees);
                }

                data.Datasets = new List<Dataset>();
                data.Datasets.Add(dataset);

                chart.Data = data;

                ViewData["chart"] = chart;

            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}