namespace MFormatik.Application.Helpers;

public static class EventDispatcher
{
    private static readonly Dictionary<string, List<Action<object>>> Subscribers = new();

    public static void Subscribe(string message, Action<object> callback)
    {
        if (!Subscribers.ContainsKey(message))
            Subscribers[message] = new List<Action<object>>();
        Subscribers[message].Add(callback);
    }

    public static void Unsubscribe(string message, Action<object> callback)
    {
        if (Subscribers.ContainsKey(message))
            Subscribers[message].Remove(callback);
    }

    public static void Notify(string message, object param = null)
    {
        if (Subscribers.ContainsKey(message))
            foreach (var callback in Subscribers[message])
                callback(param);
    }
}

