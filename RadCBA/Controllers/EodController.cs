using RadCBA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RadCBA.Controllers
{
    //[ClaimsAuthorize("DynamicClaim", "RunEOD")]
    public class EodController : Controller
    {
        EodLogic logic = new EodLogic();

        public ActionResult Index(string message)
        {
            ViewBag.Msg = message;
            return View();
        }

        public ActionResult OpenOrCloseBusiness()
        {
            try
            {
                if (logic.isBusinessClosed())
                {
                    logic.OpenBusiness();
                }
                else
                {
                    string result = logic.RunEOD();
                    return RedirectToAction("Index", new { message = result });
                }
            }
            catch (Exception)
            {
                //ErrorLogger.Log("Message= " + ex.Message + "\nInner Exception= " + ex.InnerException + "\n");
                return PartialView("Error");
            }
            return RedirectToAction("Index");
        }
    }
}