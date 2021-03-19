using System.Text;

namespace IoC_Demystified_Demo
{
    public interface IBlogPostService
    {
        void CreateBlogPost(string title, string createdBy, string blogContent);
    }

    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly INotificationService _notificationService;

        public BlogPostService(
            IBlogPostRepository blogPostRepository,
            INotificationService notificationService)
        {
            _blogPostRepository = blogPostRepository;
            _notificationService = notificationService;
        }

        public void CreateBlogPost(string title, string createdBy, string blogContent)
        {
            _blogPostRepository.CreateBlogPost(title, createdBy, blogContent);

            var message = new StringBuilder();
            message.AppendLine("Hi subscribers!");
            message.AppendLine($"  {createdBy} has just uploaded a new blog post, '{title}'.");
            message.AppendLine("  Check it out and be sure to add a thumbs up!");
            message.AppendLine("Thanks, The Coding Bruises staff");

            _notificationService.SendNotification(message.ToString());
        }
    }
}
