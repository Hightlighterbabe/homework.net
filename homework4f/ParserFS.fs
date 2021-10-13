namespace FSLibrary

    open System

    module Parser =
        
        let ParserOperationFail = "Operation is not correct"
        
        
        let ToParseNumber (value:string) (result:outref<int>) =
            let success = ref 0;  
            if Int32.TryParse(value, success) then
                result <- !success
                true
            else
                Console.WriteLine($"value is not int. The value was {success}");
                false
        
        let ParseOperation arg =
            match arg with
            | "+" -> Calculator.Operation.Plus
            | "-" -> Calculator.Operation.Minus
            | "*" -> Calculator.Operation.Multiply
            | "/" -> Calculator.Operation.Divide
            | _ -> Calculator.Operation.Unknown

