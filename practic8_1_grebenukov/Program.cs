using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using practic8_1_grebenukov.Classes;
using practic8_1_grebenukov.Data;
using practic8_1_grebenukov.Models;

class Program
{
    static void Main (string[] args)
    {
        Seeder.InitialSeed();
        using (var context = new BlogContext())
        {

            //foreach(Post post in context.Posts.Include(p => p.Blog))
            //{
            //Console.WriteLine($"{post.Blog.BlogName} - {post.PostName}");
            //Console.WriteLine(post.PostText);
            //Console.WriteLine(post.PublicationDate.ToShortDateString() + "\n");
            //}

            //var post = context.Posts.First();
            //context.Entry(post).Reference(p => p.Blog).Load();
            //Console.WriteLine($"{post.Blog.BlogName} - {post.PostName}");
            //Console.WriteLine(post.PostText);
            //Console.WriteLine(post.PublicationDate.ToShortDateString() + "\n");


            Zad6();
        }
    }
    public static void Zad1 ()
    {
        using (var context = new BlogContext())
        {
            foreach (Post post in context.Posts.Include(p => p.Blog).OrderBy(p => p.PublicationDate))
            {
                Console.WriteLine($"{post.Blog.BlogName} - {post.PostName}");
                Console.WriteLine($"Дата публикации: {post.PublicationDate.ToShortDateString()} \n");
            }
        }
    }
    public static void Zad2 ()
    {
        using (var context = new BlogContext())
        {
            foreach (Post post in context.Posts.Include(p => p.Blog).Where(p => p.Blog.BlogName == "С# для начинающих"))
            {
                Console.WriteLine($"{post.Blog.BlogName} - {post.PostName}");
                Console.WriteLine($"Описание: {post.PostText}");
                Console.WriteLine($"Дата публикации: {post.PublicationDate.ToShortDateString()} \n");
            }
        }
    }
    public static void Zad3()
    {
        using (var context = new BlogContext())
        {
            foreach (Blog blog in context.Blogs.Where(b => b.Posts.Any()))
            {
                Console.WriteLine($"Название блога: {blog.BlogName}");
            }
        }
    }
    public static void Zad4()
    {
        using (var context = new BlogContext())
        {
           
            foreach (Blog blog in context.Blogs.Where(blog => blog.Posts.Any(post => post.PublicationDate.Year != 2023)))
            {
                Console.WriteLine($"Название блога: {blog.BlogName}");
            }
        }
    }
    public static void Zad5()
    {
        using (var context = new BlogContext())
        {
            foreach (Blog blog in context.Blogs.Include(p => p.Posts))
            {
                Console.WriteLine($"Название блога: {blog.BlogName} - Количество постов в нем: {blog.Posts.Count}");
            }
        }
    }
    public static void Zad6()
    {
        using (var context = new BlogContext())
        {
            foreach (Post post in context.Posts.Where(p => p.PublicationDate.Year == 2024))
            {
                Console.WriteLine($"Название поста: {post.PostName} - Год публикации: {post.PublicationDate.Year}");
            }
        }
    }
    public static void Zad7()
    {
        using (var context = new BlogContext())
        {
            var query = context.Blogs
                .Include(blog => blog.Posts)
                .Where(blog => blog.Posts.Any())
                .ToList();

            foreach (var blog in query)
            {
                Console.WriteLine($"Название блога: {blog.BlogName}");

                foreach (var post in blog.Posts)
                {
                    Console.WriteLine(@$"Название поста: {post.PostName}
Текст поста: {post.PostText}
Дата публикации: {post.PublicationDate}");
                }

                Console.WriteLine(new string('-', 100));
            }
        }
    }
}