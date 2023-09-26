using API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using API.Models.DTOs;
using Newtonsoft.Json.Linq;

namespace API.Services
{
	public class CarService : ICarService
	{ 
		private readonly IConfiguration _configuration;
		List<CarModel> cars_list = new List<CarModel>();

		public CarService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// To Do: Get car list form database
		public List<CarModel> GetCarListFromDB()
		{
			using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				SqlCommand command = connection.CreateCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "[sp_GetListOfCars]";
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
				DataTable dataTableCars = new DataTable();

				connection.Open();
				sqlDataAdapter.Fill(dataTableCars);
				connection.Close();

				// Mapping databes entity to our C# model
				foreach (DataRow dataRow in dataTableCars.Rows)
				{
					cars_list.Add(new CarModel
					{
						Car_id = Convert.ToInt32(dataRow["car_id"]),
						Model_of_the_car = dataRow["model_of_the_car"].ToString(),
						Purchase_price = Convert.ToInt32(dataRow["purchase_price"])
					});
				}
			}
			return cars_list;
		}

		// To Do: Insert car info in DB
		public void CreateCarObject(CarModelDto model)
		{
			// Problem: How to auto generate id in database in ADO.NET
			// With dataTableCarsCounter we nail down the value
			// Auto increment in SQL - uvesti auto increment nad poljem ID
			int dataTableCarsCounter = 12;

			using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				SqlCommand command = connection.CreateCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "[sp_InsertCarInfo]";
				command.Parameters.Add("@car_id", SqlDbType.BigInt).Value = dataTableCarsCounter;
				command.Parameters.AddWithValue("@model_of_the_car", model.Model_of_the_car);
				command.Parameters.AddWithValue("@purchase_price", model.Purchase_price);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
				DataTable dataTableCars = new DataTable();

				connection.Open();
				sqlDataAdapter.Fill(dataTableCars);
				connection.Close();
			}
		}

		// To Do: Get car by id from database
		public CarModel GetCar(int id)
		{
			using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				SqlCommand command = connection.CreateCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "[sp_GetCarById]";
				command.Parameters.Add("@car_id", SqlDbType.BigInt).Value = id;
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
				DataTable dataTableCars = new DataTable();

				connection.Open();
				sqlDataAdapter.Fill(dataTableCars);
				connection.Close();

				// Mapping databes entity to our C# model
				var dataRow = dataTableCars.Rows[0];

				// Way one
				var cm = new CarModel {
					Car_id = Convert.ToInt32(dataRow["car_id"]),
					Model_of_the_car = dataRow["model_of_the_car"].ToString(),
					Purchase_price = Convert.ToInt32(dataRow["purchase_price"])
				};

				// Way two
				CarModel cm1 = new CarModel();
				cm1.Car_id = Convert.ToInt32(dataRow["car_id"]);
				cm1.Model_of_the_car = dataRow["model_of_the_car"].ToString();
				cm1.Purchase_price = Convert.ToInt32(dataRow["purchase_price"]);

				return cm;

				// Custom parser
				// Data row cast to class ado.net

				//	DataRow dataRow = dataTableCars.

				//dataRow.ItemArray
			}
			throw new NotImplementedException();
		}

		// To Do: Delete car by id from database

	}
}
