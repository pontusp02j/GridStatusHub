namespace GridStatusHub.Domain.Context 
{
    public interface IDeleteGridSystemCommandHandler
    {
        Task<bool> HandleAsync(int id);
    }
}
