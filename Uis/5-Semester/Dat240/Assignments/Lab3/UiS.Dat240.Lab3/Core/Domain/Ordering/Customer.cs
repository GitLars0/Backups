using System;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering;

public class Customer{

    public Customer(string name){
        Name = name;
        Id = Guid.NewGuid();
    }
    public Guid Id {get; protected set;}
    public string? Name { get; protected set;}
}