﻿using Microsoft.Extensions.DependencyInjection;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Labels.Service.Features.RenderLabel;

namespace Ws.Labels.Service;

public static class DependencyInjection
{
    public static void AddLabelsServices(this IServiceCollection services)
    {
        services.AddScoped<IPrintLabelService, PrintLabelService>();
        services.AddScoped<IRenderLabelService, RenderLabelService>();
    }
}