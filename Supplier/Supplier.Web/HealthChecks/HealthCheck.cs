using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Supplier.Web;

public static class HealthCheck
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(configuration[ "ConnectionStrings:Feedback" ], healthQuery: "select 1" , name: "SQL servere" , failureStatus: HealthStatus.Unhealthy, tags: new [] { "Feedback" , "Database" })
            .AddCheck<RemoteHealthCheck>( "Verificação de integridade de endpoints remotos" , failureStatus: HealthStatus.Unhealthy)
            .AddCheck<MemoryHealthCheck>( $"Verificação de memória do serviço de feedback" , failureStatus: HealthStatus.Unhealthy, tags: new [] { "Serviço de feedback" })
            .AddUrlGroup( new Uri( "https://localhost:44333/api/v1/heartbeats/ping" ), name: "URL base" , failureStatus: HealthStatus.Unhealthy);

        //services.AddHealthChecksUI();
        services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds( 10 ); //tempo em segundos entre verificações    
                opt.MaximumHistoryEntriesPerEndpoint( 60 ); //histórico máximo de verificações    
                opt.SetApiMaxActiveRequests( 1 ); //simultaneidade de solicitações de API    
                opt.AddHealthCheckEndpoint( "feedback api" , "/api/health" ); //mapear api de verificação de integridade    

            })
            .AddInMemoryStorage();
    }
}