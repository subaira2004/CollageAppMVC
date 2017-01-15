using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageAppMVC.Models
{
    public class LecturerHelper
    {
        /// <summary>
        /// take Total number lecturer records count
        /// </summary>
        /// <returns></returns>
        public static int TotalLecturerCount()
        {
            int lectCount = 0;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                lectCount = dbContext.Lecturers.Count();
            }
            return lectCount;
        }


        /// <summary>
        /// take lecturer as list as per given fromNo to ToNo
        /// </summary>
        /// <param name="fromNo">take record from this number</param>
        /// <param name="toNo">take record up to this number</param>
        /// <returns>List of Lecturers as per given parameters</returns>
        public static List<LecturerViewModel> TakeLecturerfromNoToNo(int fromNo = 0, int toNo = 5)
        {
            List<LecturerViewModel> lecturerList;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                lecturerList = (from p in dbContext.Lecturers
                                join q in dbContext.Departments on p.DepartmentId equals q.DepartmentId
                                orderby p.LecturerId
                                select new LecturerViewModel
                                {
                                    LecturerId = p.LecturerId,
                                    LecturerName = p.Name,
                                    DepartmentId = p.DepartmentId,
                                    DepartmentName = q.Name
                                }).Take(toNo).Skip(fromNo).ToList();
            }
            return lecturerList;
        }
    }
}