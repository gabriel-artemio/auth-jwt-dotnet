using AuthJwtWebApi.Models;
using System.Data.Common;

namespace AuthJwtWebApi.DAL
{
    public class UserDAL
    {
        public User? GetByEmail(DbConnection cn, string email)
        {
            string sql = "SELECT id, nome, email, senha, role FROM usuario WHERE email = @Email";

            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;

                var param = cmd.CreateParameter();
                param.ParameterName = "@Email";
                param.Value = email;
                cmd.Parameters.Add(param);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new User
                        {
                            Id = dr.GetInt32(0),
                            Nome = dr.GetString(1),
                            Email = dr.GetString(2),
                            Senha = dr.GetString(3), // HASH
                            Role = dr.GetString(4)
                        };
                    }
                }
            }

            return null;
        }
        public void Insert(DbConnection cn, User user)
        {
            string sql = @"
                INSERT INTO usuario (nome, email, senha, role)
                VALUES (@Nome, @Email, @Senha, @Role)
            ";

            using (var cmd = cn.CreateCommand())
            {
                cmd.CommandText = sql;

                var p1 = cmd.CreateParameter();
                p1.ParameterName = "@Nome";
                p1.Value = user.Nome;

                var p2 = cmd.CreateParameter();
                p2.ParameterName = "@Email";
                p2.Value = user.Email;

                var p3 = cmd.CreateParameter();
                p3.ParameterName = "@Senha";
                p3.Value = user.Senha;

                var p4 = cmd.CreateParameter();
                p4.ParameterName = "@Role";
                p4.Value = user.Role;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
