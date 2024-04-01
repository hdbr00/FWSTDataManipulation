using Microsoft.AspNetCore.Mvc;
using SocketApplicationI.Classes;
using SocketApplicationI.Models;

namespace SocketApplicationI.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Lista all and Filter. and searched
        public List<ProductoCLS> listarProductos(string descripcion)
        {
            List<ProductoCLS> list = new List<ProductoCLS>();
            try
            {
                using (DbAa6a3bBdreportesContext bd = new DbAa6a3bBdreportesContext())
                {
                    if (descripcion == null)
                    {
                        list = (from producto in bd.Productos
                                where producto.Bhabilitado == 1
                                select new ProductoCLS
                                {
                                    iidProducto = producto.Iidproducto,
                                    description = producto.Descripcion,
                                    precioCadena = "$/"+((decimal)producto.Precio).ToString(),
                                    stockCadena = ((int)producto.Stock).ToString()+" Unid"
                                }).ToList();
                    }
                    else
                    {
                        list = (from producto in bd.Productos
                                where producto.Bhabilitado == 1
                                && producto.Descripcion.Contains(descripcion)
                                select new ProductoCLS
                                {
                                    iidProducto = producto.Iidproducto,
                                    description = producto.Descripcion,
                                    precioCadena = "$/" + ((decimal)producto.Precio).ToString(),
                                    stockCadena = ((int)producto.Stock).ToString() + "Unid"
                                }).ToList();
                    }

                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ProductoCLS recuperarProducto(int id)
        {
            ProductoCLS oProductoCLS; 

            try
            {
                using (DbAa6a3bBdreportesContext bd = new DbAa6a3bBdreportesContext())
                {
                    oProductoCLS = (from producto in bd.Productos
                                    where producto.Iidproducto == id
                                    select new ProductoCLS
                                    {
                                      iidProducto= producto.Iidproducto,
                                      description= producto.Descripcion,
                                      precio = (decimal)producto.Precio,  
                                      stock = (int) producto.Stock
                                    }).First(); 
                }
            }
            catch (Exception ex)
            {

                oProductoCLS = new ProductoCLS();
            }

            return oProductoCLS; 
        }




        public int eliminarProducto(int id)
        {
            //Error = 0
            int rpta = 0; 
            try
            {
                using (DbAa6a3bBdreportesContext bd = new DbAa6a3bBdreportesContext())
                {
                    //Eliminar lógico cambio de estado. 
                    Producto oProducto = bd.Productos.Where(p => p.Iidproducto == id).First();
                    oProducto.Bhabilitado = 0;
                    bd.SaveChanges();

                    rpta = 1;  //Successfully. 

                }

            }
            catch (Exception ex)
            {

                rpta = 0; 

            }

            return rpta; 
        }


        //A este controlador
        [HttpPost]
        public int guardarProducto(ProductoCLS oProductoCLS)
        {
            int rpta = 0;

            try
            {
                int iidproducto = oProductoCLS.iidProducto;
                using(DbAa6a3bBdreportesContext bd = new DbAa6a3bBdreportesContext())
                {

                    if (iidproducto == 0)
                    {
                        Producto oProducto = new Producto();
                        oProducto.Descripcion = oProductoCLS.description;
                        oProducto.Precio = oProductoCLS.precio; 
                        oProducto.Stock = oProductoCLS.stock;
                        oProducto.Bhabilitado = 1; 
                        bd.Productos.Add(oProducto);
                        bd.SaveChanges();
                        //Exito. 
                        rpta = 1; 
                    }
                    else
                    {
                        Producto oProducto = bd.Productos.Where(p => p.Iidproducto == iidproducto).First();
                        oProducto.Descripcion = oProductoCLS.description;
                        oProducto.Precio = oProductoCLS.precio;
                        oProducto.Stock = oProductoCLS.stock;
                        bd.SaveChanges(); 

                    }

                }
            }
            catch (Exception)
            {

                throw;
            }

            return rpta; 

        }

    }
}
