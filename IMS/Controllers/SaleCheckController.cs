using IMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class SaleCheckController : Controller
    {
        IMSEntities5 db = new IMSEntities5();
        // GET: SaleCheck
        
       
        public ActionResult Index(DateTime? start, DateTime? end)
        {
            IMSEntities5 db = new IMSEntities5();
            var data=db.Sales.SqlQuery("select * from [dbo].[Sale] where SaleDateTime between '" + start + "'and'" + end + "'").ToList();
            
            return View(data);
        }
        public ActionResult DateCheck()
        {
            return View();
        }

        public ActionResult Data()
        {
            IMSEntities5 db = new IMSEntities5();

            List<Sale> list = new List<Sale>();

            DateTime StartDate = Convert.ToDateTime("2019-07-17 13:59:32.400");
            DateTime EndDate = Convert.ToDateTime("2019-07-22 13:41:38.840");

            list.Where(x => x.SaleDateTime >= StartDate && x.SaleDateTime <= EndDate);

            return View();
        }


        //public ActionResult DateReceive(string StartDate = "", string FinishDate = "")
        //{

        //    DateTime stDate;
        //    DateTime fnDate;

        //    if (StartDate != string.Empty && FinishDate != string.Empty)
        //    {
        //        stDate = DateTime.Parse(StartDate);
        //        fnDate = DateTime.Parse(FinishDate);
        //        var cal = cal.Where(x => x.StartDate >= stDate && x.FinishDate <= fnDate);
        //    }
        //    return View();
        //}

        public ActionResult date(DateTime? id)
        {
            //string sql = "proc_name";
            
            //cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfromdate.text.trim()));
            //cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttodate.text.trim()));
            return View();
        }

    }



}