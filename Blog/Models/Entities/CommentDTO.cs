namespace Blog.Models.Entities
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
