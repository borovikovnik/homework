/////////////////////////////
//Borovikov Nikita (с) 2014//
//Intersect//////////////////
/////////////////////////////

module Intersect

type Geom = 
          | NoPoint                  // пустое множество
          | Point of float * float   // точка
          | Line  of float * float   // уравнение прямой y = a*x+b
          | VerticalLine of float    // вертикальная прямая проходящая через x
          | LineSegment of (float * float) * (float * float) // отрезок
          | Intersection of Geom * Geom // пересечение двух множеств

let deviation = 0.00001

let (==) a b = abs(a - b) < deviation
let (>>) a b = a + deviation > b
let (<<) a b = a - deviation < b

let canonLS ((x1, y1), (x2, y2)) =
    if x1 << x2 then 
        ((x1, y1), (x2, y2)) 
    elif x1 == x2 then
        if y1 >> y2 then
            ((x2, y2), (x1, y1)) 
        else
            ((x1, y1), (x2, y2))
    else 
        ((x2, y2), (x1, y1)) 

//////////////////////////////

let Point'Point (xP1, yP1) (xP2, yP2) =
    if xP1 == xP2 && yP1 == yP2 then 
        Point (xP1, yP1)
    else
        NoPoint

let Point'Line (xP, yP) (a, b) =
    if a*xP + b == yP then 
        Point (xP, yP)
    else
        NoPoint

let Point'VerticalLine (xP, yP) xL =
    if xP == xL then 
        Point (xP, yP)
    else NoPoint

let Point'LineSegment (xP, yP) ((x1, y1), (x2, y2)) =
    if (yP - y1)*(x2 - x1) == (xP - x1)*(y2 - y1) &&  xP >> x1 && xP << x2 then 
        Point (xP, yP)
    else 
        NoPoint

//////////////////////////////

let Line'Line (a, b) (c, d) =
    if a == c then 
        if b == d then 
            Line (a, b)
        else 
            NoPoint
    else 
        Point ((d - b)/(a - c), (b*c - d*a)/(c - a))

let Line'VerticalLine (a, b) xL =
    Point (xL, a*xL + b)

let Line'LineSegment (a, b) ((x1, y1), (x2, y2)) =
    match Line'Line (a, b) ((y2 - y1)/(x2 - x1), (x2*y1 - x1*y2)/(x2 - x1)) with
    | Line _ -> LineSegment ((x1, y1), (x2, y2))
    | Point (xP, yP) -> Point'LineSegment (xP, yP) ((x1, y1), (x2, y2))
    | _ -> NoPoint

//////////////////////////////

let VerticalLine'VerticalLine xL1 xL2 =
    if xL1 == xL2 then VerticalLine xL1
    else NoPoint

let VerticalLine'LineSegment xL ((x1, y1), (x2, y2)) =
    if xL >> x1 && xL << x2 then 
        if x1 == x2 then VerticalLine xL
        else Point (xL, (xL - x1)*(y2 - y1)/(x2 - x1) + y1)
    else NoPoint

//////////////////////////////

let OnOneLine ((x1, y1), (x2, y2)) ((x3, y3), (x4, y4)) =
    if y2 << y3 || y1 >> y4 then
        NoPoint
    elif y2 == y3 then
        Point (x2, y2)
    elif y1 == y4 then
        Point (x1, y1)
    elif y1 << y3 then
        if y2 >> y4 then
            LineSegment ((x3, y3), (x4, y4))
        else
            LineSegment ((x3, y3), (x2, y2))
    elif y2 >> y4 then
        LineSegment ((x1, y1), (x4, y4))
    else
        LineSegment ((x1, y1), (x2, y2))
            
let rec LineSegment'LineSegment ((x1, y1), (x2, y2)) ((x3, y3), (x4, y4)) =
    if x1 == x2 then
        if x3 == x4 then
            if x1 == x3 then
                OnOneLine ((x1, y1), (x2, y2)) ((x3, y3), (x4, y4)) 
            else NoPoint
        else
            let VL'LS = VerticalLine'LineSegment x1 ((x3, y3), (x4, y4))
            match VL'LS with
            | Point (xP, yP) -> 
                                if yP << y1 || yP >> y2 then NoPoint
                                else Point (xP, yP)
            | _ -> NoPoint
    elif x3 == x4 then LineSegment'LineSegment ((x3, y3), (x4, y4)) ((x1, y1), (x2, y2))
    else
        let L'L = Line'Line ((y2 - y1)/(x2 - x1), (x2*y1 - x1*y2)/(x2 - x1)) ((y4 - y3)/(x4 - x3), (x4*y3 - x3*y4)/(x4 - x3))
        match L'L with
        | Point (xP, yP) -> Point'LineSegment (xP, yP) ((x1, y1), (x2, y2))
        | Line (_, _) -> 
            OnOneLine ((x1, y1), (x2, y2)) ((x3, y3), (x4, y4))
        | _ -> NoPoint

//////////////////////////////
      
let rec intersect set1 set2 =
    match set1 with
    | NoPoint -> NoPoint
    | Point (xP1, yP1) ->
        match set2 with
        | NoPoint ->                            NoPoint
        | Point(xP2, yP2) ->                    Point'Point (xP1, yP1) (xP2, yP2)
        | Line (c, d) ->                        Point'Line (xP1, yP1) (c, d)
        | VerticalLine xL2 ->                   Point'VerticalLine (xP1, yP1) xL2
        | LineSegment ((x3, y3), (x4, y4)) ->   Point'LineSegment (xP1, yP1) (canonLS ((x3, y3), (x4, y4)))
        | Intersection (set3', set4') ->        intersect set1 (intersect set3' set4')
    | Line (a, b) ->
        match set2 with
        | NoPoint ->                            NoPoint
        | Point(xP2, yP2) ->                    Point'Line (xP2, yP2) (a, b)
        | Line (c, d) ->                        Line'Line (a, b) (c, d)
        | VerticalLine xL2 ->                   Line'VerticalLine (a, b) xL2
        | LineSegment ((x3, y3), (x4, y4)) ->   Line'LineSegment (a, b) (canonLS ((x3, y3), (x4, y4)))
        | Intersection (set3', set4') ->        intersect set1 (intersect set3' set4')
    | VerticalLine xL1 ->
        match set2 with
        | NoPoint ->                            NoPoint
        | Point(xP2, yP2) ->                    Point'VerticalLine (xP2, yP2) xL1
        | Line (c, d) ->                        Line'VerticalLine (c, d) xL1
        | VerticalLine xL2 ->                   VerticalLine'VerticalLine xL1 xL2
        | LineSegment ((x3, y3), (x4, y4)) ->   VerticalLine'LineSegment xL1 (canonLS ((x3, y3), (x4, y4)))
        | Intersection (set3', set4') ->        intersect set1 (intersect set3' set4')
    | LineSegment ((x1, y1), (x2, y2)) ->
        match set2 with
        | NoPoint ->                            NoPoint
        | Point(xP2, yP2) ->                    Point'LineSegment (xP2, yP2) (canonLS ((x1, y1), (x2, y2)))
        | Line (c, d) ->                        Line'LineSegment (c, d) (canonLS ((x1, y1), (x2, y2)))
        | VerticalLine xL2 ->                   VerticalLine'LineSegment xL2 (canonLS ((x1, y1), (x2, y2)))
        | LineSegment ((x3, y3), (x4, y4)) ->   LineSegment'LineSegment (canonLS ((x1, y1), (x2, y2))) (canonLS ((x3, y3), (x4, y4)))
        | Intersection (set3', set4') ->        intersect set1 (intersect set3' set4')
    | Intersection (set1', set2')-> 
                                                intersect (intersect set1' set2') set2