﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class BarCodeV2ValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			BarCodeV2Entity item = Substitute.For<BarCodeV2Entity>();
			BarCodeV2Validator validator = new();
			// Act.
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
			// Act.
			item.Value = "";
			result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
		});
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			BarCodeV2Entity item = Substitute.For<BarCodeV2Entity>();
			BarCodeV2Validator validator = new();
			// Act.
			item.Value = "Test";
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		TestsUtils.DbTableAction(() =>
		{
			// Arrange.
			BarCodeV2Validator validator = new();
			BarCodeV2Entity[]? items = TestsUtils.DataAccess.Crud.GetEntities<BarCodeV2Entity>();
			// Act.
			if (items == null || !items.Any())
			{
				TestContext.WriteLine($"{nameof(items)} is null or empty!");
			}
			else
			{
				TestContext.WriteLine($"Found {nameof(items)}.Count: {items.Count()}");
				int i = 0;
				foreach (BarCodeV2Entity item in items)
				{
					if (i < 10)
						TestContext.WriteLine(item);
					i++;
					TestContext.WriteLine(item);
					ValidationResult result = validator.Validate(item);
					TestsUtils.FailureWriteLine(result);
					// Assert.
					Assert.IsTrue(result.IsValid);
				}
			}
		});
	}
}
