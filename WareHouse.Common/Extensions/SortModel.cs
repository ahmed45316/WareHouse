namespace WareHouse.Common.Extensions
{
    public class SortModel
    {
        public string ColId { get; set; } = "id";
        public string Sort { get; set; } = "desc";
        public string PairAsSqlExpression => $"{ColId} {Sort}";
    }
}
