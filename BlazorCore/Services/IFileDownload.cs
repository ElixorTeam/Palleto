﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore.Services;

public interface IFileDownload
{
    Task DownloadAsync(IBlazorDownloadFileService? blazorDownloadFileService, TemplateResourceEntity? item);
}