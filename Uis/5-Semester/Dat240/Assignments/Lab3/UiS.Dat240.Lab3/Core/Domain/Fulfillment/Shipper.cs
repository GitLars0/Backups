using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment;

public class Shipper{
    public Guid Id {get; private set;}
    public string Name {get; private set;}

    public Shipper(string name){
        Name = name;
    }
}