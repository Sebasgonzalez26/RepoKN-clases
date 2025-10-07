using KN_ProyectoWeb.Models;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        #region Iniciar Sesion


        #region Registro

        #region RecuperarAcceso

        [HttpGet]
        public ActionResult Index()

        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)

        {

            /*Progra para ir al iniciar sesion a la BD*/


            return RedirectToAction("Principal", "Home");
        }

        #endregion


       

        [HttpGet]
        public ActionResult Registro()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {


            /*Progra para ir a guardar el usuario a la BD*/


            return View();

        }
        #endregion


        


        [HttpGet]
        public ActionResult RecuperarAcceso()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RecuperarAcceso(Usuario usuario)
        {


            /*Verificar que el usuario exista, generarle una contraseña temporal, enviarle esa clave tmp*/


            return View();

        }

        #endregion

        [HttpGet]

        public ActionResult Principal()
        {
            return View();
        }

    }
}