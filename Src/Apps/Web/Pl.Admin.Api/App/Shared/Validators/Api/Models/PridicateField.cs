using LinqKit;

namespace Pl.Admin.Api.App.Shared.Validators.Api.Models;

public record PredicateField<T>(ExpressionStarter<T> Predicate, string FieldName) where T : class
{
    public PredicateField(Expression<Func<T, bool>> expression, string fieldName)
        : this(PredicateBuilder.New(expression), fieldName)
    {
    }
}