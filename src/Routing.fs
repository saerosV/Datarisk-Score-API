module Routing

open Giraffe
//open Giraffe.HttpHandlers
//open Giraffe.HttpContextExtensions
open Microsoft.AspNetCore.Http
open Models
open Npgsql.FSharp


let connectionString: string =
    Sql.host "localhost"
    |> Sql.database "datarisk"
    |> Sql.username "postgres"
    |> Sql.password "postgres"
    |> Sql.port 5432
    |> Sql.formatConnectionString


[<Literal>]
let cpfExistsQuery =
    """
SELECT EXISTS (
    SELECT 1
    FROM users
    WHERE cpf = @cpf
) AS cpf_exists
"""


// Returns true if the given cpf already exists in the database,
// false otherwise.
let cpfExists (cpf: string) : bool =
    connectionString
    |> Sql.connect
    |> Sql.query cpfExistsQuery
    |> Sql.parameters [ "@cpf", Sql.text cpf ]
    |> Sql.executeRow (fun read -> read.bool "cpf_exists")


let addUser cpf =
    let score = System.Random().Next(1, 1000)
    let created_at = System.DateTime.Now.ToString()

    connectionString
    |> Sql.connect
    |> Sql.query "INSERT INTO users (cpf, score, created_at) VALUES (@cpf, @score, @created_at)"
    |> Sql.parameters [ "@cpf", Sql.text cpf
                        "@score", Sql.int score
                        "@created_at", Sql.text created_at ]
    |> Sql.executeNonQuery


let handleGET: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let cpf = ctx.BindQueryString<string>()

        //match cpfExists (cpf) with
        //| false -> RequestErrors.BAD_REQUEST "The cpf is not on the database"
        //| _ -> Successful.OK "add function the gets the score and created_at"


let handlePOST : HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
    let cpf = ctx.BindQueryString<string>()

    //match cpfExists (cpf) with
    //| true -> RequestErrors.CONFLICT "This cpf already exists in the database"
    //| _ -> Successful.CREATED addUser (cpf)


let webApp =
    choose [ GET >=> route "/score" >=> handleGET
             POST >=> route "/score" >=> handlePOST
             setStatusCode 404 >=> text "Not Found" ]
