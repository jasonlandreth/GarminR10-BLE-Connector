using System.Reflection;
using GarminR10Connector.Attributes;

namespace GarminR10Connector.Utilities;

public static class ExtensionMethods
{
    /// <summary>
    /// Gets an attribute from an enum member.
    /// </summary>
    /// <typeparam name="TEnum">Enum type. </typeparam>
    /// <typeparam name="TAttribute"> Attribute type.</typeparam>
    /// <param name="Value"> Enum value.</param>
    /// <returns>
    /// Attribute instance if found; otherwise <c>null</c>.
    /// </returns>
    public static TAttribute? GetCustomAttribute<TEnum, TAttribute>(this TEnum Value)
        where TEnum : struct, Enum
        where TAttribute : GarminShotAttribute
    {
        string name = Value.ToString();
        MemberInfo[] members = typeof(TEnum).GetMember(name);
        if (members.Length == 0)
        {
            return null;
        }

        return members[0].GetCustomAttribute<TAttribute>(inherit: false);
    }

    /// <summary>
    /// Gets the golf-shot description from <see cref="GarminShotAttribute"/>.
    /// </summary>
    /// <param name="Value"> Enum value. </param>
    /// <returns>
    /// Description text if present; otherwise <c>string.Empty</c>.
    /// </returns>
    public static string GetDescription(this GolfShotMetric Value)
    {
        GarminShotAttribute? attribute = Value.GetCustomAttribute<GolfShotMetric, GarminShotAttribute>();
        return attribute?.Description ?? string.Empty;
    }

    /// <summary>
    /// Gets the golf-shot result type from <see cref="GolfShotAttribute"/>.
    /// </summary>
    /// <typeparam name="TEnum"> Enum type. </typeparam>
    /// <param name="Value"> Enum value. </param>
    /// <returns>
    /// Result unit text if present; otherwise <c>string.Empty</c>.
    /// </returns>
    public static string GetUnitType(this GolfShotMetric Value)
    {
        GarminShotAttribute? attribute = Value.GetCustomAttribute<GolfShotMetric, GarminShotAttribute>();
        return attribute?.UnitType ?? string.Empty;
    }


    /// <summary>
    /// Gets the golf-shot result type from <see cref="GolfShotAttribute"/>.
    /// </summary>
    /// <typeparam name="TEnum"> Enum type. </typeparam>
    /// <param name="value"> Enum value. </param>
    /// <returns>
    /// Result type text if present; otherwise <c>string.Empty</c>.
    /// </returns>
    public static string GetTitle(this GolfShotMetric Value)
    {
        GarminShotAttribute? attribute = Value.GetCustomAttribute<GolfShotMetric, GarminShotAttribute>();
        return attribute?.Title ?? string.Empty;
    }
}
