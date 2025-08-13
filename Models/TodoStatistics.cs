using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_CLI_App.Models
{
    public class TodoStatistics
    {
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Pending { get; set; }
        public double CompletionRate { get; set; }


        // Creates statistics from a collection of todo items
    
        public static TodoStatistics FromTodoItems(IEnumerable<TodoItem> items)
        {
            var itemsList = items.ToList();
            var total = itemsList.Count;
            var completed = itemsList.Count(item => item.IsCompleted);
            var pending = total - completed;
            var completionRate = total > 0 ? (double)completed / total * 100 : 0;

            return new TodoStatistics
            {
                Total = total,
                Completed = completed,
                Pending = pending,
                CompletionRate = Math.Round(completionRate, 1)
            };
        }

        // Returns formatted statistics string
        public override string ToString()
        {
            return $"Total: {Total}, Completed: {Completed}, Pending: {Pending}, Completion Rate: {CompletionRate}%";
        }
    }
}
