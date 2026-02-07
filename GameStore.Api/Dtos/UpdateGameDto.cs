namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    string Name,
    string Genre,
    double Price,
    DateOnly ReleaseDate
);
