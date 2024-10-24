using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using Bogus.DataSets;
using UiS.Dat240.Lab3.SharedKernel;
using UiS.Dat240.Lab3.Core.Domain.Ordering;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;

public class Invoice : BaseEntity{
    
    public Guid Id {get; private set;}
    public Guid OrderId {get; private set;}
    public decimal Amount {get; private set;}
    public Customer Customer {get; private set;}
    public Address Address {get; private set;}
    public Status Status {get; private set;}
    private Invoice(){}

    public Invoice(Guid orderId, Customer customer, Address address, decimal amount){
        Id = Guid.NewGuid();
        OrderId = orderId;
        Customer = customer;
        Address = address;
        Amount = amount;
        Status = Status.New;
    }
}