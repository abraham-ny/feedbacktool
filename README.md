


# FeedbackTooll

A **portable, GUI + CLI hybrid tool** for easy feedback collection and update checking in any desktop application. Integrate with **C#, Java, Python, Go, or C++** apps with zero dependencies.

---


## Features

- **Check for updates** via GitHub or custom API
- **Download and install updates** automatically (optional)
- **Submit user feedback** via POST to your API
- **Developer mode** (`F12`) to inspect and debug
- **GUI or headless CLI mode**
- **Single `.exe`** — no install, no .NET runtime required (if published self-contained)

---


## Quick Start

1. **Bundle** `feedbacktool.exe` in your app's release folder or installer.
2. **Launch** it from your app using command-line arguments (see below).

---


## Usage

### GUI Mode

```bash
feedbacktool.exe -update https://api.github.com/repos/your/repo/releases/latest
feedbacktool.exe -feedback https://yourapi.com/feedback
```

This opens a simple UI for update or feedback.

### CLI / No-GUI Mode

```bash
feedbacktool.exe -nogui -update <url> -downloadinstall true
feedbacktool.exe -nogui -feedback -url <url> -message "It crashed" -username "testuser"
```

---


## Integration Examples

### C#
```csharp
Process.Start("feedbacktool.exe", "-nogui -update https://api.github.com/repos/your/repo/releases/latest");
```

### Java
```java
ProcessBuilder builder = new ProcessBuilder("feedbacktool.exe", "-nogui", "-update", "https://api.github.com/repos/your/repo/releases/latest");
builder.start();
```

### Python
```python
import subprocess
subprocess.run([
    'feedbacktool.exe', '-nogui', '-feedback', '-url', 'https://yourapi.com', '-message', 'Bug!', '-username', 'tester'
])
```

### C++
```cpp
#include <cstdlib>
int main() {
    system("feedbacktool.exe -nogui -update https://api.github.com/repos/your/repo/releases/latest");
    return 0;
}
```

### Go
```go
import "os/exec"
exec.Command("feedbacktool.exe", "-nogui", "-update", "https://api.github.com/repos/your/repo/releases/latest").Run()
```

---


## Recommended Usage

Bundle `feedbacktool.exe` in your application's release directory:

```
YourApp/
├── yourapp.exe
├── feedbacktool.exe   ← bundle this
└── ...
```

Call it from your app’s update button, crash handler, or settings screen.

---


## Exit Codes

| Code | Meaning           |
| ---- | ----------------- |
| 0    | Success           |
| 1    | Update failed     |
| 2    | Feedback failed   |
| 3    | Invalid arguments |

---


## Options

* `-nogui` – disables GUI (for automation)
* `-dev` – enables DevTools auto-launch
* `-update <url>` – URL to check for updates
* `-downloadinstall true|false` – download if update found
* `-feedback` – activates feedback mode
* `-url <url>` – API endpoint to POST to
* `-message "<msg>"` – feedback message
* `-username "<id>"` – optional identifier

---

## Author / Maintainer

Created by [abraham-ny](https://github.com/abraham-ny) — contributions welcome!

---


## Future Plans

* [ ] Auto-restart after update
* [ ] Config file support
* [ ] Log history window
* [ ] Feedback categorization

---

### CLI / No-GUI Mode (`-nogui`)

###  Update Check (with optional auto-download)

```bash
feedbacktool.exe -nogui -update <url> -downloadinstall true
```

###  Send Feedback (silent)

```bash
feedbacktool.exe -nogui -feedback -url <url> -message "It crashed" -username "testuser"
```

---

##  Cross-Language Integration

###  C\#

```csharp
Process.Start("feedbacktool.exe", "-nogui -update https://api.github.com/repos/your/repo/releases/latest");
```

With output parsing:

```csharp
var proc = new Process
{
    StartInfo = new ProcessStartInfo
    {
        FileName = "feedbacktool.exe",
        Arguments = "-nogui -feedback -url https://yourapi.com -message \"Hello\" -username Dev",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    }
};

proc.Start();
string result = proc.StandardOutput.ReadToEnd();
proc.WaitForExit();
Console.WriteLine(result);
```

---

###  Java

```java
ProcessBuilder builder = new ProcessBuilder("feedbacktool.exe",
    "-nogui", "-update", "https://api.github.com/repos/your/repo/releases/latest");
builder.redirectErrorStream(true);
Process process = builder.start();

try (BufferedReader reader = new BufferedReader(new InputStreamReader(process.getInputStream()))) {
    String line;
    while ((line = reader.readLine()) != null) {
        System.out.println(line);
    }
}
```

---

###  Python

```python
import subprocess

result = subprocess.run(
    ['feedbacktool.exe', '-nogui', '-feedback', '-url', 'https://yourapi.com', '-message', 'Bug!', '-username', 'tester'],
    capture_output=True,
    text=True
)

print(result.stdout)
```

---

###  C++

```cpp
#include <cstdlib>

int main() {
    system("feedbacktool.exe -nogui -update https://api.github.com/repos/your/repo/releases/latest");
    return 0;
}
```

---

###  Go

```go
package main

import (
    "fmt"
    "os/exec"
)

func main() {
    out, err := exec.Command("feedbacktool.exe", "-nogui", "-update", "https://api.github.com/repos/your/repo/releases/latest").Output()
    if err != nil {
        fmt.Println("Error:", err)
    }
    fmt.Println(string(out))
}
```

---

##  Recommended Usage

Bundle `feedbacktool.exe` in your application's release directory or installer:

```
YourApp/
├── yourapp.exe
├── feedbacktool.exe   ← bundle this
└── other files...
```

You may also call it from your app’s update button, crash handler, or settings screen.

---

##  Exit Codes

| Code | Meaning           |
| ---- | ----------------- |
| 0    | Success           |
| 1    | Update failed     |
| 2    | Feedback failed   |
| 3    | Invalid arguments |

(*You can parse `stdout` or rely on exit codes for logic.*)

---

##  Options Summary

### General

* `-nogui` – disables GUI (for automation)
* `-dev` – enables DevTools auto-launch

### Update

* `-update <url>` – URL to check for updates
* `-downloadinstall true|false` – whether to download if update found

### Feedback

* `-feedback` – activates feedback mode
* `-url <url>` – API endpoint to POST to
* `-message "<msg>"` – feedback message
* `-username "<id>"` – optional identifier

---


##  Future Plans

* [ ] Auto-restart after update
* [ ] Config file support
* [ ] Log history window
* [ ] Feedback categorization

```
