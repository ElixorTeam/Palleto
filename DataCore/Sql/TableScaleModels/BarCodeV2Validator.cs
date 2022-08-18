﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "BARCODES_V2".
/// </summary>
public class BarCodeV2Validator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public BarCodeV2Validator()
	{
		RuleFor(item => item.CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.IdentityUid)
			.NotEmpty()
			.NotNull()
			.NotEqual(Guid.Empty);
		RuleFor(item => ((BarCodeV2Entity)item).Value)
			.NotEmpty()
			.NotNull();
	}
}