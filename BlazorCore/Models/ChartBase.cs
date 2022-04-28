﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BlazorCore.Models
{
    public class ChartBase
    {
        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;

        #endregion

        #region Public and private methods

        public string ChartDataFormat(object value) => ((int)value).ToString("####", CultureInfo.InvariantCulture);

        public ChartCountEntity[] GetContragentsChartEntities(ShareEnums.DbField field)
        {
            ChartCountEntity[] result = Array.Empty<ChartCountEntity>();
            ContragentEntityV2[]? items = AppSettings.DataAccess.Crud.GetEntities<ContragentEntityV2>(null,
                new FieldOrderEntity(ShareEnums.DbField.CreateDt, ShareEnums.DbOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case ShareEnums.DbField.CreateDt:
                    List<ChartCountEntity> entitiesDateCreated = new();
                    if (items?.Any() == true)
                    {
                        foreach (ContragentEntityV2 item in items)
                        {
                            entitiesDateCreated.Add(new ChartCountEntity(item.CreateDt.Date, 1));
                            i++;
                        }
                    }
                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupCreated = entitiesDateCreated.GroupBy(item => item.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> item in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(item.Key, item.Count());
                        i++;
                    }
                    break;
                case ShareEnums.DbField.ChangeDt:
                    List<ChartCountEntity> entitiesDateModified = new();
                    if (items?.Any() == true)
                    {
                        foreach (ContragentEntityV2 item in items)
                        {
                            entitiesDateModified.Add(new ChartCountEntity(item.ChangeDt.Date, 1));
                            i++;
                        }
                    }
                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupModified = entitiesDateModified.GroupBy(item => item.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupModified.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> item in entitiesGroupModified)
                    {
                        result[i] = new ChartCountEntity(item.Key, item.Count());
                        i++;
                    }
                    break;
            }
            return result;
        }

        public ChartCountEntity[] GetNomenclaturesChartEntities(ShareEnums.DbField field)
        {
            ChartCountEntity[] result = Array.Empty<ChartCountEntity>();
            NomenclatureEntity[] items = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(null,
                new FieldOrderEntity(ShareEnums.DbField.CreateDt, ShareEnums.DbOrderDirection.Asc));
            int i = 0;
            switch (field)
            {
                case ShareEnums.DbField.CreateDt:
                    List<ChartCountEntity> entitiesDateCreated = new();
                    foreach (NomenclatureEntity item in items)
                    {
                        if (item.CreateDt != default)
                            entitiesDateCreated.Add(new ChartCountEntity(item.CreateDt.Date, 1));
                        i++;
                    }
                    IGrouping<DateTime, ChartCountEntity>[] entitiesGroupCreated = entitiesDateCreated.GroupBy(item => item.Date).ToArray();
                    result = new ChartCountEntity[entitiesGroupCreated.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> item in entitiesGroupCreated)
                    {
                        result[i] = new ChartCountEntity(item.Key, item.Count());
                        i++;
                    }
                    break;
                case ShareEnums.DbField.ChangeDt:
                    List<ChartCountEntity> entitiesDateModified = new();
                    foreach (NomenclatureEntity item in items)
                    {
                        if (item.ChangeDt != default)
                            entitiesDateModified.Add(new ChartCountEntity(item.ChangeDt.Date, 1));
                        i++;
                    }

                    IGrouping<DateTime, ChartCountEntity>[] entitiesModied = entitiesDateModified.GroupBy(item => item.Date).ToArray();
                    result = new ChartCountEntity[entitiesModied.Length];
                    i = 0;
                    foreach (IGrouping<DateTime, ChartCountEntity> item in entitiesModied)
                    {
                        result[i] = new ChartCountEntity(item.Key, item.Count());
                        i++;
                    }

                    break;
            }
            return result;
        }

        #endregion
    }
}