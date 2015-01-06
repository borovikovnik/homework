/////////////////////////////
//Borovikov Nikita (с) 2014//
//Intersection tests/////////
/////////////////////////////

namespace IntersectTests


open NUnit.Framework
open Intersect


[<TestFixture>]
type Tests() =
    [<Test>]
    member x.Test1() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(0.0, 0.0)
        let p3 = new Point(0.0, 0.0)
        let p4 = new Point(0.0, 0.0)
        let p5 = new Point(0.0, 0.0)
        let p6 = new Point(0.0, 0.0)
        let p7 = new Point(0.0, 0.0)
        let p8 = new Point(0.0, 0.0)
        let rct1 = new Rectangle(p1, p2, p3, p4)
        let rct2 = new Rectangle(p5, p6, p7, p8)
        Assert.IsTrue((Intersect.isIntersect rct1 rct2), "ERROR of ZeroRectangle")
    member x.Test2() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(-1.0, -2.0)
        let p3 = new Point(-1.0, 0.0)
        let p4 = new Point(0.0, -2.0)
        let p5 = new Point(5.0, 5.0)
        let p6 = new Point(6.0, 5.0)
        let p7 = new Point(5.0, 3.0)
        let p8 = new Point(6.0, 3.0)
        let rct1 = new Rectangle(p1, p2, p3, p4)
        let rct2 = new Rectangle(p5, p6, p7, p8)
        Assert.IsFalse((Intersect.isIntersect rct1 rct2), "ERROR of SimpleCases")
    member x.Test3() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(-1.0, -2.0)
        let p3 = new Point(-1.0, 0.0)
        let p4 = new Point(0.0, -2.0)
        let p5 = new Point(0.0, 5.0)
        let p6 = new Point(6.0, 5.0)
        let p7 = new Point(0.0, 0.0)
        let p8 = new Point(6.0, 0.0)
        let rct1 = new Rectangle(p1, p2, p3, p4)
        let rct2 = new Rectangle(p5, p6, p7, p8)
        Assert.IsTrue((Intersect.isIntersect rct1 rct2), "ERROR of PointIntersection")
    member x.Test4() =
        let p1 = new Point(-2.0, 2.0)
        let p2 = new Point(2.0, 2.0)
        let p3 = new Point(-2.0, -2.0)
        let p4 = new Point(2.0, -2.0)
        let p5 = new Point(-1.0, 1.0)
        let p6 = new Point(1.0, 1.0)
        let p7 = new Point(-1.0, -1.0)
        let p8 = new Point(1.0, -1.0)
        let rct1 = new Rectangle(p1, p2, p3, p4)
        let rct2 = new Rectangle(p5, p6, p7, p8)
        Assert.IsFalse((Intersect.isIntersect rct1 rct2), "ERROR of FirstMatrioshkaVariant")
    member x.Test5() =
        let p1 = new Point(-2.0, 2.0)
        let p2 = new Point(2.0, 2.0)
        let p3 = new Point(-2.0, -2.0)
        let p4 = new Point(2.0, -2.0)
        let p5 = new Point(-3.0, 3.0)
        let p6 = new Point(3.0, 3.0)
        let p7 = new Point(-3.0, -3.0)
        let p8 = new Point(3.0, -3.0)
        let rct1 = new Rectangle(p1, p2, p3, p4)
        let rct2 = new Rectangle(p5, p6, p7, p8)
        Assert.IsFalse((Intersect.isIntersect rct1 rct2), "ERROR of SecondMatrioshkaVariant")
