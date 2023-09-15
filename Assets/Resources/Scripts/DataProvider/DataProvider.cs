using System.Collections.Generic;

public class DataProvider<T> where T: IData
{
    private readonly List<T> _dataList;

    public DataProvider(List<T> dataList)
    {
        _dataList = dataList;
    }

    public TO GetObjectOfType<TO>() where TO: IData
    {
        foreach (T data in _dataList)
            if (data is TO seekingData)
                return seekingData;
        
        throw new($"Requested type {typeof(TO)} doesn't exist");
    }
}