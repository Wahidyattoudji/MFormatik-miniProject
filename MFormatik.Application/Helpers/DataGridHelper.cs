using System.Collections.ObjectModel;

namespace MFormatik.Application.Helpers;


public static class DataGridHelper
{
    public static bool IsEmpty<T>(ObservableCollection<T> collection)
    {
        return collection?.Count == 0;
    }
}

