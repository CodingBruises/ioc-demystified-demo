using System;

namespace IoC_Demystified_Demo
{
    public interface IBlogPostRepository
    {
        void CreateBlogPost(string title, string createdBy, string blogContent);
    }

    public class BlogPostRepository : IBlogPostRepository
    {
        public void CreateBlogPost(string title, string createdBy, string blogContent)
        {
            Console.WriteLine($"Blog post: '{title}' by {createdBy} successfully saved to disk");
        }
    }
}
