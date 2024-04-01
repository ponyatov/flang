module Meta

open System
open project

let gen () =
    printfn $"{config.app} @ {Environment.CurrentDirectory}/{config.meta}"

    project (config.app)
    |> dirs.gen
    |> giti.gen
    |> Cpp.gen
    |> cmake.gen
    |> skelex.gen
    |> apt.gen
    |> Makefile.gen
    |> ignore
