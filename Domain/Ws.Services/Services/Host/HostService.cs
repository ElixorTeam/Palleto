﻿using MDSoft.NetUtils;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.Services.Services.Host;

public class HostService : IHostService
{
    public SqlHostEntity GetCurrentHostOrCreate()
    {
        string pcName = MdNetUtils.GetLocalDeviceName(false);
        SqlHostEntity host = new SqlHostRepository().GetItemByName(pcName);
        
        if (host.IsNew) 
            host.Name = pcName;
        host.Ip = MdNetUtils.GetLocalIpAddress();
        host.LoginDt = DateTime.Now;
        
        return new SqlHostRepository().SaveOrUpdate(host);
    }
    
    public SqlLineEntity GetLineByHost(SqlHostEntity host)
    {
        return new SqlLineRepository().GetItemByHost(host);
    }
}