namespace MpcNET.Types.Filters
{
    /// <summary>
    /// Filter "added-since". Matches songs that were added to the database since the given timestamp.
    /// https://mpd.readthedocs.io/en/stable/protocol.html#filters
    /// </summary>
    public class FilterAddedSince : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterAddedSince"/> class.
        /// </summary>
        /// <param name="Value">Timestamp to test (ISO 8601 string or Unix timestamp).</param>
        /// <param name="Operator">Operator of the filter (must be <see cref="FilterOperator.None"/>).</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterAddedSince(string Value, FilterOperator Operator, bool Negation = false) : base("added-since", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.None)
                throw new System.ArgumentException("Operator is not compatible: for \"AddedSince\" use FilterOperator.None.");

            this.Name = "added-since";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }
}
