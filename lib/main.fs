module main

[<EntryPoint>]
let main args =

    Array.concat [ [| config.app |]; args ]
    |> Array.iteri (fun argc argv -> printfn $"argv[{argc}] = <{argv}>")

    Meta.gen ()
    0
