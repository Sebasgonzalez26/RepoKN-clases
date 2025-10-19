using KN_ProyectoWeb.EF;
using KN_ProyectoWeb.Models;
using System.Linq;
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

            using (var context = new BD_KNEntities())
            {

                //var resultado = context.tbUsuario.Where(x => x.CorreoElectronico == usuario.CorreoElectronico
                //                                                                                && x.Contrasenna == usuario.Contrasenna
                //                                                                                && x.Estado == true).FirstOrDefault();


                var resultado = context.ValidarUsuarios(usuario.CorreoElectronico, usuario.Contrasenna).FirstOrDefault();


                if(resultado != null)
                {
                    return RedirectToAction("Principal", "Home");

                }

                ViewBag.Mensaje = "La informacion no se ha podido registrar";

                return View();





            }


            
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


            using (var context = new BD_KNEntities())
            {
                //var nuevoUsuario = new tbUsuario
                //{

                //    Identificacion = usuario.Identificacion,
                //    Nombre = usuario.Nombre,
                //    CorreoElectronico = usuario.CorreoElectronico,
                //    Contrasenna = usuario.Contrasenna,
                //    idPerfil = 2,
                //    Estado = true



                //};

                //context.tbUsuario.Add(nuevoUsuario);
                //context.SaveChanges();

                var resultado = context
                    .CrearUsuarios(usuario.Identificacion, usuario.Nombre, usuario.CorreoElectronico, usuario.Contrasenna)
                    .FirstOrDefault();   


                if (resultado > 0)
                {
                   return  RedirectToAction("Index", "home");
                }
                
                
                    ViewBag.Mensaje = "La informacion no se ha podido registrar.";
                    return View();

                

            }
         
            /*Progra para ir a guardar el usuario a la BD*/


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