using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Data
{
    public abstract class DataRepository <T> where T : Record
    {
        private HashSet<T> _records;

        /// <summary>
        /// Get the instance if it exists
        /// </summary>
        /// <param name="id">The key</param>
        /// <returns>The isntance of the T object</returns>
        public virtual T Get(long id)
        {
            return _records.FirstOrDefault(r => r.Id == id);
        }
        
        /// <summary>
        /// Insert or update an instance
        /// </summary>
        /// <param name="id">The key, 0 if insert</param>
        /// <param name="record">The instance of the object</param>
        public virtual void Put(long id, T record)
        {
            // New item, get new id
            if (id <= 0)
                record.Id = (_records.Count > 0) ? _records.Max(r => r.Id) + 1 : 1;
            else // Existing item, remove to add the updated instance
                _records.RemoveWhere(r => r.Id == id);

            _records.Add(record);
        }

        /// <summary>
        /// Delete the object if it exists
        /// </summary>
        /// <param name="id">The key</param>
        public virtual void Delete(long id)
        {
            _records.RemoveWhere(r => r.Id == id);
        }
    }
}
