using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource(); 
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string Title, string AuthorName)
        {
            return DataSource().Where(x => x.Title.Contains(Title) || x.Author.Contains(AuthorName)).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id =1, Title = "c#", Author = "santhosh"},
                new BookModel(){Id =2, Title = "Java", Author = "Tim"},
                new BookModel(){Id =3, Title = "dot Net", Author = "santhosh"},
                new BookModel(){Id =4, Title = "Oracle", Author = "John"},
                new BookModel(){Id =5, Title = "Python", Author = "swat"}
            };

        }
    }
}
