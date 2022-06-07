using CRUD_Solid.DataTransferObjects;
using CRUD_Solid.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Solid.Services
{
	public interface IUserService
	{
		UserModel GetUser(int id);
		List<UserModel> GetUsers();
		void CreateUser(UserDtoModel model);
		void UpdataUser(UserDtoModel model);
		void DeleteUser(int id);
	}
}
