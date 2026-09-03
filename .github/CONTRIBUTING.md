# Contributing

Thanks for your interest in improving GarminR10-BLE-Connector.

GarminR10-BLE-Connector is a .NET 9 (Windows) library that connects to a Garmin Approach R10 launch
monitor over Bluetooth LE and streams ball, club, and swing data for every shot — no Garmin Golf app
or simulator subscription required.

## Getting set up

1. Install the [.NET 9 SDK](https://dotnet.microsoft.com/download).
2. Fork and clone the repository.
3. Build: `dotnet build GarminR10-BLE-Connector.sln -c Debug`

A physical Approach R10 is needed to test connection and shot capture end to end. If you don't
have one, say so in your pull request so a maintainer can verify on hardware.

## Making changes

- Target the `main` branch and keep pull requests focused on a single change.
- Follow the existing code style (nullable enabled, implicit usings, XML doc comments on public
  members). `GarminR10-BLE-Connector.sln.DotSettings` holds the shared ReSharper/Rider settings.
- Extract reusable UI styling logic (borders, gradients, etc.) into shared helper classes rather
  than duplicating it across controls.
- Add the Apache 2.0 header to any new source file, matching the existing files:

    ```csharp
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
     */
    ```

    If the file contains code derived from [gsp-r10-adapter](https://github.com/mholow/gsp-r10-adapter),
    append the MIT block used by the existing derived files after a `---` separator line.

- Do not remove or alter the MIT attribution headers on files derived from
  [gsp-r10-adapter](https://github.com/mholow/gsp-r10-adapter). If you add code derived from another
  project, keep its license header and add an entry to
  [THIRD-PARTY-NOTICES.md](../THIRD-PARTY-NOTICES.md).
- Make sure the solution builds without new warnings before opening a pull request.

## Commit messages

Use short, imperative subject lines, e.g. `Add battery polling to GarminR10Client`.

## Reporting bugs

Open an issue using the bug report template and include:

- Windows version and Bluetooth adapter
- R10 firmware version (shown in the `Connected` event)
- Relevant `log4net` output
- Steps to reproduce

## Security

Do not open public issues for security problems — see [SECURITY.md](SECURITY.md).

## License

By contributing, you agree that your contributions are licensed under the Apache License 2.0.
