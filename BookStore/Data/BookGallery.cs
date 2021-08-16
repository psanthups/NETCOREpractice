using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class BookGallery                                                                            /* this claas is created to create bookgallery table in database*/
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public Books Book { get; set; }                                                              /*here we making farienkey for perticular bookid(above property) by writing this.*/
    }
}
