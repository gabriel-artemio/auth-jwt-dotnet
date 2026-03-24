using AuthJwtWebApi.Models;
using System.Data.Common;
using System.Text;

namespace AuthJwtWebApi.DAL
{
    public class UserDAL
    {
        public User? GetLogin(DbConnection cn, string email, string senha)
        {
            return GetAll(cn, email, senha).FirstOrDefault();
        }
        public List<User> GetAll(DbConnection cn)
        {
            return GetAll(cn, string.Empty, string.Empty);
        }
        private List<User> GetAll(DbConnection cn, string email, string senha)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append("id, nome, email, senha, role ");
            sb.Append("FROM usuario ");
            sb.Append("WHERE 1 = 1 ");

            List<DbParameter> p = new List<DbParameter>();
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(senha))
            {
                sb.AppendFormat(" AND email = '{0}'", email);
                sb.AppendFormat(" AND senha = '{0}'", senha);
            }

            List<User> list = new List<User>();
            using (DbCommand cmd = cn.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddRange(p.ToArray());
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new User
                        {
                            Id = dr.GetInt32(0),
                            Nome = dr.GetString(1),
                            Email = dr.GetString(2),
                            Senha = dr.GetString(3),
                            Role = dr.GetString(4)
                        });
                    }
                }
            }
            return list;
        }
    }
}
