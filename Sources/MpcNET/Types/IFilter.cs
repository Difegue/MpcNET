using System.ComponentModel;

namespace MpcNET.Types
{
    /// <summary>
    /// Operators available for filters in the protocol
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Equal (==)
        /// </summary>
        [Description("==")]
        Equal,
        /// <summary>
        /// Different (!=)
        /// </summary>
        [Description("!=")]
        Different,
        /// <summary>
        /// Contains (contains)
        /// </summary>
        [Description("contains")]
        Contains,
        /// <summary>
        /// Mask (=~)
        /// </summary>
        [Description("=~")]
        Mask,
        /// <summary>
        /// None ("")
        /// </summary>
        [Description("")]
        None,
    }

    /// <summary>
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// <see cref="GetDescription"/> of the enum
        /// </summary>
        /// <param name="enumValue"></param>
        public static string GetDescription<T>(this T enumValue)
            where T : struct, System.IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            string description = enumValue.ToString();
            System.Reflection.FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes != null && attributes.Length > 0)
                    description = ((DescriptionAttribute)attributes[0]).Description;
            }

            return description;
        }
    }

    /// <summary>
    /// Filter interface specialized by filters in the protocol
    /// </summary>
    public abstract class IFilter
    {
        /// <summary>
        /// Name of the filter
        /// </summary>
        public string Name;
        /// <summary>
        /// Operator of the filter
        /// </summary>
        public FilterOperator Operator;
        /// <summary>
        /// Value to test
        /// </summary>
        public string Value;
        /// <summary>
        /// If the expression is negated
        /// </summary>
        public bool Negation;

        /// <summary>
        /// Initializes a new instance of the <see cref="IFilter"/> class.
        /// </summary>
        /// <param name="Name">Name of the filter</param>
        /// <param name="Operator">Operator of the filter</param>
        /// <param name="Value">Value to test</param>
        /// <param name="Negation">If the expression is negated</param>
        public IFilter(string Name, FilterOperator Operator, string Value, bool Negation = false)
        {
            this.Name = Name;
            this.Operator = Operator;
            this.Value = Value;
            this.Negation = Negation;
        }

        /// <summary>
        /// Gets the formatted command
        /// 
        /// String values are quoted with single or double quotes, 
        /// and special characters within those values must be escaped with the backslash (\). 
        /// Keep in mind that the backslash is also the escape character on the protocol level, 
        /// which means you may need to use double backslash. 
        /// 
        /// Example expression which matches an artist named foo'bar":
        /// (Artist == "foo\'bar\"")
        /// 
        /// At the protocol level, the command must look like this:
        /// find "(Artist == \"foo\\'bar\\\"\")"
        /// 
        /// (https://mpd.readthedocs.io/en/stable/protocol.html#filters)
        /// </summary>
        public string GetFormattedCommand()
        {
            string make = "";

            if (Negation)
                make += "(!";

            make += "(" + Name + " ";
            make += Operator.GetDescription();
            make += " " + "\\\"" + Value + "\\\"" + ")";

            if (Negation)
                make += ")";

            return make;
        }
    }
}
