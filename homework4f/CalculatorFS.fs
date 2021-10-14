namespace FSLibrary

open System


type ResultBuilder(errormessage: string) =
    member b.Zero() = Error errormessage
    
    member b.Bind(x, f) =
        match x with
        |Ok x -> f x
        |Error x -> Error x
    member b.Return(x)= Ok x
    
module Calculator=
    
    type Operation=
        | Plus
        | Minus
        | Multiply
        | Divide
    let DivisionByZero = "Num2 is zero"
    let result = ResultBuilder(DivisionByZero)
    let inline Calculate (val1: Result<'T, string> when 'T:(static member (+): 'T *'T -> 'T))
                         (val2: Result<'T, string>)
                         operation =
        result{
            let! val1 = val1
            let! val2 = val2
            match operation with
            | Plus -> return val1 + val2
            | Minus -> return val1 - val2
            | Multiply -> return val1 * val2
            | Divide ->
                if val2 <> new 'T() then
                    return val1/val2
        }