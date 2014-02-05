﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mycroft.App
{
    public delegate void MsgHandler(dynamic data);
    public delegate void ConnectDisconnect();
    public class MessageEventHandler
    {
        private Dictionary<string, Delegate> events;

        public MessageEventHandler()
        {
            events = new Dictionary<string, Delegate>();
        }

        public void On(string msgType, MsgHandler del)
        {
            if (!events.ContainsKey(msgType))
                events.Add(msgType, null);
            events[msgType] = (MsgHandler)events[msgType] + del;
        }

        public void On(string msgType, ConnectDisconnect del)
        {
            if (!events.ContainsKey(msgType))
                events.Add(msgType, null);
            events[msgType] = (ConnectDisconnect)events[msgType] + del;
        }

        public void Handle(string msgType, dynamic data = null)
        {
            if (events.ContainsKey(msgType))
            {
                if (data == null)
                {
                    ConnectDisconnect c = (ConnectDisconnect)events[msgType];
                    c();
                }
                else
                {
                    MsgHandler h = (MsgHandler)events[msgType];
                    h(data);
                }
            }
            else
            {
                Logger.GetInstance().Warning("Not handling Message: " + msgType);
            }
        }
    }
}
