using System.Collections.Concurrent;

namespace BlazorApp.Data;

public interface IStateService
{
    void Set<T>(string key, T value);
    bool TryGet<T>(string key, out T value);
    T Get<T>(string key, T defaultValue = default);
    void Remove(string key);
    bool Contains(string key);
}


public class StateService : IStateService
{
    private readonly ConcurrentDictionary<string, object> _state = new();
    public void Set<T>(string key, T value)
    {
        _state.AddOrUpdate(key, value!, (k, oldValue) => value!);
    }
    public bool TryGet<T>(string key, out T value)
    {
        if (_state.TryGetValue(key, out object? storedValue) && storedValue is T castValue)
        {
            value = castValue;
            return true;
        }

        value = default!;
        return false;
    }
    public T Get<T>(string key, T defaultValue = default)
    {
        return TryGet(key, out T value) ? value : defaultValue;
    }
    public void Remove(string key)
    {
        _state.TryRemove(key, out _);
    }
    public bool Contains(string key)
    {
        return _state.ContainsKey(key);
    }
}