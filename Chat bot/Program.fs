/////////////////////////////
//Borovikov Nikita (c) 2014//
//Chat bot///////////////////
/////////////////////////////

module ChatBot

open elizaBotLibrary
open System.Windows.Forms
open System.Drawing

let readMsg = 
    let frm = new Form(Width = 500, Height = 160, Location = Point(900,900))
    let lbY = new Label(Text = sprintf "You: ...", Width = 500, Top = 10, Height = 20)
    let lbE = new Label(Text = sprintf "Eliza: Enter your message...", Width = 500, Top = 30, Height = 15)
    let tb = new TextBox(Text = " ", Top = 50, Width = 200, Height = 20)
    let btSend = new Button(Text="Sent", Top = 80, Left = 45, Width = 400, Height = 20)
    let btBeep = new Button(Text="BEEP", Top = 50, Left = 300, Height = 20)
    let btExit = new Button(Text="Exit", Top = 100, Left = 207, Height = 20)

    tb.Focus |> ignore
    frm.Icon <- new System.Drawing.Icon("main.ico")
    let answer userMsg =
        lbY.Text <- "You: " + userMsg 
        lbE.Text <- "Eliza: " + (meliza_response userMsg)
        tb.Text <- ""
    
    tb.KeyDown.Add (fun key -> if key.KeyCode = Keys.Escape then 
                                   frm.Close()
                                   Application.Exit()
                               else if key.KeyCode = Keys.Enter then 
                                   answer tb.Text
                               )

    btSend.Click.Add (fun _ -> answer tb.Text)

    btBeep.Click.Add (fun _ -> System.Console.Beep()
                               lbE.Text <- " BEEP!! "
                               btBeep.Hide()
                               btBeep.Show()
                               )

    btExit.Click.Add (fun _ -> frm.Close()
                               Application.Exit()
                               )
    tb.LostFocus.Add (fun _ -> tb.Focus|> ignore)
    frm.Controls.AddRange [| lbY; lbE; tb; btSend; btExit; btBeep |]
    frm.Show()


Application.Run()
