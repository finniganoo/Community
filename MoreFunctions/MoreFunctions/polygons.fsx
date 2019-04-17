﻿
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
    let xLen = float (x1-x0)
    let yLen = float (y1-y0)

    let x0,y0,x1,y1 = if x0 > x1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if xLen <> 0.0 then 
        for x in x0..x1 do
            let proportion = float (x-x0) / xLen
            let y = int (Math.Round ( proportion * yLen )) + y0
            printfn "%i" y
            plotter.bitmap.SetPixel (x,y,plotter.color)
    let x0,y0,x1,y1 = if y0 > y1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if yLen <> 0.0 then 
        for y in y0..y1 do
            let proportion = float (y-y0) / yLen
            let x = int (Math.Round (proportion * xLen)) + x0
            printfn "%i" x
            plotter.bitmap.SetPixel (x,y,plotter.color)
    updatedPlotter

let turn amt plotter =
    let newDir = plotter.direction + amt
    let angled = { plotter with direction = newDir }
    printfn "%A" angled
    angled

let move dist plotter =
    let currPos = plotter.position
    let angle = plotter.direction
    let startX = fst currPos
    let startY = snd currPos
    let rads = (angle - 90.0) * Math.PI/180.0
    let endX = (float startX) + (float dist) * cos rads
    let endY = (float startY) + (float dist) * sin rads
    let plottered = naiveLine (int endX, int endY) plotter
    printfn "%A" plottered
    plottered

let pathAndFileName = 
    Path.Combine(__SOURCE_DIRECTORY__,"polygons.png")

let bitmap = new Bitmap(100,100)

let initialPlotter = {
    position = (20,35)
    color = Color.Orange
    direction = 90.0
    bitmap = bitmap
    }

let rectangle x y = 
    initialPlotter
    |> move x
    |> turn 90.0
    |> move y
    |> turn 90.0
    |> move x
    |> turn 90.0
    |> move y

let rect = rectangle 60 30

rect.bitmap.Save(pathAndFileName)

