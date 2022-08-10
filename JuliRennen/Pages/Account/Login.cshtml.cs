using JuliRennen.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Linq;

namespace JuliRennen.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly JuliRennen.Data.JuliRennenContext _context;
        public LoginModel(JuliRennen.Data.JuliRennenContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Credential Credentials { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();


            //User s = _context.User.Find(Credentials.Username);

            //var s = _context.User.AsQueryable();
            //User s = (User)_context.User.Where(x => x.UserName == Credentials.Username);
            List<User> l = _context.User.ToList();
            User s = l.Find(x => x.UserName.Contains(Credentials.Username)); //IEnumerable<User> User = from UserName in User

            if (s != null && Credentials.Username == s.UserName && Credentials.Password == s.SetPassword)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, s.UserName),
                    new Claim(ClaimTypes.Email, s.EmailAddress),

                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                HttpContext.Session.SetInt32("UserID", s.ID);

                return RedirectToPage("/Index");
            }
            if (Credentials.Username == "admin" && Credentials.Password == "password")
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@yc.nl")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }
            return Page();
        }

        public class Credential
        {
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
