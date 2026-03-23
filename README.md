# AzureFunction

![C#](https://img.shields.io/badge/language-C%23-239120) ![Azure](https://img.shields.io/badge/platform-Azure%20Functions-0078D4?logo=microsoftazure)

A minimal Azure Functions project used as the backend serverless layer for the [MultiplayerGamePrototype](https://github.com/Oppenheimr/MultiplayerGamePrototype). Demonstrates HTTP-triggered functions for lightweight game service endpoints.

## Features

- **HTTP Trigger** — Single Azure Function endpoint (`HttpTriggerMGP`) for handling game requests
- **Isolated Worker** — Uses .NET isolated worker model (`Program.cs`)
- **Lightweight** — Minimal setup, easy to extend with additional triggers or bindings

## Tech Stack

| Layer | Technology |
|---|---|
| Platform | Azure Functions v4 |
| Language | C# / .NET 6+ |
| Model | Isolated worker process |

## Project Structure

```
AzureFunction/
├── HttpTriggerMGP.cs    # Main HTTP trigger function
├── Program.cs           # Worker host configuration
└── AzureFunction.csproj # Project file
```

## Getting Started

**Prerequisites**
- [.NET 6 SDK](https://dotnet.microsoft.com/download) or newer
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local) v4+
- An Azure account (for deployment)

**Running locally**
1. Clone the repository
   ```bash
   git clone https://github.com/Oppenheimr/AzureFunction.git
   ```
2. Navigate into the project directory
   ```bash
   cd AzureFunction
   ```
3. Start the local Functions runtime
   ```bash
   func start
   ```

**Deploying to Azure**
```bash
az login
func azure functionapp publish <YOUR_FUNCTION_APP_NAME>
```

## Related

This project is used alongside [MultiplayerGamePrototype](https://github.com/Oppenheimr/MultiplayerGamePrototype) to provide serverless backend support for multiplayer game features.

## Author

**Umutcan Bağcı** — [github.com/Oppenheimr](https://github.com/Oppenheimr)

## License

This project is licensed under the [MIT License](LICENSE).
