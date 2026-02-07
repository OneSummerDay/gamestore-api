namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    string Name,
    string Genre,
    double Price,
    DateOnly ReleaseDate
);
