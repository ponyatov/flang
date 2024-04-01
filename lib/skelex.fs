module skelex

let lex app =
    System.IO.File.WriteAllText(config.meta + $"/src/{app}.lex", """""")
    app

let yacc app =
    System.IO.File.WriteAllText(config.meta + $"/src/{app}.yacc", """""")
    app


let gen app = app |> lex |> yacc
