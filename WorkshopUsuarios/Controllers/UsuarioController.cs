using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WorkshopUsuarios.Models;
using System.Web.Security;
using System;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace WorkshopUsuarios.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {

        private static List<Usuario> RepositorioUsuario { get; set; }

        [OutputCache(CacheProfile = "OneMinuteCache")]
        public ActionResult Index()
        {
            if(RepositorioUsuario==null)
                LlenarRepositorioUsuario();
            
            return View(RepositorioUsuario);
        }

        public ViewResult Grabar()
        {
            return View(new Usuario());
        }

        public ViewResult Actualizar(int id)
        {
            var usuario = RepositorioUsuario.First(x => x.Id == id);
            return View("Grabar", usuario);
        }

        public ViewResult GrabarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.Id > 0)
                {
                    var usu = RepositorioUsuario.First(x => x.Id == usuario.Id);
                    usu.Nombre = usuario.Nombre;
                    usu.ApellidoPaterno = usuario.ApellidoPaterno;
                    usu.ApellidoMaterno = usuario.ApellidoMaterno;
                    usu.Edad = usuario.Edad;
                    usu.Email = usuario.Email;
                    usu.Login = usuario.Login;
                    usu.Password = usuario.Password;
                    return View("Index",RepositorioUsuario);
                }

                usuario.Id = ObtenerCorrelativoUsuario();
                RepositorioUsuario.Add(usuario);
                ViewBag.MensajeSatisfactorio = "El usuario se registro satisfactoriamente.";
                ModelState.Clear();
            }

            return View("Grabar");
        }

        [HttpGet]
        [OutputCache(CacheProfile = "OneMinuteWithParamCache")]
        public ViewResult Buscar(Usuario usuario)
        {
            ModelState.Remove("Nombre");
            ModelState.Remove("Login");

            if (!RepositorioUsuario.Any())
                return View("Index");

            var lista = RepositorioUsuario.Where(x => (string.IsNullOrEmpty(usuario.Nombre) || x.Nombre.Contains(usuario.Nombre)) &&
                        (string.IsNullOrEmpty(usuario.Login) || x.Login.Contains(usuario.Login))).ToList();
            return View("Index", lista);
        }

        private int ObtenerCorrelativoUsuario()
        {
            return RepositorioUsuario.Max(x => x.Id + 1);
        }

        private void LlenarRepositorioUsuario()
        {
            RepositorioUsuario = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nombre = "Luis",
                    ApellidoPaterno = "Rojas",
                    ApellidoMaterno = "Vásquez",
                    Edad = 29,
                    Email = "lrojas@avances.com.pe",
                    Login = "lrojas"
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Juan",
                    ApellidoPaterno = "Perez",
                    ApellidoMaterno = "Carranza",
                    Edad = 30,
                    Email = "jperez@avances.com.pe",
                    Login = "jperez"
                },
                new Usuario
                {
                    Id = 3,
                    Nombre = "Miguel",
                    ApellidoPaterno = "Dávila",
                    ApellidoMaterno = "Lazón",
                    Edad = 26,
                    Email = "mdavila@avances.com.pe",
                    Login = "mdavila"
                }
            };
        }

        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(Usuario usuario)
        {
            if (usuario.Login == "lrojas" && usuario.Password == "luis")
            {
                FormsAuthentication.SetAuthCookie(usuario.Login, true);

                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                var usuarioClaim = new Claim(ClaimTypes.Name, usuario.Login);
                identity.AddClaim(usuarioClaim);
                //var identity = new ClaimsIdentity(new[] {
                //            new Claim(ClaimTypes.Name, usuario.Login),
                //        },
                //        DefaultAuthenticationTypes.ApplicationCookie,
                //        ClaimTypes.Name, ClaimTypes.Role);
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, identity);


                return RedirectToAction("Index");
            }

            return View("NoAutorizado");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}