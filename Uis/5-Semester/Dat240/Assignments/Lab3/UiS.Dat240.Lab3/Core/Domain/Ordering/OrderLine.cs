using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering;

    public class OrderLine{
        public Guid Id {get;  set;}
        public string? ItemName {get;  set;}
        public decimal Price {get;  set;}
        public int Amount {get;  set;}

        public OrderLine(string itemName, decimal price, int amount){
            Id = Guid.NewGuid();
            ItemName = itemName;
            Price = price;
            Amount = amount;
        }
    }