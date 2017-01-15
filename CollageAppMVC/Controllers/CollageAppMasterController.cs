using CollageAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollageAppMVC.Controllers
{
    public class CollageAppMasterController : Controller
    {
        CollageAppEDM dbContext = new CollageAppEDM();

        // GET: CollageAppMaster
        public ActionResult Index()
        {
            ViewBag.Title = "Master";

            setDepartmentListInViewBag();

            return View(new Department());
        }

        private void setDepartmentListInViewBag(int goToPage = 1, int noOfDataPerPage = 5)
        {
            var toNo = goToPage * noOfDataPerPage;
            var fromNo = toNo - noOfDataPerPage;
            var deptCount = DepartmentHelper.TotalDepartmentCount();
            var TotalPages = DataPagination.TotalNoOfPages(deptCount, noOfDataPerPage);
            noOfDataPerPage = deptCount < noOfDataPerPage ? deptCount : noOfDataPerPage;
            ViewBag.Departments = DepartmentHelper.TakeDepartmentfromNoToNo(fromNo, toNo);

            ViewBag.DataPagination = new DataPagination
            {
                IsShowPageNavigation = deptCount > 0 && TotalPages > 1,
                CurrentPage = goToPage,
                TotalPages = TotalPages,
                NoOfDataPerPage = noOfDataPerPage,
                NavPageUrl = "Master/DepartmentList",
                JSCallBackFunctionName = "callBackDepartmentList",
                TotalNoOfData = deptCount
            };
        }

        public ActionResult Department()
        {
            setDepartmentListInViewBag();

            return PartialView(new Department());
        }

        [HttpPost]
        public ActionResult Department(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.DepartmentId > 0) //update
                {
                    dbContext.Entry(department).State = System.Data.Entity.EntityState.Modified;
                }
                else //insert
                {
                    dbContext.Departments.Add(department);
                }
                dbContext.SaveChanges();
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors }).ToList();
                return Json(errors, JsonRequestBehavior.AllowGet);
            }

            return DepartmentList(1, 5);
        }

        public ActionResult DepartmentList(int goToPage = 1, int noOfDataPerPage = 5)
        {
            setDepartmentListInViewBag(goToPage, noOfDataPerPage);

            return PartialView("DepartmentList");
        }

        public ActionResult DeleteDepartment(int DepartmentId)
        {
            var SectionsOfThisDept = dbContext.DeptSections.Where(p => p.DepartmentId == DepartmentId).ToList();
            var lecturerOfThisDept = dbContext.Lecturers.Where(p => p.DepartmentId == DepartmentId).ToList();
            if ((SectionsOfThisDept.Count == 0) && (lecturerOfThisDept.Count == 0))
            {
                var DelDept = dbContext.Departments.Where(p => p.DepartmentId == DepartmentId).AsEnumerable();
                dbContext.Departments.RemoveRange(DelDept);
                dbContext.SaveChanges();
            }
            return DepartmentList(1, 5);
        }

        [HttpGet]
        public ActionResult EditDepartments(int DepartmentId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            return Json(dbContext.Departments.Where(p => p.DepartmentId == DepartmentId).First(), JsonRequestBehavior.AllowGet);
        }

        private SelectList getDepartmentSelectList()
        {
            var departments = dbContext.Departments.ToList();
            departments.Insert(0, new Department { DepartmentId = 0, Name = "--Select Department--" });
            return new SelectList(departments, "DepartmentId", "Name", 0);
        }

        public ActionResult Section()
        {
            setSectionListInViewBag();

            ViewBag.DepartmentsDropDownList = getDepartmentSelectList();

            return PartialView(new DeptSection());
        }

        private void setSectionListInViewBag(int goToPage = 1, int noOfDataPerPage = 5)
        {
            var toNo = goToPage * noOfDataPerPage;
            var fromNo = toNo - noOfDataPerPage;
            var sectCount = SectionHelper.TotalSectionCount();
            var TotalPages = DataPagination.TotalNoOfPages(sectCount, noOfDataPerPage);
            noOfDataPerPage = sectCount < noOfDataPerPage ? sectCount : noOfDataPerPage;
            ViewBag.Sections = SectionHelper.TakeSectionfromNoToNo(fromNo, toNo);

            ViewBag.DataPagination = new DataPagination
            {
                IsShowPageNavigation = sectCount > 0 && TotalPages > 1,
                CurrentPage = goToPage,
                TotalPages = TotalPages,
                NoOfDataPerPage = noOfDataPerPage,
                NavPageUrl = "Master/SectionList",
                JSCallBackFunctionName = "callBackSectionList",
                TotalNoOfData = sectCount
            };
        }

        public ActionResult SectionList(int goToPage = 1, int noOfDataPerPage = 5)
        {
            setSectionListInViewBag(goToPage, noOfDataPerPage);

            return PartialView("SectionList");
        }

        [HttpPost]
        public ActionResult Section(DeptSection section)
        {
            if (ModelState.IsValid)
            {
                if (section.SectionId > 0) //update
                {
                    dbContext.Entry(section).State = System.Data.Entity.EntityState.Modified;
                }
                else //insert
                {
                    dbContext.DeptSections.Add(section);
                }
                dbContext.SaveChanges();
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors }).ToList();
                return Json(errors, JsonRequestBehavior.AllowGet);
            }

            return SectionList(1, 5);
        }

        [HttpGet]
        public ActionResult GetSectionsByDepartmentId(int DepartmentId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var sections = dbContext.DeptSections.Where(p => p.DepartmentId == DepartmentId).ToList();// SectionHelper.TakeSectionsByDepartmentId(DepartmentId);

            return Json(sections, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSection(int SectionId)
        {
            var studentsOfthisSection = dbContext.Students.Where(p => p.SectionId == SectionId).ToList();
            if ((studentsOfthisSection.Count == 0))
            {
                var delSection = dbContext.DeptSections.Where(p => p.SectionId == SectionId).AsEnumerable();
                dbContext.DeptSections.RemoveRange(delSection);
                dbContext.SaveChanges();
            }
            return SectionList(1, 5);
        }

        [HttpGet]
        public ActionResult EditSection(int SectionId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            return Json(dbContext.DeptSections.Where(p => p.SectionId == SectionId).First(), JsonRequestBehavior.AllowGet);
        }

        private SelectList getSectionSelectListByDepartmentId(int DepartmentId)
        {
            var sections = SectionHelper.TakeSectionsByDepartmentId(DepartmentId);
            sections.Insert(0, new DeptSection { SectionId = 0, Name = "--Select Section--" });
            return new SelectList(sections, "SectionId", "Name", 0);
        }

        public ActionResult Student()
        {
            setStudentListInViewBag();

            ViewBag.DepartmentsDropDownList = getDepartmentSelectList();
            ViewBag.SectionsDropDownList = getSectionSelectListByDepartmentId(0);

            return PartialView(new StudentViewModel());
        }

        [HttpPost]
        public ActionResult Student(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var studentModel = new Student
                {
                    StudentId = student.StudentId,
                    Name = student.StudentName,
                    SectionId = student.SectionId,
                    DateOfJoin = student.DateOfJoin,
                    DateofGraduaton = student.DateofGraduaton
                };
                if (studentModel.StudentId > 0) //update
                {
                    dbContext.Entry(studentModel).State = System.Data.Entity.EntityState.Modified;
                }
                else //insert
                {
                    dbContext.Students.Add(studentModel);
                }
                dbContext.SaveChanges();
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors }).ToList();
                return Json(errors, JsonRequestBehavior.AllowGet);
            }

            return StudentList(1, 5);
        }

        private void setStudentListInViewBag(int goToPage = 1, int noOfDataPerPage = 5)
        {
            var toNo = goToPage * noOfDataPerPage;
            var fromNo = toNo - noOfDataPerPage;
            var studentCount = StudentHelper.TotalStudentCount();
            var TotalPages = DataPagination.TotalNoOfPages(studentCount, noOfDataPerPage);
            noOfDataPerPage = studentCount < noOfDataPerPage ? studentCount : noOfDataPerPage;
            ViewBag.Students = StudentHelper.TakeStudentfromNoToNo(fromNo, toNo);

            ViewBag.DataPagination = new DataPagination
            {
                IsShowPageNavigation = studentCount > 0 && TotalPages > 1,
                CurrentPage = goToPage,
                TotalPages = TotalPages,
                NoOfDataPerPage = noOfDataPerPage,
                NavPageUrl = "Master/StudentList",
                JSCallBackFunctionName = "callBackStudentList",
                TotalNoOfData = studentCount
            };
        }

        public ActionResult StudentList(int goToPage = 1, int noOfDataPerPage = 5)
        {
            setStudentListInViewBag(goToPage, noOfDataPerPage);

            return PartialView("StudentList");
        }

        public ActionResult EditStudent(int StudentId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            return Json(StudentHelper.TakeStudentByStudentId(StudentId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteStudent(int StudentId)
        {
            var delStudent = dbContext.Students.Where(p => p.StudentId == StudentId).AsEnumerable();
            dbContext.Students.RemoveRange(delStudent);
            dbContext.SaveChanges();
            return StudentList(1, 5);
        }

        public ActionResult Lecturer()
        {

            setLecturerListInViewBag();

            ViewBag.DepartmentsDropDownList = getDepartmentSelectList();

            return PartialView(new Lecturer());
        }

        [HttpPost]
        public ActionResult Lecturer(Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                if (lecturer.LecturerId > 0) //update
                {
                    dbContext.Entry(lecturer).State = System.Data.Entity.EntityState.Modified;
                }
                else //insert
                {
                    dbContext.Lecturers.Add(lecturer);
                }
                dbContext.SaveChanges();
            }
            else
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors }).ToList();
                return Json(errors, JsonRequestBehavior.AllowGet);
            }

            return LecturerList(1, 5);
        }

        private void setLecturerListInViewBag(int goToPage = 1, int noOfDataPerPage = 5)
        {
            var toNo = goToPage * noOfDataPerPage;
            var fromNo = toNo - noOfDataPerPage;
            var lecturerCount = LecturerHelper.TotalLecturerCount();
            var TotalPages = DataPagination.TotalNoOfPages(lecturerCount, noOfDataPerPage);
            noOfDataPerPage = lecturerCount < noOfDataPerPage ? lecturerCount : noOfDataPerPage;
            ViewBag.Lecturers = LecturerHelper.TakeLecturerfromNoToNo(fromNo, toNo);

            ViewBag.DataPagination = new DataPagination
            {
                IsShowPageNavigation = lecturerCount > 0 && TotalPages > 1,
                CurrentPage = goToPage,
                TotalPages = TotalPages,
                NoOfDataPerPage = noOfDataPerPage,
                NavPageUrl = "Master/LecturerList",
                JSCallBackFunctionName = "callBackLecturerList",
                TotalNoOfData = lecturerCount
            };
        }

        public ActionResult LecturerList(int goToPage = 1, int noOfDataPerPage = 5)
        {
            setLecturerListInViewBag(goToPage, noOfDataPerPage);

            return PartialView("LecturerList");
        }

        public ActionResult EditLecturer(int LecturerId)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            return Json(dbContext.Lecturers.Where(p => p.LecturerId == LecturerId).First(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLecturer(int LecturerId)
        {
            var delLecturer = dbContext.Lecturers.Where(p => p.LecturerId == LecturerId).AsEnumerable();
            dbContext.Lecturers.RemoveRange(delLecturer);
            dbContext.SaveChanges();
            return LecturerList(1, 5);
        }

    }
}
