using Microsoft.AspNetCore.Mvc;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopApp1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {

        // POST api/<InitialDataController>
        [HttpPost]
        public IActionResult Post()
        {
            var context = new ShopApp1Context();
            if (context.Users.Any())
            {
                return Conflict();
            }
            var categories = new List<Category>
            {
                new Category{ Name = "Men" },
                new Category{ Name = "Women" },
                new Category{ Name = "Unisex" }
                
            };
            var subcategories = new List<Category>
            {
                new Category{ Name = "Sweater" , ParentCategory = categories.ElementAt(0) },
                new Category{ Name = "Sweater" , ParentCategory = categories.ElementAt(1) },
                new Category{ Name = "Shirt" , ParentCategory = categories.ElementAt(0) },
                new Category{ Name = "Shirt" , ParentCategory = categories.ElementAt(1) },
                new Category{ Name = "Shirt" , ParentCategory = categories.ElementAt(2) },
                new Category{ Name = "Jeans" , ParentCategory = categories.ElementAt(0) },
                new Category{ Name = "Jeans" , ParentCategory = categories.ElementAt(1) },
                new Category{ Name = "Dress" , ParentCategory = categories.ElementAt(1) },
                new Category{ Name = "Skirt" , ParentCategory = categories.ElementAt(1) },
                new Category{ Name = "Cargo shorts" , ParentCategory = categories.ElementAt(0) },
                new Category{ Name = "Hoodie" , ParentCategory = categories.ElementAt(2) },
                new Category{ Name = "Ski hat" , ParentCategory = categories.ElementAt(2) },
                new Category{ Name = "Poncho" , ParentCategory = categories.ElementAt(2) },
            };
            var materials = new List<Material>
            {
                new Material{ Name = "Cotton" },
                new Material{ Name = "Silk" },
                new Material{ Name = "Wool" },
                new Material{ Name = "Leather" },
                new Material{ Name = "Satin" },
                new Material{ Name = "Denim" },
                new Material{ Name = "Polyester" },
                new Material{ Name = "Viscose" }
            };
            var users = new List<User>
            {
                new User
                {
                    Email = "user@mail.com",
                    FirstName = "User",
                    LastName = "User",
                    Username = "user",
                    Password = "Useruser1%"
                },
                new User
                {
                    Email = "admin@mail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Username = "admin",
                    Password = "Adminadmin1%"
                }
            };
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Satin wrapover dress",
                    Description = "Short dress in satin with a slight sheen. Pointed collar and a wrapover front with wide ties at one side and concealed ties at other side. ",
                    Price = 15,
                    Category = subcategories.ElementAt(7)
                },
                new Product
                {
                    Name = "Rain Poncho",
                    Description = "Classic protection plus durability! Rain Poncho boasts total coverage for those outside events and activities. ",
                    Price = 20,
                    Category = subcategories.Last()
                },
                new Product
                {
                    Name = "Slim cargo shorts",
                    Description = "Adjustable toggle waist. Large pockets. Slim fit.",
                    Price = 11,
                    Category = subcategories.ElementAt(9)
                },
                new Product
                {
                    Name = "Short Leather Skirt",
                    Description = "Short skirt in cotton-blend leather. High waist with concealed waistband. Unlined.",
                    Price = 12,
                    Category = subcategories.ElementAt(8)
                },
                new Product
                {
                    Name = "Regular Fit Cotton Shirt",
                    Description = "Short-sleeved shirt in woven cotton fabric with a turn-down collar. Regular Fit – classic fit with good room for movement and gently shaped waist for a comfortable, tailored silhouette.",
                    Price = 8,
                    Category = subcategories.ElementAt(2)
                },
                new Product
                {
                    Name = "90s Baggy Low Jeans",
                    Description = "Loose-fit, 90s-inspired 5-pocket jeans in thick cotton denim. Low waist, zip fly with button, and straight legs.",
                    Price = 25,
                    Category = subcategories.ElementAt(6)
                }

            };
            var prod_mat = new List<ProductMaterial>
            {
                new ProductMaterial
                {
                    Product = products.ElementAt(0),
                    Material = materials.ElementAt(4)
                },
                new ProductMaterial
                {
                    Product = products.ElementAt(1),
                    Material = materials.ElementAt(6)
                },
                new ProductMaterial
                {
                    Product = products.ElementAt(2),
                    Material = materials.ElementAt(0)
                },
                new ProductMaterial
                {
                    Product = products.ElementAt(3),
                    Material = materials.ElementAt(0)
                },
                new ProductMaterial
                {
                    Product = products.ElementAt(3),
                    Material = materials.ElementAt(3)
                },
                new ProductMaterial
                {
                    Product = products.ElementAt(4),
                    Material = materials.ElementAt(0)
                },
                new ProductMaterial
                {
                    Product = products.ElementAt(5),
                    Material = materials.ElementAt(5)
                }

            };
            var order_statuses = new List<OrderStatus>
            {
                new OrderStatus
                {
                    Name = "Processing"
                },
                new OrderStatus
                {
                    Name = "Delivered"
                },
                new OrderStatus
                {
                    Name = "Received"
                },
                new OrderStatus
                {
                    Name = "Terminated"
                },

            };
            List<int> userUseCases = new List<int>();
            for (int i = 2; i <= 9; i++)
            {
                userUseCases.Add(i);
            }
            List<int> adminUseCases = new List<int>();
            for (int i = 10; i <= 25; i++)
            {
                adminUseCases.Add(i);
            }

            userUseCases.ForEach(x => context.UserUseCases.Add(new UserUseCase
            {
                UserUseCaseId = x,
                User = users.ElementAt(0)
            }));
            adminUseCases.ForEach(x => context.UserUseCases.Add(new UserUseCase
            {
                UserUseCaseId = x,
                User = users.ElementAt(1)
            }));

            context.Categories.AddRange(categories);
            context.Materials.AddRange(materials);
            context.Products.AddRange(products);
            context.ProductMaterials.AddRange(prod_mat);
            context.Users.AddRange(users);
            context.OrderStatuses.AddRange(order_statuses);
            context.SaveChanges();
            return StatusCode(201);
        }

        
    }
}
