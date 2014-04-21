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
    abstract member Sell : unit * unit

type Mineral(name: string, size: int, value: int, authenticity: bool) =
    inherit RareStone(size, value, authenticity)
    let mutable name = name
    let mutable size = size
    let mutable selfvalue = value*size
    let mutable authenticity = authenticity
    member this.Check () =
        if authenticity = false then
            selfvalue <- selfvalue/10
            authenticity <- true
            name <- "False" + name
    override this.Sell = printfn "You got %A $"  selfvalue , size <- 0 


type Superman(name: string, live: bool) =
    let mutable live = live
    member this.Death () = live <- false
    member this.CheckLife = live

type Kryptonit(size: int) =
    inherit RareStone(size, 9999, true)
    let mutable size = size
    member this.TryKillSuperman (myAim: Superman) = 
        if size > 10 then myAim.Death()
    override this.Sell = printfn "%A" "You sell Kryptonit?! You are fool!", size <- 0

type PhilosophersStone(authenticity: bool) =
    inherit RareStone(10, 0, authenticity)
    member this.Alchemy(stone: Stone) = 
        let AlchemyResult = new Mineral("Gold", stone.Size, 100, authenticity)
        AlchemyResult
    override this.Sell = failwith "Unexpected choice..."

(*
let mySuperman = new Superman("Klark", true)
let myStone = new Kryptonit(15)
myStone.TryKillSuperman(mySuperman)
let myStone2 = new Mineral("Paper", 15, 25, false)
myStone2.Check()
*)