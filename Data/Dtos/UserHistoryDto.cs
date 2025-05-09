
using System.ComponentModel.DataAnnotations;


namespace OrderManagementSystem.Dto
{
    public class UserHistoryDto
    {
        public int HistoryId { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; }
        public string ActionType { get; set; }
        public DateTime ActionDate { get; set; }
        public string Details { get; set; }
    }
}
