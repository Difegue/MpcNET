namespace MpcNET.Types.Filters
{
    /// <summary>
    /// Filter "modified-since"
    /// </summary>
    public class FilterModifiedSince : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterModifiedSince"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterModifiedSince(string Value, FilterOperator Operator, bool Negation = false) : base("modified-since", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.None)
                throw new System.ArgumentException("Operator is not compatible: for \"ModifiedSince\" use FilterOperator.None.");

            this.Name = "modified-since";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }
}
