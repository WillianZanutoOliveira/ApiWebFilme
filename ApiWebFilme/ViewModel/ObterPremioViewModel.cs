namespace ApiWebFilme.ViewModel;

public readonly struct ObterPremioViewModel
{
    public required IEnumerable<ProducerViewModel> Min { get; init; }

    public required IEnumerable<ProducerViewModel> Max { get; init; }
}