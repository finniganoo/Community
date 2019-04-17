open System.Drawing
open System.IO
open System

let naiveLine (x0,y0) (x1, y1) color (bitmap:Bitmap) = 
    let xLen = float (x1-x0)
    let yLen = float (y1-y0)

    let x0,y0,x1,y1 = if x0 > x1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if xLen <> 0.0 then 
        for x in x0..x1 do
            let proportion = float (x-x0) / xLen
            let y = int (Math.Round ( proportion * yLen )) + y0
            printfn "%i" y
            bitmap.SetPixel (x,y,color)
    let x0,y0,x1,y1 = if y0 > y1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if yLen <> 0.0 then 
        for y in y0..y1 do
            let proportion = float (y-y0) / yLen
            let x = int (Math.Round (proportion * xLen)) + x0
            printfn "%i" x
            bitmap.SetPixel (x,y,color)

let pathAndFileName = 
    Path.Combine(__SOURCE_DIRECTORY__,"refactor.png")

let bitmap = new Bitmap(64,64)

naiveLine (32,32) (44,44) Color.Goldenrod bitmap

bitmap.Save (pathAndFileName)




