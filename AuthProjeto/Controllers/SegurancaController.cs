using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthProjeto.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthProjeto.Controllers
{
    public class SegurancaController : Controller
    {
        public IActionResult Login(string requestPath)
        {
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            if (!ValidaLogin(login.Usuario, login.Senha))
                return View();

            // cria claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "authString"),
                new Claim(ClaimTypes.Email, login.Usuario)
            };

            // cria um identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // cria a claims principal
            //Uma implementação do IPrincipal que dá suporte a várias identidades      
            //baseadas em declarações.
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                    scheme: "authString",
                    principal: principal,
                    properties: new AuthenticationProperties
                    {
                        //IsPersistent = true, // para o recurso 'lembrar-me'
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                    });

            return Redirect(login.RequestPath ?? "/");
        }

        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(scheme: "authString");
            return RedirectToAction("Login");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        private bool ValidaLogin(string usuario, string senha)
        {
            //codigo para acessar um banco de dados 
            //e obter as credenciais armazenadas
            return (usuario == "mac" && senha == "numsey");
        }
    }
}