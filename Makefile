fmt:
	dotnet format

build:
	dotnet build

test:
	dotnet test

clean:
	dotnet clean

mcp_broker_run:
	dotnet run --project Services/Mcp.Broker