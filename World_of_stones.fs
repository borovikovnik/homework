/////////////////////////////
//Borovikov Nikita (c) 2014//
//World of Stones////////////
/////////////////////////////

module Stones

type Stone(size: int) =
    let mutable size = size
    member this.Crash () =
        size <- size/2
    member this.Size = size

[<AbstractClass>]
type RareStone(size: int, value: int, authenticity: bool) =
    inherit Stone(size)
    let selfvalue = value*size
    abstract member Value : int
    default x.Value = selfvalue

type Mineral(name: string, size: int, value: int, authenticity: bool) =
    inherit RareStone(size, value, authenticity)
    let mutable name = name
    let mutable size = size
    let mutable selfvalue = value*size
    let mutable authenticity = authenticity

    member this.IsGenuine
        with get() = authenticity
        and set(truth) = authenticity <- truth

    member this.Price 
        with get() = selfvalue
        and set(newPrise) = selfvalue <- newPrise

    member this.Name 
        with get() = name
        and set(newName) = name <- newName


type Superman(name: string, isAlive: bool) =
    let mutable isAlive = isAlive
    member this.Death () = isAlive <- false
    member this.CheckLife = isAlive
    member this.CheckStone (aim: Mineral) =
        if not aim.IsGenuine then
            aim.Price <- aim.Price / 10
            aim.IsGenuine <- false
            aim.Name <- "False" + aim.Name
            

type Kryptonit(size: int) =
    inherit RareStone(size, 9999, true)
    member this.TryKillSuperman (myAim: Superman) = 
        if size > 10 then myAim.Death()

type PhilosophersStone(authenticity: bool) =
    inherit RareStone(10, 0, authenticity)
    member this.Alchemy(stone: Stone) = 
        let alchemyResult = new Mineral("Gold", stone.Size, 100, authenticity)
        alchemyResult

(*
let mySuperman = new Superman("Klark", true)
let myStone = new Kryptonit(15)
myStone.TryKillSuperman(mySuperman)
let myStone2 = new Mineral("Paper", 15, 25, false)
myStone2.Check()
*)