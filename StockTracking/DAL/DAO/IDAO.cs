using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    // T = tipos de datos y K = clase que utilizaremos
    interface IDAO<T,K> where T:class where K:class
    {
        List<K> Select();
        bool Insert(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool GetBack(int ID);

    }
}
