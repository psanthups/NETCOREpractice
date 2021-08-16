using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Description { get; set; }

        public string Categiry { get; set; }

        public int LanguageId { get; set; }

        public int TotalPages { get; set; }

        public string CoverImageUrl { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public Language  Language { get; set; }                                                     /* here using this we creating relationship between books ang language table*/
        public ICollection<BookGallery> bookGallery { get; set; }                                   /* here using this we creating relationship between books ang bookgallery table*/
    }
}
