namespace MpcNET.Types.Filters
{
    /// <summary>
    /// Filter "AudioFormat"
    /// </summary>
    public class FilterAudioFormat : IFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterAudioFormat"/> class.
        /// </summary>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public FilterAudioFormat(string Value, FilterOperator Operator, bool Negation = false) : base("AudioFormat", Operator, Value, Negation)
        {
            if (Operator != FilterOperator.Equal && Operator != FilterOperator.Mask)
                throw new System.ArgumentException("Operator is not compatible: for \"AudioFormat\" use FilterOperator.Equal or FilterOperator.Mask.");

            this.Name = "AudioFormat";
            this.Value = Value;
            this.Operator = Operator;
            this.Negation = Negation;
        }
    }
}
