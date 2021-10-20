namespace homework5

open Giraffe
open FSLibrary

module CalculatorHttpHandler=
    
    [<CLIMutable>]    
    type Values =
        {
            Operation: string
            Num1: string
            Num2: string
        }
    let someHttpHandler:HttpHandler =
        fun next ctx ->
            let values = ctx.TryBindQueryString<Values>()
            match values with
            |Ok v ->
                let operations =
                    match v.Operation with
                    | "plus" -> "+"
                    | "minus" -> "-"
                    | "divide" -> "/"
                    | "multiply" -> "*"
                    | _ -> "vi idete naxyi" 
                let number1 = Parser.ParseDecimalNumber(v.Num1)
                let number2 = Parser.ParseDecimalNumber(v.Num2)
                let operations = Parser.ParseOperation operations
                match operations with
                |Ok res ->
                    let result = Calculator.Calculate number1 number2 res
                    match result with
                    |Ok result ->
                        (setStatusCode 200 >=> json result) next ctx
                    |Error result ->
                        (setStatusCode 450 >=> json result) next ctx
                |Error res ->
                    (setStatusCode 450 >=> json res) next ctx
            |Error v ->
                (setStatusCode 400 >=> json v) next ctx
