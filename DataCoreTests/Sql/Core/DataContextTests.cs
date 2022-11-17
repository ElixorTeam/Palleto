﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using NUnit.Framework.Interfaces;

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class DataContextTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void GetListNotNullable_Exec_DoesNotThrow()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCore.DataContext.GetTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange & Act.
						GetListNotNullable<AccessModel>();
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						GetListNotNullable<AppModel>();
						break;
					case var cls when cls == typeof(BarCodeModel):
						// Arrange & Act.
						GetListNotNullable<BarCodeModel>();
						break;
					case var cls when cls == typeof(ContragentModel):
						// Arrange & Act.
						GetListNotNullable<ContragentModel>();
						break;
					case var cls when cls == typeof(DeviceModel):
						// Arrange & Act.
						GetListNotNullable<DeviceModel>();
						break;
					case var cls when cls == typeof(DeviceTypeModel):
						// Arrange & Act.
						GetListNotNullable<DeviceTypeModel>();
						break;
					case var cls when cls == typeof(DeviceTypeFkModel):
						// Arrange & Act.
						GetListNotNullable<DeviceTypeFkModel>();
						break;
					case var cls when cls == typeof(DeviceScaleFkModel):
						// Arrange & Act.
						GetListNotNullable<DeviceScaleFkModel>();
						break;
					case var cls when cls == typeof(LogModel):
						// Arrange & Act.
						GetListNotNullable<LogModel>();
						break;
					case var cls when cls == typeof(LogTypeModel):
						// Arrange & Act.
						GetListNotNullable<LogTypeModel>();
						break;
					case var cls when cls == typeof(NomenclatureModel):
						// Arrange & Act.
						GetListNotNullable<NomenclatureModel>();
						break;
					case var cls when cls == typeof(OrderModel):
						GetListNotNullable<OrderModel>();
						break;
					case var cls when cls == typeof(OrganizationModel):
						GetListNotNullable<OrganizationModel>();
						break;
					case var cls when cls == typeof(PackageModel):
						GetListNotNullable<PackageModel>();
						break;
					case var cls when cls == typeof(PluLabelModel):
						GetListNotNullable<PluLabelModel>();
						break;
					case var cls when cls == typeof(PluModel):
						GetListNotNullable<PluModel>();
						break;
					case var cls when cls == typeof(PluPackageModel):
						GetListNotNullable<PluPackageModel>();
						break;
					case var cls when cls == typeof(PluScaleModel):
						GetListNotNullable<PluScaleModel>();
						break;
					case var cls when cls == typeof(PluWeighingModel):
						GetListNotNullable<PluWeighingModel>();
						break;
					case var cls when cls == typeof(PrinterModel):
						GetListNotNullable<PrinterModel>();
						break;
					case var cls when cls == typeof(PrinterResourceModel):
						GetListNotNullable<PrinterResourceModel>();
						break;
					case var cls when cls == typeof(PrinterTypeModel):
						GetListNotNullable<PrinterTypeModel>();
						break;
					case var cls when cls == typeof(ProductionFacilityModel):
						GetListNotNullable<ProductionFacilityModel>();
						break;
					case var cls when cls == typeof(ProductSeriesModel):
						GetListNotNullable<ProductSeriesModel>();
						break;
					case var cls when cls == typeof(ScaleModel):
						GetListNotNullable<ScaleModel>();
						break;
					case var cls when cls == typeof(ScaleScreenShotModel):
						GetListNotNullable<ScaleScreenShotModel>();
						break;
					case var cls when cls == typeof(TaskModel):
						GetListNotNullable<TaskModel>();
						break;
					case var cls when cls == typeof(TaskTypeModel):
						GetListNotNullable<TaskTypeModel>();
						break;
					case var cls when cls == typeof(TemplateModel):
						GetListNotNullable<TemplateModel>();
						break;
					case var cls when cls == typeof(TemplateResourceModel):
						GetListNotNullable<TemplateResourceModel>();
						break;
					case var cls when cls == typeof(VersionModel):
						GetListNotNullable<VersionModel>();
						break;
					case var cls when cls == typeof(WorkShopModel):
						GetListNotNullable<WorkShopModel>();
						break;
				}
			}
		});
	}

	private void GetListNotNullable<T>() where T : SqlTableBase, new()
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(true, true);
		// Arrange & Act.
		List<T> items = DataCore.DataContext.GetListNotNullable<T>(sqlCrudConfig);
		TestContext.WriteLine($"Get {DataCore.DataContext.GetTableModelName<T>()} list: {items.Count}");
		foreach (T item in items)
		{
			// Assert.
			DataCore.AssertSqlValidate(item, true);
		}
	}

	#endregion
}