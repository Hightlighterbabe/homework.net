namespace homework5

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting


module Program =
    let CreateHostBuilder (_ : string array) =
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(fun webHostBuilder ->
                webHostBuilder
                    .UseStartup<Startup>()
                |> ignore)
        
    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        0 // Exit code