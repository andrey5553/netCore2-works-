using System;
using System.Collections.Generic;
using System.Net;

namespace WebStore.Interfaces.TestApi
{
    public interface IValueService
    {
        /// <summary>
        /// получение всего списка
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Get();
        
        /// <summary>
        /// получение по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string Get(int id);
        
        /// <summary>
        /// сохранение значения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Uri Post(string value);
        
        /// <summary>
        /// обновление
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        HttpStatusCode Update(int id, string value);
        
        /// <summary>
        /// удаление по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HttpStatusCode Delete(int id);
    }
}
