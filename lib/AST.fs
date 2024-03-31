module AST

type Object =
    val value: string
    val nest: Object list
    new(v: string) = { value = v; nest = [] }

type Primitive(v: string) =
    inherit Object(v: string)

type Num =
    inherit Primitive
    val value: float
    new(f: float) = { value = f }
    new(n: int) = { value = float (n) }
    new(v: string) = { value = float (v) }


type Container(v: string) =
    inherit Object(v: string)

type Active(v: string) =
    inherit Object(v: string)

type IO(v: string) =
    inherit Object(v: string)
