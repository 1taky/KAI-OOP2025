using Lab3.DAL;

namespace Lab3.BLL;

public class EntityService
{
    private readonly EntityContext<Student> _context;

    public EntityService(EntityContext<Student> ctx)
    {
        _context = ctx;
    }

    public void AddStudent(Student s)
    {
        List<Student> list = _context.Load();
        list.Add(s);
        _context.Save(list);
    }

    public List<Student> GetAllStudents()
    {
        return _context.Load();
    }

    public void DeleteStudent(string id)
    {
        List<Student> list = _context.Load();
        Student found = list.FirstOrDefault(s => s.StudentId == id);
        if (found != null)
        {
            list.Remove(found);
            _context.Save(list);
        }
    }

    public int CountMale3rdDorm()
    {
        List<Student> list = _context.Load();
        return list.Count(s => s.Gender == Gender.Male && s.Course == 3 && s.LivesInHostel);
    }
}
