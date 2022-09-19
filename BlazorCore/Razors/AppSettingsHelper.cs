﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using Microsoft.AspNetCore.Components;
using System.Threading;
using DataCore.Models;

namespace BlazorCore.Razors;

public class AppSettingsHelper : LayoutComponentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static AppSettingsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static AppSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public DataSourceDicsHelper DataSourceDics { get; } = DataSourceDicsHelper.Instance;
    public MemoryModel Memory { get; private set; } = new();
    public static int Delay => 5_000;
    public string MemoryInfo => Memory.MemorySize.PhysicalTotal != null
        ? $"{LocaleCore.Memory.Memory}: {Memory.MemorySize.PhysicalAllocated.MegaBytes:N0} MB " +
          $"{LocaleCore.Strings.From} {Memory.MemorySize.PhysicalTotal.MegaBytes:N0} MB"
        : $"{LocaleCore.Memory.Memory}: - MB";
    public uint MemoryFillSize => Memory.MemorySize.PhysicalTotal == null || Memory.MemorySize.PhysicalTotal.MegaBytes == 0
        ? 0 : (uint)(Memory.MemorySize.PhysicalAllocated.MegaBytes * 100 / Memory.MemorySize.PhysicalTotal.MegaBytes);
    public bool IsSqlServerRelease => DataAccess.JsonSettingsLocal.Sql is { DataSource: { } } &&
        DataAccess.JsonSettingsLocal.Sql.DataSource.Contains(LocaleCore.DeviceControl.SqlServerRelease, StringComparison.InvariantCultureIgnoreCase);
    public bool IsSqlServerDebug => DataAccess.JsonSettingsLocal.Sql is { DataSource: { } } &&
        DataAccess.JsonSettingsLocal.Sql.DataSource.Contains(LocaleCore.DeviceControl.SqlServerDebug, StringComparison.InvariantCultureIgnoreCase);

    #endregion

    #region Public and private methods

    public void SetupMemory()
    {
        Memory.Close();
        Memory = new();
        //Memory.OpenAsync(callRefreshAsync);
        Memory.MemorySize.Open();
    }

    #endregion
}
