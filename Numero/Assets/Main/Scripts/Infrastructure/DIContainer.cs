using System.Collections.Generic;
using UnityEngine;

public static class DIContainer
{

    private static readonly Dictionary<System.Type, Object> _references = new Dictionary<System.Type, Object>();

    public static void Bind<T>(T value) where T : Object
    {

        _references[typeof(T)] = value;

    }

    public static T Resolve<T>() where T : Object
    {

        if (_references.TryGetValue(typeof(T), out Object value))
            return (T)value;

        throw new System.InvalidCastException($"DIContainer: invalid resolve object {typeof(T)}");

    }

}