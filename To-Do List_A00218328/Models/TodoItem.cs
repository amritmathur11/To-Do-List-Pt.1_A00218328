using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_Do_List_A00218328.Models
{
    public class TodoItem
    {
        public int TodoItemId { get; set; }
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Done { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}
