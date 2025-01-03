using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Website.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public int? SenderId { get; set; }

        public string? SenderName { get; set; }
        public string MessageText { get; set; }
        public bool Read {  get; set; }

        [ForeignKey(nameof(SenderId))]
        public virtual User Sender { get; set; }

        public int ReciverId { get; set; }

        [ForeignKey(nameof(ReciverId))]
        public virtual User Reciver { get; set; }
    }
}
