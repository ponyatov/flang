module dirs

let dirs =
    [ config.meta ]
    @ List.map (fun s -> config.meta + "/" + s) [
        ".vscode"
        "bin"
        "doc"
        "lib"
        "inc"
        "src"
        "tmp"
    ]

let gen project =
    let mk dir = System.IO.Directory.CreateDirectory dir
    List.map mk dirs |> ignore
    project
