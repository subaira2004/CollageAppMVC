using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageAppMVC.Models
{
    public class DepartmentHelper
    {

        /// <summary>
        /// take Total number department records count
        /// </summary>
        /// <returns></returns>
        public static int TotalDepartmentCount()
        {
            int deptCount = 0;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                deptCount = dbContext.Departments.Count();
            }
            return deptCount;
        }

        /// <summary>
        /// take department as list as per given fromNo to ToNo
        /// </summary>
        /// <param name="fromNo">take record from this number</param>
        /// <param name="toNo">take record up to this number</param>
        /// <returns></returns>
        public static List<Department> TakeDepartmentfromNoToNo(int fromNo = 0, int toNo = 5)
        {
            List<Department> departmentList;
            using (CollageAppEDM dbContext = new CollageAppEDM())
            {
                departmentList = dbContext.Departments.OrderBy(p =>p.DepartmentId).Take(toNo).Skip(fromNo).ToList();
            }
            return departmentList;
        }
    }
}