using CRUDASPNETCORE_SP_1TABLA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CRUDASPNETCORE_SP_1TABLA.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("sp_usuarios", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter adapter = new(cmd);
                    DataTable dt = new();
                    adapter.Fill(dt);
                    adapter.Dispose();
                    List<Usuario> list = new();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dt.Rows[i][0]),
                            Nombre = (dt.Rows[i][1]).ToString(),
                            Edad = Convert.ToInt32(dt.Rows[i][2]),
                            Correo = (dt.Rows[i][3]).ToString()
                        });
                    }
                    ViewBag.Usuarios = list;
                    con.Close();
                }
            }
                    return View();
        }

        public IConfiguration Configuration { get;}

        public UsuariosController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new("sp_registrar", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 50).Value = usuario.Nombre;
                    cmd.Parameters.Add("@edad", System.Data.SqlDbType.Int).Value = usuario.Edad;
                    cmd.Parameters.Add("@correo", System.Data.SqlDbType.VarChar, 50).Value = usuario.Correo;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Index");
        }
    }
}
