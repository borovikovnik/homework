/////////////////////////////
//Borovikov Nikita (c) 2013//
//Web'CPS////////////////////
/////////////////////////////

module webCPS

open WebR
open MapCPS

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
    map getUrl urls (fun x -> Seq.map (fun y -> takePic y) x |> Seq.distinct |> Seq.concat |> Seq.toList |> c)

let marker = ref false

findPic urls (fun v -> printfn "%A" v; marker := true)

while not !marker do System.Threading.Thread.Sleep(100)
