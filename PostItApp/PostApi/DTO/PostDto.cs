namespace PostApi.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; }
        public int LikeCount { get; set; }
    }
}