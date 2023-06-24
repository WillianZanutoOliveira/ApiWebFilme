namespace ApiWebFilme.ViewModel;

public readonly struct ProducerViewModel
{
    public required string Producer { get; init; }
    public required int Interval { get; init; }
    public required int PreviousWin { get; init; }
    public required int FollowingWin { get; init; }
}