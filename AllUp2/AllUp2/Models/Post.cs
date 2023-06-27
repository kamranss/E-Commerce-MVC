namespace AllUp2.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int SocialPlatformId { get; set; }
        public SocialPlatform SocialPlatform { get; set; }
        public string ImageUrl { get; set; }
    }
}
