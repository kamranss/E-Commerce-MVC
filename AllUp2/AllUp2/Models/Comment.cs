using System.ComponentModel.DataAnnotations;

namespace AllUp2.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CommentContent { get; set; }
        public bool IsReply { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string ImageUrl { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }

        public Comment()
        {
            CreatedDate = DateTime.Now;
            ImageUrl = "comment-author.jpg";
            Replies = new List<Comment>();
            IsReply = false;
        }
    }
}
