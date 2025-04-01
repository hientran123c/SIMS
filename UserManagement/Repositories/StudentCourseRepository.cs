// using Microsoft.EntityFrameworkCore;
// using UserManagement.Data;
// using UserManagement.Models;

// namespace UserManagement.Repositories
// {
//     public class StudentCourseRepository : IStudentCourseRepository
//     {
//         private readonly AppDbContext _context;

//         public StudentCourseRepository(AppDbContext context)
//         {
//             _context = context;
//         }

//         public void CreatStudentCourse(StudentCourse studentCourse)
//         {
//             _context.StudentCourses.Add(studentCourse);
//             _context.SaveChanges();
//         }

//         public bool CreateStudentCourse(StudentCourse studentCourse)
//         {
//             try
//             {
//                 _context.StudentCourses.Add(studentCourse);
//                 _context.SaveChanges();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 return false;
//             }
//         }

//         public bool DeleteStudentCourse(int id)
//         {
//             try
//             {
//                 var studentCourse = _context.StudentCourses.Find(id);
//                 if (studentCourse != null)
//                 {
//                     _context.StudentCourses.Remove(studentCourse);
//                     _context.SaveChanges();
//                     return true;
//                 }
//                 return false;
//             }
//             catch (Exception ex)
//             {
//                 return false;
//             }
//         }

//         public IEnumerable<StudentCourse> GetAllStudentCourses()
//         {
//             try
//             {
//                 return _context.StudentCourses.ToList();
//             }
//             catch (Exception ex)
//             {
//                 return Enumerable.Empty<StudentCourse>(); // null
//             }
//         }

//         public StudentCourse GetStudentCourseById(int id)
//         {
//             try
//             {
//                 return _context.StudentCourses.Find(id);
//             }
//             catch (Exception ex)
//             {
//                 return null;
//             }
//         }

//         public StudentCourse GetStudentCourseByStudentId(int studentId)
//         {
//             try
//             {
//                 return _context.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId);
//             }
//             catch (Exception ex)
//             {
//                 return null;
//             }
//         }

//         public StudentCourse GetStudentCourseByCourseId(int courseId)
//         {
//             try
//             {
//                 return _context.StudentCourses.FirstOrDefault(sc => sc.CourseId == courseId);
//             }
//             catch (Exception ex)
//             {
//                 return null;
//             }
//         }

//         public bool UpdateStudentCourse(StudentCourse studentCourse)
//         {
//             try
//             {
//                 _context.StudentCourses.Update(studentCourse);
//                 _context.SaveChanges();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 return false;
//             }
//         }

//         public IEnumerable<StudentCourse> GetCourses()
//         {
//             try
//             {
//                 return _context.StudentCourses.ToList();
//             }
//             catch (Exception ex)
//             {
//                 return Enumerable.Empty<StudentCourse>(); // null
//             }
//         }

//     }
// }
