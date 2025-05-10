
using OrderManagementSystem.Data.Entity;
using System.Collections.Generic;

    namespace OrderManagementSystem.Services
    {
        public interface IUserHistoryService
        {
            void AddHistory(Data.Entity.UserHistory history);
            List<Data.Entity.UserHistory> GetUserHistory(int userId);
        }
    }

