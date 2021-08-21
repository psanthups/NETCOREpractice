using BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IOptionsMonitor<NewBookAlertConfig> _newBookAlertconfiguration;                                                      /*here we using IoptionMonitor instead of only Newbookalertconfig at that place. cause we have this repo confi serv in singleton pattern so by using this we get updated the settings in json file without rebuild */       
        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertconfiguration)                                               /*this constr used here to read appset.josn file into this repository by injecting the cinfi service in this constructor.*/
        {
            _newBookAlertconfiguration = newBookAlertconfiguration;
        }
        public string GetName()
        {
            return _newBookAlertconfiguration.CurrentValue.BookName;
        }
    }
}
