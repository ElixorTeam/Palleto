﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Files;

public class FileLogHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static FileLogHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static FileLogHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);
	public string FileName { get; set; } = "";

	#endregion

	#region public methods

	public void Recreate()
	{
		if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
			return;
		File.Delete(FileName);
	}

	public void WriteMessage(string message)
	{
		StreamWriter streamWriter = !File.Exists(FileName) ? File.CreateText(FileName) : File.AppendText(FileName);
		streamWriter.WriteLine(message);
		streamWriter.Close();
		streamWriter.Dispose();
	}

	#endregion
}