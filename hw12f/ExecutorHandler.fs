module hw12f.ExecutorHandler
open Giraffe.Core
open Giraffe

[<CLIMutable>]
type Values=
    {
        V1 : string
        V2 : string
        Op : string
    }

let ExecutorHttpHandler : HttpHandler =
    fun next ctx ->
        let values = ctx.TryBindQueryString<Values>()
        match values with
        | Ok v ->
            let res = Ok(0)//CalculatorAdapter.calculate v.V1 v.V2 v.Op
            match res with
            | Ok res -> (setStatusCode 200 >=> json res) next ctx
            | Error err -> (setStatusCode 450 >=> json err) next ctx
        | Error err ->
            (setStatusCode 400 >=> json err) next ctx
