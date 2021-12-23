namespace hw12f

open View
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open ExecutorHandler

module private StartupUtil =
    let indexHandler (name: string) =
        let greetings = $"Hello {name}, Giraffe :)!"
        let model = { Text = greetings }
        let view = View.index model
        htmlView view

    let webApp =
        choose [ GET
                 >=> choose [ route "/bod" >=> text "abobus"
                              route "/calc" >=> ExecutorHttpHandler ] ]

open StartupUtil

type Startup() =
    member _.ConfigureServices(services: IServiceCollection) =
        // Register default Giraffe dependencies
        services.AddGiraffe() |> ignore

    member _.Configure (app: IApplicationBuilder) (env: IHostEnvironment) (loggerFactory: ILoggerFactory) =
        // Add Giraffe to the ASP.NET Core pipeline
        app.UseStaticFiles().UseGiraffe(webApp)
