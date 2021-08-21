using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using BookStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using BookStore.Repository;

namespace BookStore.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly NewBookAlertConfig _newBookAlertconfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookconfiguration;
        private readonly IMessageRepository _messageRepository;

        /* [ViewData]
           public string Title { get; set; }
           [ViewData]
           public BookModel book { get; set; }*/

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertconfiguration, IMessageRepository messageRepository)                       /*here we are injecting the confi service(Binding conf using Ioption) include model class into constr along with the singleton service(IMessageRepository) and binding the values in below lines*/            
        {
            _newBookAlertconfiguration = newBookAlertconfiguration.Get("InternalBook");
            _thirdPartyBookconfiguration = newBookAlertconfiguration.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
        }

        //[Route("")]
        public ViewResult Index()
        {
            /*var value = _messageRepository.GetName(); */                                                                                                   /*here we getting value from messagerepository these are for just check wether value coming or not*/
            bool isDisplay = _newBookAlertconfiguration.DisplayNewBookAlert;
            bool isDisplay1 = _thirdPartyBookconfiguration.DisplayNewBookAlert;

            //var newBookAlert = new NewBookAlertConfig();                                                                                               /*Binding configuration to objects using bind method appsetting.json.to map the properties of an obj to obj of model*/
            //configuration.Bind("NewBookAlert", newBookAlert);

            //var newBook = configuration.GetSection("NewBookAlert");
            //var result1 = newBook.GetValue<bool>("DisplayNewBookAlert");                                                                                  /*here we using configuration with section by doing this no need to write field name and obj name(obj in Json file)means no repitation. see diff b/w below and this line*/
            //var result2 = configuration.GetValue<string>("NewBookAlert:BookName");
            //var result = configuration["AppName"];       
            //var key1 = configuration["infoObj:key1"];
            //var key2 = configuration["infoObj:key2"];
            //var key3 = configuration["infoObj:key3:key3Obj1"];
            return View();
        }

        [Route("About-us")]
        public ViewResult Aboutus()
        {
            return View();
        }
        public ViewResult Contactus()
        {
            return View();
        }
    }
}
