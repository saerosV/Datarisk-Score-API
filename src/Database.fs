module Database

open Npgsql.FSharp
open Routing

let connectionString : string =
    Sql.host "localhost"
    |> Sql.database "datarisk"
    |> Sql.username "postgres"
    |> Sql.password "postgres"
    |> Sql.port 5432
    |> Sql.formatConnectionString

// string -> bool
// Returns true if the given cpf already exists in the database, 
// false otherwise.
let cpfExists (cpf : string) =
    // search for matching id in database
    // Found = true
    // notFound = false
    // SELECT user FROM users WHERE cpf =
    cpf
