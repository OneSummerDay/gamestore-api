using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = new List<GameDto>
{
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platform", 59.99, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 59.99, new DateOnly(2018, 10, 26)),
    new GameDto(4, "The Witcher 3: Wild Hunt", "Action RPG", 39.99, new DateOnly(2015, 5, 19)),
    new GameDto(5, "Minecraft", "Sandbox", 26.95, new DateOnly(2011, 11, 18))
};

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) =>
{
    var game = games.FirstOrDefault(game => game.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
});

app.MapPost("/games", (GameDto newGame) =>
{
    int newId = games.Max(game => game.Id) + 1;
    var gameToAdd = new GameDto(newId, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
    games.Add(gameToAdd);
    return Results.Created($"/games/{newId}", gameToAdd);
});

app.Run();
