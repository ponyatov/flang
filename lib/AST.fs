module AST

type Object =
    val value: string
    val mutable nest: Object list
    // new() = { nest = [] }
    new(v: string) = { value = v; nest = [] }

open Xunit

// type Num =
//     inherit Object
//     val value: float // override

//     new(f: float) = { value = f }
//     new(n: int) = { value = float (n) }
//     new(v: string) = { value = float (v) }

// new Num(123)

// // type Container(v: string) =
// //     inherit Object(v: string)

// // type Active(v: string) =
// //     inherit Object(v: string)

// // type IO(v: string) =
// //     inherit Object(v: string)
