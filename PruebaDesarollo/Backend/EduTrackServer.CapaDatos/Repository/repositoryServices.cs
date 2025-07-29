using EduTrackServer.CapaBase;
using EduTrackServer.CapaBase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SistemaTickets.Data;
using System.Runtime.CompilerServices;
using System.Text;



namespace SistemaTickets.Repository
{
    public class repositoryServices<T> : IDbHandler<T> where T : class
    {
        private readonly IHttpContextAccessor Icontext;
        private readonly appDbContext _context;
        private bool disposed = false;
        protected string userName;
        protected int rol;
        private string whereClouse;
        private string sql;
        private StringBuilder sb;

        public repositoryServices(appDbContext context, IHttpContextAccessor _Icontext)
        {
            Icontext = _Icontext;
            _context = context;
            
            this.userName = authorizeServices.GetUserName(Icontext);
            this.rol = authorizeServices.GetRoleUser(Icontext);
            this.whereClouse = "";
            this.sql = "";
            this.sb = new StringBuilder().Clear();
        }

        protected DbSet<T> entitySet => _context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsyncForAllWithClouse(int isFlag,T? w = null)
        {
            try
            {
                bool isWhere = false;
                string clouse = "";
                this.sb.Clear();

                if (w != null)
                {
                    var property = w.GetType().GetProperties();
                    var valueField = property.Where(s => s.GetValue(w) != null);
                    Dictionary<string, object?> whereDictionary = new Dictionary<string, object?>();

                    foreach (var c in valueField)
                    {
                        var value = c.GetValue(w);

                        if (value != null)
                        {
                            
                            if (value.GetType().IsEnum)
                            {
                                value = (int)value;
                            }

                            whereDictionary.Add(c.Name, value);
                        }
                    }

                    if (whereDictionary.Count > 0)
                    {

                        this.sb.Append($" WHERE ( " +
                                     $"{string.Join(" AND ", whereDictionary.Select(s => $"{s.Key} = '{s.Value}'"))}" +
                                     $" {clouse} AND Enabled = TRUE )");
                      
                    }
                    else
                    {
                        this.sb.Append($" WHERE (Enabled = TRUE {clouse} )");
                    }
                }
                else
                {
                    this.sb.Append($" WHERE (Enabled = TRUE {clouse} )");
                }


                sql = $" SELECT * FROM {typeof(T).Name} {this.sb.ToString()}";

                return await entitySet.FromSqlRaw(sql).ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "GetAllAsyncForAllWithClouse");
                return null;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsyncForAllWithClouse(T w = null)
        {
            try
            {
                bool isWhere = false;
                string clouse = "";
                this.sb.Clear();

                if ((rol == 1)) clouse = $"AND Username = {this.userName}";

                if (w != null)
                {
                    var property = w.GetType().GetProperties();
                    var valueField = property.Where(s => s.GetValue(w) != null);
                    Dictionary<string, object?> whereDictionary = new Dictionary<string, object?>();

                    foreach (var c in valueField)
                    {
                        var getValue = valueField.Where(v => v.Name == c.Name).Select(s => s.GetValue(w)).First();
                        whereDictionary.Add(c.Name, getValue);
                    }

                    if (whereDictionary.Count() > 0)
                    {

                        this.sb.Append($" WHERE ( " +
                                   $"{string.Join(" AND ", whereDictionary.Select(s => $"{s.Key} = '{s.Value}'"))}" +
                                   $" {clouse} AND Enabled = TRUE )");
                    }
                    else
                    {
                        this.sb.Append($" WHERE (Enabled = TRUE {clouse} )");
                    }

                }
                else
                {
                    this.sb.Append($" WHERE (Enabled = TRUE {clouse} )");
                }

                sql = $" SELECT * FROM {typeof(T).Name} {this.sb.ToString().Trim()} {clouse}".Trim();

                return await entitySet.FromSqlRaw<T>(sql).ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "GetAllAsyncForAllWithRol");
                return null;
            }
        }
        public async Task<dynamic> GetAllAsyncSp(string nameSp, T e)
        {
            try
            {
                var properties = e.GetType().GetProperties();

                var parameters = new List<object>();

                foreach (var prop in properties)
                {
                    var value = prop.GetValue(e);
                    parameters.Add(value ?? DBNull.Value);
                }

               
                parameters.Add(this.userName);

                var placeholders = string.Join(",", parameters.Select((value, i) => $"{{{i}}}"));
                var query = FormattableStringFactory.Create($"CALL {nameSp}({placeholders})", parameters.ToArray());

                return await entitySet.FromSqlInterpolated(query).ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "GetSpAllAsync");
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<T>> GetCodeAsyncAll(string nameSp)
        {
            try
            {
                return await entitySet.FromSqlRaw($"CALL {nameSp}()").ToListAsync();
            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "GetSPAsync");
                return null;
            }

        }
        public async Task<int> CreateAllAsync(T entity)
        {
            try
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                var properties = entity.GetType().GetProperties();

                // Filtramos propiedades que no son null
                var nonNullProperties = properties.Where(p => p.GetValue(entity) != null);

                foreach (var prop in nonNullProperties)
                {
                    var value = prop.GetValue(entity);
                    var type = prop.PropertyType;

                    // Si es Nullable<T>, obtenemos el tipo subyacente
                    if (Nullable.GetUnderlyingType(type) != null)
                        type = Nullable.GetUnderlyingType(type)!;

                    if (type.IsEnum)
                    {
                        // Convertimos enum a int
                        result.Add(prop.Name, (int)value!);
                    }
                    else if (type == typeof(DateTime))
                    {
                        var date = (DateTime)value!;
                        result.Add(prop.Name, date.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        result.Add(prop.Name, value);
                    }
                }

                // Agregamos el username
                result.Add("Username", this.userName);

                var tableName = entity.GetType().Name;
                var columns = string.Join(", ", result.Keys);
                var parameters = string.Join(", ", result.Keys.Select(k => $"@{k}"));

                var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";
                var sqlParams = result.Select(d => new MySqlConnector.MySqlParameter($"@{d.Key}", d.Value)).ToArray();

                await _context.Database.ExecuteSqlRawAsync(sql, sqlParams);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "InsertAsync");
                return 0;
            }
        }
        public async Task<int> UpdateAsyncAll(T entity, object _wh)
        {

            try
            {

                if (_wh != null)
                {

                    Dictionary<string, object> dictionayWh_ = new Dictionary<string, object>();
                    Dictionary<string, object> queryStrl = new Dictionary<string, object>();
                    var property_wh = entity.GetType().GetProperties();

                    var ignoreFields = property_wh.
                      Where(s => s.CustomAttributes.Count() >= 1)
                     .Where(s => s.CustomAttributes.First().AttributeType.Name == "KeyAttribute"
                     || s.CustomAttributes.Last().AttributeType.Name == "ColumnAttribute").
                     Select(s => s.Name).ToList();


                    foreach (var c in property_wh)
                    {
                        var getValue = property_wh.Where(s => s.Name == c.Name).Select(s => s.GetValue(entity))?.First() ?? "";
                        var typeOf = property_wh.Where(x => x.Name == c.Name).Select(x => x.PropertyType.GenericTypeArguments.Count() != 0 ? x.PropertyType.GenericTypeArguments[0].Name : x.PropertyType.Name).First();

                        if (getValue != "")
                        {

                            if (typeOf.Contains("DateTime"))
                            {
                                DateTime date = (DateTime)getValue;
                                queryStrl.Add(c.Name, $"'{date.ToString("yyyy-MM-dd H:mm:ss")}'");
                            }
                            else
                            {
                                queryStrl.Add(c.Name, typeOf.Contains("String") ? string.IsNullOrEmpty((string)getValue) ? "''" : $"'{getValue.ToString()}'" : getValue);
                            }
                        }

                    }

                    var _ = _wh.GetType().GetProperties().Where(s => s.GetValue(_wh) != null)
                        .Select(p => new { Name = p.Name, Value = p.GetValue(_wh) });

                    foreach (var property in _) { dictionayWh_.Add(property.Name, property.Value); }

                    var set_ = $"{string.Join(", ", queryStrl.Where(s => !ignoreFields.Contains(s.Key))
                      .Select(s => $"{s.Key} = {(s.Value)}"))}";

                    string Sql = $"UPDATE {typeof(T).Name} SET {set_.Replace("''", "null")}" +
                    $" WHERE {dictionayWh_.Select(s => $"{s.Key} = @{s.Key}").FirstOrDefault()}" +
                    $" AND Enabled = TRUE;";

                    var paramsMysql = dictionayWh_.Select(s => new MySqlConnector.MySqlParameter(s.Key, s.Value)).ToArray();

                    await _context.Database.ExecuteSqlRawAsync(Sql, paramsMysql);
                    return 1;
                }
                else
                {
                    throw new Exception("Error: requiere una clausula para hacer un update.");
                }

            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "UpdateAsync");
                return 0;
            }
        }
        public async Task<int>? DeleteAsync(T w = null)
        {

            try
            {

                this.sb.Clear();


                bool isWhere = false;
                string clouse = "";
                this.sb.Clear();

                if (w != null)
                {
                    var property = w.GetType().GetProperties();
                    var valueField = property.Where(s => s.GetValue(w) != null);
                    Dictionary<string, object?> whereDictionary = new Dictionary<string, object?>();

                    foreach (var c in valueField)
                    {
                        var getValue = valueField.Where(v => v.Name == c.Name).Select(s => s.GetValue(w)).First();
                        whereDictionary.Add(c.Name, getValue);
                    }

                    if (whereDictionary.Count() > 0)
                    {

                        this.sb.Append($" WHERE ( " +
                                   $"{string.Join(" AND ", whereDictionary.Select(s => $"{s.Key} = '{s.Value}'"))}" +
                                   $" {clouse} AND Enabled = TRUE )");
                    }
                    else
                    {
                        this.sb.Append($" WHERE (Enabled = TRUE {clouse} )");
                    }

                }
                else
                {
                    this.sb.Append($" WHERE (Enabled = TRUE {clouse} )");
                }


                string sql = $"SET sql_safe_updates = 0; " +
              $"UPDATE {typeof(T).Name} SET Enabled = FALSE {this.sb.ToString()}; " +
              $"SET sql_safe_updates = 1;";

                await _context.Database.ExecuteSqlRawAsync(sql);

                return 1;
            }
            catch (Exception ex)
            {
                ExceptionFolder(ex, "UpdateForField");
                return -1;
            }
        }
        public async Task Save() => await _context.SaveChangesAsync();
        public void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void ExceptionFolder(Exception ex, string method)
        {
            string folderPath = @"C:/LogsApplication";
            DateTime today = DateTime.Now;

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);


            string logFileName = $"{today.ToString("yyyy_MM_dd")}.txt";
            string logFilePath = Path.Combine(folderPath, logFileName);


            using (StreamWriter sw = new StreamWriter(logFilePath, append: true))
            {
                sw.WriteLine($"{today} || ${ex.Message} || ${method}");
                sw.WriteLine();
                sw.Close();
            }
        }

     
    }
}
