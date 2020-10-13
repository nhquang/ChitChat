using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace ChitChat
{

    public enum Type { exists, id, name, password, age, gender, note, ip  }
    public class Database : IDisposable
    {
        private SqlConnection sql_ { get; set; }
        private SqlCommand sqlCommand_ { get; set; }

        public Database()
        {
            sql_ = new SqlConnection(ConfigurationSettings.AppSettings["connectionString"].Trim().Replace("{your_password}", Utilities.decryption(ConfigurationSettings.AppSettings["password"].Trim())));
            if (sql_.State == System.Data.ConnectionState.Closed)
                sql_.Open();

        }
        public async Task addUserAsync(User user)
        {
            var data = new Dictionary<string, object>();
            data.Add("@name_", user.name_);
            data.Add("@username_", user.username_);
            data.Add("@pass_", user.pass_);
            data.Add("@age_", user.age_);
            data.Add("@male_", user.male_);
            data.Add("@note_", user.note_);
            data.Add("@IP_", user.ip_.ToString());

            this.constructStoredProcedure("AddUser", data);
            await sqlCommand_.ExecuteNonQueryAsync();
        }

        public async Task<object> selectUsersDataByUsernameAsync(User user, Type type) 
        {
            object rslt = null;
            var data = new Dictionary<string, object>();
            data.Add("@Username", user.username_);
            this.constructStoredProcedure("SelectUser", data);
            var row = await sqlCommand_.ExecuteReaderAsync();
            switch (type)
            {
                case Type.exists:
                    rslt = row.HasRows;
                    break;
                case Type.name:
                    while (row.Read()) rslt = row.GetString(1);
                    break;
                case Type.password:
                    while (row.Read()) rslt = row.GetString(3);
                    break;
                case Type.age:
                    while (row.Read()) rslt = row.GetString(4);
                    break;
                case Type.gender:
                    while (row.Read()) rslt = row.GetString(5);
                    break;
                case Type.note:
                    while (row.Read()) rslt = row.GetString(6);
                    break;
                case Type.ip:
                    while (row.Read()) rslt = row.IsDBNull(7) ? null : row.GetString(7);
                    break;

            }
            row.Close();
            return rslt;
        }

        public async Task<User> selectUserByUsernameAsync(User user)
        {
            var data = new Dictionary<string, object>();
            data.Add("@username", user.username_);
            this.constructStoredProcedure("SelectUser", data);
            var rslt = await sqlCommand_.ExecuteReaderAsync();
            while (rslt.Read())
                user = new User((int)rslt.GetValue(0),rslt.GetString(1), rslt.GetString(2), null, null, (bool)rslt.GetValue(5), rslt.GetString(6), rslt.IsDBNull(7) ? null : rslt.GetString(7));
            rslt.Close();
            return user;
        }

        public async Task<User> selectUserByIDAsync(int id)
        {
            User user = null;
            var data = new Dictionary<string, object>();
            data.Add("@ID", id);
            this.constructStoredProcedure("SelectUserByID", data);
            var rslt = await sqlCommand_.ExecuteReaderAsync();
            while (rslt.Read())
                user = new User((int)rslt.GetValue(0), rslt.GetString(1), rslt.GetString(2), null, null, (bool)rslt.GetValue(5), rslt.GetString(6), rslt.IsDBNull(7) ? null : rslt.GetString(7));
            rslt.Close();
            return user;
        }

        

        public async Task<List<User>> selectAllUsersAsync()
        {
            var data = new Dictionary<string, object>();
            var users = new List<User>();
            this.constructStoredProcedure("SelectAllUsers", data);
            var rslt = await sqlCommand_.ExecuteReaderAsync();
            while (rslt.Read())
                users.Add(new User((int)rslt.GetValue(0), rslt.GetString(1), rslt.GetString(2), null, null, (bool)rslt.GetValue(5), rslt.GetString(6), rslt.IsDBNull(7) ? null : rslt.GetString(7)));
            rslt.Close();
            return users;
        }

        public async Task updateIPAsync(string username, string ip)
        {
            var data = new Dictionary<string, object>();
            data.Add("@Username", username);
            data.Add("@NewIP", ip);
            this.constructStoredProcedure("UpdateIP", data);
            sqlCommand_.ExecuteNonQueryAsync();

        }

        public async Task<Dictionary<int,string>> selectContactsAsync(int userId)
        {
            var contacts = new Dictionary<int, string>();
            var data = new Dictionary<string, object>();
            data.Add("@ID", userId);
            this.constructStoredProcedure("SelectContacts", data);
            var rslt = await sqlCommand_.ExecuteReaderAsync();
            while (rslt.Read())
                contacts.Add((int)rslt.GetValue(0), rslt.GetString(1));
            rslt.Close();
            return contacts;

        }
        public async Task addContactAsync(User user1, User user2)
        {
            var data = new Dictionary<string, object>();
            data.Add("@ID1", user1.id_);
            data.Add("@ID2", user2.id_);
            this.constructStoredProcedure("AddContact", data);
            await sqlCommand_.ExecuteNonQueryAsync();
                        
        }

        private void constructStoredProcedure(string proc, Dictionary<string,object> parameters)
        {
            sqlCommand_ = new SqlCommand(proc, sql_);
            sqlCommand_.CommandType = System.Data.CommandType.StoredProcedure;
            foreach(var item in parameters)
                sqlCommand_.Parameters.Add(new SqlParameter(item.Key, item.Value));
        }


        

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                if(sql_?.State == System.Data.ConnectionState.Open)
                    sql_.Dispose();
                sqlCommand_?.Dispose();
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Database() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
