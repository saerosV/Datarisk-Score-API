module Routing

open Giraffe
open Giraffe.HttpHandlers
open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http
open Models


let webApp =
    choose [
        GET  >=> route "/score" >=> 
        POST >=> route "/score" >=>
        
        setStatusCode 404 >=> text "Not Found" ]