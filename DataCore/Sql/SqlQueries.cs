﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.Sql
{
    public static class SqlQueries
    {
        public static string GetTopRecords(int topRecords) =>
            topRecords == 0 ? string.Empty : $"TOP {topRecords}";

        public static string GetWhereIsMarked(bool isShowMarkedItems, string alias) =>
            isShowMarkedItems ? string.Empty : $"WHERE {alias}.[IS_MARKED] = 0";

        public static string GetWhereIsMarkedAndNumber(bool isShowMarkedItems, string aliasLog, string aliasLogType, Guid logTypeUid)
        {
            if (isShowMarkedItems)
            {
                if (logTypeUid != Guid.Empty)
                    return $"WHERE {aliasLogType}.[UID] = '{logTypeUid}'";
            }
            else
            {
                if (logTypeUid != Guid.Empty)
                    return $"WHERE {aliasLog}.[IS_MARKED] = 0 AND {aliasLogType}.[UID] = '{logTypeUid}'";
                return $"WHERE {aliasLog}.[IS_MARKED] = 0";
            }
            return string.Empty;
        }

        public static class DbSystem
        {
            public static class Properties
            {
                public static string GetInstance => @"
SELECT SERVERPROPERTY('INSTANCENAME') [INSTANCENAME]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                public static string GetDbSpace => @"
SELECT
		[NAME] [DB_NAME]
	,[SIZE] [DB_SIZE]
	,[SIZE] * 8 / 1024 [DB_SIZE_MB]
	,[MAX_SIZE]
	,[MAX_SIZE] * 8 / 1024 [MAX_SIZE_MB]
FROM [SYS].[DATABASE_FILES]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            }
        }

        public static class DbServiceManaging
        {
            public static class Tables
            {
                public static class Access
                {
                    public static string GetAccessRightsAll => @"
-- Table Access
SELECT
	 [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[IS_MARKED]
	,[USER]
	,[RIGHTS]
FROM [DB_SCALES].[ACCESS]
ORDER BY [USER] ASC
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetAccessRights(string userName) => @$"
-- Table Access
SELECT
	 [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[IS_MARKED]
	,[USER]
	,[RIGHTS]
FROM [DB_SCALES].[ACCESS]
WHERE [USER] = N'{userName}'
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Apps
                {
                    public static string AddApp => @"
if not exists (select 1 from [db_scales].[APPS] where [NAME] = @app) begin
	insert into [db_scales].[APPS]([NAME]) values(@app)
end
select [UID]
from [db_scales].[APPS]
where [NAME] = @app
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Errors
                {
                    public static string GetErrors(int topRecords) => $@"
SELECT {GetTopRecords(topRecords)}
	 [Id]
	,[CreatedDate]
	,[ModifiedDate]
	,[FilePath]
	,[LineNumber]
	,[MemberName]
	,[Exception]
	,[InnerException]
FROM [db_scales].[Errors]
ORDER BY [CreatedDate] DESC
        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Logs
                {
                    public static string AddLog => @"
declare @log_type_uid uniqueidentifier = (select [UID] from [db_scales].[LOG_TYPES] where [NUMBER] = @logNumber)
insert into [db_scales].[LOGS]([HOST_ID],[APP_UID],[VERSION],[FILE],[LINE],[MEMBER],[LOG_TYPE_UID],[MESSAGE]) 
values (@hostId,@appUid,@version,@file,@line,@member,@log_type_uid,@message)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string AddLogType => @"
insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) 
values (@number,@icon)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetLogTypes => @"
-- Table LOG_TYPES
select 
	 [UID]
	,[NUMBER]
	,[ICON]
from [db_scales].[LOG_TYPES]
order by [NUMBER]
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetLogs(int topRecords, bool isShowMarkedItems, Guid logTypeUid) => $@"
-- Table LOGS diagram summary
select {GetTopRecords(topRecords)}
	 [l].[UID]
	,[l].[CREATE_DT]
	,[s].[Description] [SCALE]
	,[h].[Name] [HOST_PRETTY_NAME]
	,[h].[HOSTNAME] [HOSTNAME]
	,[a].[NAME] [APP]
	,[l].[VERSION]
	,[l].[FILE]
	,[l].[LINE]
	,[l].[MEMBER]
	,[lt].[ICON]
	,[l].[MESSAGE]
from [db_scales].[LOGS] [l]
left join [db_scales].[Hosts] [h] on [h].[ID] = [l].[HOST_ID]
left join [db_scales].[Scales] [s] on [s].[HostId] = [h].[ID]
left join [db_scales].[APPS] [a] on [a].[UID] = [l].[APP_UID]
left join [db_scales].[LOG_TYPES] [lt] on [lt].[UID] = [l].[LOG_TYPE_UID]
{GetWhereIsMarkedAndNumber(isShowMarkedItems, "[l]", "[lt]", logTypeUid)}
order by [l].[CREATE_DT] desc
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }
            }
        }

        public static class DbScales
        {
            public static class Tables
            {
                public static class Contragents
                {
                    public static string GetAllItems => @"
SELECT
	[UID]
   ,[DWH_ID]
   ,[CREATE_DT]
   ,[CHANGE_DT]
   ,[MARKED]
   ,[NAME]
   ,[FULL_NAME]
   ,[IDRREF]
   ,[XML]
FROM [DB_SCALES].[CONTRAGENTS_V2]
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                    public static string GetItemByUid => @"
SELECT
	[UID]
   ,[DWH_ID]
   ,[CREATE_DT]
   ,[CHANGE_DT]
   ,[MARKED]
   ,[NAME]
   ,[FULL_NAME]
   ,[IDRREF]
   ,[XML]
FROM [DB_SCALES].[CONTRAGENTS_V2]
WHERE [UID] = @UID
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class BarCodeTypes
                {
                    public static string GetAllItems => @"
SELECT
	[ID]
   ,[NAME]
FROM [DB_SCALES].[BarCodeTypes]
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                    public static string GetItemById => @"
SELECT
	[ID]
   ,[NAME]
FROM [DB_SCALES].[BarCodeTypes]
WHERE [Id] = @ID
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Hosts
                {
                    public static string GetBusyHosts => @"
------------------------------------------------------------------------------------------------------------------------
-- Table Select Hosts Get Busy
------------------------------------------------------------------------------------------------------------------------
SELECT
	[H].[Id]
   ,[H].[CreateDate]
   ,[H].[ModifiedDate]
   ,[H].[ACCESS_DT]
   ,[H].[Name]
   ,[S].[Id] [SCALE_ID]
   ,[S].[DESCRIPTION] [SCALE_DESCRIPTION]
   ,[H].[IP]
   ,[H].[MAC]
   ,[H].[IdRRef]
   ,[H].[Marked]
FROM [db_scales].[Hosts] [H]
LEFT JOIN [db_scales].[Scales] [S] ON [H].[Id] = [S].[HOSTID]
WHERE [H].[Id] IN (SELECT [HOSTID]
	FROM [db_scales].[Scales]
	WHERE [Scales].[HOSTID] IS NOT NULL)
ORDER BY [H].[Name]
------------------------------------------------------------------------------------------------------------------------
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetFreeHosts => @"
------------------------------------------------------------------------------------------------------------------------
-- Table Select Hosts Get Free
------------------------------------------------------------------------------------------------------------------------
SELECT
	[H].[ID]
   ,[H].[CREATEDATE]
   ,[H].[MODIFIEDDATE]
   ,[H].[ACCESS_DT]
   ,[H].[NAME]
   ,[H].[IP]
   ,[H].[MAC]
   ,[H].[IDRREF]
   ,[H].[MARKED]
FROM [DB_SCALES].[HOSTS] [H]
WHERE [H].[ID] NOT IN (SELECT [HOSTID]
	FROM [DB_SCALES].[SCALES] [S]
	WHERE [S].[HOSTID] IS NOT NULL)
ORDER BY [H].[NAME]
------------------------------------------------------------------------------------------------------------------------
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetHostId => @"
SELECT [ID]
FROM [DB_SCALES].[HOSTS] 
where [Name] = @host and [IdRRef] = @idrref
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetHostIdByIdRRef => @"
SELECT [ID]
FROM [DB_SCALES].[HOSTS] 
where [IdRRef] = @idrref
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetHostByUid => @"
select
	 [H].[ID]
	,[H].[NAME]
	,[H].[HOSTNAME]
	,[H].[IP]
	,[H].[MAC]
	,[H].[IDRREF]
	,[H].[MARKED]
	,[H].[ACCESS_DT]
	,[SCALES].[ID] [SCALE_ID]
	,[SCALES].[DESCRIPTION] [SCALE_DESCRIPTION]
from [db_scales].[HOSTS] [H]
left join [db_scales].[SCALES] [SCALES] on [H].[ID] = [SCALES].[HOSTID]
where [H].[MARKED] = 0 and [H].[IDRREF] = @idrref
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetHostByHostName => @"
SELECT
	 [H].[ID]
	,[H].[NAME]
	,[H].[HOSTNAME]
	,[H].[IP]
	,[H].[MAC]
	,[H].[IDRREF]
	,[H].[MARKED]
	,[H].[ACCESS_DT]
	,[SCALES].[ID] [SCALE_ID]
	,[SCALES].[DESCRIPTION] [SCALE_DESCRIPTION]
FROM [db_scales].[HOSTS] [H]
LEFT JOIN [db_scales].[SCALES] [SCALES] ON [H].[ID] = [SCALES].[HOSTID]
WHERE [H].[MARKED] = 0 AND [H].[HOSTNAME] = @HOST_NAME
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

					public static string InsertNew => @"
INSERT INTO [db_scales].[HOSTS] (IdRRef, NAME, MAC, IP) 
VALUES(@uid, @name, @mac, @ip, @doc)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
				}

				public static class Plu
				{
					public static string GetCount => @"
SELECT COUNT(*) [COUNT] FROM DB_SCALES.PLU WHERE [SCALEID] = @SCALE_ID
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
				}

				public static class Labels
                {
                    public static string Save => @"
INSERT INTO [db_scales].[Labels] ([WeithingFactId], [Label])
	VALUES (@ID, CONVERT(VARBINARY(MAX), @LABEL))
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string SaveZpl => @"
INSERT INTO [db_scales].[Labels] ([WeithingFactId], [ZPL])
VALUES (@ID, @ZPL)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetLabels(int topRecords) => @$"
-- Table Select Labels
SELECT {GetTopRecords(topRecords)}
	[L].[ID]
   ,[L].[CREATEDATE]
   ,[S].[ID] [SCALE_ID]
   ,[S].[DESCRIPTION]
   ,[PLU].[ID] [PLU_ID]
   ,[PLU].[PLU] [PLU_NUMBER]
   ,[PLU].[GOODSNAME] [PLU_NAME]
   ,[WF].[WEITHINGDATE]
   ,[WF].[NETWEIGHT]
   ,[WF].[TAREWEIGHT]
   ,[WF].[PRODUCTDATE]
   ,[WF].[REGNUM]
   ,[WF].[KNEADING]
   ,[L].[ZPL]
   ,[T].[ID] [TEMPLATE_ID]
   ,[T].[TITLE] [TEMPLATE]
   --,REPLACE(REPLACE([L].[ZPL], CHAR(13), ''), CHAR(10), '') [ZPL_STR]
FROM [DB_SCALES].[LABELS] [L]
LEFT JOIN [DB_SCALES].[WEITHINGFACT] [WF] ON [L].[WEITHINGFACTID] = [WF].[ID]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [WF].[SCALEID] = [S].[ID]
LEFT JOIN [DB_SCALES].[PLU] [PLU] ON [WF].[SCALEID] = [PLU].[SCALEID] AND [WF].[PLUID] = [PLU].[PLU]
LEFT JOIN [DB_SCALES].[TEMPLATES] [T] ON [PLU].[TEMPLATEID] = [T].[ID]
ORDER BY [CREATEDATE] DESC
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Scales
                {
                    public static string GetScaleId => @"
select [ID]
from [db_scales].[SCALES]
where [DESCRIPTION] = @SCALE_DESCRIPTION
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetScaleDescription => @"
SELECT
	[DESCRIPTION]
FROM [db_scales].[Scales]
WHERE [id] = @scale_id
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetScaleById => @"
select
	 [s].[Id]
	,[s].[CreateDate]
	,[s].[ModifiedDate]
	,[s].[Description]
	,[s].[DeviceIP]
	,[s].[DevicePort]
	,[s].[DeviceMAC]
	,[s].[DeviceSendTimeout]
	,[s].[DeviceReceiveTimeout]
	,[s].[DeviceComPort]
	,[s].[ZebraIP]
	,[s].[ZebraPort]
	,[s].[ZebraPrinterId]
	,[s].[UseOrder]
	,[s].[VerScalesUI]
	,[s].[NUMBER]
	,[s].[TemplateIdDefault]
	,[s].[TemplateIdSeries]
	,[s].[ScaleFactor]
	,[s].[Marked]
	,[s].[HostId]
	,[lt].[ICON]
from [db_scales].[Scales] [s]
left join [db_scales].[LOG_TYPES] [lt] on [lt].[UID] = [s].[LOG_TYPE_UID]
where [Id] = @id
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string UpdateScale => @"
EXECUTE [db_scales].[UpdateScale]
@ID,
@Description,
@IP,
@Port,
@MAC,
@SendTimeout,
@ReceiveTimeout,
@ComPort,
@UseOrder,
@VerScalesUI,
@ScaleFactor;
			".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string UpdateScaleDirect => @"
UPDATE [db_scales].[SCALES]
SET [Description] = @Description
	--,[DeviceIP] = @IP
	,[DevicePort] = @Port
	--,[DeviceMAC] = @MAC
	,[DeviceSendTimeout] = @SendTimeout
	,[DeviceReceiveTimeout] = @ReceiveTimeout
	,[DeviceComPort] = @ComPort
	,[UseOrder] = @UseOrder
	,[VerScalesUI] = @VerScalesUI
	,[ModifiedDate] = GETDATE()
	,[ScaleFactor] = @ScaleFactor
WHERE [Id] = @ID
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string QueryFindGuid => @"
IF EXISTS (SELECT 1 FROM [DB_SCALES].[SCALES] WHERE [DB_SCALES].[SCALES].[1CRREFID] = @GUID)
	SELECT 'TRUE' [RESULT]
ELSE
	SELECT 'FALSE' [RESULT]
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class Tasks
                {
                    public static string GetTaskUid => @"
select [tasks].[UID]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID] = [tasks].[TASK_UID]
where [types].[NAME] = @task_type
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetTasks => @"
select
		[tasks].[UID] [TASK_UID]
	,[scales].[ID] [SCALE_ID]
	,[scales].[DESCRIPTION] [SCALE]
	,[types].[UID] [TASK_TYPE_UID]
	,[types].[NAME] [TASK]
	,[tasks].[ENABLED]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID] = [tasks].[TASK_UID]
left join [db_scales].[SCALES] [scales] on [scales].[ID] = [tasks].[SCALE_ID]
order by [SCALE], [TASK]
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetTaskByTypeAndScale => @"
select
		[tasks].[UID] [TASK_UID]
	,[scales].[ID] [SCALE_ID]
	,[scales].[DESCRIPTION] [SCALE]
	,[types].[UID] [TASK_TYPE_UID]
	,[types].[NAME] [TASK]
	,[tasks].[ENABLED]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID] = [tasks].[TASK_UID]
left join [db_scales].[SCALES] [scales] on [scales].[ID] = [tasks].[SCALE_ID]
where [types].[UID] = @task_type_uid and [scales].[Id] = @scale_id
order by [SCALE], [TASK]
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetTaskByUid => @"
select
		[tasks].[UID] [TASK_UID]
	,[scales].[ID] [SCALE_ID]
	,[scales].[DESCRIPTION] [SCALE]
	,[types].[UID] [TASK_TYPE_UID]
	,[types].[NAME] [TASK]
	,[tasks].[ENABLED]
from [db_scales].[TASKS] [tasks]
left join [db_scales].[TASKS_TYPES] [types] on [types].[UID] = [tasks].[TASK_UID]
left join [db_scales].[SCALES] [scales] on [scales].[ID] = [tasks].[SCALE_ID]
where [tasks].[UID] = @task_uid
order by [SCALE], [TASK]
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string InsertOrUpdateTask => @"
if exists(select 1 from [db_scales].[TASKS] where [UID] = @uid) begin
	update [db_scales].[TASKS] set [ENABLED] = @enabled where [UID] = @uid
end else begin
	insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
	values(@task_type_uid,@scale_id,@enabled)
end
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string InsertTask => @"
insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
values(@task_type_uid,@scale_id,@enabled)
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string UpdateTask => @"
update [db_scales].[TASKS] set [ENABLED] = @enabled where [UID] = @uid
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class TaskTypes
                {
                    public static string GetTaskTypeUid => @"
SELECT [UID]
FROM [DB_SCALES].[TASKS_TYPES] 
WHERE [NAME] = @task_type
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetTasksTypes => @"
SELECT
	 [UID]
	,[NAME]
FROM [DB_SCALES].[TASKS_TYPES]
ORDER BY [NAME]
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetTasksTypesByName => @"
SELECT
	 [UID]
	,[NAME]
FROM [DB_SCALES].[TASKS_TYPES]
WHERE [NAME] = @task_name
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string GetTasksTypesByUid => @"
select
		[UID]
	,[NAME]
from [db_scales].[TASKS_TYPES]
where [UID] = @task_uid
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string AddTaskType => @"
insert into [db_scales].[TASKS_TYPES]([NAME])
values(@name)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class WeithingFacts
                {
                    public static string GetWeithingFacts(int topRecords) => @$"
-- Table WeithingFact diagram summary
SELECT {GetTopRecords(topRecords)}
	 cast([wf].[WeithingDate] as date) [WeithingDate]
	,count(*) [Count]
	,[s].[Description] [Scale]
	,[h].[Name] [Host]
	,[p].[Name] [Printer]
from [db_scales].[WeithingFact] [wf]
left join [db_scales].[Scales] [s] on [wf].[ScaleId] = [s].[Id]
left join [db_scales].[Hosts] [h] on [s].[HostId] = [h].[Id]
left join [db_scales].[ZebraPrinter] [p] on [s].[ZebraPrinterId] = [p].[Id]
group by cast([WeithingDate] as date), [s].[Description], [h].[Name], [p].[Name]
order by [WeithingDate] desc
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                    public static string Save => @"
DECLARE @SSCC varchar(50);
DECLARE @WeithingDate datetime;
DECLARE @xmldata xml;
DECLARE @ID int;
EXECUTE [db_scales].[SetWeithingFact] @OrderID,@ScaleID,@PLU,@NetWeight,@TareWeight,@ProductDate,@Kneading,@SSCC OUTPUT,@WeithingDate OUTPUT,@xmldata OUTPUT,@ID OUTPUT;
SELECT  @SSCC, @WeithingDate, convert(varchar(max), @xmldata) xmldata, @ID;
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }

                public static class ZebraPrinter
                {
                    public static string GetAllItems => @"
SELECT [ZP].[Id]
	  ,[ZP].[Name]
	  ,[ZP].[IP]
	  ,[ZP].[Port]
	  ,[ZP].[Password]
	  ,[ZP].[PrinterTypeId]
	  ,[ZP].[Mac]
	  ,[ZP].[PeelOffSet]
	  ,[ZP].[DarknessLevel]
	  ,[ZP].[CreateDate]
	  ,[ZP].[ModifiedDate]
	  ,[ZP].[Marked]
FROM [db_scales].[ZebraPrinter] [ZP]
ORDER BY [ZP].[Name]
				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                    public static string GetItemByUid => @"

				".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                }
            }

            public static class Functions
            {
                [Obsolete(@"Use GetCurrentProductSeriesV2")]
                public static string GetCurrentProductSeries => @"
DECLARE @SSCC VARCHAR(50)
DECLARE @WeithingDate DATETIME
DECLARE @XML XML
EXECUTE [db_scales].[NewProductSeries] @ScaleID, @SSCC OUTPUT, @XML OUTPUT
SELECT [Id], [CreateDate], [UUID], [SSCC], [CountUnit], [TotalNetWeight], [TotalTareWeight]
FROM [db_scales].[GetCurrentProductSeries](@ScaleId)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');

                public static string GetCurrentProductSeriesV2 => @"
DECLARE @SSCC VARCHAR(50)
DECLARE @WeithingDate DATETIME
DECLARE @XML XML

EXECUTE [db_scales].[SP_SET_PRODUCT_SERIES_V2] @SCALE_ID, @SSCC OUTPUT, @XML OUTPUT

SELECT [ID], [CREATE_DT], [UUID], [SSCC], [COUNT_UNIT],[TOTAL_NET_WEIGHT], [TOTAL_TARE_WEIGHT], [IS_MARKED]
FROM [db_scales].[FN_GET_PRODUCT_SERIES_V2](@SCALE_ID)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            }

            public static class StoredProcedures
            {
                public static string GetBarCode => @"
SELECT * FROM
[db_scales].[GetBarCode] (@BarCodeTypeId,@NomenclatureId,@NomenclatureUnitId,@ContragentId)
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                public static string SetBarCode => @"
DECLARE @ID int
EXECUTE [db_scales].[SetBarCode]
     @BarCodeTypeId
    ,@NomenclatureId
    ,@NomenclatureUnitId
    ,@ContragentId
    ,@Value
    ,@ID OUTPUT
    ,@ID OUTPUT
SELECT @ID as ID
						".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            }
        }
    }
}
