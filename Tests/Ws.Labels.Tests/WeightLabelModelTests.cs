﻿using Ws.Labels.Service.Features.PrintLabel.Models;

namespace Ws.Labels.Tests;

public class WeightLabelModelTests
{
    [Fact]
    public void Check_WeightLabelModel_v1_Barcodes()
    {
        WeightLabelModel model = new()
        {
            Weight = 16.696m,
            Kneading = 16,
            LineCounter = 288095,
            LineNumber = 10430,
            ProductDtValue = new(2023, 12, 5, 15, 19, 49),
            PluGtin = "02600770000002",
            PluNumber = 333 ,
        };
        Assert.Equal("2991043000288095",model.BarCodeRight);
        Assert.Equal("298104300028809523120515194933316696016",model.BarCodeTop);
        Assert.Equal("(01)02600770000002(3103)016696(11)231205(10)2312",model.BarCodeBottom);
    }
    
    [Fact]
    public void Check_WeightLabelModel_v2_Barcodes()
    {
        WeightLabelModel model = new()
        {
            Weight = 2.360m,
            Kneading = 1,
            LineCounter = 200,
            LineNumber = 12312,
            ProductDtValue = new(2023, 12, 12, 16, 17, 38),
            PluGtin = "02600914000004",
            PluNumber = 101 ,
        };
        Assert.Equal("2991231200000200",model.BarCodeRight);
        Assert.Equal("298123120000020023121216173810102360001",model.BarCodeTop);
        Assert.Equal("(01)02600914000004(3103)002360(11)231212(10)2312",model.BarCodeBottom);
    }
}