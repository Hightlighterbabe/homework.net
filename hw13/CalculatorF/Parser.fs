namespace CalculatorF

module Parser =

open CalculatorF
open MaybeBuilder

    let CheckArgsLenght (args:string[]) =
        if args.Length <> 3 then    
            None
        else
            Some args
            
    let TryParseArg (args:string[]) =
        try
            Some(args.[0] |> int, args.[1], args.[2] |> int)
        with
            | _ -> None

    let TryParseOperator (val1:int, operation, val2:int) = maybeBuilder{
        let! operator = match operation with
        | "+" -> Some Calculator.Operation.Plus
        | "-" -> Some Calculator.Operation.Minus
        | "*" -> Some Calculator.Operation.Multiply
        | "/" -> Some Calculator.Operation.Divide
        | _ -> None
        return (val1, operator, val2)
        }
        
    let Parse argv = maybeBuilder{
        let! checkArgLenght = CheckArgsLenght argv
        let! parsedArgs = TryParseArg checkArgLenght
        let! parsedArgsAndOperator = TryParseOperator parsedArgs
        return parsedArgsAndOperator
        }