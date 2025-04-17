fmt:
	dotnet format

build:
	dotnet build

test:
	dotnet test -v d

clean:
	dotnet clean

agents_broker_run:
	dotnet run --project Services/Rum.Agents.Broker

agents_cli_run:
	dotnet run --project Services/Rum.Agents.Cli