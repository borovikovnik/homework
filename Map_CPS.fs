/////////////////////////////
//Borovikov Nikita (c) 2013//
//Map'CPS////////////////////
/////////////////////////////

let func x y = y (x + 3)

let rec map f l c =
    match l with
    | hd :: tl -> f hd (fun x -> map f tl (fun y -> c (x :: y)))
    | [] -> c []

let list = [1..20]

map func list (printfn "%A") 