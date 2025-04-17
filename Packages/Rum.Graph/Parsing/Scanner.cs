using System.Text;

namespace Rum.Graph.Parsing;

public class Scanner
{
    private byte Current => _end >= _bytes.Length ? TokenType.Eof.ToByte() : _bytes[_end];
    private byte[] Slice => _bytes[_start.._end];

    private int _line = 0;
    private int _start = 0;
    private int _end = 0;
    private readonly byte[] _bytes = [];

    public Scanner(params byte[] data)
    {
        _bytes = data;
    }

    public Scanner(string data)
    {
        _bytes = Encoding.UTF8.GetBytes(data);
    }

    public Scanner(params char[] data)
    {
        _bytes = Encoding.UTF8.GetBytes(data);
    }

    public Token Scan()
    {
        if (_end >= _bytes.Length)
        {
            return Create(TokenType.Eof);
        }

        _start = _end;
        var current = Current;
        _end++;

        switch ((char)current)
        {
            case ' ':
            case '\r':
            case '\t':
                return Scan();
            case '\n':
                _line++;
                return Scan();
            case '(':
                return Create(TokenType.LeftParen);
            case ')':
                return Create(TokenType.RightParen);
            case '{':
                return Create(TokenType.LeftBrace);
            case '}':
                return Create(TokenType.RightBrace);
            case ',':
                return Create(TokenType.Comma);
            case ':':
                return Create(TokenType.Colon);
            case '\'':
                return OnByte();
            case '"':
                return OnString();
            default:
                if (current.IsInt()) return OnNumeric();
                if (current.IsAlpha()) return OnIdentifier();
                throw Error("unexpected character");
        }
    }

    private Token OnByte()
    {
        _end++;

        if (Current != '\'')
        {
            throw Error("unterminated byte");
        }

        _start++;
        var token = Create(TokenType.Byte);
        _end++;
        return token;
    }

    private Token OnString()
    {
        while (Current != '"' && Current != 0)
        {
            if (Current == '\n')
            {
                _line++;
            }
            else if (Current == '\\')
            {
                OnEscape();
            }

            _end++;
        }

        if (_end == _bytes.Length)
        {
            throw Error("unterminated string");
        }

        _start++;
        var token = Create(TokenType.String);
        _end++;
        return token;
    }

    private void OnEscape()
    {
        _end++;

        switch (Current)
        {
            case (byte)'a':
                _bytes[_end] = (byte)'\a';
                break;
            case (byte)'b':
                _bytes[_end] = (byte)'\b';
                break;
            case (byte)'f':
                _bytes[_end] = (byte)'\f';
                break;
            case (byte)'n':
                _bytes[_end] = (byte)'\n';
                break;
            case (byte)'r':
                _bytes[_end] = (byte)'\r';
                break;
            case (byte)'t':
                _bytes[_end] = (byte)'\t';
                break;
            case (byte)'v':
                _bytes[_end] = (byte)'\v';
                break;
            case (byte)'\'':
                _bytes[_end] = (byte)'\'';
                break;
            case (byte)'"':
                _bytes[_end] = (byte)'"';
                break;
            case (byte)'\\':
                _bytes[_end] = (byte)'\\';
                break;
            default:
                throw Error("unknown escape sequence");
        }

        _end--;
    }

    private Token OnNumeric()
    {
        var type = TokenType.Int;

        while (Current.IsInt())
        {
            _end++;
        }

        if (Current == '.')
        {
            type = TokenType.Float;
            _end++;

            while (Current.IsInt())
            {
                _end++;
            }
        }

        return Create(type);
    }

    private Token OnIdentifier()
    {
        while (Current.IsAlpha() || Current.IsInt())
        {
            _end++;
        }

        var name = ToString();
        var type = TokenTypes.GetKeywordToken(name);
        return type != TokenType.Eof ? Create(type) : Create(TokenType.Identifier);
    }

    private Token Create(TokenType type)
    {
        return new()
        {
            Type = type,
            Line = _line,
            Start = _start,
            End = _end,
            Value = Slice
        };
    }

    private ParseException Error(params string[] message)
    {
        return new ParseException(
            _line,
            _start,
            _end,
            message
        );
    }

    public override string ToString()
    {
        return Encoding.UTF8.GetString(Slice) ?? string.Empty;
    }
}