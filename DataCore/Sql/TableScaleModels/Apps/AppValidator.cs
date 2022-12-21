﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Apps;

/// <summary>
/// Table validation "APPS".
/// </summary>
public class AppValidator : SqlTableValidator<AppModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AppValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}