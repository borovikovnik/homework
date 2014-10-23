/////////////////////////////
//Borovikov Nikita (c) 2014//
//Email Checker tests////////
/////////////////////////////

﻿module Tests

open MailRegex
open NUnit.Framework


[<TestFixture>]
type Tests() =
    [<Test>]
    member x.Test0() = 
        Assert.IsTrue(isEmail "a@b.cc", "error")

    [<Test>]
    member x.Test1() = 
        Assert.IsTrue(isEmail "victor.polozov@mail.ru", "error")

    [<Test>]
    member x.Test2() = 
        Assert.IsTrue(isEmail "my@domain.info", "error")

    [<Test>]
    member x.Test3() = 
        Assert.IsTrue(isEmail "_.1@mail.com", "error")

    [<Test>]
    member x.Test4() = 
        Assert.IsTrue(isEmail "paints_department@hermitage.museum", "error")

    [<Test>]
    member x.Test5() = 
        Assert.IsFalse(isEmail "a@b.c", "error")

    [<Test>]
    member x.Test6() = 
        Assert.IsFalse(isEmail "a..b@mail.ru", "error")

    [<Test>]
    member x.Test7() = 
        Assert.IsFalse(isEmail ".a@mail.ru", "error")

    [<Test>]
    member x.Test8() = 
        Assert.IsFalse(isEmail "yo@domain.somedomain", "error")

    [<Test>]
    member x.Test9() = 
        Assert.IsFalse(isEmail "1@mail.ru", "error")
