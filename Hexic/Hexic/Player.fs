/////////////////////////////        _/ \
//Borovikov Nikita (c) 2014//       / \_/
//Hexic//////////////////////       \_/ \ ...
/////////////////////////////       / \_/

open System
open Field


let turnIsOk (f: int[,]) i j =
    let curr = f.[i, j]
    let c = -1 + 2*(i % 2) //depends on varaint of column
    if curr = f.[i+1, j] then 
        if   Field.leftRotate f i j (i+1) j i (j+1) then true
        elif Field.rightRotate f i j (i+1) j i (j+1) then true
        elif Field.leftRotate f i j (i+1) j i (j-1) then true
        elif Field.rightRotate f i j (i+1) j i (j-1) then true    
        elif curr = f.[i-1, j] then
            if   Field.leftRotate f i j (i-1) j (i-1) (j+1) then true
            elif Field.rightRotate f i j (i-1) j (i-1) (j+1) then true
            elif Field.leftRotate f i j (i-1) j (i-1) (j-1) then true
            elif Field.rightRotate f i j (i-1) j (i-1) (j-1) then true
            elif curr = f.[i, j+1] then 
                if   Field.leftRotate f i j i (j+1) (i-1) (j+1) then true
                elif Field.rightRotate f i j i (j+1) (i-1) (j+1) then true
                elif Field.leftRotate f i j i (j+1) (i+1) j then true
                elif Field.rightRotate f i j i (j+1) (i+1) j then true 
                elif curr = f.[i, j-1] then 
                    if   Field.leftRotate f i j i (j-1) (i-1) (j-1) then true
                    elif Field.rightRotate f i j i (j-1) (i-1) (j-1) then true
                    elif Field.leftRotate f i j i (j-1) (i+1) j then true
                    elif Field.rightRotate f i j i (j-1) (i+1) j then true
                    elif curr = f.[i+c, j+1] then 
                        if   Field.leftRotate f i j (i+c) (j+1) (i-1) j then true
                        elif Field.rightRotate f i j (i+c) (j+1) (i-1) j then true
                        elif Field.leftRotate f i j (i+c) (j+1) i (j+1) then true
                        elif Field.rightRotate f i j (i+c) (j+1) i (j+1) then true
                        elif curr = f.[i+c, j-1] then 
                            if   Field.leftRotate f i j (i+c) (j+1) (i-1) j then true
                            elif Field.rightRotate f i j (i+c) (j+1) (i-1) j then true
                            elif Field.leftRotate f i j (i+c) (j+1) i (j-1) then true
                            elif Field.rightRotate f i j (i+c) (j+1) i (j-1) then true
                            else false
                        else false
                    else false
                else false
            else false
        else false
    else false

let rec game f =
    for i in 1 .. Field.hg do
        for j in 1 .. Field.wd do
            if (turnIsOk f i j) then
                game f |> ignore
    Field.death        


let f = Field.getField