using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageAppMVC.Models
{
    public class StudentHelper
    {
        /// <summary>
        /// take Total number student records count
        /// </summary>
        /// <returns></returns>
        public static int TotalStudentCount()
        {
            int studentCount = 0;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                studentCount = dbContext.Students.Count();
            }
            return studentCount;
        }

        /// <summary>
        /// take student as list as per given fromNo to ToNo
        /// </summary>
        /// <param name="fromNo">take record from this number</param>
        /// <param name="toNo">take record up to this number</param>
        /// <returns></returns>
        public static List<StudentViewModel> TakeStudentfromNoToNo(int fromNo = 0, int toNo = 5)
        {
            List<StudentViewModel> studentList;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                studentList = (from p in dbContext.Students
                               join q in dbContext.DeptSections on p.SectionId equals q.SectionId
                               join r in dbContext.Departments on q.DepartmentId equals r.DepartmentId
                               orderby p.StudentId
                               select new StudentViewModel
                               {
                                   StudentId = p.StudentId,
                                   StudentName = p.Name,
                                   DateofGraduaton = p.DateofGraduaton,
                                   DateOfJoin = p.DateOfJoin,
                                   SectionId = p.SectionId,
                                   SectionName = q.Name,
                                   DepartmentId = q.DepartmentId,
                                   DepartmentName = r.Name
                               }).Take(toNo).Skip(fromNo).ToList();
            }
            return studentList;
        }
       
        /// <summary>
        /// take student as per given stuent id
        /// </summary>
        /// <param name="StudentId">input student id</param>
        /// <returns>student view model </returns>
        public static StudentViewModel TakeStudentByStudentId(int StudentId)
        {
            StudentViewModel student;
            using (CollageAppEDM db = new CollageAppEDM())
            {
                student = (from p in  db.Students 
                           join q in db.DeptSections on p.SectionId equals q.SectionId
                           join r in db.Departments on q.DepartmentId equals r.DepartmentId
                           where p.StudentId == StudentId
                           select new StudentViewModel
                           {
                               StudentId = p.StudentId,
                               StudentName = p.Name,
                               DateOfJoin = p.DateOfJoin,
                               DateofGraduaton = p.DateofGraduaton,
                               SectionId = p.SectionId,
                               SectionName = q.Name,
                               DepartmentId = q.DepartmentId,
                               DepartmentName= r.Name                               
                           }).First();
            }
            return student;
        }
    }
}