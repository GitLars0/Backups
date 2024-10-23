using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;

public class Payment{
    public Guid Id {get; protected set;}
    public decimal Amount {get; private set;}

    public Payment(decimal amount){
        Amount = amount;
    }
}