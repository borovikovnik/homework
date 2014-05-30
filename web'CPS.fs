/////////////////////////////
//Borovikov Nikita (c) 2013//
//Web'CPS////////////////////
/////////////////////////////

open WebR

let urls =
    [
     "http://www.imagesfestival.com/";
     "http://www.hdwallpapers.in/";
     "http://wallpaperswide.com"
    ]

let condition site =
    let rec condition' (site: string) (pos: int) acc =
        let index = site.IndexOf("<img", pos)
        if index <> -1 then
            condition' site (index + 4) (acc + 1)
        else
            acc > 5
    condition' site 0 0

let takePic site =
    let rec takePic' (site: string) (pos: int) =
        if condition site then
            let index = site.IndexOf("<img", pos)
            if index <> -1 then
                let imgFrom = site.IndexOf("src=", index) + 5
                let imgTo = site.IndexOf("\"", imgFrom)
                site.Substring(imgFrom, imgTo - imgFrom) :: takePic' site imgTo
            else []
        else []
    takePic' site 0


let rec findPic urls c =
    match urls with
    | hd :: tl -> getUrl hd (fun x -> findPic tl (fun y -> (takePic x @ y) |> Seq.distinct |> Seq.toList |> c ))
    | [] -> c []

findPic urls (printfn "%A")

System.Console.ReadKey() |> ignore