using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;


namespace Sertifikasi.Models
{
    public class user
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public user(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }
        public userModel getDataByUsername_Password(string username, string password)
        {
            userModel userModel = new userModel();
            try
            {
                string query = "SELECT * FROM tb_pengguna where username = @p1 AND password = @p2";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", username);
                command.Parameters.AddWithValue("@p2", password);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                userModel.Id = Convert.ToInt32(reader["id_user"].ToString());
                userModel.nama = reader["nama"].ToString();
                userModel.role = reader["role"].ToString();
                userModel.status = reader["status"].ToString();
                userModel.username = reader["username"].ToString();
                userModel.password = reader["password"].ToString();
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                return null;
                Console.WriteLine(ex.Message);
            }
            return userModel;

        }
    }
}
