using RestaurantManagementApp;
using System;
using System.Collections.Generic;
using System.Linq;

RestaurantManagement restaurant = new RestaurantManagement(10); // Quản lý 10 bàn

bool running = true;

while (running)
{
    Console.WriteLine("Chọn chức năng:");
    Console.WriteLine("1. Quản lý thực đơn");
    Console.WriteLine("2. Quản lý bàn ăn");
    Console.WriteLine("3. Quản lý nhân viên");
    Console.WriteLine("4. Thống kê doanh thu");
    Console.WriteLine("5. Thanh toán");
    Console.WriteLine("6. Thoát");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            restaurant.ManageMenu();
            break;

        case "2":
            restaurant.ManageTables();
            break;

        case "3":
            if (restaurant.UserManager is UserManager userManager)
            {
                userManager.ManageEmployees();
            }
            else
            {
                Console.WriteLine("UserManager is not set correctly.");
            }
            break;

        case "4":
            restaurant.ShowStatistics();
            break;

        case "5":
            restaurant.ProcessPayment();
            break;

        case "6":
            running = false;
            break;

        default:
            Console.WriteLine("Lựa chọn không hợp lệ.");
            break;
    }
}

namespace RestaurantManagementApp
{
    // Lớp Table
    public class Table
    {
        public string TableId { get; set; }
        public int MaxCapacity { get; set; }
        public TableStatus Status { get; set; }

        public Table(string tableId, int maxCapacity)
        {
            TableId = tableId;
            MaxCapacity = maxCapacity;
            Status = TableStatus.Empty; // Mặc định bàn trống khi tạo
        }

        public override string ToString()
        {
            return $"Bàn {TableId} - Sức chứa: {MaxCapacity} người - Trạng thái: {Status}";
        }
    }

    // Enum trạng thái bàn
    public enum TableStatus
    {
        Empty,
        InUse,
        Paid
    }

    // Lớp Dish
    public class Dish
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public Dish(string name, double price, string description, string image)
        {
            Name = name;
            Price = price;
            Description = description;
            Image = image;
        }
    }

    // Lớp Invoice
    public class Invoice
    {
        public List<Dish> Dishes { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; }

        public Invoice()
        {
            Dishes = new List<Dish>();
            TotalAmount = 0;
            Date = DateTime.Now;
        }

        public void AddDish(Dish dish)
        {
            Dishes.Add(dish);
            TotalAmount += dish.Price;
        }

        public override string ToString()
        {
            return $"Hóa đơn: {Dishes.Count} món - Tổng tiền: {TotalAmount} VNĐ - Ngày: {Date.ToShortDateString()}";
        }
    }

    // Quản lý nhân viên
    public class UserManager
    {
        public List<Employee> Employees { get; set; }

        public UserManager()
        {
            Employees = new List<Employee>();
        }

        public void ManageEmployees()
        {
            bool employeeMenuRunning = true;
            while (employeeMenuRunning)
            {
                Console.WriteLine("Quản lý nhân viên:");
                Console.WriteLine("1. Thêm nhân viên");
                Console.WriteLine("2. Sửa thông tin nhân viên");
                Console.WriteLine("3. Xóa nhân viên");
                Console.WriteLine("4. Hiển thị nhân viên");
                Console.WriteLine("5. Quay lại menu chính");
                string choice = Console.ReadLine();

                Console.WriteLine(); // Dòng trống để tách các lựa chọn

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;

                    case "2":
                        UpdateEmployee();
                        break;

                    case "3":
                        RemoveEmployee();
                        break;

                    case "4":
                        DisplayEmployees();
                        break;

                    case "5":
                        employeeMenuRunning = false; // Quay lại menu chính
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        public void AddEmployee()
        {
            Console.Write("Nhập tên nhân viên: ");
            string name = Console.ReadLine();
            Console.Write("Nhập vai trò (Quản lý, Phục vụ, Thu ngân): ");
            string role = Console.ReadLine();

            Employees.Add(new Employee(name, role));
            Console.WriteLine("Nhân viên đã được thêm.");
        }

        public void UpdateEmployee()
        {
            Console.Write("Nhập tên nhân viên cần sửa: ");
            string name = Console.ReadLine();
            var employee = Employees.FirstOrDefault(e => e.Name == name);

            if (employee != null)
            {
                Console.Write("Nhập vai trò mới: ");
                employee.Role = Console.ReadLine();
                Console.WriteLine("Thông tin nhân viên đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy nhân viên.");
            }
        }

        public void RemoveEmployee()
        {
            Console.Write("Nhập tên nhân viên cần xóa: ");
            string name = Console.ReadLine();
            var employee = Employees.FirstOrDefault(e => e.Name == name);

            if (employee != null)
            {
                Employees.Remove(employee);
                Console.WriteLine("Nhân viên đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy nhân viên.");
            }
        }

        public void DisplayEmployees()
        {
            foreach (var employee in Employees)
            {
                Console.WriteLine($"Tên: {employee.Name} - Vai trò: {employee.Role}");
            }
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public Employee(string name, string role)
        {
            Name = name;
            Role = role;
        }
    }

    // Quản lý thực đơn, hóa đơn, bàn ăn
    namespace RestaurantManagementApp
    {
        // Enum trạng thái bàn
        public enum TableStatus
        {
            Empty,
            InUse,
            Paid
        }

        // Lớp Table
        public class Table
        {
            public string TableId { get; set; }
            public int MaxCapacity { get; set; }
            public TableStatus Status { get; set; }

            public Table(string tableId, int maxCapacity)
            {
                TableId = tableId;
                MaxCapacity = maxCapacity;
                Status = TableStatus.Empty; // Mặc định bàn trống khi tạo
            }

            public override string ToString()
            {
                return $"Bàn {TableId} - Sức chứa: {MaxCapacity} người - Trạng thái: {Status}";
            }
        }

        // Lớp Dish
        public class Dish
        {
            public string Name { get; set; }
            public double Price { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }

            public Dish(string name, double price, string description, string image)
            {
                Name = name;
                Price = price;
                Description = description;
                Image = image;
            }
        }

        // Lớp Invoice
        public class Invoice
        {
            public List<Dish> Dishes { get; set; }
            public double TotalAmount { get; set; }
            public DateTime Date { get; set; }

            public Invoice()
            {
                Dishes = new List<Dish>();
                TotalAmount = 0;
                Date = DateTime.Now;
            }

            public void AddDish(Dish dish)
            {
                Dishes.Add(dish);
                TotalAmount += dish.Price;
            }

            public override string ToString()
            {
                return $"Hóa đơn: {Dishes.Count} món - Tổng tiền: {TotalAmount} VNĐ - Ngày: {Date.ToShortDateString()}";
            }
        }

        // Quản lý nhân viên
        public class UserManager
        {
            public List<Employee> Employees { get; set; }

            public UserManager()
            {
                Employees = new List<Employee>();
            }

            public void ManageEmployees()
            {
                bool employeeMenuRunning = true;
                while (employeeMenuRunning)
                {
                    Console.WriteLine("Quản lý nhân viên:");
                    Console.WriteLine("1. Thêm nhân viên");
                    Console.WriteLine("2. Sửa thông tin nhân viên");
                    Console.WriteLine("3. Xóa nhân viên");
                    Console.WriteLine("4. Hiển thị nhân viên");
                    Console.WriteLine("5. Quay lại menu chính");
                    string choice = Console.ReadLine();

                    Console.WriteLine(); // Dòng trống để tách các lựa chọn

                    switch (choice)
                    {
                        case "1":
                            AddEmployee();
                            break;

                        case "2":
                            UpdateEmployee();
                            break;

                        case "3":
                            RemoveEmployee();
                            break;

                        case "4":
                            DisplayEmployees();
                            break;

                        case "5":
                            employeeMenuRunning = false; // Quay lại menu chính
                            break;

                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
            }

            public void AddEmployee()
            {
                Console.Write("Nhập tên nhân viên: ");
                string name = Console.ReadLine();
                Console.Write("Nhập vai trò (Quản lý, Phục vụ, Thu ngân): ");
                string role = Console.ReadLine();

                Employees.Add(new Employee(name, role));
                Console.WriteLine("Nhân viên đã được thêm.");
            }

            public void UpdateEmployee()
            {
                Console.Write("Nhập tên nhân viên cần sửa: ");
                string name = Console.ReadLine();
                var employee = Employees.FirstOrDefault(e => e.Name == name);

                if (employee != null)
                {
                    Console.Write("Nhập vai trò mới: ");
                    employee.Role = Console.ReadLine();
                    Console.WriteLine("Thông tin nhân viên đã được cập nhật.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên.");
                }
            }

            public void RemoveEmployee()
            {
                Console.Write("Nhập tên nhân viên cần xóa: ");
                string name = Console.ReadLine();
                var employee = Employees.FirstOrDefault(e => e.Name == name);

                if (employee != null)
                {
                    Employees.Remove(employee);
                    Console.WriteLine("Nhân viên đã được xóa.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nhân viên.");
                }
            }

            public void DisplayEmployees()
            {
                foreach (var employee in Employees)
                {
                    Console.WriteLine($"Tên: {employee.Name} - Vai trò: {employee.Role}");
                }
            }
        }

        public class Employee
        {
            public string Name { get; set; }
            public string Role { get; set; }

            public Employee(string name, string role)
            {
                Name = name;
                Role = role;
            }
        }

        // Quản lý thực đơn, hóa đơn, bàn ăn
        public class Restaurant
        {
            public List<Table> Tables { get; set; }
            public List<Dish> Menu { get; set; }
            public List<Invoice> Invoices { get; set; }
            public double Revenue { get; set; }

            public Restaurant(int tableCount)
            {
                Tables = new List<Table>();
                Menu = new List<Dish>();
                Invoices = new List<Invoice>();
                Revenue = 0;

                // Create tables
                for (int i = 0; i < tableCount; i++)
                {
                    Tables.Add(new Table((i + 1).ToString(), 4)); // Default capacity 4
                }
            }

            public void ManageMenu()
            {
                bool menuRunning = true;
                while (menuRunning)
                {
                    Console.Clear();  // Clear the console before displaying menu
                    Console.WriteLine("Quản lý thực đơn:");
                    Console.WriteLine("1. Hiển thị thực đơn");
                    Console.WriteLine("2. Thêm món");
                    Console.WriteLine("3. Xóa món");
                    Console.WriteLine("4. Quay lại menu chính");
                    string choice = Console.ReadLine();

                    Console.WriteLine(); // Dòng trống để tách các lựa chọn

                    switch (choice)
                    {
                        case "1":
                            DisplayMenu();
                            break;

                        case "2":
                            AddDish();
                            break;

                        case "3":
                            RemoveDish();
                            break;

                        case "4":
                            menuRunning = false; // Quay lại menu chính
                            break;

                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                            break;
                    }
                }
            }

            public void DisplayMenu()
            {
                Console.Clear();  // Clear the console before displaying menu
                if (Menu.Count == 0)
                {
                    Console.WriteLine("Thực đơn hiện tại trống.");
                }
                else
                {
                    foreach (var dish in Menu)
                    {
                        Console.WriteLine($"{dish.Name} - {dish.Price} VNĐ - {dish.Description}");
                    }
                }
                Console.WriteLine("Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
            }

            public void AddDish()
            {
                Console.Clear();  // Clear the console before adding dish
                Console.Write("Nhập tên món ăn: ");
                string name = Console.ReadLine();

                double price;
                while (true)
                {
                    Console.Write("Nhập giá món ăn: ");
                    if (double.TryParse(Console.ReadLine(), out price) && price > 0)
                        break;
                    Console.WriteLine("Giá món ăn không hợp lệ. Vui lòng nhập lại.");
                }

                Console.Write("Nhập mô tả món ăn: ");
                string description = Console.ReadLine();
                Console.Write("Nhập đường dẫn hình ảnh món ăn: ");
                string image = Console.ReadLine();

                Menu.Add(new Dish(name, price, description, image));
                Console.WriteLine("Món ăn đã được thêm vào thực đơn.");
                Console.WriteLine("Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
            }

            public void RemoveDish()
            {
                Console.Write("Nhập tên món ăn cần xóa: ");
                string name = Console.ReadLine();
                var dish = Menu.FirstOrDefault(d => d.Name == name);

                if (dish != null)
                {
                    Menu.Remove(dish);
                    Console.WriteLine("Món ăn đã được xóa khỏi thực đơn.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy món ăn.");
                }
                Console.WriteLine("Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
            }

            public void ManageTables()
            {
                bool tablesRunning = true;
                while (tablesRunning)
                {
                    Console.Clear();
                    Console.WriteLine("Quản lý bàn ăn:");
                    Console.WriteLine("1. Hiển thị bàn ăn");
                    Console.WriteLine("2. Chuyển khách");
                    Console.WriteLine("3. Quay lại menu chính");
                    string choice = Console.ReadLine();

                    Console.WriteLine();

                    switch (choice)
                    {
                        case "1":
                            DisplayTables();
                            break;

                        case "2":
                            TransferCustomer();
                            break;

                        case "3":
                            tablesRunning = false;
                            break;

                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
            }

            public void DisplayTables()
            {
                Console.Clear();
                foreach (var table in Tables)
                {
                    Console.WriteLine(table.ToString());
                }
                Console.WriteLine("Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
            }

            public void TransferCustomer()
            {
                Console.Write("Nhập số bàn cũ: ");
                string oldTableId = Console.ReadLine();
                var oldTable = Tables.FirstOrDefault(t => t.TableId == oldTableId);

                if (oldTable != null && oldTable.Status == TableStatus.InUse)
                {
                    Console.Write("Nhập số bàn mới: ");
                    string newTableId = Console.ReadLine();
                    var newTable = Tables.FirstOrDefault(t => t.TableId == newTableId);

                    if (newTable != null && newTable.Status == TableStatus.Empty)
                    {
                        oldTable.Status = TableStatus.Empty;
                        newTable.Status = TableStatus.InUse;
                        Console.WriteLine($"Đã chuyển khách từ bàn {oldTableId} sang bàn {newTableId}.");
                    }
                    else
                    {
                        Console.WriteLine("Bàn mới không có sẵn.");
                    }
                }
                else
                {
                    Console.WriteLine("Bàn cũ không hợp lệ hoặc không có khách.");
                }
                Console.WriteLine("Nhấn phím bất kỳ để quay lại.");
                Console.ReadKey();
            }

            public void CalculateRevenue()
            {
                Console.WriteLine($"Tổng doanh thu hiện tại: {Revenue} VNĐ");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Restaurant restaurant = new Restaurant(10);
                UserManager userManager = new UserManager();
                bool mainMenuRunning = true;

                while (mainMenuRunning)
                {
                    Console.Clear();
                    Console.WriteLine("Quản lý nhà hàng:");
                    Console.WriteLine("1. Quản lý thực đơn");
                    Console.WriteLine("2. Quản lý bàn ăn");
                    Console.WriteLine("3. Quản lý nhân viên");
                    Console.WriteLine("4. Thoát");
                    string choice = Console.ReadLine();

                    Console.WriteLine(); // Dòng trống để tách các lựa chọn

                    switch (choice)
                    {
                        case "1":
                            restaurant.ManageMenu();
                            break;

                        case "2":
                            restaurant.ManageTables();
                            break;

                        case "3":
                            userManager.ManageEmployees();
                            break;

                        case "4":
                            mainMenuRunning = false;
                            break;

                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
            }
        }
    }
}
