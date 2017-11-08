using OnionApp.Domain.Core;
using OnionApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionApp.Infrastructure.Business
{
    public class CreditOrder : IOrder
    {
        public void MakeOrder(Book book)
        {
            // код покупки книги с помощью кредитной карты
        }
    }
}
