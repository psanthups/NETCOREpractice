using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public int AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                TotalPages = model.TotalPages,
                UpdatedOn = DateTime.UtcNow
            };
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return newBook.Id;
        }

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
                new BookModel(){Id =1, Title = "c#", Author = "santhosh", Description = "this is discription for c# language book", Categiry = " Programming", Language = "English", TotalPages=120},
                new BookModel(){Id =2, Title = "Java", Author = "Tim", Description = "this is discription for Java language book", Categiry = " Programming", Language = "English", TotalPages=200}, 
                new BookModel(){Id =3, Title = "dot Net", Author = "santhosh", Description = "this is discription for dot Net language book", Categiry = " framework", Language = "English", TotalPages=100},
                new BookModel(){Id =4, Title = "Oracle", Author = "John", Description = "this is discription for Oracle language book", Categiry = " Programming", Language = "English", TotalPages=150},
                new BookModel(){Id =5, Title = "Python", Author = "swat", Description = "this is discription for Python language book", Categiry = " Programming", Language = "English", TotalPages=80 },
                new BookModel(){Id =6, Title = "Dev oops", Author = "Tiana", Description = "this is discription for Dev oops language book", Categiry = " Programming", Language = "English", TotalPages=160}
            };

        }
    }
}
