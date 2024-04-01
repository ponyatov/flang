module giti

open project

let gen (app: project) =

    let git path lines =
        System.IO.File.WriteAllLines(
            config.meta + path,
            lines @ [ "!.gitignore" ]
        )

    let any dir = git ("/" + dir + "/.gitignore") [ "*" ]
    let empty dir = git ("/" + dir + "/.gitignore") []

    git "/.gitignore" [ "*~"; "*.swp"; "*.log"; "/docs/" ]
    List.map any [ "bin"; "tmp" ] |> ignore
    List.map empty [ ".vscode"; "doc"; "lib"; "inc"; "src" ] |> ignore

    app
