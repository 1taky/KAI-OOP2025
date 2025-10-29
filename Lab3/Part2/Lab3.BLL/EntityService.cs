using Lab3.DAL;

namespace Lab3.BLL;

public class EntityService
{
    private readonly EntityContext<Student> _context;

    public EntityService(EntityContext<Student> context)
    {
        _context = context;
    }

    public void AddStudent(Student s)
    {
        if (string.IsNullOrWhiteSpace(s.FirstName) || string.IsNullOrWhiteSpace(s.LastName))
        {
            throw new InvalidStudentDataException("First name or last name could not be empty!");
        }

        List<Student> all = _context.Load();
        foreach (Student st in all)
        {
            if (st.StudentId == s.StudentId)
            {
                throw new InvalidStudentDataException("Student with this ID already exist!");
            }
        }

        all.Add(s);
        _context.Save(all);
    }

    public void DeleteStudent(string id)
    {
        List<Student> all = _context.Load();
        bool found = false;

        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].StudentId == id)
            {
                all.RemoveAt(i);
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new StudentNotFoundException($"Student with ID {id} not found!");
        }

        _context.Save(all);
    }

    public List<Student> GetAllStudents() => _context.Load();

    public int CountMale3rdDorm()
    {
        List<Student> all = _context.Load();
        int count = 0;

        foreach (Student s in all)
        {
            if (s.Gender == Gender.Male && s.Course == 3 && s.IsInHostel())
            {
                count++;
            }
        }

        return count;
    }
}
