module Meta


open System

let project = Environment.CurrentDirectory.Split "/" |> Array.last

let () =
    printfn "Hello %s" Environment.CurrentDirectory
    project |> dirs.gen |> giti.gen |> ignore
