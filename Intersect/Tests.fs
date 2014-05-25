/////////////////////////////
//Borovikov Nikita (ñ) 2014//
//NUnit tests////////////////
/////////////////////////////

module Tests

open Intersect
open NUnit.Framework

let deviation = 0.00001

let (==) x y = abs(x - y) < deviation

let cmp set1 set2 = 
    match set1 with 
    | NoPoint -> set1 = set2
    | Point (xP1, yP1) -> 
        match set2 with
        | Point(xP2, yP2) -> 
            xP1 == xP2 && yP1 == yP2 
        | _ -> false

    | Line (a, b) -> 
        match set2 with
        | Line(c, d) -> a == c && b == d
        | _ -> false

    | VerticalLine xL1 -> 
        match set2 with
        | VerticalLine xL2 -> 
            if xL1 = xL2 then true
            else false
        | _ -> false

    | LineSegment ((x1, y1), (x2, y2)) -> 
        match set2 with
        | LineSegment ((x3, y3), (x4, y4)) -> 
            x1 == x3 && x2 == x4 && y1 == y3 && y2 == y4 || 
            x1 == x4 && x2 == x3 && y1 == y4 && y2 == y3
        | _ -> false
    | _ -> false

[<TestFixture>]
type Tests() =
    [<Test>]
    member x.Test1() =
         Assert.AreEqual((cmp (intersect (Line (4.244369, 1.946667)) (Line (4.244369, 1.0))) (NoPoint)), true, "ERROR of Line'Line intersection")
    [<Test>]
    member x.Test2() =
        Assert.AreEqual((cmp (intersect (VerticalLine 9.345) (LineSegment ((-5.0, 5.598844), (8.0, 2.496325)))) (NoPoint)), true, "ERROR of VerticalLine'LineSegment intersection")
    [<Test>]
    member x.Test3() =
        Assert.AreEqual((cmp (intersect (VerticalLine 3.789993) (LineSegment ((3.789993, 1.336974), (3.789993, 8.684997)))) (LineSegment ((3.789993, 1.336974), (3.789993, 8.684997)))), true, "ERROR of VerticalLine'LineSegment intersection")
    [<Test>]
    member x.Test4() =
        Assert.AreEqual((cmp (intersect (LineSegment ((3.159456, -3.02), (9.159456, 16.507402))) (LineSegment ((4.159456, 0.0), (3.159456, 2.254567)))) (Point(4.1168782, 0.095994502))), true, "ERROR of LineSegment'LineSegment intersection")
    [<Test>]
    member x.Test5() =
        Assert.AreEqual((cmp (intersect (Intersection ((VerticalLine 24.847998), (Point (24.847998, 11.587888)))) (Intersection ((Point (24.847998, 11.587888)), (LineSegment ((12.42345, 11.587888), (65.234235, 11.587888)))))) (Point (24.847998, 11.587888))), true, "ERROR of Intersects intersection (or deeper)")
    [<Test>]
    member x.Test6() =
        Assert.AreEqual((cmp (intersect (Point (-6.84965547846, 5.84964754854)) (Point (-6.8496554, 5.8496475))) (NoPoint)), false, "ERROR of accuracy")
    [<Test>]
    member x.Test7() =
       Assert.AreEqual((cmp (intersect (LineSegment ((3.5, 6.733701), (6.5, 17.406402))) (LineSegment ((2.5, 3.479134), (4.5, 9.988268)))) (LineSegment ((3.5, 6.733701), (4.5, 9.988268)))), true, "ERROR of LineSegment'LineSegment intersection")
    [<Test>]
    member x.Test8() =
        Assert.AreEqual((cmp (intersect (Point (6.87489, 84.879555)) (LineSegment ((0.0, 0.0), (6.87489, 84.879555)))) (Point (6.87489, 84.879555))), true, "ERROR of Point'LineSegment intersection")
    [<Test>]
    member x.Test9() =
        Assert.AreEqual((cmp (intersect (Point (6.87489, 84.879555)) (LineSegment ((0.0, 0.0), (6.87489, 82.879555)))) (Point (6.87489, 84.879555))), false, "ERROR of Point'LineSegment intersection")
    [<Test>]
    member x.Test10() =
        Assert.AreEqual((cmp (intersect (Line (59.785489, 8.874444)) (LineSegment ((5.88944, -6.879544), (1.234567, -8.9101112)))) (NoPoint)), true, "ERROR of Line'LineSegment intersection")
    