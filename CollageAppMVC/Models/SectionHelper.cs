using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageAppMVC.Models
{
    public class SectionHelper
    {
        /// <summary>
        /// take Total number section records count
        /// </summary>
        /// <returns></returns>
        public static int TotalSectionCount()
        {
            int sectCount = 0;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                sectCount = dbContext.DeptSections.Count();
            }
            return sectCount;
        }

        /// <summary>
        /// take section as list as per given fromNo to ToNo
        /// </summary>
        /// <param name="fromNo">take record from this number</param>
        /// <param name="toNo">take record up to this number</param>
        /// <returns></returns>
        public static List<DeptSectionViewModel> TakeSectionfromNoToNo(int fromNo = 0, int toNo = 5)
        {
            List<DeptSectionViewModel> sectionList;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                sectionList = (from p in dbContext.DeptSections
                               join q in dbContext.Departments on p.DepartmentId equals q.DepartmentId
                               orderby p.SectionId
                               select new DeptSectionViewModel
                               {
                                   DepartmentId = p.DepartmentId,
                                   SectionId = p.SectionId,
                                   SectionName = p.Name,
                                   DepartmentName = q.Name
                               }).Take(toNo).Skip(fromNo).ToList();
            }
            return sectionList;
        }

        public static List<DeptSection> TakeSectionsByDepartmentId(int DepartmentId)
        {
            List<DeptSection> sections;
            using (CollageAppEDM db = new CollageAppEDM())
            {
                sections = db.DeptSections.Where(p => p.DepartmentId == DepartmentId).ToList();
            }
            return sections;
        }
    }
}