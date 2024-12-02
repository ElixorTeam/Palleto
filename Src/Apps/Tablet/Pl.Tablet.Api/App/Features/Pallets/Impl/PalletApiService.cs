using Pl.Tablet.Api.App.Features.Pallets.Common;
using Pl.Tablet.Models.Features.Pallets.Input;
using Pl.Tablet.Models.Features.Pallets.Output;

namespace Pl.Tablet.Api.App.Features.Pallets.Impl;

internal sealed class PalletApiService : IPalletService
{
    #region Commands

    public PalletDto Create(PalletCreateDto palletCreateDto)
    {
        List<BatchDto> batches = palletCreateDto.Batches.ConvertAll(i => new BatchDto
            {
                PluName = "Сосиска",
                Date = i.Date,
                Weight = i.Weight,
                Number = 1111
            })
            .ToList();

        batches.Add(new ()
        {
            PluName = "Сарделька",
            Date = DateTime.Today,
            Weight = 145,
            Number = 2222
        });

        return new()
        {
            DocumentBarcode = palletCreateDto.DocumentBarcode,
            PalletBarcode = "123456789",
            ZplLabel = "^XA ^CI28 ^LH0,0 ^FWB\n^FX 945 dots x 1182 dots\n\n^FX Основной шк - GS-1 Expanded Stack\n^FT800,1070 ^BRB,6,9,1,100,4\n^FD010800439500018017230215^FS\n\n^PQ4,0,1,Y\n\n^XZ",
            Batches = batches,
            User = new("Семенов", "Павел", "Анатольевич"),
            WarehouseName = "Склад № 1",
            CreateDt = DateTime.Now,
            Number = "21233131"
        };
    }

    #endregion
}