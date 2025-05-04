//using MediCore.Models;
//using MediCore.Enums;

//namespace MediCore.Data
//{
//    public class AddDepartmentsDATA
//    {
//        public static void InitializeData(IApplicationBuilder app)
//        {
//            using var scope = app.ApplicationServices.CreateScope();
//            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

//            var departments = new List<Department>
//{
//    new Department { DepartmentType = "Cardiology" },
//    new Department { DepartmentType = "Neurology" },
//    new Department { DepartmentType = "Orthopedics" },
//    new Department { DepartmentType = "Pediatrics" },
//    new Department { DepartmentType = "Dermatology" },
//    new Department { DepartmentType = "Psychiatry" },
//    new Department { DepartmentType = "Gastroenterology" },
//    new Department { DepartmentType = "Radiology" }
//};

//            foreach (var department in departments)
//            {
//                var exists = context.Departments.Any(d => d.DepartmentType == department.DepartmentType);
//                if (!exists)
//                {
//                    context.Departments.Add(department);
//                }
//            }
//            context.SaveChanges();
//        }
//    }
//}
