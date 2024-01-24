﻿using Ws.Database.Core.Entities.Print.ViewLabels;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Label;

internal class LabelService : ILabelService
{
    public LabelEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<LabelEntity>(uid);

    public IEnumerable<ViewLabel> GetAll() => new ViewLabelRepository().GetList(new());

    public ViewLabel GetViewByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<ViewLabel>(uid);
}