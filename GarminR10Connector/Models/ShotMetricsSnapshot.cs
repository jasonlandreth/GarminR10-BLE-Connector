/*
 * Copyright (c) 2026 Jason Landreth, Open Swing Golf Simulator
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 */

namespace GarminR10Connector.Models
{
    /// <summary>
    /// Represents normalized shot metrics for consumer code.
    /// </summary>
    public sealed class ShotMetricsSnapshot
    {
        /// <summary>
        /// Gets or sets the shot identifier.
        /// </summary>
        public uint ShotId { get; init; }

        /// <summary>
        /// Gets or sets metric values keyed by <see cref="GolfShotMetric"/>.
        /// </summary>
        public required IReadOnlyDictionary<GolfShotMetric, double> Values { get; init; }

        /// <summary>
        /// Gets a metric value if present.
        /// </summary>
        /// <param name="Metric">Metric key.</param>
        /// <param name="Value">Output value.</param>
        /// <returns>True when present; otherwise false.</returns>
        public bool TryGet(GolfShotMetric Metric, out double Value)
        {
            return Values.TryGetValue(Metric, out Value);
        }
    }
}
