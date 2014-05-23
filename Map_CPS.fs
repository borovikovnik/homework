/////////////////////////////
//Borovikov Nikita (c) 2013//
//Map'CPS////////////////////
/////////////////////////////

let swap x y = y x

let rec map f l c =
    match l with
    | hd :: tl -> f hd (fun x -> map f tl (fun y -> c (x :: y)))
    | [] -> c []

let list = [1..20]

map swap list (printfn "%A") 