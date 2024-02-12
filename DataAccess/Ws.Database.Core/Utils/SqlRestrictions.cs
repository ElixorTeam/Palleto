﻿namespace Ws.Database.Core.Utils;

public static class SqlRestrictions
{
    #region Equal

    public static ICriterion Equal(string propertyName, object value) => Restrictions.Eq(propertyName, value);
    
    public static ICriterion NotEqual(string propertyName, object value) => 
        Restrictions.Not(Restrictions.Eq(propertyName, value));
    
    #endregion

    #region Less

    public static ICriterion Less(string propertyName, object value) => Restrictions.Lt(propertyName, value);
    
    public static ICriterion LessOrEqual(string propertyName, object value) => Restrictions.Le(propertyName, value);
    
    #endregion

    #region More
    
    public static ICriterion More(string propertyName, object value) => Restrictions.Gt(propertyName, value);
    
    public static ICriterion MoreOrEqual(string propertyName, object value) => Restrictions.Ge(propertyName, value);
    
    #endregion

    #region In

    public static ICriterion In<T>(string propertyName, List<T> value) => Restrictions.In(propertyName, value);
    
    public static ICriterion NotIn(string propertyName, List<object> value) => 
        Restrictions.Not(Restrictions.In(propertyName, value));

    #endregion
}