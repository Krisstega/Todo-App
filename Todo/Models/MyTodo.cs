using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class MyTodo
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Activity { get; set; }
        public string Time { get; set; }
        public string Remark { get; set; }
        public MyTodo()
        {

        }
    }
}
