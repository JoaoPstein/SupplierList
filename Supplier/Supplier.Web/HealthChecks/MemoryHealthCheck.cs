using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Supplier.Web;

public class MemoryHealthCheck: IHealthCheck
{ 
    private  readonly IOptionsMonitor<MemoryCheckOptions> _options; 

    public  MemoryHealthCheck ( IOptionsMonitor<MemoryCheckOptions> options )
    { 
        _options = options; 
    } 

    public  string Name => "memory_check" ; 

    public Task<HealthCheckResult> CheckHealthAsync ( 
        HealthCheckContext context, 
        CancellationToken cancellationToken = default (CancellationToken ))
    { 
        var options = _options.Get(context.Registration.Name); 

        // Incluir informações de GC nos diagnósticos relatados. 
        var allocated = GC.GetTotalMemory(forceFullCollection: false ); 
        var data = new Dictionary< string , object >() 
        { 
            { "AllocatedBytes" , allocated }, 
            { "Gen0Collections" , GC.CollectionCount( 0 ) }, 
            { "Gen1Collections" , GC.CollectionCount( 1 ) }, 
            { "Gen2Collections" , GC.CollectionCount( 2 ) }, 
        }; 
        var status = (allocated < options.Threshold) ? HealthStatus.Healthy : HealthStatus.Unhealthy; 

        return Task.FromResult( new HealthCheckResult( 
            status, 
            description: "Relata status degradado se bytes alocados " + 
                         $">= {options.Threshold} bytes." , 
            exception: null , 
            data: data)); 
    } 
} 
public  class  MemoryCheckOptions
{ 
    public  string Memorystatus { get; definir ; } 
    //public int Threshold { obter; definir; } 
    // Limite de falha (em bytes) 
    public  long Threshold { obter ; definir ; } = 1024L * 1024L * 1024L ; 
} 
}