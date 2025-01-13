using API.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName ="GetGame";
List<GameDto> games = [
    new (
        1, 
        "Street Fighter 11",
        "Fighting",
        19.99M,
        new DateOnly(1999,04,09)
        ),
    new (
        2, 
        "E Football",
        "Sports",
        49.99M,
        new DateOnly(2025,04,09)
    ),
    new (
        3, 
        "FIFA 24",
        "Sports",
        59.79M,
        new DateOnly(2024,12,09)
    ),
];
app.MapGet("games", () => games).WithName("Games");

app.MapGet("games/{id:int}", (int id) =>games.FirstOrDefault(g => g.Id == id)).WithName(GetGameEndpointName);
app.MapGet("/", () => "Hello World!");

app.MapPost("games", (CreateGameDTO newGame) =>
{
    GameDto game = new(
        games.Capacity + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    games.Add(game);
    
    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

app.Run();