using KN_ProyectoWeb.EF;
using KN_ProyectoWeb.Models;
using KN_ProyectoWeb.Services;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        Utilitarios utilitarios = new Utilitarios();

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


                if (resultado != null)


                {
                    Session["NombreUsuario"] = resultado.Nombre;
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
                    return RedirectToAction("Index", "home");
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
            using (var context = new BD_KNEntities())
            {
                //Actualizar la contrasenna del usuario
                var resultadoConsulta = context.tbUsuario
                    .Where(x => x.CorreoElectronico == usuario.CorreoElectronico).FirstOrDefault();





                if (resultadoConsulta != null)
                {

                    var contrasennaGenerada = utilitarios.GenerarContrasenna();


                    //Actualizar la nueva contraseña
                    resultadoConsulta.Contrasenna = contrasennaGenerada;
                    var resultadoActualizacion = context.SaveChanges();



                    //Envia el correo Electronico al usuario con la nueva contrasenna
                    if (resultadoActualizacion > 0)
                    {
                        //StringBuilder mensaje = new StringBuilder();
                        //mensaje.Append("Estimado(a) " + resultadoConsulta.Nombre + "<br>");
                        //mensaje.Append("Se ha generado una solicitud de recuperación de acceso a su nombre." + "<br><br>");
                        //mensaje.Append("Su nueva contraseña de acceso es: " + contrasennaGenerada + "<br><br>");
                        //mensaje.Append("Procure realizar el cambio de su contraseña una vez ingrese al sistema.<br>");
                        //mensaje.Append("Muchas gracias.");



                        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                        string path = Path.Combine(projectRoot, "TemplateRecuperacion.html");

                        // Leer todo el HTML
                        string htmlTemplate = System.IO.File.ReadAllText(path);


                        // Reemplazar placeholders
                        string mensaje = htmlTemplate
                            .Replace("{{Nombre}}", resultadoConsulta.Nombre)
                            .Replace("{{Contrasena}}", contrasennaGenerada);





                        utilitarios.EnviarCorreo("Contraseña de acceso", mensaje, resultadoConsulta.CorreoElectronico);

                        return RedirectToAction("Index", "Home");



                    }



                    //Esta es su nueva contrasenna


                }






                ViewBag.Mensaje = "La informacion no se ha podido restablecer .";

                return View();
            }
        }

        #endregion

        [HttpGet]

        public ActionResult Principal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {

            Session.Clear();


            return RedirectToAction("Index", "Home");
        }



    }
    
}