/////////////////////////////
//Borovikov Nikita (c) 2014//
//Lists//////////////////////
/////////////////////////////


let list1 = [11; 12; 13]
let list2 = [21; 22; 23]
     

let rec length l =
    match l with
    |[] -> 0
    |hd::tl -> length tl + 1
 
     
let rec sum l =
    match l with
    |[] -> 0
    |hd::tl -> sum tl + hd


let rec to_end l a =
    match l with
    |[] -> a::l
    |hd::tl -> hd::(to_end tl a)


let rec conc l1 l2 =
    match l2 with
    |[] -> l1
    |hd::tl -> conc (to_end l1 hd) tl


let rec sqr l a = 
    if a > 0 then
        sqr (a*a::l) (a - 1)
    else 
        l

let quads_of = sqr []

let list3 = [1..5000]
let gTT x = x > 2
let multiplicity x = (x % 51 = 0)
                    

let rec filter f l =
    match l with
    | [] -> []
    | hd::tl -> 
         if f hd then 
             hd :: filter f tl
         else 
             filter f tl

let list' = filter multiplicity list3
//new
let genSqr n =
    let rec genSqr' a =
        if a*a < n then
            a*a::genSqr' (a+1)
        else 
            []
    genSqr' 1

let list4 = genSqr 64

