module Meta

open System

let gen () =
    printfn $"{config.app} @ {Environment.CurrentDirectory}/{config.meta}"
    config.app |> dirs.gen |> giti.gen |> Cpp.gen |> ignore
