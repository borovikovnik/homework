/////////////////////////////        _/ \
//Borovikov Nikita (c) 2014//       / \_/
//Hexic//////////////////////       \_/ \ ...
/////////////////////////////       / \_/

module Field

open System

let wd = Convert.ToInt32(Console.ReadLine())
let hg = Convert.ToInt32(Console.ReadLine())
let private rnd = new Random(Convert.ToInt32(Console.ReadLine()))
let private clrs = 6
let mutable private scr = 0.0
let mutable private scr' = 0.0

let rec private fixCorrupt (f: int[,]) i j =
    let a = f.[i, j]
    if (i % 2 <> 0) then
        if
            a = f.[i-1, j] && a = f.[i, j+1] || 
            a = f.[i+1, j+1] && a = f.[i, j+1]
        then
            f.[i, j] <- (rnd.Next(clrs) + 1)
            fixCorrupt f i j
    else
        if
            a = f.[i-1, j] && a = f.[i-1, j+1] ||
            a = f.[i, j+1] && a = f.[i-1, j+1]
        then
            f.[i, j] <- (rnd.Next(clrs) + 1)
            fixCorrupt f i j


    

let private genField = 
    let f = Array2D.create (hg + 2) (wd + 2) (rnd.Next(clrs) + 1)
    //border
    for i in 0 .. hg + 1 do
        f.[i, 0] <- -1
        f.[i, wd+1] <- -1
    for j in 1 .. wd do
        f.[1, j] <- -1
        f.[hg, j] <- -1

    for i in 1 .. hg do
        for j in 1 .. wd do 
            fixCorrupt f i j
    f


let getField = genField

let private update (f: int[,]) =
    let mutable counter = 0
    for j in 1 .. wd do 
        for i in hg .. 0 do
            match f.[i,j] with
            | 0 -> counter <- counter + 1
            | -1 -> if counter > 0 then
                        for k in counter .. 1 do
                            f.[i+k, j] <- (rnd.Next(clrs) + 1)                       
            | a -> 
                    if a >= clrs then
                        failwith "Field generation error"
                    else
                        f.[i+counter, j] <- (rnd.Next(clrs) + 1)  

let rec private isTriplet (f: int[,]) i j =
    let a = f.[i, j]
    if a <> 0 then
        if (i % 2 = 0) then
            a = f.[i-1, j] && a = f.[i, j+1] || 
            a = f.[i+1, j+1] && a = f.[i, j+1]
        else
            a = f.[i-1, j] && a = f.[i-1, j+1] ||
            a = f.[i, j+1] && a = f.[i-1, j+1]
    else false
     

let private combo x =
    3.0*2.0**(x-3.0)

let rec private annihilation (f: int[,]) i j =
    let c = -1 + 2*(i % 2) //depends on varaint of column
    let temp = f.[i, j]
    f.[i, j] <- 0
    scr' <- scr' + 1.0
    if temp = f.[i+1, j] then annihilation f (i+1) j
    if temp = f.[i-1, j] then annihilation f (i-1) j
    if temp = f.[i, j+1] then annihilation f i (j+1)
    if temp = f.[i, j-1] then annihilation f i (j-1)
    if temp = f.[i+c, j+1] then annihilation f (i+c) (j+1)
    if temp = f.[i+c, j-1] then annihilation f (i+c) (j-1)
    scr <- scr + combo scr'
    scr' <- 0.0

let rec private plague (f: int[,]) =
    let mutable flag = false
    for i in 1 .. hg do
        for j in 1 .. wd do 
            if (isTriplet f i j) then
                annihilation f i j
                flag <- true
    if flag then
        update f |> ignore
        plague f |> ignore
    flag

let leftRotate (f: int[,]) i1 j1 i2 j2 i3 j3 = 
    if f.[i1, j1] = -1 || f.[i2, j2] = -1 || f.[i3, j3] = -1 then
        false
    else
        let temp = f.[i1, j1]                               
        f.[i1, j1] <- f.[i3, j3]
        f.[i3, j3] <- f.[i2, j2]
        f.[i2, j2] <- temp
        let result = plague f
        if not result then
            f.[i3, j3] <- f.[i1, j1]  
            f.[i2, j2] <- f.[i3, j3]
            f.[i1, j1] <- temp
        result

let rightRotate (f: int[,]) i1 j1 i2 j2 i3 j3 =   
    if f.[i1, j1] = -1 || f.[i2, j2] = -1 || f.[i3, j3] = -1 then
        false
    else       
        let temp = f.[i1, j1]                               
        f.[i1, j1] <- f.[i2, j2]
        f.[i2, j2] <- f.[i3, j3]
        f.[i3, j3] <- temp
        let result = plague f
        if not result then
            f.[i2, j2] <- f.[i1, j1]  
            f.[i3, j3] <- f.[i2, j2]
            f.[i1, j1] <- temp
        result

let death =
    printfn "%A" ((int)scr)