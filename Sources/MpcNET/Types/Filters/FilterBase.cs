namespace MpcNET.Types.Filters
{
    /// <summary>
    /// Filter "base" (not the interface base class, the base command: restrict search songs to a directory)
    /// </summary>
    public class FilterBase : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterBase"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterBase(string Value, FilterOperator Operator, bool Negation = false) : base("base", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.None)
                throw new System.ArgumentException("Operator is not compatible: for \"base\" use FilterOperator.None.");

            this.Name = "base";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }
}
