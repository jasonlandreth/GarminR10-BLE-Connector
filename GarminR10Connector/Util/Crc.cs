/*
 * SPDX-License-Identifier: Apache-2.0
 *
 * Copyright (c) 2026 Jason Landreth
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * -----------------------------------------------------------------------------
 * Portions of this file are derived from gsp-r10-adapter and remain under MIT:
 *
 * MIT License
 *
 * Copyright (c) 2022 mholow
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 * Derived from:
 * https://github.com/mholow/gsp-r10-adapter
 */

namespace GarminR10Connector.Util
{
  /// <summary>
  /// CRC (ARC/IBM polynomial) checksum computation, used to validate received frames and generate checksums for outgoing ones.
  /// </summary>
  public static class Crc
  {
    /// <summary>
    /// The CR polynomial used by this implementation (ARC/IBM, 0xA001).
    /// </summary>
    private const ushort POLYNOMIAL = 0xA001;

    /// <summary>
    /// The precomputed lookup table, one entry per possible byte value, built once in the static constructor.
    /// </summary>
    static readonly ushort[] Table = new ushort[256];

    /// <summary>
    /// Computes the CRC16 checksum of a byte sequence using the precomputed lookup table.
    /// </summary>
    /// <param name="Bytes">The bytes to checksum.</param>
    /// <returns>The two-byte little-endian checksum.</returns>
    public static byte[] ComputeChecksum(IEnumerable<byte> Bytes)
    {
      ushort crc = 0;
      foreach (byte b in Bytes)
      {
        crc = (ushort)((crc >> 8) ^ Table[(byte)(crc ^ b)]);
      }
      return BitConverter.GetBytes(crc);
    }

    /// <summary>
    /// Builds the CRC16 lookup table used by <see cref="ComputeChecksum"/>.
    /// </summary>
    static Crc()
    {
      ushort value;
      ushort temp;
      for (ushort i = 0; i < Table.Length; ++i)
      {
        value = 0;
        temp = i;
        for (byte j = 0; j < 8; ++j)
        {
          if (((value ^ temp) & 0x0001) != 0)
          {
            value = (ushort)((value >> 1) ^ POLYNOMIAL);
          }
          else
          {
            value >>= 1;
          }
          temp >>= 1;
        }
        Table[i] = value;
      }
    }
  }
}
