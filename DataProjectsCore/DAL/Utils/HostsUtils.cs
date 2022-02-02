﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DataProjectsCore.DAL.Utils
{
    public static class HostsUtils
    {
        #region Public and private fields and properties

        private static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";

        #endregion

        #region Public and private methods

        public static HostDirect LoadReader(SqlDataReader reader)
        {
            HostDirect result = new();
            if (reader.Read())
            {
                //result.IdRRef = idrref;
                result.Id = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ID");
                result.Name = SqlConnectFactory.GetValueAsNullable<string>(reader, "NAME");
                result.Ip = SqlConnectFactory.GetValueAsNullable<string>(reader, "IP");
                result.Mac = SqlConnectFactory.GetValueAsNullable<string>(reader, "MAC");
                result.Marked = SqlConnectFactory.GetValueAsNotNullable<bool>(reader, "MARKED");
                string? settingFile = SqlConnectFactory.GetValueAsNullable<string>(reader, "SETTINGSFILE");
                if (settingFile is string sf)
                    result.SettingsFile = XDocument.Parse(sf);
                result.ScaleId = SqlConnectFactory.GetValueAsNotNullable<int>(reader, "SCALE_ID");
            }
            return result;
        }

        public static HostDirect Load(Guid idrref)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = idrref },
            };
            HostDirect result = SqlConnectFactory.ExecuteReaderForEntity(SqlQueries.DbScales.Tables.Hosts.GetHostByUid, parameters, LoadReader);
            if (result == null)
                result = new HostDirect();
            result.IdRRef = idrref;
            return result;
        }

        public static HostDirect TokenRead()
        {
            if (!File.Exists(FilePathToken))
            {
                return new HostDirect();
            }
            XDocument doc = XDocument.Load(FilePathToken);
            Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
            //string EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            //string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
            return Load(idrref);
        }

        public static Guid TokenWrite(string connectionString)
        {
            Guid tokenSalt = Guid.NewGuid();
            XDocument doc = new();
            XElement root = new("root");
            root.Add(
                new XElement("ID", tokenSalt),
                new XElement("EncryptConnectionString", new XCData(EncryptDecryptUtils.Encrypt(connectionString)))
                );
            doc.Add(root);

            string name = Environment.MachineName;
            string uuid = tokenSalt.ToString();
            string mac = NetUtils.GetMacAddress();
            string ip = NetUtils.GetLocalIpAddress();

            string sqlExpression = $"INSERT INTO [db_scales].[HOSTS](IdRRef, NAME, MAC, IP,SettingsFile) VALUES ( '{uuid}','{name}', '{mac}', '{ip}','{doc.ToString()}')";

            using (SqlConnection con = new(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new(sqlExpression, con))
                {
                    int n = cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            // записать токен на диск если в БД отметилось без ошибки
            doc.Save(FilePathToken);
            return tokenSalt;
        }

        public static bool TokenExist()
        {
            if (!File.Exists(FilePathToken))
                return false;

            XDocument doc = XDocument.Load(FilePathToken);
            Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
            //string EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            //string connectionString = EncryptDecryptUtils.Decrypt(EncryptConnectionString);
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = idrref },
            };

            bool result = default;
            SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostIdByIdRRef, parameters, delegate (SqlDataReader reader) {
                result = reader.Read();
            });
            return result;
        }

        #endregion
    }
}
