using ChartJSCore.Helpers;
using ChartJSCore.Models;
using hermes_datavision.DAL;
using hermes_datavision.Helpers;
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
                var dive = Context.Remoras.Find(remoraId);
                ViewData["dive"] = dive;

                var records = Context.RemoraRecords.Where(r => r.RemoraId == remoraId).OrderBy(r => r.CreationDate).ToList();

                Chart chartDepth = new Chart();
                chartDepth.Type = Enums.ChartType.Line;

                var depth = new Data();
                depth.Labels = new List<string>();
                foreach (var record in records)
                {
                    depth.Labels.Add(DateConversion.UnixTimeStampToDateTime(dive.StartTime + record.Timestamp).ToString());
                }

                LineDataset depthDataset = new LineDataset()
                {
                    Label = "Depth",
                    Data = new List<double?>(),
                    Fill = "false",
                    BorderColor = new List<ChartColor> { ChartColor.FromRgb(54, 162, 235) },
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
                    depthDataset.Data.Add(record.Depth);
                }

                depth.Datasets = new List<Dataset>();
                depth.Datasets.Add(depthDataset);

                chartDepth.Data = depth;

                ViewData["chartDepth"] = chartDepth;


                Chart chartDegrees = new Chart();
                chartDegrees.Type = Enums.ChartType.Line;

                var degrees = new Data();
                degrees.Labels = new List<string>();
                foreach (var record in records)
                {
                    degrees.Labels.Add(DateConversion.UnixTimeStampToDateTime(dive.StartTime + record.Timestamp).ToString());
                }

                LineDataset degreesDataset = new LineDataset()
                {
                    Label = "Degrees",
                    Data = new List<double?>(),
                    Fill = "false",
                    BorderColor = new List<ChartColor> { ChartColor.FromRgb(75, 192, 192) },
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
                    degreesDataset.Data.Add(record.Degrees);
                }

                degrees.Datasets = new List<Dataset>();
                degrees.Datasets.Add(degreesDataset);

                chartDegrees.Data = degrees;

                ViewData["chartDegrees"] = chartDegrees;

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