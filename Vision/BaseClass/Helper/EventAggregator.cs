using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vision.BaseClass.Helper;

public class ConnectionStatusEventArgs : EventArgs
{
    public string ServerKey { get; }  // 唯一标识服务器（IP:Port）
    public bool IsConnected { get; }

    public ConnectionStatusEventArgs(bool isConnected,string serverKey = "")
    {
        ServerKey = serverKey;
        IsConnected = isConnected;
    }
}

public static class EventAggregator
{
    private static readonly WeakEventManager<ConnectionStatusEventArgs> _eventManager
        = new WeakEventManager<ConnectionStatusEventArgs>();

    public static void Subscribe(Action<ConnectionStatusEventArgs> handler)
    {
        _eventManager.Subscribe(handler);
    }

    public static void Unsubscribe(Action<ConnectionStatusEventArgs> handler)
    {
        _eventManager.Unsubscribe(handler);
    }

    public static void Publish(ConnectionStatusEventArgs args)
    {
        _eventManager.RaiseEvent(args);
    }
}

public class WeakEventManager<TEventArgs>
{
    private readonly List<WeakDelegate> _handlers = new List<WeakDelegate>();

    public void Subscribe(Action<TEventArgs> handler)
    {
        var weakDelegate = new WeakDelegate(handler);
        _handlers.Add(weakDelegate);
    }

    public void Unsubscribe(Action<TEventArgs> handler)
    {
        _handlers.RemoveAll(wd => !wd.IsAlive || wd.IsMatch(handler));
    }

    public void RaiseEvent(TEventArgs args)
    {
        _handlers.RemoveAll(wd => !wd.IsAlive);

        foreach (var handler in _handlers.ToArray())
        {
            handler.Invoke(args);
        }
    }

    private class WeakDelegate
    {
        private readonly WeakReference _weakTarget;
        private readonly MethodInfo _method;
        private readonly Type _delegateType;

        public bool IsAlive => _weakTarget.IsAlive;

        public WeakDelegate(Action<TEventArgs> handler)
        {
            _method = handler.Method;
            _delegateType = handler.GetType();

            if (handler.Target != null)
                _weakTarget = new WeakReference(handler.Target);
        }

        public void Invoke(TEventArgs args)
        {
            if (!IsAlive) return;

            var target = _weakTarget.Target;
            var del = (Action<TEventArgs>)Delegate.CreateDelegate(_delegateType, target, _method);
            del.Invoke(args);
        }

        public bool IsMatch(Action<TEventArgs> handler)
        {
            return handler.Target == _weakTarget?.Target &&
                   handler.Method == _method;
        }
    }
}

