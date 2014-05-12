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

    | Add(Const a, Const b) -> Const(a + b)
    | Add(Const 0, ex) -> simplify ex
    | Add(ex, Const 0) -> simplify ex
    | Add(fs, sc) -> 
                    let fs' = simplify fs
                    let sc' = simplify sc
                    if (fs' = fs)&&(sc' = sc) then
                        Add(fs, sc)
                    else
                        simplify (Add(fs', sc'))

    | Sub(Const a, Const b) -> Const(a - b)
    | Sub(ex, Const 0) -> simplify ex
    | Sub(fs, sc) -> 
                    if fs = sc then 
                        Const 0
                    else
                        let fs' = simplify fs
                        let sc' = simplify sc
                        if (fs' = fs)&&(sc' = sc) then
                            Sub(fs, sc)
                        else
                            simplify (Sub(fs', sc'))

    | Mul(Const a, Const b) -> Const(a * b)
    | Mul(Const 0, _) -> Const 0
    | Mul(Const 1, ex) -> simplify ex
    | Mul(_, Const 0) -> Const 0
    | Mul(ex, Const 1) -> simplify ex
    | Mul(fs, sc) -> 
                    let fs' = simplify fs
                    let sc' = simplify sc
                    if (fs' = fs)&&(sc' = sc) then
                        Mul(fs, sc)
                    else
                        simplify (Mul(fs', sc'))

    | Div(_, Const 0) -> failwith("Division by 0")
    | Div(Const 0, _) -> Const 0
    | Div(Const a, Const b) -> Const(a / b)
    | Div(ex, Const 1) -> simplify ex
    | Div(fs, sc) -> 
                    let fs' = simplify fs
                    let sc' = simplify sc
                    if (fs' = fs)&&(sc' = sc) then
                        Div(fs, sc)
                    else
                        simplify (Div(fs', sc'))

let a = simplify (Add (Add (Const 2, Const 5), (Add (Const 4, Const 5))))