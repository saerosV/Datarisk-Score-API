module Routing

open Giraffe
open Giraffe.HttpHandlers
open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http
open Models


// let score = System.Random().Next(1,1000)
// let created_at = System.DateTime.Now

let handleCPF (next: HttpFunc) (cpf: HttpContext) = 
    let score = System.Random().Next(1,1000)





let webApp =
    choose [
        GET  >=> route "/score" >=> 
        POST >=> route "/score" >=> calculateScore
        
        setStatusCode 404 >=> text "Not Found" ]