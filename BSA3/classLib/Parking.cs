using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace classLib
{
    public class Parking
    {
		public List<Car> cars { get; private set; }
		public List<Transaction> transactions { get; private set; }
        private static readonly Lazy<Parking> myParking = new Lazy<Parking>(() => new Parking());

        public static Parking Source { get { return myParking.Value; } }

        private Parking()
        {
            cars = new List<Car>();
            transactions = new List<Transaction>();

        }
        
        public float parkingIncome { get; private set; }
        
        public string addCar(uint id, carType type, float balance)
        {
			if (id <=0 || balance <=0){
				return "Not correct data";
			} 

			if (type != carType.bus && type != carType.motorcycle && type != carType.passanger && type != carType.truck)
            {
				return "Not correct data";
            }

			if (Settings.myParking.cars.Count >= Settings.parkingPlaces)
            {
                 return "There are no free spaces.";
            }
            else
            {
                var carInParking = cars.FirstOrDefault<Car>(c => c.currentID == id);
                if (carInParking != null)
                {
                    return "Car has been already added.";
                }
                else
                {
                    cars.Add(new Car(id, type, balance));
					return String.Format("{0} with id {1} and balance {2} has been added", type,id,balance);
                }
            }

        }
        public string removeCar(uint id)
        {
            Car someCar = cars.FirstOrDefault<Car>(c => c.currentID == id);
			if (someCar == null)
            {
                return "There is no such car";
            }
            if (someCar.currentBalance > 0)
            {
                cars.Remove(someCar);
				return String.Format("Car of type {0} with id {1} was removed", someCar.currentType, someCar.currentID);
            }
            else
            {
				return String.Format("Can't remove car with balance less than 0. Please add money. Current balance is {0}",someCar.currentBalance);
            }
        }

        public string addBalance(uint id, float moneyOnBalance)
        {

			Car someCar = cars.FirstOrDefault<Car>(c => c.currentID == id);
			if (someCar != null)
            {
				someCar.addToBalance(moneyOnBalance);
				return String.Format("Car with id {0} has recieved {1} on balance. Now it balance is {2}.",id,moneyOnBalance,someCar.currentBalance);
            }
            else
            {
				return "There is no such car.";
            }
            
        }

        public string showCarBalance(uint id)
        {

			Car someCar = cars.FirstOrDefault<Car>(c => c.currentID == id);
			if (someCar != null)
            {
				return String.Format("Car-type {0} with this id {1} has this money on the balancee {3}.", someCar.currentType, id, someCar.currentBalance);
            }
            else
            {
                return "There is no such car.";
            }
        }
        public string getCarTransactiosPerLastMinute(uint id)
        {
			var transactionsSumPerLastMinute = (from t in transactions where (DateTime.Now - t.tranactionDate) < new TimeSpan(0, 1, 0) && (t.curId == id) select t.moneyRemobeBalance).Sum();
			if (transactionsSumPerLastMinute >= 0)
			{
				return String.Format("Car with id = {0} per last minute have transactions on this sum: {1}", id, transactionsSumPerLastMinute);
			}
			return "There is now such car";
        }

		public string getTransactiosPerLastMinute()
        {
            var transactionsSumPerLastMinute = (from t in transactions where (DateTime.Now - t.tranactionDate) < new TimeSpan(0, 1, 0) select t.moneyRemobeBalance).Sum();
			return String.Format("Transactions per last minute {0}", transactionsSumPerLastMinute);
        }

        public float addIncome(float income)
        {
            parkingIncome += income;
            return parkingIncome;
        }

		public string showAllCars(){
			string text = "";       
			foreach(Car c in cars){
				String.Format("Car with id: {0} Type: {1} Balance:{2}", c.currentID, c.currentType, c.currentBalance);
			}
			if (text == "") { return "There is no car"; }
			return text;
		}



    }
}
