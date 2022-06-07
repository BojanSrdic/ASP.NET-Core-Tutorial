using CRUD_Solid.DataTransferObjects;
using CRUD_Solid.DbConnection;
using CRUD_Solid.Entitys;
using CRUD_Solid.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTests
{
	class UserServiceUnitTest : IDisposable
	{
		private UserService _userService;
		private readonly DatabaseContext context;

		private readonly UserModel UserOne = new UserModel { Id = 1, UserName = "Toni", Email = "Toni@gmial.com", Age = 27 };
		private readonly UserModel UserTwo = new UserModel { Id = 2, UserName = "Nikola", Email = "Nikola@gmial.com", Age = 28 };
		private readonly UserModel UserThree = new UserModel { Id = 3, UserName = "Dusan", Email = "Dusan@gmial.com", Age = 28 };
		private readonly UserModel UserFour = new UserModel { Id = 4, UserName = "Relja", Email = "Relja@gmial.com", Age = 28 };

		private readonly List<UserModel> ListofUsers = new List<UserModel>();

		public UserServiceUnitTest()
		{
			var options = new DbContextOptionsBuilder<DatabaseContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.Options;

			context = new DatabaseContext(options);

			context.Database.EnsureCreated();

			Seed(context);
		}

		[SetUp]
		public void Setup()
		{
			_userService = new UserService(context);
			ListofUsers.Add(UserOne);
			ListofUsers.Add(UserTwo);
			ListofUsers.Add(UserThree);
			ListofUsers.Add(UserFour);
		}

		[Test]
		public void CreateUser_CreateNewUserModel_UserCreatedSuccessufl()
		{
			//Arrange
			var userDtoModel = new UserDtoModel { UserName = "Pedja", Email = "Pedja@gmial.com", Password = "Pedja1!" };

			//Act
			_userService.CreateUser(userDtoModel);
			var find = _userService.GetUser(5);
			ListofUsers.Add(find);

			//Assert
			Assert.AreEqual("Pedja", find.UserName);
			Assert.AreEqual("Pedja@gmial.com", find.Email);
			Assert.AreNotEqual("Nikola@gmial.com", find.Email);
		}

		[Test]
		public void GetUser_FindSpecificUser_ExpectedUserFound()
		{
			//Act
			var find = _userService.GetUser(1);

			//Assert
			Assert.AreEqual(UserOne.UserName, find.UserName);
			Assert.AreEqual(UserOne.Email, find.Email);
			Assert.AreEqual(UserOne.Age, find.Age);
		}

		[Test]
		public void GetUsers_FindAllUsers_UsersFound()
		{
			//Act
			var listOfUsers = _userService.GetUsers();

			//Assert
			Assert.That(listOfUsers, Has.All.Matches<UserModel>(f => IsInExpected(f, ListofUsers)));
		}

		private void Seed(DatabaseContext context)
		{
			var users = new[]
			{
				UserOne,
				UserTwo,
				UserThree,
				UserFour
			};

			context.Users.AddRange(users);
			context.SaveChanges();
		}

		private static bool IsInExpected(UserModel item, IEnumerable<UserModel> expected)
		{
			var matchedItem = expected.FirstOrDefault(f =>
				f.Id == item.Id &&
				f.UserName == item.UserName &&
				f.Email == item.Email
			);

			return matchedItem != null;
		}

		public void Dispose()
		{
			context.Database.EnsureDeleted();
			context.Dispose();
		}
	}
}