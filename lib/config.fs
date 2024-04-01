module config

// let app = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
let app = System.Environment.CurrentDirectory.Split "/" |> Array.last

let brief = "cross-compiler library"

let author = "Dmitry Ponyatov"

let email = "dponyatov@gmail.com"

let github = "https://github.com/ponyatov"

let meta = "meta"

let dotnet = "8.0"

type Screen =
    | width = 240
    | height = 320
