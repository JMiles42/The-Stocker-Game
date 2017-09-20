using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JMiles42.IO.Generic
{
	[Serializable, Obsolete("Cos I hate it and it does not work the way it should")]
	public class SaveableClass<T>
	{
		public const string FILESPLIT = @"/";
		public void LoadData(out T data) { LoadData(GetFileName(), out data); }

		public void LoadData(string name, out T data)
		{
			//Does space magic if file exists
			if (File.Exists(FilePath(name))) //Does the file exist
			{
				//Space Magic
				var bf = new BinaryFormatter();
				string filepath = FilePath(name);
				var file = File.Open(filepath, //Name file
									 FileMode.Open);
				data = (T) bf.Deserialize(file);
				file.Close(); //End file
			}
			else
				data = InitData();
		}

		public bool FileOnDisk(string name)
		{
			//Does space magic if file exists
			return (File.Exists(FilePath(name)));
		}

		public void SaveData(T data) { SaveData(GetFileName(), data); }

		public void SaveData(string name, T data)
		{
			//Space Magic
			var bf = new BinaryFormatter();
			string filepath = FilePath(name);
			var file = File.Open(filepath, //Name file
								 FileMode.OpenOrCreate //How to open file
								);
			bf.Serialize(file, data); //Magic happens
			file.Close(); //End file
		}

		protected virtual string FilePath(string name) { return Application.persistentDataPath + FILESPLIT + name + GetFileType(); }
		protected virtual T InitData() { return default (T); }
		protected virtual string GetFileType() { return ".data"; }
		protected virtual string GetFileName() { return "saveData"; }
	}
}