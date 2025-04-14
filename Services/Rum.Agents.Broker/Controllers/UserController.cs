using Microsoft.AspNetCore.Mvc;

using Rum.Agents.Broker.Models;
using Rum.Agents.Broker.Storage;

namespace Rum.Agents.Broker.Controllers;

[Route("/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserStorage _userStorage;

    public UserController(IUserStorage userStorage)
    {
        _userStorage = userStorage;
    }

    [HttpGet]
    public async Task<IResult> Get(CancellationToken cancellationToken = default)
    {
        var users = await _userStorage.Get(cancellationToken);
        return Results.Ok(users);
    }

    [HttpPost]
    public async Task<IResult> Create([FromBody] User.CreateRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userStorage.Create(new()
        {
            Name = request.Name
        }, cancellationToken);

        return Results.Ok(user);
    }
}