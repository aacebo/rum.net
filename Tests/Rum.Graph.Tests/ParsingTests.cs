namespace Rum.Graph.Parsing.Tests;

public class ParsingTests
{
    [Fact]
    public void Should_ParseFields()
    {
        var query = new Parser(@"{
            id,
            name,
            created_at
        }").Parse();

        Assert.Empty(query.Args);
        Assert.Equal(3, query.Fields.Count);
    }

    [Fact]
    public void Should_ParseFieldArgs()
    {
        var query = new Parser(@"{
			users(page: 1, pageSize: 10)
		}").Parse();

        Assert.Empty(query.Args);
        Assert.Single(query.Fields);

        var users = query.Fields.GetOrDefault("users");

        Assert.NotNull(users);
        Assert.Equal(2, users.Args.Count);

        var page = users.Args.GetOrDefault<int>("page");
        Assert.Equal(1, page);

        var pageSize = users.Args.GetOrDefault<int>("pageSize");
        Assert.Equal(10, pageSize);
    }

    [Fact]
    public void Should_ParseSubQuery()
    {
        var query = new Parser(@"{
			id,
			users { id, name, created_at },
			test
		}").Parse();

        Assert.Empty(query.Args);
        Assert.Equal(3, query.Fields.Count);

        var users = query.Fields.GetOrDefault("users");

        Assert.NotNull(users);
        Assert.Empty(users.Args);
        Assert.Equal(3, users.Fields.Count);
    }

    [Fact]
    public void Should_ParseSubQueryWithArgs()
    {
        var query = new Parser(@"{
			id,
			users(search: ""some text"", deleted: true) { id, name, created_at },
			test
		}").Parse();

        Assert.Empty(query.Args);
        Assert.Equal(3, query.Fields.Count);

        var users = query.Fields.GetOrDefault("users");

        Assert.NotNull(users);
        Assert.Equal(2, users.Args.Count);
        Assert.Equal(3, users.Fields.Count);

        var search = users.Args.GetOrDefault<string>("search");

        Assert.NotNull(search);
        Assert.Equal("some text", search);

        var deleted = users.Args.GetOrDefault<bool?>("deleted");

        Assert.NotNull(deleted);
        Assert.Equal(true, deleted);
    }

    [Fact]
    public void Should_ParseRootQueryWithArgs()
    {
        var query = new Parser(@"(id: ""123""){
			id,
			test
		}").Parse();

        Assert.NotEmpty(query.Args);
        Assert.Equal("123", query.Args.Get("id"));
        Assert.Equal(2, query.Fields.Count);
    }
}