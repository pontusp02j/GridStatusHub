namespace GridStatusHub.Domain.Context 
{
    public interface ICreateGridSystemCommandHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}