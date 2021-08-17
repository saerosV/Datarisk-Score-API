module Routing

open Giraffe
open Giraffe.HttpHandlers
open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http
open Models

// string -> bool
// Returns true if the given cpf already exists in the database, 
// false otherwise.
let cpfExists (cpf : string) =
    // search for matching id in database
    // Found = true
    // notFound = false
    // SELECT user FROM users WHERE cpf =


let handleGET (next: HttpFunc) (ctx: HttpContext) =
    let cpf = ctx.BindQueryString<CPF>()

    match cpfExists(cpf) with
    | false -> RequestErrors.BAD_REQUEST "The cpf is not on the database"
    | _ -> Successful.OK // add function the gets the score and created_at 


let handlePOST (next: HttpFunc) (ctx: HttpContext) =
    let cpf = ctx.BindQueryString<CPF>()

    match cpfExists(cpf) with
    | true -> RequestErrors.CONFLICT "This cpf already exists in the database"
    | _ -> Successful.CREATED createNewUser cpf

let createNewUser cpf =
    let score = System.Random().Next(1,1000)
    let created_at = System.DateTime.Now.ToString()
    
    let userInfo =
    {
        CPF = cpf
        Score = score
        CreatedAt = created_at
    }

    // save userInfo to database
    score

let getScoreAndDate cpf =
    // 


let webApp =
    choose [
        GET  >=> route "/score" >=> handleGET
        POST >=> route "/score" >=> handlePOST
        setStatusCode 404 >=> text "Not Found" ]