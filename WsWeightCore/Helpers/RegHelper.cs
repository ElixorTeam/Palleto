﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using Microsoft.Win32;

namespace WeightCore.Helpers;

public class RegHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
	private static RegHelper _instance;
#pragma warning restore CS8618
	public static RegHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields and properties

	public WmiHelper Wmi { get; } = WmiHelper.Instance;

	#endregion

	#region Public and private methods

	public WmiSoftwareModel SearchingSoftwareFromRegistry(string search, StringTemplateEnum template)
	{
		try
		{
			string reg64 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
			string reg32 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
			RegistryKey keyPrograms = Registry.LocalMachine.OpenSubKey(Environment.Is64BitOperatingSystem ? reg64 : reg32);
			if (keyPrograms is not null)
			{
				foreach (string guid in keyPrograms.GetSubKeyNames())
				{
					RegistryKey key = keyPrograms.OpenSubKey(guid);
					if (key?.GetValue("DisplayName") is not null)
					{
						bool isFind = false;
						string name = key.GetValue("DisplayName") as string;
						if (!string.IsNullOrEmpty(name))
						{
							switch (template)
							{
								case StringTemplateEnum.Equals:
									if (name.Equals(search, StringComparison.InvariantCultureIgnoreCase))
										isFind = true;
									break;
								case StringTemplateEnum.Contains:
									if (name.ToUpper().Contains(search.ToUpper()))
										isFind = true;
									break;
								case StringTemplateEnum.StartsWith:
									if (name.ToUpper().StartsWith(search.ToUpper()))
										isFind = true;
									break;
								case StringTemplateEnum.EndsWith:
									if (name.ToUpper().EndsWith(search.ToUpper()))
										isFind = true;
									break;
							}
						}
						if (isFind)
						{
							string vendor = key.GetValue("Publisher") as string;
							string version = key.GetValue("DisplayVersion") as string;
							string language = key.GetValue("Language") as string;
							return new WmiSoftwareModel(name, vendor, version, guid, language);
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			return new WmiSoftwareModel(ex.Message, string.Empty, string.Empty, string.Empty, string.Empty);
		}
		return new WmiSoftwareModel();
	}

	public WmiSoftwareModel SearchingSoftware(WinProviderEnum winProvider, string search, StringTemplateEnum template)
	{
		switch (winProvider)
		{
			case WinProviderEnum.Registry:
				return SearchingSoftwareFromRegistry(search, template);
			case WinProviderEnum.Alias:
				break;
			case WinProviderEnum.Environment:
				break;
			case WinProviderEnum.FileSystem:
				break;
			case WinProviderEnum.Function:
				break;
			case WinProviderEnum.Variable:
				break;
			case WinProviderEnum.Wmi:
				return Wmi.GetSoftware(search);
		}

		return new WmiSoftwareModel();
	}

	#endregion
}