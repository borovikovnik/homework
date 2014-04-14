/////////////////////////////
//Borovikov Nikita (с) 2014//
//Class work/////////////////
/////////////////////////////

let even a = a % 2 = 0

let fib_sum lim filt=
    let rec fib_sum' (f, fn) s =
        if fn < lim then
            if filt fn then
                fib_sum' (fn, f + fn) (s + fn)
            else
                fib_sum' (fn, f + fn) s
        else s
    fib_sum' (1, 1) 0


let x1 = fib_sum 4000000 even

/////////////////////////////////


let GPD a =
    let rec gpd a d =
        if d*d > a then
            a
        else 
            if a % d = 0L then
                gpd (a/d) d
            else
                gpd a (d+1L)
    gpd a 2L

let x2 = GPD 600851475143L

/////////////////////////////////           

let fact n =
    let rec fact' n s = 
        if n < 2 then
            s
        else
            fact' (n - 1) (s * bigint n)
    fact' n 1I
                
let dig_sum n = 
    let rec ds n s =
        if n = 0I  then
            s
        else 
            ds (n / 10I) (s + int (n % 10I))
    ds (fact n) 0

let x3 = dig_sum 100