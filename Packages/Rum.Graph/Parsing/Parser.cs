namespace Rum.Graph.Parsing;

public class Parser
{
    public Token Current => _curr ?? throw new Exception("expected current token, received null");
    public Token Previous => _prev ?? throw new Exception("expected previous token, received null");

    private Token? _curr;
    private Token? _prev;
    private readonly Scanner _scanner;

    public Parser(params byte[] data)
    {
        _scanner = new(data);
        Next();
    }

    public Parser(string data)
    {
        _scanner = new(data);
        Next();
    }

    public Parser(params char[] data)
    {
        _scanner = new(data);
        Next();
    }

    public Query Parse()
    {
        var query = new Query();

        Consume(TokenType.LeftBrace, "expected '{'");

        while (Current.Type != TokenType.Eof && Current.Type != TokenType.RightBrace)
        {
            var (name, subquery) = ParseField();
            query.Fields[name] = subquery;

            if (!TryConsume(TokenType.Comma)) break;
        }

        Consume(TokenType.RightBrace, "expected '}'");
        return query;
    }

    private (string, Query) ParseField()
    {
        var query = new Query();
        var name = Consume(TokenType.Identifier, "expected field name");

        if (TryConsume(TokenType.LeftParen))
        {
            query.Args = ParseArgs();
        }

        if (Current.Type == TokenType.LeftBrace)
        {
            var subquery = Parse();
            query.Fields = subquery.Fields;
        }

        return (name.ToString(), query);
    }

    private ArgumentCollection ParseArgs()
    {
        var args = new ArgumentCollection();

        while (Current.Type != TokenType.RightParen)
        {
            var param = Consume(TokenType.Identifier, "expected parameter name");
            Consume(TokenType.Colon, "expected ':'");

            if (TryConsume(TokenType.String))
            {
                args.Set(param.ToString(), Previous.ToString());
            }
            else if (TryConsume(TokenType.Int))
            {
                args.Set(param.ToString(), Previous.ToInt());
            }
            else if (TryConsume(TokenType.Float))
            {
                args.Set(param.ToString(), Previous.ToFloat());
            }
            else if (TryConsume(TokenType.Byte))
            {
                args.Set(param.ToString(), Previous.ToByte());
            }
            else if (TryConsume(TokenType.Bool))
            {
                args.Set(param.ToString(), Previous.ToBool());
            }
            else if (TryConsume(TokenType.Null))
            {
                args.Set(param.ToString(), null);
            }
            else
            {
                throw Current.ToError("invalid parameter value");
            }

            if (TryConsume(TokenType.Comma)) continue;
        }

        Next();
        return args;
    }

    private bool TryConsume(TokenType type)
    {
        return _curr != null && _curr.Type == type && Next();
    }

    private Token Consume(TokenType type, params string[] message)
    {
        if (_curr == null || _curr.Type != type)
        {
            throw new ParseException(
                _curr?.Line ?? 0,
                _curr?.Start ?? 0,
                _curr?.End ?? 0,
                message
            );
        }

        Next();
        return _prev!;
    }

    private bool Next()
    {
        _prev = _curr;
        _curr = _scanner.Scan();
        return _curr.Type != TokenType.Eof;
    }
}