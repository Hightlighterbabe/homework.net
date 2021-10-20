namespace homework5

open FSLibrary
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Giraffe
type Startup() =

    let webApp=
        choose [ route "/ping" >=> text "pong"
                 route "/calc" >=> CalculatorHttpHandler.someHttpHandler ]
    member _.ConfigureServices(services: IServiceCollection) =
        services.AddGiraffe() |> ignore 

    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        app.UseGiraffe webApp
        app.Use |> ignore

        app.UseRouting()
           .UseEndpoints(fun endpoints ->
                endpoints.MapGet("/", fun context ->
                    context.Response.WriteAsync("Hello World!")) |> ignore
            ) |> ignore