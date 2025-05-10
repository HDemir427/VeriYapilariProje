
using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Utilities;

    namespace OrderManagementSystem.Services
    {
        public class UserHistoryService : IUserHistoryService
        {
            private readonly CustomLinkedList<UserHistory> _userHistories;

            public UserHistoryService(CustomLinkedList<UserHistory> userHistories)
            {
                _userHistories = userHistories;
            }

            public void AddHistory(UserHistory history)
            {
                _userHistories.AddLast(history);
            }

            public List<UserHistory> GetUserHistory(int userId)
            {
                return _userHistories.Where(h => h.UserId == userId).ToList();
            }
        }
    }

