using System;
namespace classLib
{
	public enum carType
    {
        passanger,
        truck,
        bus,
        motorcycle
    }
	public class Car
	{

		public uint currentID { get; private set; }

		public carType currentType { get; private set; }

		public float currentBalance { get; private set; }

		public Car(uint id, carType type, float balance)
		{
			currentID = id;
			currentType = type;
			currentBalance = balance;
		}

		public float addToBalance(float someMoney)
		{
			currentBalance += someMoney;
			return currentBalance;
		}

		public float removeFromBalance(float someMoney)
		{
			currentBalance -= someMoney;
			return currentBalance;
		}
	}
}

 
