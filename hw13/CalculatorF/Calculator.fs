namespace CalculatorF

module Calculator  =

    type Operation =
        | Plus = 1
        | Minus = 2
        | Divide = 3
        | Multiply = 4

    let Calculate (val1:int, operation:Operation, val2:int) =
        match operation with
        | Operation.Plus -> Some (val1 + val2)
        | Operation.Minus -> Some (val1 - val2)
        | Operation.Multiply -> Some (val1 * val2)
        | Operation.Divide ->
            if val2.Equals(0) then None
            else Some (val1 / val2)
        | _ -> None