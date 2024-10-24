﻿using System;
using System.Collections.Generic;
using UiS.Dat240.Lab3.Core.Domain.Products.Events;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Products;

public class FoodItem : BaseEntity
{

	public FoodItem(string name, string description, decimal price)
	{
		_name = name;
		Description = description;
		_price = price;
	}
	public int Id { get; protected set; }

	private string _name;
	public string Name
	{
		get => _name;
		set
		{
			if (_name is not null && _name != "" && _name != value)
			{
				Events.Add(new FoodItemNameChanged(Id, oldName: _name, newName: value));
				_name = value;
			}
		}
	}
	public string Description { get; set; }
	private decimal _price;
	public decimal Price { 
		get =>  _price;
		set {
			if (_price != value){
				Events.Add(new FoodItemPriceChanged(Id, oldPrice: _price, newPrice: value));
				_price = value;
			}
		}
	
	}
	public int CookTime { get; set; }
}

public class FoodItemNameValidator : IValidator<FoodItem>
{
	public (bool, string) IsValid(FoodItem item)
	{
		_ = item ?? throw new ArgumentNullException(nameof(item), "Cannot validate a null object");
		if (string.IsNullOrWhiteSpace(item.Name)) return (false, $"{nameof(item.Description)} cannot be empty");
		return (true, "");
	}
}
public class FoodItemDescriptionValidator : IValidator<FoodItem>
{
	public (bool, string) IsValid(FoodItem item)
	{
		_ = item ?? throw new ArgumentNullException(nameof(item), "Cannot validate a null object");
		if (string.IsNullOrWhiteSpace(item.Description)) return (false, $"{nameof(item.Description)} cannot be empty");
		return (true, "");
	}
}
public class FoodItemPriceValidator : IValidator<FoodItem>
{
	public (bool, string) IsValid(FoodItem item)
	{
		_ = item ?? throw new ArgumentNullException(nameof(item), "Cannot validate a null object");
		if (item.Price <= 0) return (false, $"{nameof(item.Price)} must be greater than 0");
		return (true, "");
	}
}
