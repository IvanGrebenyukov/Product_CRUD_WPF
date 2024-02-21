using practic8_1_grebenukov.Data;
using practic8_1_grebenukov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practic8_1_grebenukov.Classes
{
    public class Seeder
    {

        public static void InitialSeed ()
        {
            using (var context = new BlogContext())
            {

                if (!context.Blogs.Any())
                {
                    Blog blog1 = new Blog
                    {
                        BlogName = "Кулинария от Василия",
                        AuthorEmail = "Vasya@mail.com",
                        DateCreation = DateOnly.FromDateTime(DateTime.Now.AddDays(-42))
                    };
                    context.Blogs.Add(blog1);
                    Blog blog2 = new Blog
                    {
                        BlogName = "С# для начинающих",
                        AuthorEmail = "programmingcsharp@mail.com",
                        DateCreation = new DateOnly(2020, 1, 25)
                    };
                    context.Blogs.Add(blog2);
                    Blog blog3 = new Blog
                    {
                        BlogName = "О природе",
                        AuthorEmail = "nature@mail.com",
                        DateCreation = DateOnly.FromDateTime(DateTime.Now.AddDays(-365))
                    };
                    context.Blogs.Add(blog3);

                    blog1.Posts = new List<Post>()
                    {
                        new Post
                        {
                            PostName = "О себе",
                            PostText = "Всем привет, я Вася-повар! В этом блоге я расскажу вам о многих интересных рецептах",
                            PublicationDate = DateTime.Now.AddDays(-41),
                            IsPublished = true
                        },
                        new Post
                        {
                            PostName = "Рецепт борща",
                            PostText = "Всем привет, я Вася-повар! В этом посте я расскажу вам как приготовить вкусный борщ!",
                            PublicationDate = DateTime.Now.AddDays(-21),
                            IsPublished = true
                        },
                        new Post
                        {
                            PostName = "Как варить пельмени",
                            PostText = "Всем привет, я Вася-повар! В этом посте я расскажу вам как приготовить вкусный борщ!",
                            PublicationDate = DateTime.Now.AddDays(-11)
                        }
                    };
                    


                    blog2.Posts = new List<Post>()
                    {
                        new Post
                        {
                            PostName = "Как начать работу с ЯП C#",
                            PostText = "Всем привет, я программист! В этом посте я расскажу вам как начать работу с ЯП C#!",
                            PublicationDate = new DateTime(2020, 1, 26)
                        },
                        new Post
                        {
                            PostName = "Что такое переменные в C#",
                            PostText = "Всем привет, я программист! В этом посте я расскажу вам, что такое переменные!",
                            PublicationDate = new DateTime(2020, 2, 2),
                            IsPublished = true
                        },
                        new Post
                        {
                            PostName = "Что такое условные операторы в C#",
                            PostText = "Всем привет, я программист! В этом посте я расскажу вам, что такое условные операторы!",
                            PublicationDate = new DateTime(2020, 2, 15),
                            IsPublished = true
                        },
                        new Post
                        {
                           PostName = "Что такое циклы в C#",
                            PostText = "Всем привет, я программист! В этом посте я расскажу вам, что такое циклы!",
                            PublicationDate = new DateTime(2020, 3, 1),
                            IsPublished = true
                        }
                    };
                    context.SaveChanges();
                }
            }
        }
    }
}
