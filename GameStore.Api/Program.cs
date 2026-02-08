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

app.MapPost("/games", (CreateGameDto newGame) =>
{
    var game = new GameDto(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
    games.Add(game);
    return Results.Created($"/games/{game.Id}", game);
});

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var game = games.FirstOrDefault(game => game.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    var updatedGameDto = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);
    games[games.IndexOf(game)] = updatedGameDto;
    return Results.Ok(updatedGameDto);
});

app.MapDelete("/games/{id}", (int id) =>
{
    var game = games.FirstOrDefault(game => game.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    games.Remove(game);
    return Results.NoContent();
});

app.Run();
