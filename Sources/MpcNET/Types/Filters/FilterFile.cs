namespace MpcNET.Types.Filters
{
    /// <summary>
    /// Filter "file"
    /// </summary>
    public class FilterFile : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterFile"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterFile(string Value, FilterOperator Operator, bool Negation = false) : base("file", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.Equal)
                throw new System.ArgumentException("Operator is not compatible: for \"File\" use FilterOperator.Equal.");

            this.Name = "file";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }
}
