﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterTypeValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterTypeValidator() : base(ColumnName.Id, false, false)
	{
		RuleFor(item => ((PrinterTypeEntity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
