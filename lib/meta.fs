module Meta

let meta = "meta"
let dotnet = "8.0"

open System

let project = Environment.CurrentDirectory.Split "/" |> Array.last


let dirs = [ ".vscode"; "bin"; "doc"; "lib"; "inc"; "src"; "tmp" ]
let dirs = [ meta ] @ List.map (fun s -> meta + "/" + s) dirs

let mk dir = System.IO.Directory.CreateDirectory dir
List.map mk dirs |> ignore
