using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Data
{
    public class XmlDataProvider
    {
        private Dictionary<string, object> _data;

        private  XmlDataProvider()
        {
            _data = new Dictionary<string, object>();
        }

        private static XmlDataProvider _instance;
        public static XmlDataProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new XmlDataProvider();

                return _instance;
            }
        }

        public T Get<T>(string fileName) where T : class, new()
        {
            if (_data.ContainsKey(fileName))
                return _data[fileName] as T;

            var fileData = Serializer.DeSerializeObject<T>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/" + fileName));
            _data.Add(fileName, fileData);

            return fileData;
        }
    }
}
