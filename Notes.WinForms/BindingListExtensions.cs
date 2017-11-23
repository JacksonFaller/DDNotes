using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Notes.WinForms
{
    static class BindingListExtensions
    {
        public static void AddRange<T>(this BindingList<T> list, IEnumerable<T> values)
        {
            bool restore = list.RaiseListChangedEvents;

            try
            {
                list.RaiseListChangedEvents = false;
                foreach (var value in values)
                {
                    list.Add(value);
                }
            }
            finally
            {
                list.RaiseListChangedEvents = restore;
                if (list.RaiseListChangedEvents)
                    list.ResetBindings();
            }
        }
    }
}
