﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading;

namespace DataCore.Localizations
{
    public class LocaleSql
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LocaleSql _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LocaleSql Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        public ShareEnums.Lang Lang { get; set; } = ShareEnums.Lang.Russian;

        #region Public and private fields and properties

        public string SqlDb => Lang == ShareEnums.Lang.English ? "SQL-DB" : "SQL-БД";
        public string SqlDbCurSize => Lang == ShareEnums.Lang.English ? "DB size" : "Размер БД";
        public string SqlDbFillSize => Lang == ShareEnums.Lang.English ? "DB fill percentage" : "Процент заполнения БД";
        public string SqlDbMaxSize => Lang == ShareEnums.Lang.English ? "DB size" : "Максимальный размер БД";
        public string SqlServer => Lang == ShareEnums.Lang.English ? "SQL-server" : "SQL-сервер";
        public string SqlUser => Lang == ShareEnums.Lang.English ? "SQL-user" : "SQL-пользователь";

        #endregion
    }
}