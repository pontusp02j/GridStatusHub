namespace GridStatusHub.Domain.Service.Rules
{
    public class GridCellColorService
    {
        private static readonly Dictionary<string, string> ColorStatusTransition = new Dictionary<string, string>
        {
            { "gray", "green" },
            { "green", "orange" },
            { "orange", "red" },
            { "red", "gray" }
        };

        public string GetNextStatus(string currentStatus)
        {
            return ColorStatusTransition.TryGetValue(currentStatus, out var nextStatus) 
                ? nextStatus 
                : currentStatus;
        }
    }
}
