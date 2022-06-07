﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Controllers;
using DataCore.Sql.Models;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using static DataCore.ShareEnums;

namespace DataCore.Sql
{
    public static class SqlUtils
    {
        #region Public and private fields and properties

        public static SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;
        public static DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        public static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
        public static readonly string FilePathLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.log";

        #endregion

        #region Public and private methods - Tasks

        public static void SaveNullTask(TaskTypeDirect taskType, long scaleId, bool enabled)
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.InsertTask);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@task_type_uid", taskType.Uid);
            cmd.Parameters.AddWithValue("@scale_id", scaleId);
            cmd.Parameters.AddWithValue("@enabled", enabled);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void SaveTask(TaskDirect task, bool enabled)
        {
            if (task == null)
                return;
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.UpdateTask);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@uid", task.Uid);
            cmd.Parameters.AddWithValue("@enabled", enabled);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static Guid GetTaskUid(string taskName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                StringUtils.SetStringValueTrim(ref taskName, 32);
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskName);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static TaskDirect? GetTask(Guid taskTypeUid, long scaleId)
        {
            TaskDirect? result = null;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByTypeAndScale))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = new TaskDirect
                            {
                                Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
                                TaskType = GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
                                //Scale = ScalesUtils.GetScale(dataAccess, SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Scale = DataAccess.Crud.GetEntity<ScaleEntity>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
                            };
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static TaskDirect? GetTask(Guid taskUid)
        {
            TaskDirect? result = null;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_uid", taskUid);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = new TaskDirect
                            {
                                Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
                                TaskType = GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
                                //Scale = ScalesUtils.GetScale(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Scale = DataAccess.Crud.GetEntity<ScaleEntity>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
                            };
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static Guid GetTaskTypeUid(string taskTypeName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                StringUtils.SetStringValueTrim(ref taskTypeName, 32);
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTaskTypeUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskTypeName);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static TaskTypeDirect GetTaskType(string name)
        {
            TaskTypeDirect result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypesByName))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_name", name);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result.Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
                            result.Name = SqlConnect.GetValueAsString(reader, "NAME");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static TaskTypeDirect GetTaskType(Guid uid)
        {
            TaskTypeDirect result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypesByUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_uid", uid);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result.Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
                            result.Name = SqlConnect.GetValueAsString(reader, "NAME");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static List<TaskTypeDirect> GetTasksTypes()
        {
            List<TaskTypeDirect> result = new();
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypes))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(new TaskTypeDirect(
                                SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID"), SqlConnect.GetValueAsString(reader, "NAME")));
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        #endregion

        #region Public and private methods - PLUs

        public static ushort GetPluCount(long scaleId)
        {
            ushort result = 0;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Plu.GetCount))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SCALE_ID", scaleId);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = SqlConnect.GetValueAsNotNullable<ushort>(reader, "COUNT");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }


        #endregion

        #region Public and private methods - Scales

        public static long GetScaleId(string scaleDescription)
        {
            long result = 0;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                StringUtils.SetStringValueTrim(ref scaleDescription, 150);
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleId))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SCALE_DESCRIPTION", scaleDescription);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static ScaleEntity GetScaleFromHost(long hostId)
        {
            ScaleEntity scale = DataAccess.Crud.GetEntity<ScaleEntity>(
                new FieldListEntity(new Dictionary<string, object?> {
                    { $"Host.IdentityId", hostId },
                    { DbField.IsMarked.ToString(), false } }),
                new FieldOrderEntity(DbField.CreateDt, DbOrderDirection.Desc));
            return scale;
        }

        public static ScaleEntity GetScale(long id)
        {
            return DataAccess.Crud.GetEntity<ScaleEntity>(
                new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, id }, { DbField.IsMarked, false } }));
        }

        public static ScaleEntity GetScale(string description)
        {
            return DataAccess.Crud.GetEntity<ScaleEntity>(
                new FieldListEntity(new Dictionary<DbField, object?> { { DbField.Description, description }, { DbField.IsMarked, false } }));
        }


        #endregion

        #region Public and private methods - Hosts

        public static HostDirect LoadReader(SqlDataReader reader)
        {
            HostDirect result = new();
            if (reader.Read())
            {
                result.Id = SqlConnect.GetValueAsNotNullable<int>(reader, "ID");
                result.Name = SqlConnect.GetValueAsNullable<string>(reader, "NAME");
                result.HostName = SqlConnect.GetValueAsNullable<string>(reader, "HOSTNAME");
                result.Ip = SqlConnect.GetValueAsNullable<string>(reader, "IP");
                result.Mac = SqlConnect.GetValueAsNullable<string>(reader, "MAC");
                result.IdRRef = SqlConnect.GetValueAsNotNullable<Guid>(reader, "IDRREF");
                result.IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "MARKED");
                result.ScaleId = SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID");
            }
            return result;
        }

        public static HostEntity GetHostEntity(string hostName)
        {
            HostEntity host = DataAccess.Crud.GetEntity<HostEntity>(
                new FieldListEntity(new Dictionary<DbField, object?> {
                    { DbField.HostName, hostName },
                    { DbField.IsMarked, false } }),
                new FieldOrderEntity(DbField.CreateDt, DbOrderDirection.Desc));
            return host;
        }

        public static HostDirect Load(Guid uid)
        {
            HostDirect result = SqlConnect.ExecuteReaderForEntity(SqlQueries.DbScales.Tables.Hosts.GetHostByUid,
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = uid }, LoadReader);
            if (result == null)
                result = new HostDirect();
            return result;
        }

        public static HostDirect Load(string hostName)
        {
            HostDirect result = SqlConnect.ExecuteReaderForEntity(SqlQueries.DbScales.Tables.Hosts.GetHostByHostName,
                new SqlParameter("@HOST_NAME", System.Data.SqlDbType.NVarChar, 255) { Value = hostName }, LoadReader);
            if (result == null)
                result = new HostDirect();
            return result;
        }

        public static HostDirect GetHostDirect()
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

        public static HostDirect GetHostDirect(string hostName) => Load(hostName);

        public static bool CheckHostUidInFile()
        {
            if (!File.Exists(FilePathToken))
                return false;

            XDocument doc = XDocument.Load(FilePathToken);
            Guid idrref = Guid.Parse(doc.Root.Elements("ID").First().Value);
            bool result = default;
            SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Hosts.GetHostIdByIdRRef,
                new SqlParameter("@idrref", System.Data.SqlDbType.UniqueIdentifier) { Value = idrref }, (reader) =>
                {
                    result = reader.Read();
                });
            return result;
        }

        #endregion

        #region Public and private methods - Sscc



        #endregion
    }
}