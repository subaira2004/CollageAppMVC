using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollageAppMVC.Models
{
    public class DataPagination
    {
        public bool IsShowPageNavigation { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int NoOfDataPerPage { get; set; }
        public string NavPageUrl { get; set; }
        public string JSCallBackFunctionName { get; set; }
        public int TotalNoOfData { get; set; }

        /// <summary>
        /// calculate total number of pages as per given noOfDataPerPage
        /// </summary>
        /// <param name="TotalNoOfRecords">Total No Of Records</param>
        /// <param name="noOfDataPerPage">no of data to in a page</param>
        /// <returns></returns>
        public static int TotalNoOfPages(int TotalNoOfRecords, int noOfDataPerPage = 5)
        {
            int PageCount = 0;
            if (TotalNoOfRecords > 0)
            {
                PageCount = TotalNoOfRecords / noOfDataPerPage;
                if ((TotalNoOfRecords % noOfDataPerPage) > 0)
                {
                    PageCount++;
                }
            }
            return PageCount;
        }
    }
}