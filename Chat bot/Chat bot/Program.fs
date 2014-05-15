/////////////////////////////
//Borovikov Nikita (c) 2014//
//Chat bot///////////////////
/////////////////////////////

module ChatBot

open elizaBotLibrary
open System.Windows.Forms
open System.Drawing

let rec readMsg (msg: string, answer: string) = 
    let frm = new Form(Width = 500, Height = 160, Location = Point(900,900))
    let lbY = new Label(Text = sprintf "You: %A" msg, Width = 300, Top = 10, Height = 20)
    let lbE = new Label(Text = sprintf "Eliza: %A" answer, Width = 300, Top = 30, Height = 20)
    let tb = new TextBox(Text = " ", Top = 50, Width = 200, Height = 20)
    let btSend = new Button(Text="Sent", Top = 80, Width = 500, Height = 20)
    let btExit = new Button(Text="Exit", Top = 100, Left = 211, Height = 20)
    btSend.Click.Add (fun _ -> let userMsg = tb.Text
                               frm.Close()
                               readMsg (userMsg, meliza_response userMsg)
                               )
    btExit.Click.Add (fun _ -> frm.Close()
                               )
    frm.Controls.AddRange [| lbY; lbE; tb; btSend; btExit |]
    frm.Show()


readMsg("", "Enter your message...")

System.Console.ReadKey() |> ignore
