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
        // GET: Hiển thị profile của Staff
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được xem
            {
                return RedirectToAction("Index", "Home");
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (!userId.HasValue) // Kiểm tra nếu chưa đăng nhập
            {
                return RedirectToAction("Index", "Login");
            }

            var account = _accountService.GetAccountById(userId.Value);
            if (account == null) return NotFound();

            return View(account);
        }

        // POST: Cập nhật thông tin cơ bản (không bao gồm mật khẩu)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(SystemAccount account)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được chỉnh sửa
            {
                return RedirectToAction("Index", "Home");
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (!userId.HasValue || userId.Value != account.AccountId) // Đảm bảo chỉ sửa profile của chính mình
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                var existingAccount = _accountService.GetAccountById(userId.Value);
                if (existingAccount != null)
                {
                    // Chỉ cập nhật AccountName và AccountEmail
                    existingAccount.AccountName = account.AccountName;
                    existingAccount.AccountEmail = account.AccountEmail;
                    _accountService.UpdateAccount(existingAccount);

                    // Cập nhật lại Session nếu email thay đổi
                    HttpContext.Session.SetString("UserEmail", existingAccount.AccountEmail);
                    TempData["SuccessMessage"] = "Cập nhật thông tin cá nhân thành công!";
                    return RedirectToAction("Profile");
                }
            }

            return View(account);
        }

        // GET: Hiển thị form đổi mật khẩu
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được đổi mật khẩu
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Xử lý đổi mật khẩu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 1) // Chỉ Staff (role = 1) được đổi mật khẩu
            {
                return RedirectToAction("Index", "Home");
            }

            int? userId = HttpContext.Session.GetInt32("UserID");
            if (!userId.HasValue) // Kiểm tra nếu chưa đăng nhập
            {
                return RedirectToAction("Index", "Login");
            }

            var account = _accountService.GetAccountById(userId.Value);
            if (account == null) return NotFound();

            // Kiểm tra mật khẩu cũ
            if (account.AccountPassword != oldPassword)
            {
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không đúng.");
                return View();
            }

            // Kiểm tra mật khẩu mới và xác nhận
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Mật khẩu mới và xác nhận không khớp.");
                return View();
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("newPassword", "Mật khẩu mới không được để trống.");
                return View();
            }

            // Cập nhật mật khẩu mới
            account.AccountPassword = newPassword;
            _accountService.UpdateAccount(account);
            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Profile");
        }
    }
}
