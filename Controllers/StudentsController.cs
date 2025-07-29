using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCoreMVCEntityCore.models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ASPCoreMVCEntityCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _env;
        public StudentsController(MyContext context, IWebHostEnvironment env  )
        {
            _context = context;
            _env = env;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewBag.State = new SelectList(BindDtls("State", 0), "Id", "Name","--Select--");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDTO student)
        {
            
            if (ModelState.IsValid)
            {
                string photoPath = "";
                string filePath = "";
                if (student.PhotoPath != null && student.PhotoPath.Length > 0)
                {
                    string photoName = Guid.NewGuid().ToString() + Path.GetExtension(student.PhotoPath.FileName);
                    photoPath = Path.Combine("Uploads", photoName);
                    string phy = Path.Combine(_env.WebRootPath, photoPath);
                    using (var str = new FileStream(phy, FileMode.Create))
                    {
                        student.PhotoPath.CopyTo(str);
                    }
                    ;
                }
                if (student.FilePath != null && student.FilePath.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(student.FilePath.FileName);
                        filePath = Path.Combine("Uploads", fileName);
                        string phy1 = Path.Combine(_env.WebRootPath, filePath);
                        using (var str = new FileStream(phy1, FileMode.Create))
                        {
                            student.FilePath.CopyTo(str);
                        }
                    ;

                    }
                    Student student1 = new Student()
                    {
                        Name = student.Name,
                        PhotoPath = photoPath,
                        FilePath=filePath,
                        Dob = student.Dob,
                        Gender = student.Gender,
                        State = student.State,
                        District = student.District,
                        Hoby = string.Join(",", student.Hoby),
                        IsActive = student.IsActive
                    };
                    _context.Add(student1);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
               
              
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            StudentDTO dt = new StudentDTO();
            dt.Name = student.Name;
            dt.Id=student.Id;
            dt.State = student.State;
            dt.Hoby=student.Hoby.Split(',').ToList();
            dt.Dob=student.Dob;
            dt.Gender = student.Gender;
            dt.District= student.District;
            dt.IsActive = Convert.ToBoolean( student.IsActive);
            return View(dt);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,StudentDTO student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string photoPath = "";
                    string filePath = "";
                    if (student.PhotoPath != null && student.PhotoPath.Length > 0)
                    {
                        string photoName = Guid.NewGuid().ToString() + Path.GetExtension(student.PhotoPath.FileName);
                        photoPath = Path.Combine("Uploads", photoName);
                        string phy = Path.Combine(_env.WebRootPath, photoPath);
                        using (var str = new FileStream(phy, FileMode.Create))
                        {
                            student.PhotoPath.CopyTo(str);
                        }
                        ;
                    }
                    if (student.FilePath != null && student.FilePath.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(student.FilePath.FileName);
                        filePath = Path.Combine("Uploads", fileName);
                        string phy1 = Path.Combine(_env.WebRootPath, filePath);
                        using (var str = new FileStream(phy1, FileMode.Create))
                        {
                            student.FilePath.CopyTo(str);
                        }
                        ;

                    }
                    Student student1 = new Student()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        PhotoPath = photoPath,
                        FilePath = filePath,
                        Dob = student.Dob,
                        Gender = student.Gender,
                        State = student.State,
                        District = student.District,
                        Hoby = string.Join(",", student.Hoby),
                        IsActive = student.IsActive
                    };
                    _context.Update(student1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        private List<BindDtlsDTO> BindDtls(string flag,int param)
        {
            List<BindDtlsDTO> res=new List<BindDtlsDTO>();
            if (flag == "State")
            {
                res = _context.StateMasters.Select(m => new BindDtlsDTO() {Id= m.Id,Name= m.Name }).ToList();
            }
            else if(flag == "District")
            {
                res = _context.DistrictMasters.Where(m=>m.StateId==param).Select(m => new BindDtlsDTO() { Id = m.Id, Name = m.Name }).ToList();

            }
            return res;
        }
        public IActionResult BindAjax(string flag, int param)
        {        
            return Json(BindDtls(flag, param));
        }
    }
}
