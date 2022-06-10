// | Author(s)       | date    |
//------------------------------
// | JRA             | June 8  |
// | JRA             | June 9  |

// Aesthethic 2: A parent should be centred over its offspring.
module AestheticRule2
open FsCheck
open FsCheck.NUnit
open TreeTypes
open PositionedTree

let rec centeringProperty (PosNode (_, pos, subtrees) as tree ) =
    match subtrees with
    | [] -> true
    | sts ->
        let getSubtreePositions (PosNode (_, pos, _)) = pos
        let subtreePositions = subtrees |> List.map getSubtreePositions
        if List.min subtreePositions = - List.max subtreePositions then
            sts |> List.forall centeringProperty
        else
            false

let testProperty posTreePropertyFunction tree =
    designTree tree |> posTreePropertyFunction
    
//[<Property>]
open NUnit.Framework
[<Test>]
let positioningOffsprings () =
    Assert.IsTrue(testProperty centeringProperty Program.t)
