/////////////////////////////
//Borovikov Nikita (c) 2013//
//Arithm/////////////////////
/////////////////////////////


type Expr = Const of int 
          | Var of string 
          | Add of Expr * Expr
          | Sub of Expr * Expr
          | Mul of Expr * Expr
          | Div of Expr * Expr


let rec simplify expr =
    match expr with
    | Const c -> Const c
    | Var x -> Var x

    | Add(fs, sc) -> 
        let fs' = simplify fs
        let sc' = simplify sc
        match fs', sc' with
        | Const a, Const b -> Const(a + b)
        | Const 0, Var x -> Var x
        | Var x, Const 0 -> Var x
        | _ -> Add(fs', sc')

    | Sub(fs, sc) -> 
        let fs' = simplify fs
        let sc' = simplify sc
        match fs', sc' with
        | Const a, Const b -> Const(a - b)
        | Var x, Const 0 -> Var x
        | _ -> Sub(fs', sc')

    | Mul(fs, sc) -> 
        let fs' = simplify fs
        let sc' = simplify sc
        match fs', sc' with
        | Const a, Const b -> Const(a * b)
        | Const 0, _ -> Const 0
        | _, Const 0 -> Const 0
        | Const 1, Var x -> Var x
        | Var x, Const 1 -> Var x
        | _ -> Mul(fs', sc')

    | Div(fs, sc) -> 
        let fs' = simplify fs
        let sc' = simplify sc
        match fs', sc' with
        | _, Const 0 -> failwith "Division by 0"
        | Const 0, _ -> Const 0
        | Const a, Const b -> Const(a / b)
        | Var x, Const 1 -> Var x
        | _ -> Div(fs', sc')

let a = simplify (Add (Add (Add (Add (Add (Add (Const 2, Const 5), Const 5), Const 5), Const 5), Const 5), (Add (Const 4, Const 5))))