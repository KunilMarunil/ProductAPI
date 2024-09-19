using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Product.Models;
using Product.Views;

namespace Product.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : Controller
    {
        private static string conString = "";
        #region GetConfiguration
        public static void GetConfiguration(IConfiguration configuration)
        {
            conString = configuration["ConnectionStrings:DefaultConnection"];
        }
        #endregion

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromQuery] User user)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string token = LoginView.UserLogin(user);

                    transaction.Commit();
                    return Ok(token);

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return BadRequest(e.Message);
                }
            }

        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserRegister user)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    LoginView.Register(user);
                    transaction.Commit();
                    return Ok();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return BadRequest(e.Message);
                }
            }
        }
    }
}
