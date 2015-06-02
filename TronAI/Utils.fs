module TronAI.Utils

open Engine

let randomGame' (r: System.Random) (size: Size) (bots: (Player * Bot) seq) =
    let initialWorld = initializeGame r size (bots |> Seq.map fst)

    Engine.game initialWorld bots

let randomGame size bots =
    randomGame' (new System.Random()) size bots