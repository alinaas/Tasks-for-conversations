using System;
using System.Configuration;
using System.IO;

namespace FileAssignment.Repository
{
	public class DataRepository
	{
		private const string NoDateMessage = "There is no date yet";
		private string _fileName => $"{Path.GetTempPath()}\\{ConfigurationManager.AppSettings["FileName"]}";
		private static object _lockObject = new object();

		public bool Save(string date)
		{
			try
			{
				lock (_lockObject)
				{
					File.WriteAllText(_fileName, date);
				}

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public string Read()
		{
			try
			{
				string data;

				lock (_lockObject)
				{
					if (!File.Exists(_fileName))
						return NoDateMessage;

					data = File.ReadAllText(_fileName);
				}

				if (string.IsNullOrWhiteSpace(data))
					return NoDateMessage;

				return data;
			}
			catch (Exception)
			{
				return NoDateMessage;
			}
		}
	}
}