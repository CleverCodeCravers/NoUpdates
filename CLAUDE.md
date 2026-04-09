# CLAUDE.md

## Projektbeschreibung

NoUpdates ist ein Kommandozeilen-Tool, das den Windows Update Service (`wuauserv`) in Build-Umgebungen zuverlaessig unterdrueckt. Es deaktiviert den Dienst, setzt den Starttyp auf "Disabled" und prueft in regelmaessigen Intervallen (alle 10 Minuten), ob der Dienst wieder aktiviert wurde.

## TechStack

- .NET 10.0
- C# (Top-Level Statements)
- System.ServiceProcess.ServiceController
- System.Management (WMI)
- xUnit (Tests)

## Architektur-Vorlage

`dotnet-windows-service`

## Projektstruktur

```
Source/NoUpdates/
  NoUpdates.Console/       # Einstiegspunkt (Konsolen-App mit Admin-Manifest)
  NoUpdates.BL/            # Business Logic
  NoUpdates.BL.Tests/      # Unit Tests (xUnit)
  NoUpdates.Infrastructure/ # Infrastruktur-Schicht
  NoUpdates.Interfaces/    # Interfaces/Abstraktionen
  NoUpdates.sln            # Solution-Datei
```

## Build-Befehle

```bash
# Restore + Build
dotnet build Source/NoUpdates

# Tests ausfuehren
dotnet test Source/NoUpdates

# Release-Build
dotnet build Source/NoUpdates --configuration Release

# Publish (Self-Contained, Single File)
dotnet publish Source/NoUpdates/NoUpdates.Console/NoUpdates.Console.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## Konventionen

- Alle Aenderungen direkt auf `main` (Trunk-Based Development)
- Commit-Messages: `[ANFORDERUNGS_ID] <beschreibung>` oder praefix-basiert
- Nullable Reference Types aktiviert
- Implicit Usings aktiviert
- Admin-Rechte erforderlich (via app.manifest `requireAdministrator`)
