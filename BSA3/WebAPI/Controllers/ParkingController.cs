using System;
using System.Collections.Generic;
using System.Linq;
using classLib;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/Car")]
	public class ParkingController : Controller
	{

		//Список всех машин      
		[HttpGet("getInfo/all")]
		public string GetAllCars()
		{
			string text = Settings.myParking.showAllCars();
			return text;
		}
        
		//Конкретная машина
		[HttpGet("getInfo/{id:int}")]
		public string GetSpecificCar(uint id)
		{
			string text = Settings.myParking.showCarBalance(id);
			return text;
		}

		// POST добавить машину
		[HttpPost("{id:int}/{type}/{balance:float}")]
		public string PostCar(uint id, carType type, float balance)
		{
			return Settings.myParking.addCar(id, type, balance);
		}

		//Удалить машину
		[HttpDelete("{id}")]
		public string Delete(uint id)
		{
			return Settings.myParking.removeCar(id);
		}

		//Показать сколько машин на парковке
		[HttpGet("count")]
		public string Count()
		{
			return String.Format("There are {0} cars on the parking", Settings.myParking.cars.Count());
		}

		//Показать сколько свободных мест
		[HttpGet("freeSpaces")]
		public string freeSpace()
		{
			return String.Format("There are {0} free spaces on the parking", Settings.parkingPlaces - Settings.myParking.cars.Count());
		}

		//показать доход
		[HttpGet("income")]
		public string income()
		{
			return String.Format("This parking earned {0} while program was active", Settings.myParking.parkingIncome);
		}

		//транзакции по машине за последнюю минуту
		[HttpGet("transactions/{id:int}")]
		public string GetCarTransactiosPerLastMinute(uint id)
		{
			string text = Settings.myParking.getCarTransactiosPerLastMinute(id);
			return text;
		}

		//транзакции за последнюю минуту
		[HttpGet("transactions/all")]
		public string GetTransactiosPerLastMinute()
		{
			string text = Settings.myParking.getTransactiosPerLastMinute();
			return text;
		}

		//показать файл Transaction.log
		[HttpGet("transactions/showLog")]
		public string TransactiosTotal()
		{
			return Settings.showTransactions();
		}



		//пополнить баланс машины
		[HttpPut("{id:int}/{putMoney:float}")]
		public string Put(uint id, float putMoney)
		{
			return Settings.myParking.addBalance(id, putMoney);
		}

		[HttpGet]
		public JObject info()
		{

			string json = @"{
'Список всіх машин': 'api/Car/getInfo/all',
'Деталі по одній машині': 'api/Car/getInfo/{id:int}',
'Додати машину': '{id:int}/{type}/{balance:float}',
'Видалити машину': 'api/Car/{id}',
'Кількість вільних місць': 'api/Car/freeSpaces',
'Кількість зайнятих місць': 'api/Car/count',
'Загальний дохід': 'api/Car/income',
'Вивести Transactions.log': ' api/Car/transactions/showLog',
'Вивести транзакції за останню хвилину': 'api/Car/transactions/all',
'Вивести транзакції за останню хвилину по одній конкретній машині': 'api/Car/transactions/{id:int}',
'Поповнити баланс машини': 'api/Car/{id:int}/{putMoney:float}',
}";
			JObject o = JObject.Parse(json);

			return o;


		}


	}
}
