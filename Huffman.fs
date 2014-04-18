/////////////////////////////
//Borovikov Nikita (с) 2014//
//Huffman////////////////////
/////////////////////////////


module Huffman

////////////////////////

type CodeTree = 
  | Fork of CodeTree * CodeTree * char list * int
  | Leaf of char * int


////////////////////////

let weight tree =
    match tree with
    | Fork(_, _, _, wg) -> wg
    | Leaf(_, wg) -> wg

////////////////////////

let chars tree =
    let rec chars' tree acc =
        match tree with
        |Fork(l, r, _, _) ->
            (chars' l acc)@(chars' r acc)
        |Leaf(ch,_) -> ch::[]
    chars' tree []

////////////////////////

let stringZchars str = Seq.toList str

////////////////////////

let rec add sumb overlist: (char * int) list =
    match overlist with
    | [] -> (sumb,1)::[]
    | (ch, num)::tl -> 
        if ch = sumb then
            (sumb, num+1)::tl
        else
            (ch, num)::(add sumb tl)

let times charlist = 
    let rec times' charlist overlist =
        match charlist with
        | [] -> overlist
        | hd::tl -> times' tl (add hd overlist)
    times' charlist []

////////////////////////

let rec addLeaf (ch, num) (codeTreeList: CodeTree list) =
    match codeTreeList with
        | [] -> Leaf(ch, num)::[]
        | Leaf(a, b)::tl ->
            if num > b then
                Leaf(a, b)::addLeaf (ch, num) tl
            else 
                CodeTree.Leaf(ch, num)::codeTreeList
        | _ -> failwith "Wrong structure of tree"

let makeOrderedLeafList overlist =
    let rec MOLL overlist codeTreeList=
        match overlist with
        | [] -> codeTreeList
        | (ch, num)::tl -> MOLL tl (addLeaf (ch, num) codeTreeList)
    MOLL overlist []

////////////////////////

let singltone (cLT: CodeTree list) =
    match cLT with
    |hd::[] -> true
    |_ -> false

////////////////////////

let rec combine cTL =
    if not (singltone cTL) then
        match cTL with
        | a::b::tl -> 
            let temp = Fork (a, b, (chars a) @ (chars b), weight a + weight b)::tl
            combine (List.sortBy (fun elem -> weight elem) temp)
        | _ -> failwith "End condition was corrupted"
    else cTL

////////////////////////
   
let createCodeTree initStr = 
    let list = combine (makeOrderedLeafList  (times (stringZchars initStr)))
    match list with
    | hd::[] -> hd
    | _ -> failwith "Wrong input"

////////////////////////

let demoTree2 = createCodeTree "vsvsvvkppkppppkskvkvk"
