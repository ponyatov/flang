module project

type project(name: string) =
    override this.ToString() = name
    member this.sdl = true
    member this.skelex = true
