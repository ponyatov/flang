module giti

let gen project =
    System.IO.File.WriteAllLines(
        config.meta + "/.gitignore",
        [ "*~"; "*.swp"; "*.log"; "/docs/"; "!.gitignore" ]
    )
