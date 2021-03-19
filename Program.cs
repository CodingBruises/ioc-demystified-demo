namespace IoC_Demystified_Demo
{
    internal static class Program
    {
        private static void Main()
        {
            var container = new Container();
            container.Register<IBlogPostService, BlogPostService>();
            container.Register<IBlogPostRepository, BlogPostRepository>();
            container.Register<INotificationService, NotificationService>();

            var service = container.Resolve<IBlogPostService>();
            service.CreateBlogPost("Demoing an IoC example", "Danl Barron", "...");
        }
    }
}
