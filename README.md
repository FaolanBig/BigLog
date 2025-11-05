# BigLog – Overview

**BigLog** is a flexible logging library for C#.
It supports output to **console** and **file**, with configurable prefixes, timestamps, log levels, and optional colors.

**Important:** Colors are **only applied to console output**. When logging to a file or using the cache, colored output is **disabled**. If cache logging is flushed to the terminal, `Color_fallback` is used.

---

## Quick Start

```csharp
using BigLog;

class Program
{
    static void Main()
    {
        Logger logger = new Logger()
        {
            LogToFile = true,
            UseShortPrefix = true
        };

        logger.Inf("Program started");
        logger.Suc("Operation successful");
        logger.War("Low memory warning");
        logger.Err("Failed to open file");
        logger.Ctm("Custom log message");
    }
}
```

---

## Log Levels

| Method  | Meaning     | Example Output                                         |
| ------- | ----------- | ------------------------------------------------------ |
| `Inf()` | Information | `log: 2025-11-05 13:30:01.1234 :: inf: Server started` |
| `Suc()` | Success     | `log: ... :: suc: File saved`                          |
| `War()` | Warning     | `log: ... :: war: Low disk space`                      |
| `Err()` | Error       | `log: ... :: err: Connection failed`                   |
| `Ctm()` | Custom      | `log: ... :: ctm: Test output`                         |

All methods accept either a `string` or an `Exception`.

Example:

```csharp
try
{
    throw new Exception("File read error");
}
catch (Exception ex)
{
    logger.Err(ex);
}
```

---

## Settings

### General

| Property             | Type       | Description                                                                                          | Default            |
| -------------------- | ---------- | ---------------------------------------------------------------------------------------------------- | ------------------ |
| `AutoFlush`          | `bool`     | If `true`, logs are written immediately. If `false`, logs are cached until `flushCache()` is called. | `true`             |
| `LogToTerminal`      | `bool`     | Enables console output.                                                                              | `true`             |
| `LogToFile`          | `bool`     | Enables file output.                                                                                 | `false`            |
| `FileName`           | `string`   | File path for logging.                                                                               | `"log.txt"`        |
| `UseDefaultEncoding` | `bool`     | Use system default encoding.                                                                         | `true`             |
| `Encoding`           | `Encoding` | Custom encoding (disables `UseDefaultEncoding`).                                                     | `Encoding.Default` |

---

### Timestamp and Formatting

| Property                    | Type     | Description                                | Default                        |
| --------------------------- | -------- | ------------------------------------------ | ------------------------------ |
| `PrintTimeStamp`            | `bool`   | Adds a timestamp to each log.              | `true`                         |
| `PrintTimeStampBeforeLevel` | `bool`   | Positions timestamp before log level.      | `true`                         |
| `TimeStampPrefix`           | `string` | Text before timestamp.                     | `"log: "`                      |
| `TimeFormat`                | `string` | .NET timestamp format.                     | `"yyyy-MM-dd HH:mm:ss.ffff K"` |
| `PrePrefix`                 | `string` | Separator between timestamp and prefix.    | `" :: "`                       |
| `UseShortPrefix`            | `bool`   | Short (`inf:`) or long (`info:`) prefixes. | `true`                         |

---

### Prefixes

| Log Type | Short  | Long       |
| -------- | ------ | ---------- |
| Info     | `inf:` | `info:`    |
| Success  | `suc:` | `success:` |
| Warning  | `war:` | `warning:` |
| Error    | `err:` | `error:`   |
| Custom   | `ctm:` | `custom:`  |

Example:

```csharp
logger.PrefixInf = "[INFO] ";
logger.PrefixErr = "[ERROR] ";
```

---

### Colors

| Property              | Type           | Description                                                    | Default |
| --------------------- | -------------- | -------------------------------------------------------------- | ------- |
| `ColorAll`            | `bool`         | Colors the entire console line (only console output).          | `false` |
| `ColorMessage`        | `bool`         | Colors only the message text (only console output).            | `true`  |
| `ColorLevelPrefix`    | `bool`         | Colors only the prefix (only console output).                  | `false` |
| `EnableDefaultColors` | `bool`         | Enables default colors per log level.                          | `true`  |
| `Color_fallback`      | `ConsoleColor` | Default color when no color mode or cache logging to terminal. | `White` |

**Default colors per level (console only):**

| Level   | Color    |
| ------- | -------- |
| Info    | `White`  |
| Success | `Green`  |
| Warning | `Yellow` |
| Error   | `Red`    |
| Custom  | `Cyan`   |

> Note: Colors **cannot be used** when logging to a file or when messages are cached.
> When cached messages are flushed to the terminal, `Color_fallback` is applied.

Example:

```csharp
logger.ColorWar = ConsoleColor.Magenta;
logger.ColorErr = ConsoleColor.DarkRed;
```

---

### Cache

If `AutoFlush = false`, messages are cached instead of being written immediately.

```csharp
Logger logger = new Logger()
{
    AutoFlush = false,
    LogToFile = true
};

logger.Inf("Batch started...");
logger.War("Minor warning...");
logger.flushCache(); // writes all cached logs and clears cache afterwards
```

Additional cache commands:

```csharp
logger.ClearCache(); // clears the cache without writing
```

---

## Common Configurations

**Console output with colors**

```csharp
Logger logger = new Logger()
{
    LogToFile = false,
    ColorAll = true
};
```

**File logging without colors**

```csharp
Logger logger = new Logger()
{
    LogToTerminal = false,
    LogToFile = true,
    EnableDefaultColors = false
};
```

**Minimal output**

```csharp
Logger logger = new Logger()
{
    PrintTimeStamp = false,
    UseShortPrefix = true
};
```

---

## License

This project is licensed under the **GNU Affero General Public License (AGPL)**.
See [LICENSE](./LICENSE.txt) for details.
