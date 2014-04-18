(*
weight: CodeTree -> int
chars: CodeTree -> char list
let makeCodeTree left right =
    Fork(left,right,chars left @ chars right, weight left, weight right)
let stringZchars str = Seq.toList str
times: char list -> (char*int) list
makeOrderedLeafList: (char*int) list -> CodeTree list // листья упорядочены по в весу
singltone: CodeTree list -> bool
combine: CodeTree list -> CodeTree list
(until singltone combine) trees 

*)
module Huffman
//<-0, -> 1
type CodeTree = 
  | Fork of CodeTree * CodeTree * char list * int
  | Leaf of char * int


// code tree
let demoStr = "aabcabcd"
let demoTree = Fork(Fork(Fork(Leaf('d', 1), Leaf('c', 2), ['c';'d'], 3), Leaf('b', 2), ['b';'c';'d'], 5), Leaf('a', 3), ['a';'b';'c';'d'], 8)

let stringZchars str = Seq.toList str


let rec add sumb overlist: (char*int) list =
    match overlist with
    | [] -> (sumb,1)::[]
    | hd::tl -> 
        match hd with
        | (ch,num) -> 
            if ch = sumb then
                (sumb,num+1)::tl
            else
                hd::(add sumb tl)

let times charlist = 
    let rec times' charlist overlist =
        match charlist with
        | [] -> overlist
        | hd::tl -> times' tl (add hd overlist)
    times' charlist []

let addList: (char*int) (Leaf list) -> Leaf list
let rec addLeaf pair CodeTreeList =
    match CodeTreeList with
        | [] -> (Leaf:= pair)::[]
        | hd::tl ->
            if snd pair > snd hd then
                addLeaf pair tl
            else 

let makeOrderedLeafList overlist =
    let rec MOLL overlist CodeTreeList =
        match overlist with
        | [] -> CodeTreeList
        | hd::tl -> addLeaf hd CodeTreeList
            
            
    

let demoList = times (stringZchars demoStr)
    

