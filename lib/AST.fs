module AST

type Object(v: string) =
    let value = v

type Primitive(v: string) =
    inherit Object(v: string)

type Container(v: string) =
    inherit Object(v: string)

type Active(v: string) =
    inherit Object(v: string)

type IO(v: string) =
    inherit Object(v: string)
