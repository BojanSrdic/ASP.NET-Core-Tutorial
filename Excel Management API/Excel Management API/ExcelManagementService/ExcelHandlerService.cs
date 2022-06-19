using Excel_Management_API.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Excel_Management_API.ExcelManagementService
{
	public static class ExcelHandlerService
	{
		public static async Task CreateExcelFile(List<User> people, FileInfo file)
		{
			// Delete excel file if exists
			DeleteIfExists(file);

			// Create new excel file
			using var package = new ExcelPackage(file);

			// Create new worksheet
			var ws = package.Workbook.Worksheets.Add("MiniReport");
			var range = ws.Cells["A2"].LoadFromCollection(people, true);
			range.AutoFitColumns();

			// Format the file
			ws.Cells["A1"].Value = "Report title";
			ws.Cells["A1:C1"].Merge = true;
			ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			ws.Row(1).Style.Font.Size = 24;
			ws.Row(1).Style.Font.Color.SetColor(Color.Green);

			ws.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			ws.Row(2).Style.Font.Bold = true;
			ws.Column(3).Width = 20;

			// Save excel file 
			await package.SaveAsync();
		}

		public static async Task<List<User>> LoadExcelFile(FileInfo file)
		{
			List<User> output = new();

			using var package = new ExcelPackage(file);

			await package.LoadAsync(file);

			var ws = package.Workbook.Worksheets[0];
			int row = 3;
			int col = 1;

			while (string.IsNullOrWhiteSpace(ws.Cells[row, col].Value?.ToString()) == false)
			{
				User user = new();
				user.Id = int.Parse(ws.Cells[row, col].Value.ToString());
				user.Username = ws.Cells[row, col + 1].Value.ToString();
				user.Email = ws.Cells[row, col + 2].Value.ToString();
				output.Add(user);
				row += 1;
			}

			return output;
		}

		public static void ExcelLicence()
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		}

		private static void DeleteIfExists(FileInfo file)
		{
			if (file.Exists)
			{
				file.Delete();
			}
		}
	}
}
