// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using RulesManager.Implementations;
using RulesManager.Models;


List<Rule> rules = new List<Rule>()
{
    new Rule("Age", "GreaterThan", "20", "Age must be greater than 20"),
    new Rule("Name", "Equal", "John", "Name must equal to John")
};

var user1 = new User()
{
    Age = 10, Name ="Apple"
};

var user2 = new User()
{
    Age = 17,
    Name = "John"
};

var user3 = new User()
{
    Name = "Ram",
    Age = 40
};

ExpressionBuilder builder = new ExpressionBuilder();
foreach(var rule in rules)
{
    Func<User, bool> compiledRule = builder.CompileRule<User>(rule);
    if(!compiledRule(user1))
    {
        Console.WriteLine(rule.ErrorMessage);
    }
}
