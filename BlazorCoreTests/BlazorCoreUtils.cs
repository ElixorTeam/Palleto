﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models.CssStyles;
using FluentValidation;
using System;
using DataCoreTests;

namespace BlazorCoreTests;

public static class BlazorCoreUtils
{
	#region Public and private methods

	private static IValidator<TEntity> GetStyleValidator<TEntity>() 
		where TEntity : IBaseStyleModel, new()
	{
		if (typeof(TEntity) == typeof(TheadStyleModel))
		{
			return new TheadStyleValidator();
		}
		throw new NotImplementedException();
	}

	public static void AssertStyleValidate<TEntity>(TEntity item, bool assertResult) 
		where TEntity : IBaseStyleModel, new()
	{
		// Arrange.
		IValidator<TEntity> validator = GetStyleValidator<TEntity>();
		// Act & Assert.
		DataCoreUtils.AssertValidate(item, validator, assertResult);
	}

	#endregion
}
