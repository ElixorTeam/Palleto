﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Xml.Serialization;
using WebApiTerra1000.Utils;

namespace WebApiTerra1000.Common
{
    [XmlRoot(TerraConsts.Info, Namespace = "", IsNullable = false)]
    public class ServiceInfoEntity : BaseSerializeDeprecatedEntity<ServiceInfoEntity>
    {
        #region Public and private fields and properties

        public string App { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string WinCurrentDate { get; set; } = string.Empty;
        public string SqlCurrentDate { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public int ConnectTimeout { get; set; } = 0;
        public string DataSource { get; set; } = string.Empty;
        public string ServerVersion { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public ulong PhysicalMegaBytes { get; set; } = 0;
        public ulong VirtualMegaBytes { get; set; } = 0;

        #endregion

        #region Constructor and destructor

        public ServiceInfoEntity(string description, string version, string winCurrentDate, string sqlCurrentDate, string connectionString,
            int connectTimeout, string dataSource, string serverVersion, string database, ulong physicalMegaBytes, ulong virtualMegaBytes)
        {
            App = description;
            Version = version;
            WinCurrentDate = winCurrentDate;
            SqlCurrentDate = sqlCurrentDate;
            ConnectionString = connectionString;
            ConnectTimeout = connectTimeout;
            DataSource = dataSource;
            ServerVersion = serverVersion;
            Database = database;
            PhysicalMegaBytes = physicalMegaBytes;
            VirtualMegaBytes = virtualMegaBytes;
        }

        public ServiceInfoEntity()
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return 
                @$"{nameof(App)}: {App}. " + Environment.NewLine + 
                @$"{nameof(Version)}: {Version}. " + Environment.NewLine + 
                @$"{nameof(WinCurrentDate)}: {WinCurrentDate}. " + Environment.NewLine + 
                @$"{nameof(SqlCurrentDate)}: {SqlCurrentDate}. " + Environment.NewLine + 
                @$"{nameof(ConnectionString)}: {ConnectionString}. " + Environment.NewLine + 
                @$"{nameof(ConnectTimeout)}: {ConnectTimeout}. " + Environment.NewLine + 
                @$"{nameof(DataSource)}: {DataSource}. " + Environment.NewLine + 
                @$"{nameof(ServerVersion)}: {ServerVersion}. " + Environment.NewLine + 
                @$"{nameof(Database)}: {Database}. " + Environment.NewLine + 
                @$"{nameof(PhysicalMegaBytes)}: {PhysicalMegaBytes}. " + Environment.NewLine + 
                @$"{nameof(VirtualMegaBytes)}: {VirtualMegaBytes}. ";
        }

        #endregion
    }
}
