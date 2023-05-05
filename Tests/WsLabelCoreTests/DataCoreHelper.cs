// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCoreTests;

public sealed class DataCoreHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static DataCoreHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static DataCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public WsJsonSettingsHelper JsonSettings => WsJsonSettingsHelper.Instance;

    #endregion

    #region Public and private methods

    public void SetupDevelopAleksandrov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopAleksandrov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void SetupDevelopMorozov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopMorozov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    public void SetupDevelopVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseAleksandrov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseAleksandrov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseMorozov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseMorozov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

    private void SetupReleaseVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsLabelCoreTests), isShowSql);
        TestContext.WriteLine($"{nameof(JsonSettings.IsRemote)}: {JsonSettings.IsRemote}");
        TestContext.WriteLine(JsonSettings.IsRemote ? JsonSettings.Remote : JsonSettings.Local);
    }

	public void AssertAction(Action action, bool isShowSql, bool isSkipDbRelease = false)
	{
		Assert.DoesNotThrow(() =>
		{
			if (!isSkipDbRelease)
			{
				SetupReleaseVs(isShowSql);
				action.Invoke();
				TestContext.WriteLine();
			}

			SetupDevelopVs(isShowSql);
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

    public void AssertSqlDbContentValidate<T>(bool isShowMarked = false) where T : WsSqlTableBase, new()
    {
        AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(isShowMarked);
            List<T> items = ContextManager.AccessList.GetListNotNullable<T>(sqlCrudConfig);
            Assert.IsTrue(items.Any());
            //WsTestsUtils.DataCore.PrintTopRecords(items, 10, true);
            if (!items.Any())
                TestContext.WriteLine($"{nameof(items)} is null or empty!");
            else
            {
                TestContext.WriteLine($"Found {items.Count} items. Print top 5.");
                int i = 0;
                foreach (T item in items)
                {
                    if (i < 5)
                        TestContext.WriteLine(item);
                    i++;
                    AssertSqlValidate(item, true);
                    ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item);
                    FailureWriteLine(validationResult);
                    // Assert.
                    Assert.IsTrue(validationResult.IsValid);
                }
            }
        }, false, false);
    }

	public void AssertSqlValidate<T>(T item, bool assertResult) where T : WsSqlTableBase, new() =>
		AssertValidate(item, assertResult);

    public void AssertSqlDbContentSerialize<T>() where T : WsSqlTableBase, new()
    {
        AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(false);
            List<T> items = ContextManager.AccessList.GetListNotNullable<T>(sqlCrudConfig);
            Assert.IsTrue(items.Any());
            //WsTestsUtils.DataCore.PrintTopRecords(items, 10, true, true);
            if (!items.Any())
                TestContext.WriteLine($"{nameof(items)} is null or empty!");
            else
            {
                TestContext.WriteLine($"Found {items.Count} items. Print top 5.");
                int i = 0;
                foreach (T item in items)
                {
                    string xml = WsDataFormatUtils.SerializeAsXmlString<T>(item, true, false);
                    if (i < 5)
                    {
                        TestContext.WriteLine(xml);
                        TestContext.WriteLine();
                    }
                    i++;
                    // Assert.
                    Assert.IsNotEmpty(xml);
                }
            }
        }, false, false);
    }
    
    public void AssertValidate<T>(T item, bool assertResult) where T : class, new()
	{
		Assert.DoesNotThrow(() =>
		{
			ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item);
			FailureWriteLine(validationResult);
			// Assert.
			switch (assertResult)
			{
				case true:
					Assert.IsTrue(validationResult.IsValid);
					break;
				default:
					Assert.IsFalse(validationResult.IsValid);
					break;
			}
		});
	}

	public object? GetSqlPropertyValue<T>(bool isNotDefault, string propertyName) where T : WsSqlTableBase, new()
	{
		// Arrange
		T item = CreateNewSubstitute<T>(isNotDefault);
		// Act.
		object? value = item.GetPropertyAsObject(propertyName);
		TestContext.WriteLine($"{typeof(T)}. {propertyName}: {value}");
		return value;
	}

	public void AssertSqlPropertyCheckDt<T>(string propertyName) where T : WsSqlTableBase, new()
	{
		// Arrange & Act.
		object? value = GetSqlPropertyValue<T>(true, propertyName);
		if (value is DateTime dtValue)
		{
			// Assert.
			Assert.IsNotNull(dtValue);
			Assert.AreNotEqual(DateTime.MinValue, dtValue);
		}
	}

	public void AssertSqlPropertyCheckBool<T>(string propertyName) where T : WsSqlTableBase, new()
	{
		// Arrange & Act.
		object? value = GetSqlPropertyValue<T>(true, propertyName);
		if (value is bool isValue)
		{
			// Assert.
			Assert.IsNotNull(isValue);
			Assert.IsFalse(isValue);
		}
	}

	public void AssertSqlPropertyCheckString<T>(string propertyName) where T : WsSqlTableBase, new()
	{
		// Arrange & Act.
		object? value = GetSqlPropertyValue<T>(true, propertyName);
		if (value is string strValue)
		{
			// Assert.
			Assert.IsNotEmpty(strValue);
			Assert.IsNotNull(strValue);
		}
	}

	public T CreateNewSubstitute<T>(bool isNotDefault) where T : WsSqlTableBase, new()
	{
		SqlFieldIdentityModel fieldIdentity = Substitute.For<SqlFieldIdentityModel>(WsSqlFieldIdentity.Empty);
		fieldIdentity.Name.Returns(WsSqlFieldIdentity.Test);
		fieldIdentity.Uid.Returns(Guid.NewGuid());
		fieldIdentity.Id.Returns(-1);

		T item = Substitute.For<T>();
		if (!isNotDefault) return item;

		item.Identity.Returns(fieldIdentity);
		item.CreateDt.Returns(DateTime.Now);
		item.ChangeDt.Returns(DateTime.Now);
		item.IsMarked.Returns(false);
		item.Description.Returns(LocaleCore.Sql.SqlItemFieldDescription);

		switch (item)
		{
			case WsSqlAccessModel access:
				access.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				access.Rights.Returns((byte)AccessRightsEnum.None);
				access.LoginDt.Returns(DateTime.Now);
				break;
			case WsSqlAppModel app:
				app.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case WsSqlBarCodeModel barCode:
				barCode.TypeTop.Returns(WsSqlBarcodeType.Default.ToString());
				barCode.ValueTop.Returns(LocaleCore.Sql.SqlItemFieldValue);
				barCode.TypeRight.Returns(WsSqlBarcodeType.Default.ToString());
				barCode.ValueRight.Returns(LocaleCore.Sql.SqlItemFieldValue);
				barCode.TypeBottom.Returns(WsSqlBarcodeType.Default.ToString());
				barCode.ValueBottom.Returns(LocaleCore.Sql.SqlItemFieldValue);
				break;
            case WsSqlBoxModel box:
                box.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                box.Weight.Returns(3);
                break;
            case WsSqlBrandModel brand:
                brand.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                brand.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
				break;
			case WsSqlBundleModel bundle:
                bundle.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                bundle.Weight.Returns(3);
                break;
            case WsSqlClipModel clip:
                clip.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                clip.Weight.Returns(2);
                break;
            case WsSqlContragentModel contragent:
				contragent.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case WsSqlDeviceModel device:
                device.LoginDt.Returns(DateTime.Now);
				device.LogoutDt.Returns(DateTime.Now);
				device.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				device.PrettyName.Returns(LocaleCore.Sql.SqlItemFieldPrettyName);
				device.Ipv4.Returns(LocaleCore.Sql.SqlItemFieldIp);
				device.MacAddressValue.Returns(LocaleCore.Sql.SqlItemFieldMac);
				break;
			case WsSqlDeviceTypeModel deviceType:
                deviceType.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				deviceType.PrettyName.Returns(LocaleCore.Sql.SqlItemFieldPrettyName);
				break;
			case WsSqlDeviceTypeFkModel deviceTypeFk:
                deviceTypeFk.Device = CreateNewSubstitute<WsSqlDeviceModel>(isNotDefault);
				deviceTypeFk.Type = CreateNewSubstitute<WsSqlDeviceTypeModel>(isNotDefault);
				break;
			case WsSqlDeviceScaleFkModel deviceScaleFk:
                deviceScaleFk.Device = CreateNewSubstitute<WsSqlDeviceModel>(isNotDefault);
				deviceScaleFk.Scale = CreateNewSubstitute<WsSqlScaleModel>(isNotDefault);
				break;
			case WsSqlLogModel log:
				log.Version.Returns(LocaleCore.Sql.SqlItemFieldVersion);
				log.File.Returns(LocaleCore.Sql.SqlItemFieldFile);
				log.Line.Returns(1);
				log.Member.Returns(LocaleCore.Sql.SqlItemFieldMember);
                log.LogType = CreateNewSubstitute<WsSqlLogTypeModel>(isNotDefault);
                log.Message.Returns(LocaleCore.Sql.SqlItemFieldMessage);
				break;
			case WsSqlLogTypeModel logType:
				logType.Icon.Returns(LocaleCore.Sql.SqlItemFieldIcon);
				break;
            case WsSqlLogWebModel logWeb:
                logWeb.StampDt.Returns(DateTime.Now);
                logWeb.Version.Returns(LocaleCore.Sql.SqlItemFieldVersion);
                logWeb.Direction.Returns((byte)0);
                logWeb.Url.Returns(LocaleCore.Sql.SqlItemFieldUrl);
                logWeb.Params.Returns(string.Empty);
                logWeb.Headers.Returns(string.Empty);
                logWeb.DataString.Returns(string.Empty);
                logWeb.DataType.Returns((byte)0);
                logWeb.CountAll.Returns(2);
                logWeb.CountSuccess.Returns(1);
                logWeb.CountErrors.Returns(1);
                break;
			case WsSqlLogWebFkModel logWebFk:
                logWebFk.LogWebRequest = CreateNewSubstitute<WsSqlLogWebModel>(isNotDefault);
                logWebFk.LogWebResponse = CreateNewSubstitute<WsSqlLogWebModel>(isNotDefault);
                logWebFk.LogWebResponse.Direction.Returns((byte)1);
                logWebFk.App = CreateNewSubstitute<WsSqlAppModel>(isNotDefault);
                logWebFk.LogType = CreateNewSubstitute<WsSqlLogTypeModel>(isNotDefault);
                logWebFk.Device = CreateNewSubstitute<WsSqlDeviceModel>(isNotDefault);
				break;
			case WsSqlPluGroupModel pluGroup:
                pluGroup.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                pluGroup.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
                break;
            case WsSqlPluCharacteristicModel nomenclatureCharacteristic:
                nomenclatureCharacteristic.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                nomenclatureCharacteristic.AttachmentsCount.Returns(3);
                break;
            case WsSqlPluCharacteristicsFkModel nomenclatureCharacteristicFk:
                nomenclatureCharacteristicFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                nomenclatureCharacteristicFk.Characteristic = CreateNewSubstitute<WsSqlPluCharacteristicModel>(isNotDefault);
                break;
            case WsSqlPluFkModel pluFk:
                pluFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluFk.Parent = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                break;
            case WsSqlPluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = CreateNewSubstitute<WsSqlPluGroupModel>(isNotDefault);
                pluGroupFk.Parent = CreateNewSubstitute<WsSqlPluGroupModel>(isNotDefault);
                break;
			case WsSqlOrderModel order:
				order.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				order.BoxCount.Returns(1);
				order.PalletCount.Returns(1);
				break;
			case WsSqlOrderWeighingModel orderWeighing:
				orderWeighing.Order = CreateNewSubstitute<WsSqlOrderModel>(isNotDefault);
				orderWeighing.PluWeighing = CreateNewSubstitute<WsSqlPluWeighingModel>(isNotDefault);
				break;
			case WsSqlOrganizationModel organization:
				organization.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				organization.Gln.Returns(1);
				break;
            case WsSqlPluModel plu:
				plu.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				plu.Number.Returns((short)100);
				plu.FullName.Returns(LocaleCore.Sql.SqlItemFieldFullName);
				plu.Gtin.Returns(LocaleCore.Sql.SqlItemFieldGtin);
				plu.Ean13.Returns(LocaleCore.Sql.SqlItemFieldEan13);
				plu.Itf14.Returns(LocaleCore.Sql.SqlItemFieldItf14);
                plu.Code.Returns(LocaleCore.Sql.SqlItemFieldCode);
                break;
			case WsSqlPluBundleFkModel pluBundle:
                pluBundle.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluBundle.Bundle = CreateNewSubstitute<WsSqlBundleModel>(isNotDefault);
				break;
			case WsSqlPluClipFkModel pluClips:
                pluClips.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluClips.Clip = CreateNewSubstitute<WsSqlClipModel>(isNotDefault);
                break;
            case WsSqlPluLabelModel pluLabel:
				pluLabel.Zpl.Returns(LocaleCore.Sql.SqlItemFieldZpl);
				pluLabel.PluWeighing = CreateNewSubstitute<WsSqlPluWeighingModel>(isNotDefault);
				pluLabel.PluScale = CreateNewSubstitute<WsSqlPluScaleModel>(isNotDefault);
				pluLabel.ProductDt.Returns(DateTime.Now);
				pluLabel.ExpirationDt.Returns(DateTime.Now);
				break;
			case WsSqlPluScaleModel pluScale:
				pluScale.IsActive.Returns(true);
				pluScale.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
				pluScale.Scale = CreateNewSubstitute<WsSqlScaleModel>(isNotDefault);
				break;
            case WsSqlPluStorageMethodModel plusStorageMethod:
                plusStorageMethod.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                plusStorageMethod.MinTemp.Returns((short)0);
                plusStorageMethod.MaxTemp.Returns((short)0);
                break;
            case WsSqlPluStorageMethodFkModel pluStorageMethodFk:
                pluStorageMethodFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluStorageMethodFk.Method = CreateNewSubstitute<WsSqlPluStorageMethodModel>(isNotDefault);
                break;
			case WsSqlPluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = CreateNewSubstitute<WsSqlPluModel>(isNotDefault);
                pluTemplateFk.Template = CreateNewSubstitute<WsSqlTemplateModel>(isNotDefault);
				break;
			case WsSqlPluWeighingModel pluWeighing:
				pluWeighing.Sscc.Returns(LocaleCore.Sql.SqlItemFieldSscc);
				pluWeighing.NettoWeight.Returns(1.1M);
				pluWeighing.WeightTare.Returns(0.25M);
				pluWeighing.RegNum.Returns(1);
				pluWeighing.Kneading.Returns((short)1);
				pluWeighing.PluScale = CreateNewSubstitute<WsSqlPluScaleModel>(isNotDefault);
				pluWeighing.Series = CreateNewSubstitute<WsSqlProductSeriesModel>(isNotDefault);
				break;
            case WsSqlPluNestingFkModel pluNestingFk:
                pluNestingFk.IsDefault.Returns(false);
				pluNestingFk.PluBundle = CreateNewSubstitute<WsSqlPluBundleFkModel>(isNotDefault);
                pluNestingFk.Box = CreateNewSubstitute<WsSqlBoxModel>(isNotDefault);
                pluNestingFk.BundleCount.Returns((short)0);
                break;
            case WsSqlPrinterModel printer:
				printer.DarknessLevel.Returns((short)1);
				printer.PrinterType = CreateNewSubstitute<WsSqlPrinterTypeModel>(isNotDefault);
				break;
			case WsSqlPrinterResourceFkModel printerResource:
				printerResource.Printer = CreateNewSubstitute<WsSqlPrinterModel>(isNotDefault);
				printerResource.TemplateResource = CreateNewSubstitute<WsSqlTemplateResourceModel>(isNotDefault);
				break;
			case WsSqlPrinterTypeModel printerType:
				printerType.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case WsSqlProductionFacilityModel productionFacility:
				productionFacility.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				productionFacility.Address.Returns(LocaleCore.Sql.SqlItemFieldAddress);
				break;
			case WsSqlProductSeriesModel productSeries:
				productSeries.Sscc.Returns(LocaleCore.Sql.SqlItemFieldSscc);
				productSeries.IsClose.Returns(false);
				productSeries.Scale = CreateNewSubstitute<WsSqlScaleModel>(isNotDefault);
				break;
			case WsSqlScaleModel scale:
                scale.WorkShop = CreateNewSubstitute<WsSqlWorkShopModel>(isNotDefault);
				scale.PrinterMain = CreateNewSubstitute<WsSqlPrinterModel>(isNotDefault);
				scale.PrinterShipping = CreateNewSubstitute<WsSqlPrinterModel>(isNotDefault);
                scale.Number.Returns(10000);
                break;
			case WsSqlScaleScreenShotModel scaleScreenShot:
				scaleScreenShot.Scale = CreateNewSubstitute<WsSqlScaleModel>(isNotDefault);
				scaleScreenShot.ScreenShot.Returns(new byte[] { 0x00 });
				break;
			case WsSqlTaskModel task:
				task.TaskType = CreateNewSubstitute<WsSqlTaskTypeModel>(isNotDefault);
				task.Scale = CreateNewSubstitute<WsSqlScaleModel>(isNotDefault);
				break;
			case WsSqlTaskTypeModel taskType:
				taskType.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				break;
			case WsSqlTemplateModel template:
				template.Title.Returns(LocaleCore.Sql.SqlItemFieldTitle);
				break;
			case WsSqlTemplateResourceModel templateResource:
                templateResource.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
                templateResource.Type.Returns(LocaleCore.Sql.SqlItemFieldTemplateResourceType);
                templateResource.DataValue.Returns(new byte[] { 0x00 } );
                break;
			case WsSqlVersionModel version:
				version.Version.Returns((short)1);
				version.ReleaseDt.Returns(DateTime.Now);
				break;
			case WsSqlWorkShopModel workShop:
				workShop.Name.Returns(LocaleCore.Sql.SqlItemFieldName);
				workShop.ProductionFacility = CreateNewSubstitute<WsSqlProductionFacilityModel>(isNotDefault);
				break;
		}
		return item;
	}

	public void TableBaseModelAssertEqualsNew<T>() where T : WsSqlTableBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			WsSqlTableBase baseItem = new();
			// Act.
			bool itemEqualsNew = item.EqualsNew();
			bool baseEqualsNew = baseItem.EqualsNew();
			// Assert.
			Assert.AreEqual(baseEqualsNew, itemEqualsNew);
		});
	}

	public void FieldBaseModelAssertEqualsNew<T>() where T : SqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			SqlFieldBase baseItem = new();
			// Act.
			bool itemEqualsNew = item.EqualsNew();
			bool baseEqualsNew = baseItem.EqualsNew();
			// Assert.
			Assert.AreEqual(baseEqualsNew, itemEqualsNew);
		});
	}

	public void TableBaseModelAssertSerialize<T>() where T : WsSqlTableBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item1 = new();
			WsSqlTableBase base1 = new();
			// Act.
			string xml1 = WsDataFormatUtils.SerializeAsXmlString<T>(item1, true, true);
			string xml2 = WsDataFormatUtils.SerializeAsXmlString<WsSqlTableBase>(base1, true, true);
			// Assert.
			Assert.AreNotEqual(xml1, xml2);
			// Act.
			T item2 = WsDataFormatUtils.DeserializeFromXml<T>(xml1);
			TestContext.WriteLine($"{nameof(item2)}: {item2}");
			WsSqlTableBase base2 = WsDataFormatUtils.DeserializeFromXml<WsSqlTableBase>(xml2);
			TestContext.WriteLine($"{nameof(base2)}: {base2}");
			// Assert.
			Assert.AreNotEqual(item2, base2);
		});
	}

	public void TableBaseModelAssertToString<T>() where T : WsSqlTableBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			WsSqlTableBase baseItem = new();
			// Act.
			string itemString = item.ToString();
			string baseString = baseItem.ToString();
			TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
			TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
			// Assert.
			Assert.AreNotEqual(baseString, itemString);
		});
	}

	public void FieldBaseModelAssertToString<T>() where T : SqlFieldBase, new()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			T item = new();
			SqlFieldBase baseItem = new();
			// Act.
			string itemString = item.ToString();
			string baseString = baseItem.ToString();
			TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
			TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
			// Assert.
			Assert.AreNotEqual(baseString, itemString);
		});
	}

	#endregion
}
