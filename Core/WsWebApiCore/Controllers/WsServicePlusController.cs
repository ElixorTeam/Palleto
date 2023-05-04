// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Controllers;

/// <summary>
/// Веб-контроллер номенклатур.
/// </summary>
public sealed class WsServicePlusController : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    public WsServicePlusController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private bool UpdateBoxDb(WsResponse1CShortModel response, PluModel pluXml, BoxModel? boxDb, bool isCounter)
    {
        if (boxDb is null || boxDb.IsNew) return false;
        boxDb.UpdateProperties(pluXml);
        WsSqlCrudResultModel dbUpdate = AccessManager.AccessItem.Update(boxDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1C));
                response.SuccessesPlus?.Add(new(pluXml.Uid1C, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else if (dbUpdate.Exception is not null)
            AddResponseException(response, pluXml.Uid1C, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    private bool UpdateBundleDb(WsResponse1CShortModel response, PluModel pluXml, BundleModel? bundleDb, bool isCounter)
    {
        if (bundleDb is null || bundleDb.IsNew) return false;
        bundleDb.UpdateProperties(pluXml);
        WsSqlCrudResultModel dbUpdate = AccessManager.AccessItem.Update(bundleDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1C));
                response.SuccessesPlus?.Add(new(pluXml.Uid1C, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else if (dbUpdate.Exception is not null)
            AddResponseException(response, pluXml.Uid1C, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    private bool UpdateClipDb(WsResponse1CShortModel response, PluModel pluXml, ClipModel? clipDb, bool isCounter)
    {
        if (clipDb is null || clipDb.IsNew) return false;
        clipDb.UpdateProperties(pluXml);
        WsSqlCrudResultModel dbUpdate = AccessManager.AccessItem.Update(clipDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
            {
                response.Successes.Add(new(pluXml.Uid1C));
                response.SuccessesPlus?.Add(new(pluXml.Uid1C, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
            }
        }
        else if (dbUpdate.Exception is not null)
            AddResponseException(response, pluXml.Uid1C, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    //private bool UpdatePluDb(WsResponse1CShortModel response, PluModel pluXml, PluModel? pluDb, bool isCounter)
    //{
    //    if (pluDb is null || pluDb.IsNew) return false;
    //    pluDb.Identity = pluXml.Identity;
    //    pluDb.UpdateProperties(pluXml);
    //    // Native update -> Be careful, good luck.
    //    SqlCrudResultModel dbUpdate = AccessManager.AccessItem.ExecQueryNative(
    //        WsWebSqlQueries.UpdatePlu, new List<SqlParameter>
    //        {
    //            new("uid", pluXml.IdentityValueUid),
    //            new("code", pluDb.Code),
    //            new("number", pluDb.Number),
    //        });
    //    if (dbUpdate.IsOk)
    //    {
    //        if (isCounter)
    //        {
    //            response.Successes.Add(new(pluXml.Uid1C));
    //            response.SuccessesPlus?.Add(new(pluXml.Uid1C, $"{WsWebConstants.PluNumber}='{pluXml.Number}'"));
    //        }
    //    }
    //    else if (dbUpdate.Exception is not null)
    //        AddResponseException(response, pluXml.IdentityValueUid, dbUpdate.Exception);
    //    return dbUpdate.IsOk;
    //}

    #endregion

    #region Public and private methods

    /// <summary>
    /// Заполнить список ПЛУ из XML.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    private List<WsXmlContentRecord<PluModel>> GetXmlPluList(XElement xml) =>
        WsServiceContentUtils.GetNodesListCore<PluModel>(xml, LocaleCore.WebService.XmlItemNomenclature, (xmlNode, itemXml) =>
        {
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ParentGroupGuid");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.FullName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.CategoryGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BrandGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.MeasurementType));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.GroupGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.AttachmentsCount));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.BoxTypeWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.PackageTypeWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeGuid));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeName));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ClipTypeWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "PluNumber");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Description));
            //SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsCheckWeight));
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "ShelfLife");
            WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Gtin));
        });

    /// <summary>
    /// Добавить ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePlu(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            // Проверка на пустой Uid1C.
            if (Equals(pluXml.IdentityValueUid, Guid.Empty))
            {
                AddResponseExceptionString(response, pluXml.Uid1C,
                    $"{LocaleCore.WebService.IsEmpty} {LocaleCore.WebService.FieldGuid}!");
                return;
            }

            // Поиск по Uid1C -> Обновить найденную запись.
            //PluModel? pluDb = plusDb.Find(item => Equals(item.Uid1c, pluXml.IdentityValueUid));
            //if (UpdateItem1cDb(response, pluXml, pluDb, true, pluXml.Number.ToString())) return;

            // Найдено по Code -> Обновить найденную запись.
            //pluDb = plusDb.Find(item => Equals(item.Code, pluXml.Code));
            //if (UpdateItem1cDb(response, pluXml, pluDb, true, pluXml.Number.ToString())) return;

            // Найдено по Number -> Обновить найденную запись.
            PluModel pluDb = Cache.PlusDb.Find(item => 
                Equals(item.Number, pluXml.Number) && Equals(item.Uid1C, pluXml.Uid1C)) ?? ContextManager.ContextPlu.GetNewItem();
            if (UpdateItemDb(response, pluXml, pluDb, true, pluXml.Number.ToString())) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluXml, true))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.Plus);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить связь ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluFks(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ParentGuid, Guid.Empty)) return;
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Number, pluXml.Uid1C, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return;
            PluModel pluParentDb = ContextManager.ContextPlu.GetItemByUid1c(pluXml.ParentGuid);
            if (!CheckExistsPluDb(response, pluParentDb.Number, pluXml.Uid1C, LocaleCore.WebService.FieldGroup, true, out PluModel? parentDb)) return;
            PluModel pluCategorytDb = ContextManager.ContextPlu.GetItemByUid1c(pluXml.CategoryGuid);
            if (!CheckExistsPluDb(response, pluCategorytDb.Number, pluXml.Uid1C, LocaleCore.WebService.FieldGroup1Level, true, out PluModel? categoryDb)) return;
            if (pluDb is null || parentDb is null) return;

            PluFkModel pluFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Parent = parentDb,
                Category = categoryDb
            };

            // Найдено по Identity -> Обновить найденную запись.
            PluFkModel? pluFkDb = Cache.PluFksDb.Find(item =>
                Equals(item.Plu.Uid1C, pluFk.Plu.Uid1C) &&
                Equals(item.Parent.Uid1C, pluFk.Parent.Uid1C) &&
                Equals(item.Category?.Uid1C, pluFk.Category?.Uid1C));
            if (UpdatePluFkDb(response, pluXml.Uid1C, pluFk, pluFkDb, false)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluFk, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить коробку.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluBox(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            // Проверка на пустой Uid1C.
            if (Equals(pluXml.BoxTypeGuid, Guid.Empty))
            {
                // BoxTypeGuid="00000000-0000-0000-0000-000000000000" BoxTypeName!="" BoxTypeWeight!="".
                if (pluXml.PackageTypeWeight > 0)
                {
                    AddResponseExceptionString(response, pluXml.Uid1C,
                        $"{LocaleCore.WebService.IsEmpty} {nameof(pluXml.BoxTypeGuid)}!");
                    return;
                }
                // BoxTypeGuid="00000000-0000-0000-0000-000000000000" BoxTypeName="" BoxTypeWeight="".
                pluXml.BoxTypeName = LocaleCore.WebService.BoxZero;
            }

            // Найдено по Uid1C -> Обновить найденную запись.
            BoxModel? boxDb = Cache.BoxesDb.Find(item => Equals(item.Uid1C, pluXml.BoxTypeGuid));
            if (UpdateBoxDb(response, pluXml, boxDb, false)) return;

            // Найдено по Name -> Обновить найденную запись.
            boxDb = Cache.BoxesDb.Find(item => Equals(item.Name, pluXml.BoxTypeName));
            if (UpdateBoxDb(response, pluXml, boxDb, false)) return;

            // Не найдено -> Добавить новую запись.
            boxDb = new();
            boxDb.UpdateProperties(pluXml);
            if (SaveItemDb(response, boxDb, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.Boxes);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить пакет.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluBundle(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            // Проверка на пустой Uid1C.
            if (Equals(pluXml.PackageTypeGuid, Guid.Empty))
            {
                // PackageTypeGuid="00000000-0000-0000-0000-000000000000" PackageTypeName!="" PackageTypeWeight!="".
                if (pluXml.PackageTypeWeight > 0)
                {
                    AddResponseExceptionString(response, pluXml.Uid1C,
                        $"{LocaleCore.WebService.IsEmpty} {nameof(pluXml.PackageTypeGuid)}!");
                    return;
                }
                // PackageTypeGuid="00000000-0000-0000-0000-000000000000" PackageTypeName="" PackageTypeWeight="".
                pluXml.PackageTypeName = LocaleCore.WebService.PackageZero;
            }

            // Найдено по Uid1C -> Обновить найденную запись.
            BundleModel? bundleDb = Cache.BundlesDb.Find(item => Equals(item.Uid1C, pluXml.PackageTypeGuid));
            if (UpdateBundleDb(response, pluXml, bundleDb, false)) return;

            // Найдено по Name -> Обновить найденную запись.
            bundleDb = Cache.BundlesDb.Find(item => Equals(item.Name, pluXml.PackageTypeName));
            if (UpdateBundleDb(response, pluXml, bundleDb, false)) return;

            // Не найдено -> Добавить новую запись.
            bundleDb = new();
            bundleDb.UpdateProperties(pluXml);
            if (SaveItemDb(response, bundleDb, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.Bundles);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить связь бренда.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluBrandFk(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.BrandGuid, Guid.Empty)) return;
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Number, pluXml.Uid1C, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return;
            // Проверить наличие бренда в БД.
            if (!CheckExistsBrandDb(response, pluXml.BrandGuid, pluXml.Uid1C, LocaleCore.WebService.FieldBrand, out BrandModel? brandDb)) return;
            if (pluDb is null || brandDb is null) return;

            PluBrandFkModel pluBrandFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Brand = brandDb
            };

            // Найдено по Identity -> Update exists | UQ_PLUS_CLIP_PLU_FK.
            PluBrandFkModel? pluBrandFkDb = Cache.PluBrandsFksDb.Find(item => Equals(item.Plu.Uid1C, pluBrandFk.Plu.Uid1C));
            if (UpdatePluBrandFkDb(response, pluXml.Uid1C, pluBrandFk, pluBrandFkDb, false)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluBrandFk, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluBrandsFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить клипсу.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluClip(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            // Проверка на пустой Uid1C.
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty))
            {
                // ClipTypeGuid="00000000-0000-0000-0000-000000000000" ClipTypeName!="" ClipTypeWeight!="".
                if (pluXml.ClipTypeWeight > 0)
                {
                    AddResponseExceptionString(response, pluXml.Uid1C,
                        $"{LocaleCore.WebService.IsEmpty} {nameof(pluXml.ClipTypeGuid)}!");
                    return;
                }
                // ClipTypeGuid="00000000-0000-0000-0000-000000000000" ClipTypeName="" ClipTypeWeight="".
                pluXml.ClipTypeName = LocaleCore.WebService.ClipZero;
            }

            // Найдено по Uid1C -> Обновить найденную запись.
            ClipModel? clipDb = Cache.ClipsDb.Find(item => Equals(item.Uid1C, pluXml.ClipTypeGuid));
            if (UpdateClipDb(response, pluXml, clipDb, false)) return;

            // Найдено по Name -> Обновить найденную запись.
            clipDb = Cache.ClipsDb.Find(item => Equals(item.Name, pluXml.ClipTypeName));
            if (UpdateClipDb(response, pluXml, clipDb, false)) return;

            // Не найдено -> Добавить новую запись.
            clipDb = new();
            clipDb.UpdateProperties(pluXml);
            if (SaveItemDb(response, clipDb, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.Clips);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить связь клипсы ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluClipFk(WsResponse1CShortModel response, PluModel pluXml)
    {
        try
        {
            if (Equals(pluXml.ClipTypeGuid, Guid.Empty)) return;
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Number, pluXml.Uid1C, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return;
            // Проверить наличие клипсы в БД.
            if (!CheckExistsClipDb(response, pluXml.ClipTypeGuid, pluXml.Uid1C, LocaleCore.WebService.FieldClip, out ClipModel? clipDb)) return;
            if (pluDb is null || clipDb is null) return;

            PluClipFkModel pluClipFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Clip = clipDb
            };

            // Найдено по Identity -> Update exists | UQ_PLUS_CLIP_PLU_FK.
            PluClipFkModel? pluClipFkDb = Cache.PluClipsFksDb.Find(item => Equals(item.Plu.Uid1C, pluClipFk.Plu.Uid1C));
            if (UpdatePluClipFkDb(response, pluXml.Uid1C, pluClipFk, pluClipFkDb, false)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluClipFk, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluClipsFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    /// <summary>
    /// Добавить связь пакета ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    /// <returns></returns>
    private PluBundleFkModel AddResponsePluBundleFk(WsResponse1CShortModel response, PluModel pluXml)
    {
        PluBundleFkModel pluBundleFk = new();
        try
        {
            // Проверить наличие ПЛУ в БД.
            if (!CheckExistsPluDb(response, pluXml.Number, pluXml.Uid1C, LocaleCore.WebService.FieldNomenclature, false, out PluModel? pluDb)) return pluBundleFk;
            // Проверить наличие пакета в БД.
            if (!CheckExistsBundleDb(response, pluXml.PackageTypeGuid, pluXml.Uid1C, LocaleCore.WebService.FieldBundle, out BundleModel? bundleDb)) return pluBundleFk;
            if (pluDb is null || bundleDb is null) return pluBundleFk;

            pluBundleFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Bundle = bundleDb,
            };

            // Найдено по Identity -> Update exists | UQ_BUNDLES_FK.
            PluBundleFkModel? pluBundleFkDb = Cache.PluBundlesFksDb.Find(item => Equals(item.Plu.Uid1C, pluBundleFk.Plu.Uid1C));
            if (pluBundleFkDb is not null)
                if (UpdatePluBundleFkDb(response, pluXml.Uid1C, pluBundleFk, pluBundleFkDb, false)) return pluBundleFkDb;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluBundleFk, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluBundlesFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
        return pluBundleFk;
    }

    /// <summary>
    /// Добавить связь вложенности ПЛУ.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluBundleFk"></param>
    /// <param name="pluXml"></param>
    private void AddResponsePluNestingFk(WsResponse1CShortModel response, PluBundleFkModel pluBundleFk, PluModel pluXml)
    {
        try
        {
            if (pluBundleFk.IsNotExists)
            {
                if (Cache.PluBundlesFksDb.Any())
                {
                    pluBundleFk = Cache.PluBundlesFksDb.Find(
                        item => Equals(item.Plu.Number, pluXml.Number) && 
                        Equals(item.Plu.Uid1C, pluXml.Uid1C)) ?? new();
                }
            }
            if (pluBundleFk.IsNotExists) return;

            if (!GetBoxDb(response, pluXml.BoxTypeGuid, pluXml.Uid1C, "Box", out BoxModel? boxDb)) return;
            if (boxDb is null) return;

            PluNestingFkModel pluNestingFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                PluBundle = pluBundleFk,
                Box = boxDb,
                BundleCount = pluXml.AttachmentsCount,
                IsDefault = true,
            };

            // Найдено по Identity -> Update exists | UQ_PLUS_NESTING_FK.
            PluNestingFkModel? pluNestingFkDb = Cache.PluNestingFksDb.FirstOrDefault(item =>
                Equals(item.Box.Uid1C, pluNestingFk.Box.Uid1C) &&
                Equals(item.PluBundle.Plu.Uid1C, pluNestingFk.PluBundle.Plu.Uid1C) &&
                Equals(item.PluBundle.Bundle.Uid1C, pluNestingFk.PluBundle.Bundle.Uid1C) &&
                Equals(item.BundleCount, pluXml.AttachmentsCount));
            if (UpdatePluNestingFk(response, pluXml.Uid1C, pluNestingFk, pluNestingFkDb, false)) return;

            // Не найдено -> Добавить новую запись.
            if (SaveItemDb(response, pluNestingFk, false, pluXml.Uid1C))
                // Обновить список БД.
                Cache.Load(WsSqlTableName.PluNestingFks);
        }
        catch (Exception ex)
        {
            AddResponseException(response, pluXml.Uid1C, ex);
        }
    }

    private string[] GetPluPropertiesArray() => new[]
    {
        nameof(PluModel.BoxTypeGuid),
        nameof(PluModel.BoxTypeName),
        nameof(PluModel.BoxTypeWeight),
        nameof(PluModel.BrandGuid),
        nameof(PluModel.CategoryGuid),
        nameof(PluModel.ClipTypeGuid),
        nameof(PluModel.ClipTypeName),
        nameof(PluModel.ClipTypeWeight),
        nameof(PluModel.Code),
        nameof(PluModel.Description),
        nameof(PluModel.FullName),
        nameof(PluModel.GroupGuid),
        nameof(PluModel.IdentityValueUid),
        nameof(PluModel.IsCheckWeight),
        nameof(PluModel.IsGroup),
        nameof(PluModel.IsMarked),
        nameof(PluModel.MeasurementType),
        nameof(PluModel.Name),
        nameof(PluModel.Number),
        nameof(PluModel.PackageTypeGuid),
        nameof(PluModel.PackageTypeName),
        nameof(PluModel.PackageTypeWeight),
        nameof(PluModel.ParentGuid),
        nameof(PluModel.ShelfLifeDays),
    };

    /// <summary>
    /// Загрузить номенклатуру и получить ответ.
    /// </summary>
    /// <param name="xml"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponsePlus(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CShortModel>(response =>
        {
            // Прогреть кэш.
            Cache.Load();
            List<WsXmlContentRecord<PluModel>> plusXml = GetXmlPluList(xml);
            PluValidator pluValidator = new(false, false);
            // Цикл по всем XML-номенклатурам.
            foreach (WsXmlContentRecord<PluModel> record in plusXml)
            {
                PluModel itemXml = record.Item;
                // Обновить данные в таблице связей обмена номенклатуры 1С.
                List<WsSqlPlu1CFkModel> plus1CFksDb = UpdatePlus1CFksDb(response, record);
                // Проверить ПЛУ на группу с нулевым номером.
                if (itemXml.ParseResult.IsStatusSuccess)
                    IsCorrectPluNumberForNonGroup(itemXml, true);
                // Проверить номер ПЛУ в списке доступа к выгрузке.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckIsEnabledPlu(itemXml, plus1CFksDb);
                // Проверить валидацию ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckPluValidation(itemXml, pluValidator);
                // Проверить дубликат номера ПЛУ для не групп.
                if (itemXml.ParseResult.IsStatusSuccess)
                    CheckPluDublicateForNonGroup(response, itemXml);
                // Добавить ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePlu(response, itemXml);
                // Добавить связь ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluFks(response, itemXml);
                // Добавить коробку.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluBox(response, itemXml);
                // Добавить пакет.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluBundle(response, itemXml);
                // Добавить связь бренда.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluBrandFk(response, itemXml);
                // Добавить клипсу.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluClip(response, itemXml);
                // Добавить связь клипсы ПЛУ.
                if (itemXml.ParseResult.IsStatusSuccess)
                    AddResponsePluClipFk(response, itemXml);
                // Успешно.
                if (itemXml.ParseResult.IsStatusSuccess)
                {
                    // Добавить связь пакета ПЛУ.
                    PluBundleFkModel pluBundleFk = AddResponsePluBundleFk(response, itemXml);
                    // Добавить связь вложенности ПЛУ.
                    if (itemXml.ParseResult.IsStatusSuccess)
                        AddResponsePluNestingFk(response, pluBundleFk, itemXml);
                }
                // Исключение.
                if (itemXml.ParseResult.IsStatusError)
                    AddResponseExceptionString(response, itemXml.Uid1C,
                        itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    /// <summary>
    /// Проверить валидацию ПЛУ.
    /// </summary>
    /// <param name="itemXml"></param>
    /// <param name="pluValidator"></param>
    private void CheckPluValidation(PluModel itemXml, PluValidator pluValidator)
    {
        ValidationResult validation = pluValidator.Validate(itemXml);
        if (!validation.IsValid)
        {
            string[] pluProperties = GetPluPropertiesArray();
            foreach (ValidationFailure error in validation.Errors)
            {
                if (pluProperties.Contains(error.PropertyName) &&
                    !itemXml.ParseResult.Exception.Contains(error.PropertyName))
                    WsServiceContentUtils.SetItemParseResultException(itemXml, error.PropertyName);
            }
        }
    }

    /// <summary>
    /// Проверить дубликат номера ПЛУ для не групп.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="pluXml"></param>
    private void CheckPluDublicateForNonGroup(WsResponse1CShortModel response, PluModel pluXml)
    {
        // Пропуск групп с нулевым номером.
        if (Equals(pluXml.Number, (short)0)) return;
        List<PluModel> plusNumberDb = Cache.PlusDb.FindAll(x => Equals(x.Number, pluXml.Number));
        if (plusNumberDb.Count > 1)
        {
            AddResponseExceptionString(response, pluXml.Uid1C,
                $"{LocaleCore.WebService.Dublicate} {LocaleCore.WebService.FieldPluNumber} '{pluXml.Number}' " +
                $"{LocaleCore.WebService.WithFieldCode} '{pluXml.Code}' {LocaleCore.WebService.ForDbRecord} " +
                $"{LocaleCore.WebService.WithFieldCode} '{string.Join(',', plusNumberDb.Select(x => x.Code).ToList())}'");
            pluXml.ParseResult.Status = ParseStatus.Error;
        }
    }

    #endregion
}