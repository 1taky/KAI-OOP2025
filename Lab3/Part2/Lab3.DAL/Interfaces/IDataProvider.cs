namespace Lab3.DAL;

public interface IDataProvider<T>
{
    string FileExtension { get; }
    void Save(List<T> data, string fileName);
    List<T> Load(string fileName);
}
