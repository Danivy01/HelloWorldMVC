using HelloWorldMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC.Controllers {
    public class LeaveController : Controller {

        private static List<Leave> _leaves = new List<Leave> {
            new Leave {ID = 1, EmployeeName = "Daniella Yvette Rimas", LeaveType = "Sick", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), Status = "Pending"},
            new Leave {ID = 2, EmployeeName = "Juan dela Cruz", LeaveType = "Vacation", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(5), Status = "Approved"},
            new Leave {ID = 3, EmployeeName = "Maria Santos", LeaveType = "Emergency", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1), Status = "Rejected"}
        };

        [HttpGet]
        public IActionResult Index() {

            return View(_leaves);
        }

        [HttpGet]
        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Leave leave) {
            leave.ID = _leaves.Count + 1;
            leave.Status = "Pending";
            _leaves.Add(leave);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int ID) {
            var leave = _leaves.FirstOrDefault(d => d.ID == ID);
            if (leave == null) {
                return NotFound();
            }
            return View(leave);
        }

        [HttpPost]
        public IActionResult Edit(Leave leave) {
            var existing = _leaves.FirstOrDefault(x => x.ID == leave.ID);

            if (existing == null) {
                return NotFound();
            }

            existing.EmployeeName = leave.EmployeeName;
            existing.LeaveType = leave.LeaveType;
            existing.StartDate = leave.StartDate;
            existing.EndDate = leave.EndDate;
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int ID) {
            var existing = _leaves.Find(x => x.ID == ID);
            if (existing == null) { 
                return BadRequest();
            }

            _leaves.Remove(existing);
            return RedirectToAction("Index");
        }

    }
}
