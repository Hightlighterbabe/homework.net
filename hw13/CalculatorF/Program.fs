namespace CalculatorF 

module Program = 

open MaybeBuilder
open Calculator
open Parser

    
    let GetResult argv = maybeBuilder {
            let! parsed = Parse argv
            let! result = Calculate parsed
            return result
        }
    let main argv =
        let result = GetResult argv
        match result with
        | Some value ->
            printf $"{value}"
            0
        | None ->
            printf "None"
            1