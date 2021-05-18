namespace CurrentStock.Models
{
    public interface IDeprecationRule
    {
        /// <summary>
        /// The minimum number of days the rule is valid.
        /// </summary>
        int FirstApplicableDay { get; set; }
    }
}
