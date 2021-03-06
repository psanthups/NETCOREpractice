using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration _configuration;

        public BookRepository(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,                                        //here from ".hasvalue" we wrote this to convert the nullible value into int by asking hasvalue? if no the totalpages value is 0. other wise we get error as nullible value con't convert
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Categiry = book.Categiry,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,                                                                   //to get the data of this language we use this here. Here we using navigation property cause we created relationship (or we can use "join" if not created relationship). same(codeline) do in getbookbyid method below
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl

                }).ToListAsync();
        }

        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Categiry = book.Categiry,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,                                                                    //to get the data of this language we use this here. Here we using navigation property cause we created relationship (or we can use "join" if not created relationship). same(codeline) do in getbookbyid method below
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl

                }).Take(count).ToListAsync();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Categiry = book.Categiry,
                     Description = book.Description,
                     Id = book.Id,
                     LanguageId = book.LanguageId,
                     Language = book.Language.Name,                                                                     //to get the data of this language we use this here. Here we using navigation property cause we created relationship (or we can use "join" if not created relationship). same(codeline) done in above getallbooks method
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverImageUrl = book.CoverImageUrl,
                     Gallery = book.bookGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList(),
                     BookPdfUrl = book.BookPdfUrl
                 }).FirstOrDefaultAsync();

            //_context.Books.Where(x => x.Id == id).FirstOrDefault();


        }
        public List<BookModel> SearchBook(string Title, string AuthorName)
        {
            return null; /*DataSource().Where(x => x.Title.Contains(Title) || x.Author.Contains(AuthorName)).ToList();*/         //here we notwrote any search functionality now and this commented code usedto search functionality but now we are not usind our hardcoded data we are gettining data from db. so we commented this and below datasource list(hard coded data)
        }
        /*private List<BookModel> DataSource()
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

          }*/

        public string GetAppName()
        {
            /*return "Book Store Application";*/                                                                                       /*//using this method and this line we getting app name in header by using DI in this file and (DI in _header partial file by using @inject file path with object)*/
            return _configuration["AppName"];                                                                                          /*here we returning app name in header using configuration from appsettingsdev json file by using key name in []. and by use namespace in header file we get this in view.*/

        }
    }
}
