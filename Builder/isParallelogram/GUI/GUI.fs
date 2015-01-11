//////////////////////////////
//Borovikov Nikita (с) 2014///
//Intersection of rectangles//
//////////////////////////////

module GUI

open System
open System.Drawing
open System.Windows.Forms
open Pgram

type Config private() =
    static let formWidth = 1000
    static let formHeight = 600
    static let buttonWidth = 100
    static let buttonHeight = 70
    static let tbWidth = 100
    static let tbHeight = 30
    static let formColor = Color.Khaki
    static let borderX = 20
    static let borderY = 30
    static let guiName = "Paralellograms"

    static member FormWidth = formWidth
    static member FormHeight = formHeight
    static member FormColor = formColor
    static member ButtonWidth = buttonWidth
    static member ButtonHeight = buttonHeight
    static member TbWidth = tbWidth
    static member TbHeight = tbHeight
    static member BorderX = borderX
    static member BorderY = borderY
    static member GUIName = guiName
  

type Polygon(points : PointF[]) =
    static let rnd = new Random()
    let color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255))
    let brush = new SolidBrush(color)
    let p = Array.create 4 (new Point(0.0, 0.0))
    do  for i in 0 .. 3 do
            p.[i] <- new Point(float points.[i].X, float points.[i].Y)
        let prg = Pgram.canonPoints(new Pgram(p.[0], p.[1], p.[2], p.[3]))
        points.[0] <- new PointF(float32 prg.UL.X, float32 prg.UL.Y)
        points.[1] <- new PointF(float32 prg.UR.X, float32 prg.UR.Y)
        points.[2] <- new PointF(float32 prg.DR.X, float32 prg.DR.Y)
        points.[3] <- new PointF(float32 prg.DL.X, float32 prg.DL.Y)
    member this.Draw(g : Graphics) = g.FillPolygon(brush, points)

let check(p' : PointF[]) = 
    let p = Array.create 4 (new Point(0.0, 0.0))
    for i in 0 .. 3 do
        p.[i] <- new Point(float p'.[i].X, float p'.[i].Y)
    let prg = new Pgram(p.[0], p.[1], p.[2], p.[3])

    if (Pgram.isParallelogram prg) then
        "It's Parallelogram!!!"
    else
        "Oh... No. It isn't Parallelogram"

        
    


let MainForm =
    let frm = new Form(
                        Text = Config.GUIName,
                        BackColor = Config.FormColor,
                        Width = Config.FormWidth,
                        FormBorderStyle = FormBorderStyle.Fixed3D,
                        Height = Config.FormHeight,
                        MinimizeBox = false,
                        MaximizeBox = false
                       ) 

    let btnTop = Config.FormHeight/ 5 * 4
    let btnLeft = Config.FormWidth / 10 * 9
    let btnWidth = Config.ButtonWidth
    let btnHeight = Config.ButtonHeight

    let points = Array.create 4  (new PointF())

    let lbsX = Array.create 4 (new Label())
    let tbsX = Array.create 4 (new TextBox())
    let lbsY = Array.create 4 (new Label())
    let tbsY = Array.create 4 (new TextBox())
    for i in 0 .. 3 do
        let lbx' = new Label(
                                Top = btnTop, 
                                Width = Config.TbWidth, 
                                Height = Config.TbHeight,
                                Text = "Input coordinate x" + (i+1).ToString(),
                                Left = 220*i
                            )
        lbsX.[i] <-lbx'

        let lby' = new Label(
                                Top = btnTop + Config.TbHeight + 10, 
                                Width = Config.TbWidth, 
                                Height = Config.TbHeight,
                                Text = "Input coordinate y" + (i+1).ToString(),
                                Left = 220*i
                            )
        lbsY.[i] <- lby'

        let tbx' = new TextBox(
                                Top = btnTop, 
                                Text = "0.0",
                                Width = Config.TbWidth, 
                                Height = Config.TbHeight,
                                Left = 110 + 220*i
                              )
        tbsX.[i] <- tbx'
        
        let tby' = new TextBox(
                                Top = btnTop + Config.TbHeight + 10, 
                                Text = "0.0",
                                Width = Config.TbWidth, 
                                Height = Config.TbHeight,
                                Left = 110 + 220*i
                              )
        tbsY.[i] <- tby'

        frm.Controls.AddRange [| lbsX.[i]; lbsY.[i]; tbsX.[i]; tbsY.[i] |]

    let btn = new Button(
                            Text="Input", 
                            Top = btnTop, 
                            Left = btnLeft, 
                            Width = Config.ButtonWidth, 
                            Height = Config.ButtonHeight,
                            BackColor = Color.BlanchedAlmond
                        )
    let answer = new Label(
                                Top = Config.FormHeight / 2,
                                Left = Config.FormWidth / 2
                          )

    btn.Click.Add (fun _ -> 
                            for i in 0 .. 3 do
                                let currP = new PointF(float32 tbsX.[i].Text, float32 tbsY.[i].Text)
                                points.[i] <- currP
                            answer.Text <- check(points)
                            frm.Refresh()
                  )
    btn.KeyDown.Add (fun key -> if key.KeyCode = Keys.Escape then 
                                       frm.Close()
                                       Application.Exit()
                    )
                                     
    frm.Paint.Add(fun e -> 
                    e.Graphics.SmoothingMode <- Drawing2D.SmoothingMode.HighQuality
                    e.Graphics.Clear(Config.FormColor)
                    let polygon = new Polygon(points)
                    polygon.Draw(e.Graphics)
                 )   

    frm.Controls.AddRange  [|btn; answer|]
    frm.Height <- frm.Height + Config.BorderY
    frm.Width <- frm.Width + Config.BorderX
    frm.Show()


Application.Run()

