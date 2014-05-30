/////////////////////////////
//Borovikov Nikita (c) 2013//
//Dictionary/////////////////
/////////////////////////////

module Map 

open System
open System.Collections
open System.Collections.Generic
open Microsoft.FSharp.Core

type private Tree<'key, 'value when 'key: comparison and 'value: equality>=
| Fork of Tree<'key, 'value> * Tree<'key, 'value> * 'key * 'value * int
| Empty

//////////////////////////////

let private isEmpty tree =
    tree = Empty

let private left tree =
    match tree with
    | Fork(l, _, _, _, _) -> l
    | Empty -> failwith "Empty" 

let private right tree =
    match tree with
    | Fork(_, r, _, _, _) -> r
    | Empty -> failwith "Empty" 

let private key tree =
    match tree with 
    | Fork(_, _, k, _, _) -> k
    | Empty -> failwith "Empty" 

let private value tree =
    match tree with 
    | Fork(_, _, _, v, _) -> v
    | Empty -> failwith "Empty" 

let private height tree =
    match tree with
    | Fork(_, _, _, _, h) -> h
    | Empty -> 0

let private newFork l r k v =
    let m = max (height l) (height r)
    Fork(l, r, k, v, m + 1)

let private toList tree =
    let rec toList' tree acc = 
        match tree with 
        | Fork(l, r, k, v, _) -> toList' l ((k, v)::toList' r acc)
        | Empty -> acc
    toList' tree []

//////////////////////////////

let private rotateLeft tree= 
    match tree with
    | Fork(l, Fork(l2, r2, k2, v2, h2), k, v, h) ->
        newFork (newFork l l2 k v) r2 k2 v2 
    | _ -> failwith "Balance faild"
         
let private rotateRight = function
    | Fork(Fork(l2, r2, k2, v2, h2), r, k, v, h) ->
        newFork l2 (newFork r2 r k v) k2 v2 
    | _ -> failwith "Balance faild"

let private rebalance tree =
    match tree with
    | Fork(l, r, k, v, h) ->
        if height r > height l + 1 then
            if height (left r) > height (right r) then 
                newFork l (rotateRight r) k v 
            else 
                rotateLeft tree
        elif height l > height r + 1 then
            if height (right l) > height(left l) then 
                newFork l (rotateLeft r) k v 
            else 
                rotateRight tree
        else tree
    | Empty -> Empty

//////////////////////////////

let rec private add k v tree =
    match tree with
    | Fork(l, r, k', v', _) ->
        if k < k' then 
            newFork (add k v l) r k' v'
        else 
            newFork l (add k v r) k' v'
        |> rebalance
    | Empty -> Fork(Empty, Empty, k, v, 1)

//////////////////////////////

let rec private findInLeft tree =
    match tree with
    | Fork(l, r, k, v, _) -> 
        if height r = 0 then
            tree
        else 
            findInLeft r
    | Empty -> failwith "No Fork here"

let rec private findInRight tree =
    match tree with
    | Fork(l, r, k, v, _) -> 
        if height l = 0 then
            tree
        else 
            findInRight l
    | Empty -> failwith "No Fork here"

let rec private remove k tree =
    match tree with
    | Fork(l, r, k', v', _) ->
        if k <> k' then
            if k < k' then 
                newFork (remove k l) r k' v'
            else 
                newFork l (remove k r) k' v'
            |> rebalance
        elif isEmpty l && isEmpty r then 
            Empty
        elif height l > height r then
            let a = findInRight l
            newFork (remove (key a) l) r (key a) (value a) 
        else
            let a = findInLeft r
            newFork l (remove (key a) r) (key a) (value a)  
    | Empty -> Empty

//////////////////////////////

let private getEnumerator(tree) =
    let list = toList tree
    let currList = ref list
    let isStart = ref true
    let current() =
        if !isStart then failwith("Not found")
        else (!currList).Head
    {
     new IEnumerator<'key * 'value> with
            member this.Current = current()

        interface IEnumerator with
            member this.Current = current() :> obj
            member this.MoveNext() =
                if !isStart then isStart := false
                currList := (!currList).Tail
                not (!currList).IsEmpty
            member this.Reset() =
                isStart := true
                currList := list

        interface System.IDisposable with
            member this.Dispose() = ()
    }

//////////////////////////////

type Map<'key, 'value  when 'key: comparison and 'value: equality> private (tree: Tree<'key, 'value>) = 
    member this.IsEmpty = isEmpty tree 

    member this.Count = 
        let rec count tree =
            match tree with
            | Fork(l, r, k, v, _) -> 1 + count l + count r
            | Empty -> 0
        count tree

    member this.Add key value = new Map<_, _>(add key value tree)

    member this.Remove key = new Map<_, _>(remove key tree)

    member this.TryFind key =
        let rec tryFind k tree =
            match tree with
            | Fork(l, r, k', v', _) -> 
                if k = k' then 
                    Some(v')
                elif k > k' then 
                    tryFind k r
                else 
                    tryFind k l
            | Empty -> None
        tryFind key tree

    member this.ContainsKey key =
        let rec containsKey k tree =
            match tree with
            | Fork(l, r, k', v', _) -> 
                if k = k' then 
                    true
                else 
                    containsKey k l || containsKey k r
            | Empty -> false
        containsKey key tree

    member this.Item key =
        let rec item k tree =
            match tree with
            | Fork(l, r, k', v', _) -> 
                if k = k' then v'
                elif k > k' then 
                    item k r
                else 
                    item k l
            | Empty -> failwith "No element"
        item key tree

    override this.ToString() =
        let rec toString tree =
            match tree with
            | Fork(l, r, k, v, _) -> 
                "Fork(" + toString l + ", " + toString r + ", " + k.ToString() + ", " + v.ToString() + ")"
            | Empty -> "Empty"
        toString tree

    override this.GetHashCode() =
        let rec getHashCode list =
            match list with
            |(k, v)::tl -> k.GetHashCode() * v.GetHashCode() * (getHashCode tl) % 42043
            |[] -> 1
        getHashCode (toList tree)
    
    member private this.GetEnumerator() = getEnumerator(tree)
    interface IEnumerable<'key * 'value> with
        member this.GetEnumerator() = this.GetEnumerator()
    interface IEnumerable with
        member this.GetEnumerator() = this.GetEnumerator() :> IEnumerator

    override this.Equals x = 
        match x with 
        | :? Map<'key, 'value> as y -> this.Count = y.Count && Seq.forall2 (=) this y
        | _ -> false