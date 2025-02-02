using System.Reflection;
using System.Text;
using System.Transactions;
using System.Linq.Expressions;
using Xunit.Sdk;
using System.ComponentModel;
namespace FilterUnitTest;

public class FilterTest
{
    [Fact]
    public void FilterTests()
    {
        /*var test = Filter.Where("personId").Is(1);
        Assert.Equal("personId=1", test.ToString());

        test = Filter.Where("name").Is(1)
            .And("test").Is("hm");

        Assert.Equal("name=1 AND test=\"hm\"", test.ToString());*/

        Person p = new Person {
            FirstName = "Konrad",
            Age = 31
        };

        var filter = Filters.Where(() => p.FirstName == "Konrad");
        Assert.Equal("FirstName=\"Konrad\"", filter.ToString());

        filter = Filters.Where(() => p.Age == 31);
        Assert.Equal("Age=31", filter.ToString());
    }
}
public class Node
{
    public object Value = "";
    public Node? Next = null;
    public Node(object key)
    {
        Value = key;
    }
}
public class Filters
{
    Node? _start;
    Node? _current;
    public static Filters Where<T>(Expression<Func<T>> property) 
    {
        Filters f = new();
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
    Filters add(object value) 
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
public class Filter 
{
    Node? _start;
    Node? _current;
    public static Filter Where<T>(Expression<Func<T>> property)
    {
        Filter f = new();
        f.add("a").add("b");
        // Get the body of the expression
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
    public static Filter Where(string leftSide) 
    {
        Filter f = new() {
            _start = new Node(leftSide)
        };
        f._current = f._start;
        return f;
    }
    Filter add(object value) 
    {
        Node node = new(value);
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
    public Filter Is(int value) 
    {
        Node cmp = new("=");
        if (_current == null)
        {
            return this;
        }
        _current.Next = cmp;
        _current = cmp;
        _current.Next = new Node(value);
        _current = _current.Next;

        return this;
    }
    public Filter Is(string value) 
    {
        Node cmp = new("=");
        if (_current == null)
        {
            return this;
        }
        _current.Next = cmp;
        _current = cmp;
        _current.Next = new Node($"\"{value}\"");
        _current = _current.Next;

        return this;
    }
    public Filter And(string key)
    {
        if (_current == null)
        {
            return this;
        }
        _current.Next = new(" AND ");
        _current = _current.Next;
        _current.Next = new(key);        
        _current = _current.Next;
        return this;
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
}

public class Person
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public byte Age { get; set; } = 0;
}

