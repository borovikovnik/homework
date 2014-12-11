module Clouds

open Interfaces

type Daylight() = 
    interface IDaylight  with
        member x.Current = Morning

type Luminary() =
    interface ILuminary  with
        member x.IsShining = true

type Wind() =
    interface IWind  with
        member x.Speed = 0

type Courier() =
    interface ICourier  with
        member x.GiveBaby creature = ignore()

type Magic() =
    interface IMagic  with
        member x.CallStork() = new Courier() :> ICourier
        member x.CallDaemon() = new Courier() :> ICourier

type Creature(creature) = 
    interface ICreature with
        member x.Type = creature

type Cloud(daylight : IDaylight, luminary: ILuminary, wind : IWind) =
    member private x.InternalCreate() =
        match luminary.IsShining, daylight.Current, wind.Speed with
        | true, Noon, s when (s >= 0) && (s < 3) -> 
            new Creature(CreatureType.Bat)
        | true, Noon, 7 -> 
            new Creature(CreatureType.Bearcub)
        | true, Evening, s when (s > 3) && (s < 9) -> 
            new Creature(CreatureType.Piglet)
        | true, Evening, 10 -> 
            new Creature(CreatureType.Hedgehog)
        | true, Night, 0 -> 
            new Creature(CreatureType.Bat)
        | true, Night, s when (s > 0) && (s <= 10) -> 
            new Creature(CreatureType.Kitten)
        | false, Morning, 1 -> 
            new Creature(CreatureType.Bat)
        | false, Morning, 2 -> 
            new Creature(CreatureType.Puppy)
        | false, Morning, 5 -> 
            new Creature(CreatureType.Piglet)
        | false, Noon, 9 -> 
            new Creature(CreatureType.Kitten)
        | false, Evening, s when (s >= 0) && (s <= 10) -> 
            new Creature(CreatureType.Hedgehog)
        | false, Night, 6 -> 
            new Creature(CreatureType.Bearcub)
        | _, _, s when (s >= 0) && (s <= 10) ->
            new Creature(CreatureType.Balloon) 
        | _, _, _ -> raise <| new System.NotImplementedException()

    member x.Create() =
        let creature = x.InternalCreate() :> ICreature
        let magic = new Magic() :> IMagic 
        match luminary.IsShining, creature.Type with
        | true, Kitten -> magic.CallDaemon().GiveBaby(creature)   
        | true, _ -> magic.CallStork().GiveBaby(creature)
        | false, Hedgehog -> magic.CallStork().GiveBaby(creature)
        | false, _ -> magic.CallDaemon().GiveBaby(creature) 

        creature