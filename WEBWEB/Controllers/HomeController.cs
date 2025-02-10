using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBWEB.Data;
using WEBWEB.Models;

namespace WEBWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region Client
        public IActionResult GetAllClient()
        {
            List<User> users = _context.Users.Where(x=>x.IsAdmin == false).ToList();
            return View(users);
        }

        public IActionResult RemoveClient(int idUser)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == idUser);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("GetAllClient");
        }


        public IActionResult UpdateClient(int idUser)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == idUser);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateClient(int id,string firstName, string lastName, string password, string adress, string email)
        {

            User user = _context.Users.FirstOrDefault(x => x.Id == id);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.PasswordHash = password;
            user.Address = adress;
            user.IsAdmin = false;
            
            _context.Users.Update(user);
            _context.SaveChanges();


            return RedirectToAction("GetAllClient");
        }



        [HttpPost]
        public IActionResult CreateClient(string firstName, string lastName, string password, string adress, string email)
        {

            User user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = password,
                Address = adress,
                IsAdmin = false,
            };

            _context.Users.Add(user);
            _context.SaveChanges();


            return RedirectToAction("GetAllClient");
        }

        #endregion

        #region Employee
        public IActionResult GetAllEmployee()
        {
            List<User> users = _context.Users.Where(x => x.IsAdmin == true).ToList();
            return View(users);
        }

        public IActionResult RemoveEmployee(int idUser)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == idUser);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("GetAllEmployee");
        }


        public IActionResult UpdateEmployee(int idUser)
        {
            User user = _context.Users.FirstOrDefault(x => x.Id == idUser);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(int id, string firstName, string lastName, string password, string adress, string email)
        {

            User user = _context.Users.FirstOrDefault(x => x.Id == id);
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            user.PasswordHash = password;
            user.Address = adress;
            user.IsAdmin = true;

            _context.Users.Update(user);
            _context.SaveChanges();


            return RedirectToAction("GetAllEmployee");
        }



        [HttpPost]
        public IActionResult CreateEmployee(string firstName, string lastName, string password, string adress, string email)
        {

            User user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = password,
                Address = adress,
                IsAdmin = true,
            };

            _context.Users.Add(user);
            _context.SaveChanges();


            return RedirectToAction("GetAllEmployee");
        }

        #endregion

        #region Products


        public IActionResult GetAllProducts()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult RemoveProduct(int idProduct)
        {
            Product products = _context.Products.FirstOrDefault(x => x.Id == idProduct);
            _context.Products.Remove(products);
            _context.SaveChanges();
            return RedirectToAction("GetAllProducts");
        }


        public IActionResult UpdateProduct(int idProduct)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == idProduct);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(int id, string name, float price, string description, int idCatygory)
        {

            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Category = _context.Categories.FirstOrDefault(x => x.Id == idCatygory); 


            _context.Products.Update(product);
            _context.SaveChanges();


            return RedirectToAction("GetAllProducts");
        }



        [HttpPost]
        public IActionResult CreateProduct(string name, float price, string description, int idCatygory)
        {

            Product product = new Product()
            {
                Name = name,
                Price = price,
                Category = _context.Categories.FirstOrDefault(x=>x.Id == idCatygory),
                Description = description
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("GetAllProducts");
        }




        #endregion

        #region Order

        public IActionResult GetAllOrders()
        {
            List<Order> order = _context.Orders.ToList();
            List<OrderViewModel> orderViewModel = new List<OrderViewModel>();

            foreach(var item in order)
            {
                OrderViewModel orderView = new OrderViewModel()
                {
                    OrderId = item.Id,
                    Status = item.Status,
                    DateCreate = item.DateCreated,
                    Products = _context.OrderItems.Where(x => x.OrderId == item.Id).Select(x => x.Product).ToList(),
                };
                orderViewModel.Add(orderView);
            }
            return View(orderViewModel);
        }

        public IActionResult RemoveOrder(int idOrder)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == idOrder);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("GetAllOrders");
        }


        [HttpPost]
        public IActionResult CreateOrder(DateTime dateTime, float totalAmount, string status , int idUser)
        {

            Order order  = new Order()
            {
                DateCreated = dateTime,
                TotalAmount = totalAmount,
                Status = status,
                User = _context.Users.FirstOrDefault(x=>x.Id == idUser)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("GetAllOrders");
        }


        #endregion


        #region Query

        public IActionResult GetAllQuery()
        {
            return View();
        }



        public IActionResult Query1( List<User> user)
        {
            if (user.Count == 0)
            {
                List<User> users = _context.Users.Where(x=>x.IsAdmin == false).ToList();
                return View(users);
            }
            else
            {
                return View(user);

            }
        }

        [HttpPost]
        public IActionResult Query1(string name)
        {
            List<User> user = _context.Users.Where(x=>x.IsAdmin ==false && x.FirstName.ToLower().Equals(name.ToLower())).ToList();
            return View(user);
        }


        public IActionResult Query2(List<User> user)
        {
            if (user.Count == 0)
            {
                List<User> users = _context.Users.Where(x => x.IsAdmin == false).ToList();
                return View(users);
            }
            else
            {
                return View(user);

            }
        }

        [HttpPost]
        public IActionResult Query2(string email)
        {
            List<User> user = _context.Users.Where(x => x.IsAdmin == false && x.Email.ToLower().Equals(email.ToLower())).ToList();
            return View(user);
        }


        public IActionResult Query3(List<User> user)
        {
            if (user.Count == 0)
            {
                List<User> users = _context.Users.Where(x => x.IsAdmin == true).ToList();
                return View(users);
            }
            else
            {
                return View(user);

            }
        }

        [HttpPost]
        public IActionResult Query3(string name)
        {
            List<User> user = _context.Users.Where(x => x.IsAdmin == true && x.FirstName.ToLower().Equals(name.ToLower())).ToList();
            return View(user);
        }


        public IActionResult Query4(List<User> user)
        {
            if (user.Count == 0)
            {
                List<User> users = _context.Users.Where(x => x.IsAdmin == true).ToList();
                return View(users);
            }
            else
            {
                return View(user);

            }
        }

        [HttpPost]
        public IActionResult Query4(string email)
        {
            List<User> user = _context.Users.Where(x => x.IsAdmin == true && x.Email.ToLower().Equals(email.ToLower())).ToList();
            return View(user);
        }


        public IActionResult Query5(List<OrderViewModel> order)
        {
            if (order.Count == 0)
            {
                List<Order> orders = _context.Orders.ToList();
                List<OrderViewModel> orderss = new List<OrderViewModel>();
                foreach (var item in orders)
                {
                    OrderViewModel orderViewModel = new OrderViewModel()
                    {
                        OrderId = item.Id,
                        DateCreate = item.DateCreated,
                        Products = _context.OrderItems.Where(x => x.Order.Id == item.Id).Select(x => x.Product).ToList(),
                        Status = item.Status,
                    };
                    orderss.Add(orderViewModel);
                }
                return View(orderss);
            }
            else
            {
                return View(order);

            }
        }

        [HttpPost]
        public IActionResult Query5(int idUser)
        {
            List<Order> order = _context.Orders.Where(x=>x.UserId == idUser).ToList();
            List<OrderViewModel> orderss = new List<OrderViewModel>();
            foreach (var item in order)
            {
                OrderViewModel orderViewModel = new OrderViewModel()
                {
                    OrderId = item.Id,
                    DateCreate = item.DateCreated,
                    Products = _context.OrderItems.Where(x => x.Order.Id == item.Id).Select(x => x.Product).ToList(),
                    Status = item.Status,
                };
                orderss.Add(orderViewModel);
            }
            return View(orderss);
        }


        #endregion

    }
}
