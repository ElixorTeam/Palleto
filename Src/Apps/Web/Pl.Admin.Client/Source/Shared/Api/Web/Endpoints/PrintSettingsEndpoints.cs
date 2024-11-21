using Phetch.Core;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.References.Template.Queries;
using Pl.Admin.Models.Features.References.Template.Universal;
using Pl.Admin.Models.Features.References.TemplateResources.Queries;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class PrintSettingsEndpoints(IWebApi webApi)
{
    # region Template

    public ParameterlessEndpoint<TemplateDto[]> TemplatesEndpoint { get; } = new(
        webApi.GetTemplates,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, TemplateDto> TemplateEndpoint { get; } = new(
        webApi.GetTemplateByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddTemplate(TemplateDto template, string body)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? [template] : query.Data.Prepend(template).ToArray());
        TemplateEndpoint.UpdateQueryData(template.Id, _ => template);
        AddProxyTemplate(template.IsWeight, new(template.Id, template.Name));
        AddTemplateBody(template.Id, body);
    }

    public void UpdateTemplate(TemplateDto template, string body)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query => query.Data == null ? [template] :
            query.Data.ReplaceItemBy(template, p => p.Id == template.Id).ToArray());
        TemplateEndpoint.UpdateQueryData(template.Id, _ => template);
        UpdateProxyTemplate(template.IsWeight, new(template.Id, template.Name));
        UpdateTemplateBody(template.Id, body);
    }

    public void DeleteTemplate(bool isWeight, Guid templateId)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != templateId).ToArray());

        TemplateEndpoint.Invalidate(templateId);
        DeleteProxyTemplate(isWeight, templateId);
        DeleteTemplateBody(templateId);
    }

    # endregion

    # region Proxy Template

    public Endpoint<bool, ProxyDto[]> ProxyTemplatesEndpoint { get; } = new(
        webApi.GetProxyTemplatesByPluType,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyTemplate(bool isWeight, ProxyDto proxyTemplate) =>
        ProxyTemplatesEndpoint.UpdateQueryData(isWeight, query =>
            query.Data == null ? [proxyTemplate] : query.Data.Prepend(proxyTemplate).ToArray());

    public void UpdateProxyTemplate(bool isWeight, ProxyDto proxyTemplate) =>
        ProxyTemplatesEndpoint.UpdateQueryData(isWeight, query => query.Data == null ? [proxyTemplate] :
            query.Data.ReplaceItemBy(proxyTemplate, p => p.Id == proxyTemplate.Id).ToArray());

    public void DeleteProxyTemplate(bool isWeight, Guid proxyTemplateId) =>
        ProxyTemplatesEndpoint.UpdateQueryData(isWeight, query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != proxyTemplateId).ToArray());

    # endregion

    # region Template Body

    public Endpoint<Guid, string> TemplateBodyEndpoint { get; } = new(
        async value => (await webApi.GetTemplateBody(value)).Body,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddTemplateBody(Guid templateId, string body) =>
        TemplateBodyEndpoint.UpdateQueryData(templateId, _ => body);

    public void UpdateTemplateBody(Guid templateId, string body) =>
        TemplateBodyEndpoint.UpdateQueryData(templateId, _ => body);

    public void DeleteTemplateBody(Guid templateId) =>
        TemplateBodyEndpoint.Invalidate(templateId);

    # endregion

    # region Resource

    public ParameterlessEndpoint<TemplateResourceDto[]> ResourcesEndpoint { get; } = new(
        webApi.GetResources,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, TemplateResourceDto> ResourceEndpoint { get; } = new(
        webApi.GetResourceByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddResource(TemplateResourceDto resource, string body)
    {
        ResourcesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? [resource] : query.Data.Prepend(resource).ToArray());
        ResourceEndpoint.UpdateQueryData(new(), _ => resource);
        AddResourceBody(resource.Id, body);
    }

    public void UpdateResource(TemplateResourceDto resource, string body)
    {
        ResourcesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? [resource] : query.Data.ReplaceItemBy(resource, p => p.Id == resource.Id).ToArray());
        ResourceEndpoint.UpdateQueryData(new(), _ => resource);
        UpdateResourceBody(resource.Id, body);
    }

    public void DeleteResource(Guid resourceId)
    {
        ResourcesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != resourceId).ToArray());
        ResourceEndpoint.Invalidate(resourceId);
        DeleteResourceBody(resourceId);
    }

    # endregion

    # region Resource Body

    public Endpoint<Guid, string> ResourceBodyEndpoint { get; } = new(
        async value => (await webApi.GetTemplateResourceBody(value)).Body,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddResourceBody(Guid resourceId, string body) =>
        ResourceBodyEndpoint.UpdateQueryData(resourceId, _ => body);

    public void UpdateResourceBody(Guid resourceId, string body) =>
        ResourceBodyEndpoint.UpdateQueryData(resourceId, _ => body);

    public void DeleteResourceBody(Guid resourceId) =>
        ResourceBodyEndpoint.Invalidate(resourceId);

    # endregion

    # region Variables

    public Endpoint<Guid, BarcodeVarDto[]> VariablesEndpoint { get; } = new(
        webApi.GetBarcodeVariables,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    # endregion

    # region Barcodes

    public Endpoint<Guid, BarcodeItemWrapper> BarcodesEndpoint { get; set; } = new(
        webApi.GetBarcodes,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void UpdateBarcodes(Guid templateId, BarcodeItemWrapper barcodes) =>
        BarcodesEndpoint.UpdateQueryData(templateId, query => query.Data == null ? query.Data! : barcodes);

    # endregion
}