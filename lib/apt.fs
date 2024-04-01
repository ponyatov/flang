module apt

let gen app =
    System.IO.File.WriteAllLines(
        config.meta + "/apt.txt",
        [
            "git make curl"
            "code meld doxygen clang-format"
            "g++ cmake flex bison libreadline-dev libgc-dev"
            "libsdl2-dev libsdl2-image-dev libsdl2-ttf-dev"
        ]
    )

    app
