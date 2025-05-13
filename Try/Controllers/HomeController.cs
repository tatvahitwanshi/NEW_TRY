using System.Diagnostics;
using BAL.Interface;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Try.Models;

namespace Try.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ICustomer _cust;

    public HomeController(ILogger<HomeController> logger , ICustomer cust)
    {
        _logger = logger;
        _cust= cust;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        CustomerViewModel model = new CustomerViewModel
        {
            Customers = await _cust.GetAllCustomer(),
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> CustomerModal(int id)
    {
        CustomerList model = await _cust.GetModalData(id);
        return PartialView("_PartialCustomerModal",model);
    }

    [HttpPost]
    public async Task<IActionResult> CustomerModal(CustomerList modal)
    {
        var msg = string.Empty;
        if(ModelState.IsValid)
        {
            try
            {
                await _cust.SaveCustomerDetail(modal);
                msg="Customer Add Edit Successfully !";
                TempData["success"]=msg;
                return Json(new {success= true, message= msg , redirecturl=Url.Action("Privacy")});
                
            }
            catch
            {
                msg="Exception occur !";
                TempData["error"]=msg;
                return Json(new {success= false, message= msg});
            }
        }
        return Json(new {success= false, message= "Invalid model state."});
    }

    [HttpPost]
    public async Task<IActionResult> CustomerDelete(int id)
    {
        var msg = string.Empty;
        if(ModelState.IsValid)
        {
            try
            {
                await _cust.DeleteCustomerDetail(id);
                msg="Customer Deleted Successfully !";
                TempData["success"]=msg;
                return Json(new {success= true, message= msg , redirecturl=Url.Action("Privacy")});
                
            }
            catch
            {
                msg="Exception occur !";
                TempData["error"]=msg;
                return Json(new {success= false, message= msg});
            }
        }
        return Json(new {success= false, message= "Invalid model state."});
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
