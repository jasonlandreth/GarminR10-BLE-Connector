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

using System.Reflection;
using GarminR10Connector.Attributes;

namespace GarminR10Connector.Util;

public static class AttributeExtensionMethods
{
  /// <summary>
  /// Gets an attribute from an enum member.
  /// </summary>
  /// <typeparam name="TEnum">Enum type. </typeparam>
  /// <typeparam name="TAttribute"> Attribute type.</typeparam>
  /// <param name="value"> Enum value.</param>
  /// <returns>
  /// Attribute instance if found; otherwise <c>null</c>.
  /// </returns>
  public static TAttribute? GetCustomAttribute<TEnum, TAttribute>(this TEnum value)
      where TEnum : struct, Enum
      where TAttribute : GarminShotAttribute
  {
    string name = value.ToString();
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
  /// <param name="value"> Enum value. </param>
  /// <returns>
  /// Description text if present; otherwise <c>string.Empty</c>.
  /// </returns>
  public static string GetDescription(this GarminShotMetric value)
  {
    GarminShotAttribute? attribute = value.GetCustomAttribute<GarminShotMetric, GarminShotAttribute>();
    return attribute?.Description ?? string.Empty;
  }

  /// <summary>
  /// Gets the golf-shot result type from <see cref="GolfShotAttribute"/>.
  /// </summary>
  /// <typeparam name="TEnum"> Enum type. </typeparam>
  /// <param name="value"> Enum value. </param>
  /// <returns>
  /// Result unit text if present; otherwise <c>string.Empty</c>.
  /// </returns>
  public static string GetResultUnit(this GarminShotMetric value)
  {
    GarminShotAttribute? attribute = value.GetCustomAttribute<GarminShotMetric, GarminShotAttribute>();
    return attribute?.ResultUnit ?? string.Empty;
  }


  /// <summary>
  /// Gets the golf-shot result type from <see cref="GolfShotAttribute"/>.
  /// </summary>
  /// <typeparam name="TEnum"> Enum type. </typeparam>
  /// <param name="value"> Enum value. </param>
  /// <returns>
  /// Result type text if present; otherwise <c>string.Empty</c>.
  /// </returns>
  public static string GetTitle(this GarminShotMetric value)
  {
    GarminShotAttribute? attribute = value.GetCustomAttribute<GarminShotMetric, GarminShotAttribute>();
    return attribute?.Title ?? string.Empty;
  }
}
