using Microsoft.AspNetCore.Mvc;
using VideoCall.Common.Configuration;
using VideoCall.CommonCode;
using VideoCall.Models;
using VideoCall.Service.Users;

public class HomeController : BaseController
{
    private readonly ISessionManager _SessionManag;
    private readonly IUserService _user;
    public HomeController(ISessionManager SessionManag, IUserService user)
    {
        _SessionManag = SessionManag;
        _user = user;
    }
    public IActionResult Login()
    {
        var currentUser = _SessionManag.GetUserCall();
        if (!string.IsNullOrEmpty(currentUser.Username))
        {
            // Đã đăng nhập -> Chuyển hướng về trang Index
            return RedirectToAction("Index", "Home");
        }

        // Nếu chưa đăng nhập thì hiển thị form Login
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _user.Login(username,password);
        // Ở đây bạn có thể thay bằng logic kiểm tra CSDL
        if (user != null)
        {
            // Lưu thông tin user vào session
            SetCurrentUserCookie(user);


            // Chuyển hướng về trang Video Call
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult VideoCall()
    {
        var currentUser = _SessionManag.GetUserCall();
        if(currentUser.Id == Guid.Empty)
        {
            // Nếu chưa đăng nhập thì chuyển hướng về trang Login
            return RedirectToAction("Login");
        }
        return View(currentUser);
    }
    public IActionResult VoiceCall()
    {
        var currentUser = _SessionManag.GetUserCall();
        if (currentUser.Id == Guid.Empty)
        {
            // Nếu chưa đăng nhập thì chuyển hướng về trang Login
            return RedirectToAction("Login");
        }
        return View(currentUser);
    }
}
