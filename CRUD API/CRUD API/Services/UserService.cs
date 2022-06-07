using CRUD_Solid.DataTransferObjects;
using CRUD_Solid.DbConnection;
using CRUD_Solid.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Solid.Services
{
	public class UserService : IUserService
	{
		private readonly DatabaseContext _context;

		public UserService(DatabaseContext context)
		{
			_context = context;
		}

		public void CreateUser(UserDtoModel model)
		{
			// Todo: Mapp domain models and data transfer objects
			var user = new UserModel
			{
				UserName = model.UserName,
				Email = model.Email,
				Password = model.Password
			};

			// Todo: Add new models to tables in db and save changes
			_context.Users.Add(user);
			_context.SaveChanges();
		}

		public void DeleteUser(int id)
		{
			var item = _context.Users.Find(id);

			// Todo: throw exception if user doesn't exist before deleting
			if (item is null)
				throw new KeyNotFoundException("Item not found");
			
			_context.Users.Remove(item);
			_context.SaveChanges();
		}

		public UserModel GetUser(int id)
		{
			return _context.Users.Find(id);
		}

		public List<UserModel> GetUsers()
		{
			return _context.Users.ToList();
		}

		public void UpdataUser(UserDtoModel model)
		{
			throw new NotImplementedException();
		}
	}
}
