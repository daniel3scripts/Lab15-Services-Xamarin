using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoRest.Model;

namespace TodoRest.Data
{
    public interface IRestService
    {
        Task<List<TodItem>> RefreshDataAsync();

        Task SaveTodoItemAsync(TodItem item,bool isNewItem);
        Task DeleteTodoItemAsync(string id);
    }
}
