/////////////////////////////
//Borovikov Nikita (с) 2014//
//Intersection tests/////////
/////////////////////////////

namespace PgramTests


open NUnit.Framework
open Pgram


[<TestFixture>]
type Tests() =
    [<Test>]
    member x.Test1() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(0.0, 0.0)
        let p3 = new Point(0.0, 0.0)
        let p4 = new Point(0.0, 0.0)
        let prg = new Pgram(p1, p2, p3, p4)
        Assert.IsTrue((Pgram.isParallelogram prg), "ERROR of ZeroParallelogram")
    [<Test>]
    member x.Test2() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(-2.0, -2.0)
        let p3 = new Point(-1.0, 0.0)
        let p4 = new Point(0.0, -2.0)
        let prg = new Pgram(p1, p2, p3, p4)
        Assert.IsFalse((Pgram.isParallelogram prg), "ERROR of SimpleCase")
    [<Test>]
    member x.Test3() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(-2.0, -2.0)
        let p3 = new Point(2.0, -2.0)
        let p4 = new Point(-4.0, 0.0)
        let prg = new Pgram(p1, p2, p3, p4)
        Assert.IsTrue((Pgram.isParallelogram prg),  "ERROR 45 degrees")
    [<Test>]
    member x.Test4() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(0.0, -1.0)
        let p3 = new Point(0.0, -2.0)
        let p4 = new Point(0.0, 1.0)
        let prg = new Pgram(p1, p2, p3, p4)
        Assert.IsFalse((Pgram.isParallelogram prg),  "ERROR of Line")
    [<Test>]
    member x.Test5() =
        let p1 = new Point(0.0, 0.0)
        let p2 = new Point(-2.0, 0.0)
        let p3 = new Point(2.0, 0.0)
        let p4 = new Point(-4.0, 0.0)
        let prg = new Pgram(p1, p2, p3, p4)
        Assert.IsFalse((Pgram.isParallelogram prg),  "ERROR of Line2")
