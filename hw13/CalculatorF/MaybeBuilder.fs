module MaybeBuilder

    type MaybeBuilder() =
        member b.Bind(x,f)=
            match x with
            | Some x -> f x
            | None -> None
        member b.Return x = Some x
    let maybeBuilder = MaybeBuilder()