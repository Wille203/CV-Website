using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace CV_Website.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public string? SenderName { get; set; }
        public string MessageText { get; set; }
        public bool Read {  get; set; }

        public string? SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        [ValidateNever]
        public virtual User Sender { get; set; }

        public int ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        [ValidateNever]
        public virtual User Receiver { get; set; }
    }
}
