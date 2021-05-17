namespace CurrentStock.Models
{
    public class ZeroDeprecationRule : IDeprecationRule
    {
        public int LastApplicableDay { get; set; } = 0;
    }
}
