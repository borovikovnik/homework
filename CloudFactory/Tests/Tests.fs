module Tests 
 
open NUnit.Framework
open NSubstitute
open FsCheck
open FsUnit
open Clouds
open Interfaces

let mockAll (daylight : DaylightType) (luminary : bool) (wind : int) = 
    
    let mMagic = Substitute.For<IMagic>()
    let mCourier = Substitute.For<ICourier>()
    let mDayLight = Substitute.For<IDaylight>()
    let mLuminary = Substitute.For<ILuminary>()
    let mWind = Substitute.For<IWind>()
    let mCloud = new Clouds.Cloud(mDayLight, mLuminary, mWind)

    mDayLight.Current.Returns(daylight) |> ignore
    mLuminary.IsShining.Returns(luminary) |> ignore
    mWind.Speed.Returns(wind) |> ignore
    mMagic.CallStork().Returns(mockCourier) |> ignore
    mMagic.CallDaemon().Returns(mockCourier) |> ignore

    (mCloud, mMagic, mCourier)

 
type ``int from 0 to 10`` =
        static member int() = Arb.fromGen <| Gen.choose(0, 10) 

type ``int from 0 to 3`` =
        static member int() = Arb.fromGen <| Gen.choose(0, 3)

type ``int from 4 to 8`` =
        static member int() = Arb.fromGen <| Gen.choose(4, 8)

type ``int from 1 to 10`` =
        static member int() = Arb.fromGen <| Gen.choose(1, 10)

[<Test>]
let ``Call Stork and return Bat``() =
    let test wind = 
        let cloud, magic, courier = mockAll DaylightType.Noon true wind
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallStork() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Bat)) |> ignore
        )

    Arb.register<``int from 0 to 3``>() |> ignore
    Check.Quick test

[<Test>]
let ``Call Stork and return Bearcub``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Noon true 7
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallStork() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Bearcub)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Stork and return Piglet``() =
    let test wind = 
        let cloud, magic, courier = mockAll DaylightType.Evening true wind
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallStork() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Piglet)) |> ignore
        )

    Arb.register<``int from 4 to 8``>() |> ignore
    Check.Quick test

[<Test>]
let ``Call Stork and return Hedgehog``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Evening true 10
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallStork() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Hedgehog)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Stork and return Bat``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Night true 0
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallStork() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Bat)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Stork and return Hedgehog``() =
    let test wind = 
        let cloud, magic, courier = mockAll DaylightType.Evening false wind
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallStork() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Hedgehog)) |> ignore
        )

    Arb.register<``int from 0 to 10``>() |> ignore
    Check.Quick test

[<Test>]
let ``Call Daemon and return Kitten``() =
    let test wind = 
        let cloud, magic, courier = mockAll DaylightType.Night true wind
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Kitten)) |> ignore
        )

    Arb.register<``int from 1 to 10``>() |> ignore
    Check.Quick test

[<Test>]
let ``Call Daemon and return Bat``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Morning false 1
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Bat)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Daemon and return Puppy``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Morning false 2
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Puppy)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Daemon and return Piglet``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Morning false 5
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Piglet)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Daemon and return Kitten``() =
    let test = 
        let cloud, magic, courier = mockAll DaylightType.Noon false 9
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Kitten)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Daemon and return Bearcub``() =
    let test wind = 
        let cloud, magic, courier = mockAll DaylightType.Night false 6
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Bearcub)) |> ignore
        )

    Check.Quick test

[<Test>]
let ``Call Daemon and return Balloon``() =
    let test wind = 
        let cloud, magic, courier = mockAll DaylightType.Morning true wind
    
        cloud.Create() |> ignore

        Received.InOrder(fun () ->
            magic.CallDaemon() |> ignore
            courier.GiveBaby(Arg.Is<ICreature>(fun (creature : ICreature) -> creature.Type = Balloon)) |> ignore
        )

    Arb.register<``int from 0 to 10``>() |> ignore
    Check.Quick test