﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Choose Date")]
        //[EmailAddress]                                                //this for validate if this field for email address (model validaion)if the field is generated by datatype attribute model class.
        public string MyField { get; set; }
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3)]                          //this is for string length of fiels if we not written min char then it will show error text in the field while submit
        [Required(ErrorMessage ="Please enter the Title of your book")] //this is used to not to pass empty data into database so we use this required to show as necesary field for title same below required fields
                                                                        //here we use this error masage to update the validation error which we wrote in addnewbook html view (span tag) now it shows what we wrote here
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the Author name")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        public string Categiry { get; set; }
        [Required(ErrorMessage = "Please choose language of Book")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total Pages of Book")]                          //this is to display override name of field it will display "Total Pages of Book" instead of TotalPages(name of fiels).
        public int? TotalPages { get; set; }                             //here we are using nullible(int?) integer to convert the data into int in form (total pages) into integer because its(totalpages) in string type

    }
}
