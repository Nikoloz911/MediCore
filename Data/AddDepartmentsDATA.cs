using MediCore.Models;
using MediCore.Enums;

namespace MediCore.Data
{
    public static class AddDepartmentsDATA
    {
        public static readonly DataContext _context = new DataContext();

        public static void SeedDepartmentsData(DataContext context)
        {
            var departments = new List<Department>
            {
                new Department { DepartmentType = DEPARTMENT_TYPE.Cardiology, DepartmentName = "Cardiology" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Neurology, DepartmentName = "Neurology" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Orthopedics, DepartmentName = "Orthopedics" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Pediatrics, DepartmentName = "Pediatrics" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Dermatology, DepartmentName = "Dermatology" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Psychiatry, DepartmentName = "Psychiatry" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Gastroenterology, DepartmentName = "Gastroenterology" },
                new Department { DepartmentType = DEPARTMENT_TYPE.Radiology, DepartmentName = "Radiology" }
            };

            foreach (var department in departments)
            {
                var existingDepartment = _context.Departments
                    .FirstOrDefault(d => d.DepartmentType == department.DepartmentType);

                if (existingDepartment == null)
                {
                    context.Departments.Add(department);
                }
            }
            context.SaveChanges();
        }
        public static void InitializeDepartmentsData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            SeedDepartmentsData(context);
        }
    }
}