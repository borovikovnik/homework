//////////////////////////////
//Borovikov Nikita (с) 2014///
//Intersection of rectangles//
//////////////////////////////

module Intersect

type Point(x : float, y: float) =
    member this.X = x
    member this.Y = y

type Rectangle(ul : Point, ur : Point, dl : Point, dr : Point) =
    member this.UL = ul     //UpLeft point
    member this.UR = ur     //UpRight point
    member this.DL = dl     //DownLeft point
    member this.DR = dr     //DownRight point

let isntCorrupt (rct : Rectangle) =
    rct.UL.Y = rct.UR.Y && rct.UR.X = rct.DR.X &&
    rct.DR.Y = rct.DL.Y && rct.DL.X = rct.UL.X  

let canonRct (rct': Rectangle) =
    let pnt1 = rct'.UL
    let pnt2 = rct'.UR
    let pnt3 = rct'.DL
    let pnt4 = rct'.DR
    let ul = new Point(List.min [pnt1.X; pnt2.X; pnt3.X; pnt4.X], List.max [pnt1.Y; pnt2.Y; pnt3.Y; pnt4.Y])
    let ur = new Point(List.max [pnt1.X; pnt2.X; pnt3.X; pnt4.X], List.max [pnt1.Y; pnt2.Y; pnt3.Y; pnt4.Y])
    let dl = new Point(List.min [pnt1.X; pnt2.X; pnt3.X; pnt4.X], List.min [pnt1.Y; pnt2.Y; pnt3.Y; pnt4.Y])
    let dr = new Point(List.max [pnt1.X; pnt2.X; pnt3.X; pnt4.X], List.min [pnt1.Y; pnt2.Y; pnt3.Y; pnt4.Y])
    let rct = new Rectangle(ul, ur, dl, dr)
    if isntCorrupt rct then
        rct
    else 
        failwith "Wrong coordinates"

let isIntersect (rct1': Rectangle) (rct2': Rectangle) =
    let rct1 = canonRct rct1'
    let rct2 = canonRct rct2'
    not (rct1.UR.X < rct2.UL.X || rct1.UL.X > rct2.UR.X ||  //simple cases
         rct1.DL.Y > rct2.UL.Y || rct1.UL.Y < rct2.DL.Y ||  //
         
         rct1.UL.X < rct2.UL.X && rct1.UL.Y > rct2.UL.Y &&  //rct2 inside rct1
         rct1.DR.X > rct2.DR.X && rct1.DR.Y < rct2.DR.Y ||  //
         
         rct1.UL.X > rct2.UL.X && rct1.UL.Y < rct2.UL.Y &&  //rct1 inside rct2
         rct1.DR.X < rct2.DR.X && rct1.DR.Y > rct2.DR.Y)    //

    