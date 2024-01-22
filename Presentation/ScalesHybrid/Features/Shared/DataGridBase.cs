using Microsoft.AspNetCore.Components;
using Ws.Domain.Models.Common;

namespace ScalesHybrid.Features.Shared;

public class DataGridBase<TItem> : ComponentBase where TItem : EntityBase, new()
{
    protected IEnumerable<TItem> GridData { get; set; } = [];

    protected override void OnInitialized()
    {
        GetGridData();
    }

    protected virtual void GetGridData()
    {
        throw new NotImplementedException();
    }

    protected virtual Task OnItemSelect(TItem obj)
    {
        throw new NotImplementedException();
    }
}