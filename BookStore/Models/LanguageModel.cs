using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class LanguageModel             //this class is created to create the dropdown using selectlist text and value.  we use these below properties text as language name and id as value fron controller
    {                                      //but now we are using model class for language table database by changing the properties of class
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
