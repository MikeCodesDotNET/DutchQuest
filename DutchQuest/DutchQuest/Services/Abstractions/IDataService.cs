using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DutchQuest.Models;

namespace DutchQuest.Services.Abstractions
{
    public interface IDataService
    {
        Task Initialize();
        Task Sync();
        Task<IEnumerable<Topic>> GetTopics();
    }
}
