var socket;
window.onload = function () {
    socket = new WebSocket("ws://192.168.0.32:9001");
    socket.onopen = function() { 
        Exito("Nos conectamos al socket"); 
    }
    socket.onclose = function () {
        Error("Se cerro la conexion"); 
    }
    socket.onerror = function () { 
        Error("Ocurrio un error en el socket"); 
    }

    listaProductos();
}

function listaProductos() {
    pintar({
        url: "Producto/listarProductos",
        cabeceras: ["Descripcion", "Precio", "Stock"],
        propiedades: ["description", "precioCadena", "stockCadena"],
        editar: true,
        eliminar: true,
        propiedadId:"iidProducto"
    });
}

/*Search*/
function BuscarDatos(indPag=0,indBloq=0) {
    var frmBusqueda = document.getElementById('frmBusqueda');
    var frm = new FormData(frmBusqueda);
    fetchPost("Producto/listarProductos", "json", frm, function (rpta) {
        indicePagina = indPag;
        indiceBloque = indBloq;
        document.getElementById('divContenedorTabla').innerHTML = generarTabla(rpta)
        configurarPaginacion();
    });

}

/*Edit*/
function Editar(id) {
    document.getElementById("btnNuevo").click();
    recuperarGenerico("Producto/recuperarProducto/?id=" + id,"frmProducto");
}

/*Delete*/
function Eliminar(id) {

    Confirmacion("Confirmación", "Desea realmente eleminar el registro? ", function () {
        // User says "Yes", execute this!
        fetchGet("Producto/eliminarProducto/?id=" + id, "text", function (rpta) {
            if (rpta == 1) {
                Exito("Se guardo correctamente");
                BuscarDatos(indicePagina, indiceBloque); 
            } else {
                Error("Occurred an error"); 
            }
        })
    })
}