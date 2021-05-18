namespace CurrentStock.Models
{
    public class ZeroDeprecationRule : IDeprecationRule
    {
        public int FirstApplicableDay { get; set; }
    }
}
