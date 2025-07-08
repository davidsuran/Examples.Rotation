using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CameraControllerDemo.KeyBinder;

namespace CameraControllerDemo
{
    public class EventDispatcher
    {
        private readonly Dictionary<InputEventType, Delegate> _handlers = new();
        private static EventDispatcher _instance;

        public static event EventHandler<EventArgs> OnMoveLeft;
        public static event EventHandler<EventArgs> OnMoveRight;
        public static event EventHandler<EventArgs> OnMoveForward;
        public static event EventHandler<EventArgs> OnMoveBackward;
        public static event EventHandler<EventArgs> OnZoomIn;
        public static event EventHandler<EventArgs> OnZoomOut;
        public static event EventHandler<EventArgs> OnOrbit;

        public static EventDispatcher Instance
        {
            get
            {
                _instance ??= new EventDispatcher();
                return _instance;
            }
            private set => _instance = value;
        }

        private EventDispatcher()
        {
            Subscribe(InputEventType.MoveLeft, OnMoveLeft);
            Subscribe(InputEventType.MoveRight, OnMoveRight);
            Subscribe(InputEventType.MoveForward, OnMoveForward);
            Subscribe(InputEventType.MoveBackward, OnMoveBackward);
            Subscribe(InputEventType.ZoomIn, OnZoomIn);
            Subscribe(InputEventType.ZoomOut, OnZoomOut);
            Subscribe(InputEventType.Orbit, OnOrbit);
        }

        public void Subscribe<TEventArgs>(InputEventType @event, EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            if (_handlers.TryGetValue(@event, out var existing))
            {
                _handlers[@event] = Delegate.Combine(existing, handler);
            }
            else
            {
                _handlers[@event] = handler;
            }
        }

        public void Unsubscribe<TEventArgs>(InputEventType @event, EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            if (_handlers.TryGetValue(@event, out var existing))
            {
                var result = Delegate.Remove(existing, handler);
                if (result == null)
                    _handlers.Remove(@event);
                else
                    _handlers[@event] = result;
            }
        }

        public void Raise<TEventArgs>(InputEventType @event, object sender, TEventArgs args)
            where TEventArgs : EventArgs
        {
            if (_handlers.TryGetValue(@event, out var del) &&
                del is EventHandler<TEventArgs> handler)
            {
                handler.Invoke(sender, args);
            }
        }

        public void Raise(InputEventType @event)
        {
            Raise<EventArgs>(@event, null, null);
        }
    }

    public interface IAppEventArgsMap<TEnum> where TEnum : Enum
    {
        Type GetEventArgsType(TEnum @event);
    }
}
