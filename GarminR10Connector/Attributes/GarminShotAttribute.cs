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

namespace GarminR10Connector.Attributes;

/// <summary>
/// Attribute for enum members representing golf shot metrics.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class GarminShotAttribute : Attribute
{

  /// <summary>
  /// Human-readable description.
  /// </summary>
  public string Description { get; }

  /// <summary>
  /// Result type label.
  /// </summary>
  public string ResultUnit { get; }

  /// <summary>
  /// Result type label.
  /// </summary>
  public string Title { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="GarminShotAttribute"/> class.
  /// </summary>
  /// <param name="description">
  /// Human-readable description for the enum value.
  /// </param>
  /// <param name="ResultUnit">
  /// Logical result type label for the enum value.
  /// </param>
  public GarminShotAttribute(string Description, string ResultUnit)
  {
    this.Description = Description;
    this.ResultUnit = ResultUnit;
    this.Title = String.Empty;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="GarminShotAttribute"/> class.
  /// </summary>
  /// <param name="Description"></param>
  /// <param name="ResultUnit"></param>
  /// <param name="Title"></param>
  /// Human-readable description for the enum value.
  /// </param>
  /// <param name="ResultUnit">
  /// Logical result unit label for the enum value.
  /// </param>
  /// <param name="Title">
  /// Human-readable title for the enum value.
  /// </param>
  public GarminShotAttribute(string Description, string ResultUnit, string Title)
  {
    this.ResultUnit = ResultUnit;
    this.Title = Title;
    this.Description = Description;
  }

}
