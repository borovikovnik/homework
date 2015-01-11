//////////////////////////////
//Borovikov Nikita (с) 2014///
//IsParallelogram/////////////
//////////////////////////////

module Pgram

type Point(x : float, y: float) =
    member this.X = x
    member this.Y = y

let (!=) (a: Point) (b: Point) = 
    a.X <> b.X || a.Y <> b.Y

type Pgram(ul : Point, ur : Point, dl : Point, dr : Point) =
    member this.UL = ul     //UpLeft point
    member this.UR = ur     //UpRight point
    member this.DL = dl     //DownLeft point
    member this.DR = dr     //DownRight point

let canonPoints (pgram: Pgram) =
    let pnt1 = pgram.UL
    let pnt2 = pgram.UR
    let pnt3 = pgram.DL
    let pnt4 = pgram.DR
    let arrayP = [|pnt1; pnt2; pnt3; pnt4|]
    let newP = Array.sortBy (fun (p: Point) -> p.X) arrayP
    let ul, dl = 
        if newP.[0].Y > newP.[1].Y then 
            newP.[0], newP.[1]
        else
            newP.[1], newP.[0]
    let ur, dr =
        if newP.[2].Y > newP.[3].Y then 
            newP.[2], newP.[3]
        else
            newP.[2], newP.[3]
    Pgram(ul, ur, dl, dr)

let isParallelogram (pgram': Pgram) =
    let pgram = canonPoints pgram'
    pgram.UL.X - pgram.DL.X = pgram.UR.X - pgram.DR.X &&
    pgram.UL.Y - pgram.DL.Y = pgram.UR.Y - pgram.DR.Y 


    