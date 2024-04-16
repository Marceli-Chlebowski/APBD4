using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Services
{
    public interface IVisitService
    {
        IEnumerable<Visit> GetVisits();
        Visit AddVisit(Visit visit);
        Visit? GetVisit(int id);
        void UpdateVisit(int id, Visit updatedVisit);
        void DeleteVisit(int id);
    }

    public class VisitService : IVisitService
    {
        public IEnumerable<Visit> GetVisits()
        {
            StaticData.LoadRelationships();
            return StaticData.Visits;
        }

        public Visit AddVisit(Visit visit)
        {
            StaticData.Visits.Add(visit);
            return visit;
        }

        public Visit? GetVisit(int id)
        {
            StaticData.LoadRelationships();
            return StaticData.Visits.FirstOrDefault(v => v.Id == id);
        }

        public void UpdateVisit(int id, Visit updatedVisit)
        {
            var visit = StaticData.Visits.FirstOrDefault(v => v.Id == id);
            if (visit != null)
            {
                visit.Date = updatedVisit.Date;
                visit.Description = updatedVisit.Description;
                visit.Price = updatedVisit.Price;
                visit.AnimalId = updatedVisit.AnimalId;
            }
        }

        public void DeleteVisit(int id)
        {
            var visit = StaticData.Visits.FirstOrDefault(v => v.Id == id);
            if (visit != null)
            {
                StaticData.Visits.Remove(visit);
            }
        }
    }
}