using WebApplication1.Database;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Endpoints
{
    public interface IVisitEndpointService
    {
        IActionResult GetVisits(MockDb db);
        IActionResult GetVisit(int id, MockDb db);
        IActionResult AddVisit(Visit visit, MockDb db);
    }

    public class VisitEndpointService : IVisitEndpointService
    {
        public IActionResult GetVisits(MockDb db)
        {
            return (IActionResult)Results.Ok(db.Visits);
        }

        public IActionResult GetVisit(int id, MockDb db)
        {
            var visit = db.Visits.FirstOrDefault(v => v.Id == id);
            return (IActionResult)(visit != null ? Results.Ok(visit) : Results.NotFound());
        }

        public IActionResult AddVisit(Visit visit, MockDb db)
        {
            db.Visits.Add(visit);
            return (IActionResult)Results.Created($"/visits/{visit.Id}", visit);
        }
    }

    public static class VisitEndpoints
    {
        public static void MapVisitEndpoints(this WebApplication app, IVisitEndpointService visitService, MockDb db)
        {
            app.MapGet("/visits", () => visitService.GetVisits(db));
            app.MapGet("/visits/{id}", (int id) => visitService.GetVisit(id, db));
            app.MapPost("/visits", (Visit visit) => visitService.AddVisit(visit, db));
        }
    }
}