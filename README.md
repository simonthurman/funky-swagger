# funky-swagger

An Azure Functions v4 HTTP trigger app built with the .NET 8 isolated worker model.

## Function: sayHello

Returns a personalized greeting.

| Method | URL |
|--------|-----|
| GET | `/api/sayHello?name={name}` |
| POST | `/api/sayHello` (JSON body: `{ "name": "..." }`) |

**Example response:** `Hello, World. This HTTP triggered function executed successfully.`

## Function: getReverse

Reverses the input string.

| Method | URL |
|--------|-----|
| GET | `/api/getReverse?input={input}` |
| POST | `/api/getReverse` (JSON body: `{ "input": "..." }`) |

**Example:** `input=hello` → `olleh`

Authorization level is **Anonymous** — no function key required.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Functions Core Tools v4](https://learn.microsoft.com/azure/azure-functions/functions-run-tools?tabs=v4)
- [Azurite](https://learn.microsoft.com/azure/storage/common/storage-use-azurite) or an Azure Storage account (for `AzureWebJobsStorage`)

## Running locally

```bash
dotnet build funky-swagger.csproj
cd bin/Debug/net8.0
func start
```

The function will be available at `http://localhost:7071/api/sayHello`.

## Project structure

| File | Purpose |
|------|---------|
| `Program.cs` | Host entry point |
| `sayHello.cs` | HTTP trigger — greeting function |
| `getReverse.cs` | HTTP trigger — string reverse function |
| `host.json` | Functions host configuration |
| `local.settings.json` | Local app settings (not published) |
