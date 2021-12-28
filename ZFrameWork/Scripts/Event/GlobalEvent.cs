using System;
namespace EventG
{
    public class DoubleInfo
    {
        public object info1;
        public object info2;
        public DoubleInfo(object pInfo1,object pInfo2) {
            info1 = pInfo1;
            info2 = pInfo2;
        }
    }

    public class GlobalEvent
    {
        private static StringKeySender oneSender = new StringKeySender();
        public static void AddListener(string eventType, Action<object> eventHandler)
        {
            oneSender.AddListener(eventType, eventHandler);
        }
        public static void RemoveListener(string eventType, Action<object> eventHandler)
        {
            oneSender.RemoveListener(eventType, eventHandler);
        }
        public static void SendMessage(string eventType, object pObj)
        {
            oneSender.SendMessage(eventType, pObj);
        }
    }
    public class StringKeySender
    {
        private EventSender<String, object> sender = new EventSender<String, object>();
        public void Clear()
        {
            sender.Clear();
            sender = null;
        }
        public void AddListener(String eventType, Action<object> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }
        public void RemoveListener(String eventType, Action<object> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public void SendMessage(String eventType, object pObj)
        {
            sender.SendMessage(eventType, pObj);
        }
    }
    public class OneSender
    {
        private EventSender<Enum, object> sender = new EventSender<Enum, object>();
        public void Clear()
        {
            sender.Clear();
            sender = null;
        }
        public void AddListener(Enum eventType, Action<object> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }
        public void RemoveListener(Enum eventType, Action<object> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public void SendMessage(Enum eventType, object pObj)
        {
            sender.SendMessage(eventType, pObj);
        }
    }
}
