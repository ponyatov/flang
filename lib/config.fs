module config

// let app = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name
let app = System.Environment.CurrentDirectory.Split "/" |> Array.last

let brief = "F# workout & media/games cross-compiler"

let author = "Dmitry Ponyatov"

let email = "dponyatov@gmail.com"

let github = "https://github.com/ponyatov"

let meta = "meta"

let dotnet = "8.0"
