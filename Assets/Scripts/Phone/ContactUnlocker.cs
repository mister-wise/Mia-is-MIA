using System;
using SODefinitions;
using UnityEngine;

namespace Phone
{
    [Serializable]
    public struct ContactUnlocker
    {
        public ContactSO contact;
        public NotebookItemSO item;
    }
}