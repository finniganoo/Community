open System.Drawing
open System.IO
open System
open System.Windows.Forms.VisualStyles

type Plotter = {
    position: int*int
    color: Color
    direction: float
    bitmap: Bitmap
    }

let naiveLine (x1, y1) (plotter: Plotter) = 
    let updatedPlotter = { plotter with position = (x1,y1) }
    let (x0,y0) = plotter.position
    printfn "%i %i -- %i %i" x0 y0 x1 y1
    let xLen = float (x1-x0)
    let yLen = float (y1-y0)
    let x0,y0,x1,y1 = if x0 > x1 then x1,y1,x0,y0 else x0,y0,x1,y1
    printfn "%i %i -- %i %i" x0 y0 x1 y1
    if xLen <> 0.0 then 
        for x in x0..x1 do
            let proportion = float (x-x0) / xLen
            let y = int (Math.Round ( proportion * yLen )) + y0
            //printfn "    %i -- %i" x y
            plotter.bitmap.SetPixel (x,y,plotter.color)
    let x0,y0,x1,y1 = if y0 > y1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if yLen <> 0.0 then 
        for y in y0..y1 do
            let proportion = float (y-y0) / yLen
            let x = int (Math.Round (proportion * xLen)) + x0
            //printfn "    %i -- %i" x y
            plotter.bitmap.SetPixel (x,y,plotter.color)
    //printfn "%i %i -- %i %i" x0 y0 x1 y1
    updatedPlotter

let turn amt plotter =
    let newDir = plotter.direction + amt
    let angled = { plotter with direction = newDir }
    //printfn "turn %A" angled.direction
    angled

let move dist plotter =
    let currPos = plotter.position
    let angle = plotter.direction
    let startX = fst currPos
    let startY = snd currPos
    //printfn "calc %i %i dist %i" startX startY dist
    let rads = (angle - 90.0) * Math.PI/180.0
    let endX = (float startX) + (float dist) * cos rads
    //printfn "calc %f + %f * %f == %f" (float startX) (float dist) (cos rads) endX
    let endY = (float startY) + (float dist) * sin rads
    //printfn "calc %f + %f * %f == %f" (float startY) (float dist) (sin rads) endY
    //printfn "original endX %.17f endY (%f)" endX endY
    //printfn "calling naiveLine %i %i " (int (Math.Round(endX))) (int (Math.Round(endY)))
    let plottered = naiveLine (int (Math.Round(endX)), int (Math.Round(endY))) plotter
    //printfn "draw %A pixel" dist
    plottered

let pathAndFileName = 
    Path.Combine(__SOURCE_DIRECTORY__,"polygons.png")

let bitmap = new Bitmap(60,40)

let fill color (bitmap: Bitmap) =
    for x in 0..bitmap.Width-1 do
        for y in 0..bitmap.Height-1 do
            bitmap.SetPixel(x,y,color)
fill Color.BlanchedAlmond bitmap

let initialPlotter = {
    position = (0,0)
    color = Color.Red
    direction = 0.0
    bitmap = bitmap
    }

printfn "side margin: %A" (int ((bitmap.Width-60)/2))
printfn "top/bottom margin: %A" (int ((bitmap.Height-30)/2))

let rectangle x y = 
    { initialPlotter with position = (int ((bitmap.Width-x)/2), int ((bitmap.Height-y)/2) ) }
    |> turn 90.0
    |> move x
    |> turn 90.0
    |> move y
    |> turn 90.0
    |> move x
    |> turn 90.0
    |> move y

let rect = rectangle 40 20

rect.bitmap.Save(pathAndFileName)

