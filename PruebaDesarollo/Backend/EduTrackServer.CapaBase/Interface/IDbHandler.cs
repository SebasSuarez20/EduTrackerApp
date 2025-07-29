using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaBase
{
    public interface IDbHandler<T> : IDisposable where T : class
    {

        public Task<IEnumerable<T>> GetAllAsyncForAllWithClouse(int isFlag, T w = null);
        public Task<IEnumerable<T>> GetAllAsyncForAllWithClouse(T w = null);
        public Task<IEnumerable<T>> GetCodeAsyncAll(string nameSp);
        public Task<dynamic> GetAllAsyncSp(string nameSp, T e);
        public Task<int> CreateAllAsync(T entity);
        public Task<int> UpdateAsyncAll(T entity, object _wh);
        public Task<int> DeleteAsync(T w = null);
    }
}
