namespace GridStatusHub.Domain.Entities
{
    public class GridCell
    {
        public int id { get; set; }

        public int gridsystemid { get; set; }

        public int rowposition { get; set; }
        public int columnposition { get; set; }

        public string colorstatus { get; set; } = "gray";
    }
}
