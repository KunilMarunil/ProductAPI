using Microsoft.Data.SqlClient;
using Product.Models;
using System.Data;
using System.Security.Claims;

namespace Product.Views
{
    public class LoginView
    {
        private static string conString = "";
        #region GetConfiguration
        public static void GetConfiguration(IConfiguration configuration)
        {
            conString = configuration["ConnectionStrings:DefaultConnection"];
        }
        #endregion

        public static string Register(UserRegister user)
        {
            byte[] passwordHash = Array.Empty<byte>();
            byte[] passwordSalt = Array.Empty<byte>();

            (passwordHash, passwordSalt) = CryptoView.GenerateHash(user.Password);

            string query = "INSERT INTO Users(Username, PasswordHash, PasswordSalt, Role, IsActive, CreatedAt, email) VALUES(@Username, @PasswordHash, @PasswordSalt, 'User', 1, GETDATE(), @email)";

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@username",SqlDbType.VarChar) { Value = user.Username},
                new SqlParameter("@PasswordHash", SqlDbType.Binary) { Value = passwordHash },
                new SqlParameter("@PasswordSalt", SqlDbType.Binary) { Value = passwordSalt },
                new SqlParameter("@email", SqlDbType.VarChar) { Value = user.email }
            };

            CRUD.ExecuteNonQuery(query, sqlParams);

            string token = Guid.NewGuid().ToString();
            query = "INSERT INTO Token(Token, usage_type, Username) VALUES (@token, 'register', @username)";
            sqlParams = new SqlParameter[]
            {
                    new SqlParameter("@username", SqlDbType.VarChar) { Value = user.Username},
                    new SqlParameter("@token", SqlDbType.VarChar) { Value = token }
            };

            CRUD.ExecuteNonQuery(query, sqlParams);

            return "Success";

        }

        public static string UserLogin(User user)
        {
            string query = "SELECT * FROM Users WHERE Username = @username";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@username", SqlDbType.VarChar){Value= user.Username}
            };

            DataTable isUserExist = CRUD.ExecuteQuery(query, sqlParameter);


            query = "SELECT * FROM Token WHERE Username = @username AND usage_type = 'register'";

            sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@username", SqlDbType.VarChar){Value= user.Username}
            };

            DataTable isTokenExist = CRUD.ExecuteQuery(query, sqlParameter);

            int pk_user_id = new();
            string username = string.Empty;
            string role = string.Empty;
            int IsActive = new();

            byte[] passwordHash = Array.Empty<byte>();
            byte[] passwordSalt = Array.Empty<byte>();


            foreach (DataRow row in isUserExist.Rows)
            {
                pk_user_id = row["pk_user_id"] == DBNull.Value ? 0 : (int)row["pk_user_id"];
                username = row["username"] == DBNull.Value ? "" : (string)row["username"];
                role = row["role"] == DBNull.Value ? "" : (string)row["role"];
                passwordHash = row["password_hash"] == DBNull.Value ? Array.Empty<byte>() : (byte[])row["password_hash"];
                passwordSalt = row["password_salt"] == DBNull.Value ? Array.Empty<byte>() : (byte[])row["password_salt"];
                IsActive = row["IsActive"] == DBNull.Value ? 0 : (int)row["IsActive"];
            }

            bool isPasswordValid = CryptoView.CompareStringVsHash(user.Password, passwordHash, passwordSalt);

            string jwtToken = JwtView.GenerateJwtToken(new[]
            {
                new Claim("pk_user_id", pk_user_id.ToString() ?? "0"),
                new Claim ("username", username ?? ""),
                new Claim("role", "user" ?? ""),
                new Claim("IsActive", IsActive.ToString()?? "0"),

            });

            return jwtToken;
        }
    }
}
