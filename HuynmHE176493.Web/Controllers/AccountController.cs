using Microsoft.AspNetCore.Mvc;
using HuynmHE176493.Business.IService;
using HuynmHE176493.Data.Models;

namespace HuynmHE176493.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();
            return View(accounts);
        }

        public IActionResult Details(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null) return NotFound();
            return View(account);
        }

        // GET: Tạo tài khoản
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountService.CreateAccount(account);
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Chỉnh sửa tài khoản
        public IActionResult Edit(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(SystemAccount account)
        {
            _accountService.UpdateAccount(account);            
            return RedirectToAction("Index");
        }

        // GET: Xác nhận xóa tài khoản
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account != null)
            {
                _accountService.DeleteAccount(id);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }

            _accountService.DeleteAccount(id);            
            return RedirectToAction("Index");
        }
    [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            _accountService.ToggleAccountStatus(id);
            return RedirectToAction("Index");
        }

    }
}
