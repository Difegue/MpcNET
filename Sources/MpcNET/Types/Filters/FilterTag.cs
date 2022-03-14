using MpcNET.Tags;

namespace MpcNET.Types.Filters
{
    /// <summary>
    /// Filter "TAG"
    /// </summary>
    public class FilterTag : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterTag"/> class.
        /// </summary>
        /// <param name="Tag">The tag.</param>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterTag(ITag Tag, string Value, FilterOperator Operator, bool Negation = false) : base(Tag.Value, Operator, Value, Negation)
        {
            if (Operator != FilterOperator.Equal && Operator != FilterOperator.Different && Operator != FilterOperator.Contains)
                throw new System.ArgumentException("Operator is not compatible: for \"TAG\" use FilterOperator.Equal, FilterOperator.Different or FilterOperator.Contains.");

            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }
}
