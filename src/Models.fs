module Models

/// TODO:
/// Constrain CPF

[<CLIMutable>]
type CPF =

    {
        CPF : string
    }

[<CLIMutable>]
type NewUser =
    {
        CPF : string
        Score : int
        CreatedAt: string
    }
