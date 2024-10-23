using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;

public class Customer{

    public Customer(string name){
        Name = name;
        Id = Guid.NewGuid();
    }
    public Guid Id {get; protected set;}
    public string? Name { get; protected set;}
}