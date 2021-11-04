namespace FSLibrary

open System

module Parser =

    let ParserOperationFail = "Operation is not correct"
    let result = ResultBuilder(ParserOperationFail)

    let ParseIntNumber (value1: string) =
        result {
            let value = ref (Int32())

            if Int32.TryParse(value1, value) then
                return !value
        }

    let ParseDoubleNumber (value1: string) =
        result {
            let value = ref (Double())

            if Double.TryParse(value1, value) then
                return !value
        }

    let ParseDecimalNumber (value1: string) =
        result {
            let value = ref (Decimal())

            if Decimal.TryParse(value1, value) then
                return !value
        }

    let ParseFloatNumber (value1: string) =
        result {
            let value = ref (Single())

            if Single.TryParse(value1, value) then
                return !value
        }

    let ParseOperation arg =
        result {
            if arg = "+" || arg = "-" || arg = "*" || arg = "/" then
                match arg with
                | "+" -> return Calculator.Operation.Plus
                | "-" -> return Calculator.Operation.Minus
                | "*" -> return Calculator.Operation.Multiply
                | _ -> return Calculator.Operation.Divide

        }
