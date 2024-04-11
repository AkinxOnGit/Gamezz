using Gamezz.Data;
using Gamezz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gamezz.Controllers
{
    public class DataPointController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataPointController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public ActionResult Index()
        {
            // Erstelle eine leere Liste für die Datenpunkte
            List<DataPoint> dataPoints = new List<DataPoint>();

            var test = _context.GamesOrders.Include(g => g.Orders).Where(g => g.Orders.Invoice != "").ToList();

            // Führe die Abfrage aus, um die meistverkauften Spiele zu erhalten
            var mostSoldGames = _context.GamesOrders
                .Include(g => g.Orders)
                .Where(g => g.Orders.Invoice != "")
                .GroupBy(go => go.Games.Name)
                .Select(g => new
                {
                    GameName = g.Key,
                    PublisherName = g.First().Games.Publisher.Name,
                    TotalSales = g.Sum(go => go.Amount)
                })
                .OrderByDescending(g => g.TotalSales)
                .Take(7) // Hier können Sie die Anzahl der meistverkauften Spiele festlegen
                
                .ToList();

            // Füge jeden meistverkauften Spiele als eigenen Datenpunkt hinzu
            foreach (var game in mostSoldGames)
            {
                dataPoints.Add(new DataPoint(game.GameName, game.TotalSales));
            }

            // Konvertiere die Liste in ein JSON-Format und setze sie in ViewBag.DataPoints
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            // Gib die View zurück
            return View();
        }
    }
}
