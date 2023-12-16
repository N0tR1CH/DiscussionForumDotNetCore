using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionForum.Models
{
    public class Threads
    {
        public int Id { get; set; }
        public int ForumId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool Pinned { get; set; }
        public int ViewsNumber { get; set; }
        public int AnswersNumber { get; set; }
    }
}