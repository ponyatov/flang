module dirs

let meta = "meta"

let dirs =
    [ meta ]
    @ List.map (fun s -> meta + "/" + s) [
        ".vscode"
        "bin"
        "doc"
        "lib"
        "inc"
        "src"
        "tmp"
    ]

let create meta =
    let mk dir = System.IO.Directory.CreateDirectory dir
    List.map mk dirs |> ignore
    meta
