// 1. .NET
global using System.Linq.Expressions;
global using System.Security.Claims;

// 2. Microsoft
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

// 3. External
global using FluentValidation.Results;

// 4. Internal
global using Pl.Admin.Api.App.Common;
global using Pl.Admin.Api.App.Shared;
global using Pl.Admin.Api.App.Shared.Helpers;
global using Pl.Admin.Api.App.Shared.Expressions;
global using Pl.Admin.Api.App.Shared.Extensions;
global using Pl.Admin.Api.App.Shared.Utils;

// 5. Modules
global using Pl.Admin.Models.Auth;
global using Pl.Admin.Models.Shared;
global using Pl.Database;
global using Pl.Shared.Enums;