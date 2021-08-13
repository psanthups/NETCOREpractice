using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helpers
{
    public class CustomEmailTagHelper : TagHelper
    {
        public string MyEmail { get; set; }                                                                 //here this property work as a attribute
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";                                                                           //first we need to define the name of tag
            output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");                                    //second how share attribute  we can do this in otheway also (in next line)
            output.Attributes.Add("id", "my-email-id");
            output.Content.SetContent("my-email");                                                           //third how to share content(visible "my-email") in page for hold above attribute 
        }
    }
}
