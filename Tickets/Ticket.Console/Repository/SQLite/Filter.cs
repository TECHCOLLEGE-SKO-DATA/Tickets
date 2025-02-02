using System.Text;
using System.Linq.Expressions;
namespace Ticket.Console.Repository.SQLite;

public class Node
{
    public object Value = "";
    public Node? Next = null;
    public Node(object key)
    {
        Value = key;
    }
}
public class Filter
{
    Node? _start;
    Node? _current;
    public static Filter Where<T>(Expression<Func<T>> property) 
    {
        Filter f = new();
        if (property.Body is MemberExpression memberExpression)
        {
            // Get the property name
            string propertyName = memberExpression.Member.Name;
            f.add(propertyName);
        }
        else if (property.Body is BinaryExpression binaryExpression)
        {
            f.add(binaryExpression.Left);
            switch (binaryExpression.NodeType) 
            {
                case ExpressionType.Equal: f.add("="); break;
                case ExpressionType.NotEqual: f.add("!="); break;
                case ExpressionType.GreaterThan: f.add(">"); break;
                case ExpressionType.GreaterThanOrEqual: f.add(">="); break;
                case ExpressionType.LessThan: f.add("<"); break;
                case ExpressionType.LessThanOrEqual: f.add("<="); break;
            }
            f.add(binaryExpression.Right);
            
        }
        else
        {
            throw new ArgumentException("Expression is not a member expression");
        }
        return f;
    }
    public override string ToString()
    {
        StringBuilder sb = new();
        _current = _start;
        while (true) {
            if (_current == null) break;
            sb.Append(_current.Value);
            _current = _current.Next;
        }
        return sb.ToString();
    }
    Filter add(object value) 
    {
        Node node;
        if (value is ConstantExpression constantExpression && constantExpression.Value != null) {
            if (constantExpression.Value is string || constantExpression.Value is char || constantExpression.Value is DateTime) {
                node = new($"\"{constantExpression.Value}\"");    
            } 
            else 
            {
                node = new(constantExpression.Value);
            }
        } else if (value is MemberExpression memberExpression) {
            node = new(memberExpression.Member.Name);
        } else {
            node = new(value);
        }
        if (_start == null) 
        {
            _start = node;
            _current = _start;
        }
        else
        {
            _current.Next = node;
            _current = _current.Next;
        }
        return this;
    }
}