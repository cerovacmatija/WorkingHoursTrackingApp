using System.Linq;
using EmployeeTrackingApp.Data;

namespace EmployeeTrackingApp.Repositories
{
    public class WorkingHoursRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkingHoursRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddWorkingHours(WorkingHours workingHours)
        {
            _context.WorkingHours.Add(workingHours);
            _context.SaveChanges();
        }

        public void UpdateWorkingHours(WorkingHours workingHours)
        {
            _context.WorkingHours.Update(workingHours);
            _context.SaveChanges();
        }

        public IQueryable<WorkingHours> GetWorkingHoursForEmployee(int employeeId)
        {
            return _context.WorkingHours.Where(w => w.EmployeeId == employeeId);
        }

        public IQueryable<WorkingHours> GetAllWorkingHours()
        {
            return _context.WorkingHours.AsQueryable();
        }
    }
}
