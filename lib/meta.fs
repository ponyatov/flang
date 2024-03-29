module Meta

let meta = "meta"
let dotnet = "8.0"

open System

let project = Environment.CurrentDirectory.Split "/" |> Array.last
