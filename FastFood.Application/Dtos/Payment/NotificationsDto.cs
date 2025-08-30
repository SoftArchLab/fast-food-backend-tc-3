namespace FastFood.Application.Dtos.Payment;

public record NotificationsDto
{
    public string Action { get; init; }
    public string ApiVersion { get; init; }
    public NotificationDataDto Data { get; init; }
    public DateTime DateCreated { get; init; }
    public string Id { get; init; }
    public bool LiveMode { get; init; }
    public string Type { get; init; }
    public long UserId { get; init; }
}

public record NotificationDataDto
{
    public string Id { get; init; }
}