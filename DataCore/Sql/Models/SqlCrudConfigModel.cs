﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Models;

public enum EnumCrudAction
{
	Add,
	Remove
}

public class SqlCrudConfigModel : ICloneable
{
    #region Public and private fields, properties, constructor

    #region Properties
    
    public JsonSettingsHelper JsonSettings { get; } = JsonSettingsHelper.Instance;
    public List<SqlFieldFilterModel> Filters { get; private set; }
    public List<SqlFieldOrderModel> Orders { get; private set; }
    public bool IsGuiShowFilterAdditional { get; set; }
    public bool IsGuiShowFilterMarked { get; set; }
    public bool IsGuiShowFilterOnlyTop { get; set; }
    public bool IsGuiShowItemsCount { get; set; }
    public bool IsResultAddFieldEmpty { get; }
    public bool IsResultOrder { get; set; }
    public bool IsResultShowOnlyTop { get; set; }
    public bool IsResultShowMarked
    {
        get => _isResultShowMarked;
        set
        {
            _isResultShowMarked = value;
            SetFiltersIsResultShowMarked();
        }
    }
    public int ResultMaxCount
    {
        get => _resultMaxCount;
        set => _resultMaxCount = value == 1 ? 1
            : IsResultShowOnlyTop ? JsonSettings.Local.SelectTopRowsCount : value;
    }

    #endregion

    private bool _isResultShowMarked;

    private int _resultMaxCount;

    public SqlCrudConfigModel()
	{
		Filters = new();
		Orders = new();

		IsGuiShowFilterAdditional = false;
		IsGuiShowFilterMarked = false;
		IsGuiShowFilterOnlyTop = true;
		IsGuiShowItemsCount = false;

		IsResultShowMarked = false;
		IsResultShowOnlyTop = false;
		IsResultAddFieldEmpty = false;
		IsResultOrder = false;

		ResultMaxCount = 0;
	}

	public SqlCrudConfigModel(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, int maxCount = 0)
	{
		Filters = filters;
		Orders = orders;

		IsGuiShowFilterAdditional = false;
		IsGuiShowFilterMarked = false;
		IsGuiShowFilterOnlyTop = true;
		IsGuiShowItemsCount = false;

		IsResultShowMarked = isShowMarked;
		IsResultShowOnlyTop = isShowOnlyTop;
		IsResultAddFieldEmpty = isAddFieldEmpty;
		IsResultOrder = isOrder;

		ResultMaxCount = maxCount;
	}

	public SqlCrudConfigModel(List<SqlFieldFilterModel> filters,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, int maxCount = 0) :
		this(filters, new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount)
	{ }

	public SqlCrudConfigModel(List<SqlFieldOrderModel> orders,
		bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, int maxCount = 0) :
		this(new(), orders, isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount)
	{ }

	public SqlCrudConfigModel(bool isShowMarked, bool isShowOnlyTop, bool isAddFieldEmpty, bool isOrder, int maxCount = 0) :
		this(new(), new(), isShowMarked, isShowOnlyTop, isAddFieldEmpty, isOrder, maxCount)
	{ }

	#endregion

	#region Public and private methods - Filters

	public static List<SqlFieldFilterModel> GetFilters(string className, SqlTableBase? item) =>
		item is null || string.IsNullOrEmpty(className) ? new()
			: GetFiltersIdentity(className, item.Identity.Name == SqlFieldIdentityEnum.Uid ? item.IdentityValueUid : item.IdentityValueId);

	public static List<SqlFieldFilterModel> GetFilters(string className, object? value) =>
		new() { new(className, SqlFieldComparerEnum.Equal, value) };

	public static List<SqlFieldFilterModel> GetFiltersIdentity(string className, object? value) =>
		value switch
		{
			Guid uid => new() { new($"{className}.{nameof(SqlTableBase.IdentityValueUid)}", SqlFieldComparerEnum.Equal, uid) },
			long id => new() { new($"{className}.{nameof(SqlTableBase.IdentityValueId)}", SqlFieldComparerEnum.Equal, id) },
			_ => new()
		};

	private List<SqlFieldFilterModel> GetFiltersIsResultShowMarked(bool isShowMarked) =>
		new() { new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, isShowMarked) };

    public void AddFilters(List<SqlFieldFilterModel> filters)
    {
        if (!Filters.Any())
			Filters = filters;
        else
            foreach (SqlFieldFilterModel filter in filters)
            {
                if (!Filters.Contains(filter))
                {
                    Filters.Add(filter);
                }
            }
    }

    public void AddFilters(string className, SqlTableBase? item) => AddFilters(GetFilters(className, item));

    public void RemoveFilters(List<SqlFieldFilterModel> filters)
    {
		if (!Filters.Any()) return;
            bool isExists = true;
            while (isExists)
            {
                isExists = false;
                foreach (SqlFieldFilterModel filter in filters)
                {
                    if (Filters.Contains(filter))
                    {
                        isExists = true;
                        Filters.Remove(filter);
                        break;
                    }
                }
            }
    }

    public void RemoveFilters(string className, SqlTableBase? item) => RemoveFilters(GetFilters(className, item));

    private void SetFiltersIsResultShowMarked()
	{
        if (!IsResultShowMarked)
            AddFilters(GetFiltersIsResultShowMarked(false));
        else
            RemoveFilters(GetFiltersIsResultShowMarked(false));
    }

	#endregion

	#region Public and private methods - Orders

	private void AddOrders(List<SqlFieldOrderModel> orders)
	{
        if (!Orders.Any())
			Orders = orders;
        else
			foreach (SqlFieldOrderModel order in orders)
            {
                if (!Orders.Contains(order))
                {
                    Orders.Add(order);
                }
            }
    }

    public void AddOrders(SqlFieldOrderModel order) => AddOrders(new List<SqlFieldOrderModel>() { order });
    
	private void RemoveOrders(List<SqlFieldOrderModel> orders)
    {
        if (!Orders.Any()) return;
        bool isExists = true;
        while (isExists)
        {
            isExists = false;
            foreach (SqlFieldOrderModel order in orders)
            {
                if (Orders.Contains(order))
                {
                    isExists = true;
                    Orders.Remove(order);
                    break;
                }
            }
        }
    }

    public void RemoveOrders(SqlFieldOrderModel order) => RemoveOrders(new List<SqlFieldOrderModel>() { order });

    public object Clone()
    {
        SqlCrudConfigModel item = new();
        item.Filters = new(Filters);
        item.Orders = new(Orders);
        item.IsGuiShowFilterAdditional = IsGuiShowFilterAdditional;
        item.IsGuiShowFilterMarked = IsGuiShowFilterMarked;
        item.IsGuiShowFilterOnlyTop = IsGuiShowFilterOnlyTop;
        item.IsGuiShowItemsCount = IsGuiShowItemsCount;
        item.IsResultShowMarked = IsResultShowMarked;
        item.IsResultShowOnlyTop = IsResultShowOnlyTop;
        item.ResultMaxCount = ResultMaxCount;
        return item;
    }

    public SqlCrudConfigModel CloneCast() => (SqlCrudConfigModel)Clone();

    #endregion
}