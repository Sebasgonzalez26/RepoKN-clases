using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Controllers
{
    public class ProductosController : Controller
    {

        #region VerProductos
        [HttpGet]
        public ActionResult VerProductos()


        {


            using (var context = new EF.BD_KNEntities())
            {

                var productos = context.tbProducto.Include("tbCategoria").ToList();



                return View(productos);
            }


        }
            
                
        }


        

        #endregion
    }
