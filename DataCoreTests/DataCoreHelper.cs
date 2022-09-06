﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql.Core;
using DataCore.Sql.Fields;
using DataCore.Sql.Tables;
using FluentValidation;
using System;
using System.IO;
using System.Threading;

namespace DataCoreTests;

public class DataCoreHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DataCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DataCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

	public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;

	#endregion

	#region Public and private methods

	private void SetupDebug()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameDebug);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	private void SetupRelease()
	{
		DataAccess.JsonControl.SetupForTests(Directory.GetCurrentDirectory(),
			NetUtils.GetLocalHostName(true), nameof(DataCoreTests), JsonSettingsController.FileNameRelease);
		TestContext.WriteLine($"{nameof(DataAccess.JsonSettingsIsRemote)}: {DataAccess.JsonSettingsIsRemote}");
		TestContext.WriteLine(DataAccess.JsonSettingsIsRemote ? DataAccess.JsonSettingsRemote : DataAccess.JsonSettingsLocal);
	}

	public void AssertAction(Action action)
	{
		Assert.DoesNotThrow(() =>
		{
			SetupRelease();
			action.Invoke();
			TestContext.WriteLine();

			SetupDebug();
			action.Invoke();
		});
	}

	public void FailureWriteLine(ValidationResult result)
	{
		switch (result.IsValid)
		{
			case false:
				{
					foreach (ValidationFailure failure in result.Errors)
					{
						TestContext.WriteLine($"{LocaleCore.Validator.Property} {failure.PropertyName} {LocaleCore.Validator.FailedValidation}. {LocaleCore.Validator.Error}: {failure.ErrorMessage}");
					}
					break;
				}
		}
	}

	public void AssertSqlDataValidate<T>(int maxResults = 0) where T : TableModel, new()
	{
		AssertAction(() =>
		{
			foreach (bool isShowMarked in DataCoreEnums.GetBool())
			{
				// Arrange.
				IValidator<T> validator = SqlUtils.GetSqlValidator(Substitute.For<T>());
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, maxResults, isShowMarked, true);
				T[]? items = DataAccess.GetItems<T>(sqlCrudConfig);
				// Act.
				if (items == null || !items.Any())
				{
					TestContext.WriteLine($"{nameof(items)} is null or empty!");
				}
				else
				{
					TestContext.WriteLine($"Found {items.Length} items. Print top 10.");
					int i = 0;
					foreach (T item in items)
					{
						if (i < 10)
							TestContext.WriteLine(item);
						i++;
						ValidationResult result = validator.Validate(item);
						FailureWriteLine(result);
						// Assert.
						Assert.IsTrue(result.IsValid);
					}
				}
			}
		});
	}

	public void AssertSqlExtensionValidate<T>() where T : TableModel, new()
	{
		AssertAction(() =>
		{
			foreach (bool isShowMarked in DataCoreEnums.GetBool())
			{
				// Arrange.
				IValidator<T> validator = SqlUtils.GetSqlValidator(Substitute.For<T>());
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, null, 0, isShowMarked, true);
				List<T> items = DataAccess.GetList<T>(sqlCrudConfig);
				// Act.
				if (!items.Any())
				{
					TestContext.WriteLine($"{nameof(items)} is null or empty!");
				}
				else
				{
					TestContext.WriteLine($"Found {items.Count} items. Print top 10.");
					List<T> itemsCast = items.Cast<T>().ToList();
					int i = 0;
					foreach (T item in itemsCast)
					{
						if (i < 10)
							TestContext.WriteLine(item);
						i++;
						ValidationResult result = validator.Validate(item);
						FailureWriteLine(result);
						// Assert.
						Assert.IsTrue(result.IsValid);
					}
				}
			}
		});
	}

	public void AssertSqlValidate<T>(T item, bool assertResult) where T : TableModel, new()
	{
		// Arrange.
		IValidator<T> validator = SqlUtils.GetSqlValidator(item);
		// Act & Assert.
		AssertValidate(item, validator, assertResult);
	}

	public void AssertValidate<T>(T item, IValidator<T> validator, bool assertResult) where T : class, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Act.
			ValidationResult result = validator.Validate(item);
			FailureWriteLine(result);
			// Assert.
			switch (assertResult)
			{
				case true:
					Assert.IsTrue(result.IsValid);
					break;
				default:
					Assert.IsFalse(result.IsValid);
					break;
			}
		});
	}

	public T CreateNewSubstitute<T>(bool isNotDefault) where T : TableModel, new()
	{
		FieldIdentityModel fieldIdentity = Substitute.For<FieldIdentityModel>(ColumnName.Default);
		fieldIdentity.Name.Returns(ColumnName.Default);
		fieldIdentity.Uid.Returns(Guid.NewGuid());
		fieldIdentity.Id.Returns(-1);

		T item = Substitute.For<T>();
		if (!isNotDefault)
		{
			return item;
		}

		item.Identity.Returns(fieldIdentity);
		item.CreateDt.Returns(DateTime.Now);
		item.ChangeDt.Returns(DateTime.Now);
		item.IsMarked.Returns(false);

		switch (item)
		{
			case AccessModel access:
				access.User = "Test";
				break;
			case AppModel app:
				app.Name = "Test";
				break;
			case BarCodeTypeModel barCodeTypeV2:
				barCodeTypeV2.Name = "Test";
				break;
			case BarCodeModel barCodeV2:
				barCodeV2.Value = "Test";
				break;
			case ContragentModel contragentV2:
				contragentV2.Name = "Test";
				break;
			case HostModel host:
				host.Name = "Test";
				host.Ip = "127.0.0.1";
				host.MacAddressValue = "001122334455";
				host.HostName = "Test";
				host.AccessDt = DateTime.Now;
				break;
			case LogModel log:
				log.Version = "0.1.2";
				log.File = "Test.cs";
				log.Line = 1;
				log.Member = "Test";
				log.LogType = CreateNewSubstitute<LogTypeModel>(isNotDefault);
				log.Message = "Test";
				break;
			case LogTypeModel logType:
				logType.Icon = "Test";
				break;
			case NomenclatureModel nomenclature:
				nomenclature.Name = "0.1.2";
				nomenclature.Code = "ЦБД00012345";
				nomenclature.Xml = "<Product Category=\"Сосиски\" > </Product>";
				nomenclature.Weighted = false;
				break;
			case OrderModel order:
				order.Name = "Test";
				order.BoxCount = 1;
				order.PalletCount = 1;
				break;
			case OrderWeighingModel orderWeighing:
				orderWeighing.Order = CreateNewSubstitute<OrderModel>(isNotDefault);
				orderWeighing.PluWeighing = CreateNewSubstitute<PluWeighingModel>(isNotDefault);
				break;
			case OrganizationModel organization:
				organization.Name = "Test";
				organization.Gln = 1;
				organization.Xml = "Test";
				break;
			case PluModel plu:
				plu.Name = "Test";
				plu.Number = 100;
				plu.FullName = "Test";
				plu.Description = "Test";
				plu.Gtin = "Test";
				plu.Ean13 = "Test";
				plu.Itf14 = "Test";
				plu.Template = CreateNewSubstitute<TemplateModel>(isNotDefault);
				plu.Nomenclature = CreateNewSubstitute<NomenclatureModel>(isNotDefault);
				break;
			case PluLabelModel pluLabel:
				pluLabel.Zpl = "Test";
				pluLabel.PluWeighing = CreateNewSubstitute<PluWeighingModel>(isNotDefault);
				break;
			case PluScaleModel pluScale:
				pluScale.IsActive = true;
				pluScale.Plu = CreateNewSubstitute<PluModel>(isNotDefault);
				pluScale.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case PluWeighingModel pluWeighing:
				pluWeighing.Sscc = "Test";
				pluWeighing.NettoWeight = (decimal)1.1;
				pluWeighing.TareWeight = (decimal)0.25;
				pluWeighing.ProductDt = DateTime.Now;
				pluWeighing.RegNum = 1;
				pluWeighing.Kneading = 1;
				pluWeighing.PluScale = CreateNewSubstitute<PluScaleModel>(isNotDefault);
				pluWeighing.Series = CreateNewSubstitute<ProductSeriesModel>(isNotDefault);
				break;
			case PrinterModel printer:
				printer.DarknessLevel = 1;
				printer.PrinterType = CreateNewSubstitute<PrinterTypeModel>(isNotDefault);
				break;
			case PrinterResourceModel printerResource:
				printerResource.Description = "Test";
				printerResource.Printer = CreateNewSubstitute<PrinterModel>(isNotDefault);
				printerResource.Resource = CreateNewSubstitute<TemplateResourceModel>(isNotDefault);
				break;
			case PrinterTypeModel printerType:
				printerType.Name = "Test";
				break;
			case ProductionFacilityModel productionFacility:
				productionFacility.Name = "Test";
				productionFacility.Address = "Test";
				break;
			case ProductSeriesModel productSeries:
				productSeries.Sscc = "Test";
				productSeries.IsClose = false;
				productSeries.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case ScaleModel scale:
				scale.Description = "Test";
				break;
			case VersionModel version:
				version.Version = 1;
				version.Description = "Test";
				version.ReleaseDt = DateTime.Now;
				break;
			case TaskModel task:
				task.TaskType = CreateNewSubstitute<TaskTypeModel>(isNotDefault);
				task.Scale = CreateNewSubstitute<ScaleModel>(isNotDefault);
				break;
			case TaskTypeModel taskType:
				taskType.Name = "Test";
				break;
			case TemplateModel template:
				template.Title = "Test";
				break;
			case TemplateResourceModel templateResource:
				templateResource.Name = "Test";
				templateResource.Description = "Test";
				break;
			case WorkShopModel workShop:
				workShop.Name = "Test";
				workShop.ProductionFacility = CreateNewSubstitute<ProductionFacilityModel>(isNotDefault);
				break;
		}
		return item;
	}

	#endregion
}