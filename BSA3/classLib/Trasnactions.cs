using System;
namespace classLib
{
    public class Transaction
    {
		public DateTime tranactionDate { get; }
        public uint curId { get; }

        public float moneyRemobeBalance { get; }

		public Transaction(uint id, float money, DateTime date)
        {
            curId = id;
            moneyRemobeBalance = money;
            tranactionDate = date;
        }
    }
}
