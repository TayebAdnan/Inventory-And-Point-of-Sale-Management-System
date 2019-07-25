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
            
            var data=db.Sales.SqlQuery("select * from [dbo].[Sale] where SaleDateTime between '" + start + "'and'" + end + "'").ToList();
            
            return View(data);
        }
        public ActionResult DateCheck()
        {
            return View();
        }

        public ActionResult Data(DateTime start, DateTime end)
        {
            

            List<Sale> list = new List<Sale>();

            list=db.Sales.Where(x => x.SaleDateTime >= start && x.SaleDateTime <= end).ToList();
            

            return View(list);
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