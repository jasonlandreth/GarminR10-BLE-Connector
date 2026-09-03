/*
 * Copyright (c) 2026 Jason Landreth
 *
 * Licensed under the Apache License, Version 2.0 (the ""License"");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an ""AS IS"" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using GarminR10Connector.WinFormExample.Attributes;
using System.ComponentModel;
using System.Reflection;

namespace GarminR10Connector.WinFormExample.Utilities;

public static class EnumExtensionMethods
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
      where TAttribute : GolfShotAttribute
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
    /// Gets the golf-shot description from <see cref="GolfShotAttribute"/>.
    /// </summary>
    /// <typeparam name="TEnum"> Enum type. </typeparam>
    /// <param name="Value"> Enum value. </param>
    /// <returns>
    /// Description text if present; otherwise <c>string.Empty</c>.
    /// </returns>
    public static string GeShotDescription<TEnum>(this TEnum Value)
      where TEnum : struct, Enum
  {
    GolfShotAttribute? attribute = Value.GetCustomAttribute<TEnum, GolfShotAttribute>();
    return attribute?.Description ?? string.Empty;
  }

    /// <summary>
    /// Gets the golf-shot result type from <see cref="GolfShotAttribute"/>.
    /// </summary>
    /// <typeparam name="TEnum"> Enum type. </typeparam>
    /// <param name="Value"> Enum value. </param>
    /// <returns>
    /// Result type text if present; otherwise <c>string.Empty</c>.
    /// </returns>
    public static string GetUnitType<TEnum>(this TEnum Value)
      where TEnum : struct, Enum
  {
    GolfShotAttribute? attribute = Value.GetCustomAttribute<TEnum, GolfShotAttribute>();
    return attribute?.UnitType ?? string.Empty;
  }

    /// <summary>
    /// Gets the DescriptionAttribute value from an enum member.
    /// </summary>
    /// <typeparam name="TEnum">Enum type.</typeparam>
    /// <param name="Value">Enum value.</param>
    /// <returns>Description text if present; otherwise <c>string.Empty</c>.</returns>
    public static string GetDescription<TEnum>(this TEnum Value)
      where TEnum : struct, Enum
  {
    string name = Value.ToString();
    MemberInfo[] members = typeof(TEnum).GetMember(name);
    if (members.Length == 0)
    {
      return string.Empty;
    }

    DescriptionAttribute? attribute = members[0].GetCustomAttribute<DescriptionAttribute>(inherit: false);
    return attribute?.Description ?? string.Empty;
  }
}
