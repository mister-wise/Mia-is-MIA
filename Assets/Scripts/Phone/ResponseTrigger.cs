using System;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    [Serializable]
    public struct ResponseTrigger
    {
        public ContactSO contact;
        public string keyword;
        public string response;
    }
}