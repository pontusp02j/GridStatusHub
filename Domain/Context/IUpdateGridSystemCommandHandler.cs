namespace GridStatusHub.Domain.Context 
{
    public interface IUpdateGridSystemCommandHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}