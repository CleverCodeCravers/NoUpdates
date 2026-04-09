# NoUpdates

[![NoUpdates Build](https://github.com/CleverCodeCravers/NoUpdates/actions/workflows/dotnet.yml/badge.svg)](https://github.com/CleverCodeCravers/NoUpdates/actions/workflows/dotnet.yml)
[![CodeQL](https://github.com/CleverCodeCravers/NoUpdates/actions/workflows/codeql.yml/badge.svg)](https://github.com/CleverCodeCravers/NoUpdates/actions/workflows/codeql.yml)

A tool that suppresses Windows Updates in build environments by disabling and stopping the Windows Update service (`wuauserv`).

## What does it do?

NoUpdates continuously monitors the Windows Update service and ensures it stays disabled:

1. Sets the Windows Update service startup type to **Disabled**
2. Stops the service if it is currently running
3. Rechecks every **10 minutes** to ensure the service has not been re-enabled

This is useful for CI/CD build agents and test environments where unexpected Windows Updates can cause downtime, reboots, or unreliable builds.

## Prerequisites

- **Windows** operating system
- **.NET 10.0 Runtime** (or use the self-contained release)
- **Administrator privileges** -- the application requires elevated rights to manage Windows services (enforced via app manifest)

## Usage

### From Release

1. Download the latest release from the [Releases](https://github.com/CleverCodeCravers/NoUpdates/releases) page
2. Run `NoUpdates.Console.exe` as Administrator
3. The tool runs in a loop and logs its actions to the console

### From Source

```bash
# Clone the repository
git clone https://github.com/CleverCodeCravers/NoUpdates.git
cd NoUpdates

# Build
dotnet build Source/NoUpdates

# Run (requires Administrator privileges)
dotnet run --project Source/NoUpdates/NoUpdates.Console/NoUpdates.Console.csproj
```

## Project Structure

```
Source/NoUpdates/
  NoUpdates.Console/        # Entry point (console application with admin manifest)
  NoUpdates.BL/             # Business logic
  NoUpdates.BL.Tests/       # Unit tests (xUnit)
  NoUpdates.Infrastructure/ # Infrastructure layer
  NoUpdates.Interfaces/     # Interfaces and abstractions
```

## License

See [LICENSE](LICENSE) for details.
