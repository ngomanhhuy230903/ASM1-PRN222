using Microsoft.AspNetCore.Mvc;
using HuynmHE176493.Data.Models;
using HuynmHE176493.Business;
using HuynmHE176493.Data.IRepository;

namespace HuynmHE176493.Web.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public SystemAccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Index()
        {
            var accounts = _accountRepository.GetAll();
            return View(accounts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountRepository.Add(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }

        public IActionResult Edit(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountRepository.Update(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }

        public IActionResult Delete(int id)
        {
            var account = _accountRepository.GetById(id);
            if (account == null) return NotFound();
            _accountRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            var accounts = _accountRepository.Find(x => x.AccountName.Contains(searchTerm)).ToList();
            return View("Index", accounts);
        }
    }
}
