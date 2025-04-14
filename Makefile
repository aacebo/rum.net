fmt:
	dotnet format

build:
	dotnet build

test:
	dotnet test

clean:
	dotnet clean

agents_broker_run:
	dotnet run --project Services/Rum.Agents.Broker