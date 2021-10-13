namespace FSLibrary

open System

module Calculator=
    
    type Operation=
        | Plus
        | Minus
        | Multiply
        | Divide
        | Unknown
    let WrongOperation = "Operation is unknown"
    let DivisionByZero = "Num2 is zero"
    
    let Calculate (val1: int) (val2: int) operation =
        match operation with
        
        | Plus -> val1 + val2
        | Minus -> val1 - val2
        | Multiply -> val1 * val2
        | Divide ->
            try
                val1 / val2
            with
            | :? System.DivideByZeroException -> failwith DivisionByZero
        | Unknown -> failwith WrongOperation
    