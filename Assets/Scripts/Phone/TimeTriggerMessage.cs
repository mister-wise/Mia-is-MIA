using System;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    [Serializable]
    public struct TimeTriggerMessage
    {
        public Message Message;
        public int Points;
        public bool Send;
    }
}