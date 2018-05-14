using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace classLib
{
	public class Settings
    {
        static public uint parkingPlaces { get; private set; }
        static private float finePerPeriod;
        static private uint period;
        static private Timer writeToFile;
        static private Timer writeOffMoney;
        static public Parking myParking;
        static Dictionary<carType, float> pricePerPeriod;

        static Settings()
        {
            parkingPlaces = 20;
            finePerPeriod = 3;
            period = 20;

            myParking = Parking.Source;

            TimerCallback toFile = new TimerCallback(writeTo);
            writeToFile = new Timer(toFile, null, 0, 60000);

            TimerCallback offBalance = new TimerCallback(writeOff);
            writeOffMoney = new Timer(offBalance, null, 0, 1000 * period);

            pricePerPeriod = new Dictionary<carType, float>
            { {carType.truck,80} ,
              { carType.bus ,40},
              { carType.passanger,20},
              { carType.motorcycle,10}
            };
        }

        private static void writeOff(object obj)
        {
            foreach (Car car in myParking.cars)
            {
                float money = pricePerPeriod[car.currentType];
                if ((car.currentBalance - money) <= 0)
                {
                    money = money * finePerPeriod;
                }
				car.removeFromBalance(money);
                myParking.addIncome(money);
                myParking.transactions.Add(new Transaction(car.currentID, money, DateTime.Now));
            }
        }

        private static void writeTo(object obj)
        {
            var transactionsSumPerLastMinute = (from t in myParking.transactions where (DateTime.Now - t.tranactionDate) < new TimeSpan(0, 1, 0) select t.moneyRemobeBalance).Sum();
            using (FileStream fstream = new FileStream(@"Transaction.log", FileMode.Append))
            {
                string text = "";
                text += String.Format("Date: {0} \n Income: {1} \n", DateTime.Now, transactionsSumPerLastMinute);
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);

            }

        }
		public static string showTransactions()
		{
			string text;
			using (FileStream sr = File.OpenRead("Transaction.log"))
			{
				byte[] array = new byte[sr.Length];
				sr.Read(array, 0, array.Length);
				text = System.Text.Encoding.Default.GetString(array);
			}
			return text;
		}
            

    }
}

