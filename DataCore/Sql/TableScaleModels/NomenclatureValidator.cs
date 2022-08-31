﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class NomenclatureValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public NomenclatureValidator()
	{
		RuleFor(item => ((NomenclatureModel)item).CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((NomenclatureModel)item).ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((NomenclatureModel)item).Name)
			.NotNull();
		RuleFor(item => ((NomenclatureModel)item).Xml)
			.NotEmpty()
			.NotNull();
	}
}
