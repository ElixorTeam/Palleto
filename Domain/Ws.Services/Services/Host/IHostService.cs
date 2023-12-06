﻿using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.Services.Services.Host;

public interface IHostService
{
    SqlHostEntity GetCurrentHostOrCreate();
    SqlLineEntity GetLineByHost(SqlHostEntity host);
}