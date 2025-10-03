using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;
using X.PagedList.Extensions;

namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 📌 Danh sách sản phẩm
        public ActionResult Index(string? productName, int? status, int page = 1)
        {
            int pageSize = 10;

            var query = _context.Products
                .Where(p => !p.isDeteled)
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .AsQueryable();

            // lọc theo tên sản phẩm
            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.ProductName.Contains(productName));
            }

            // lọc theo trạng thái
            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            var products = query
                .OrderByDescending(p => p.ProductID)
                .ToPagedList(page, pageSize);

            return View(products);
        }


        // 📌 Chi tiết sản phẩm
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductID == id);
            if (product == null) return NotFound();
            ProductFormViewModel pv = new ProductFormViewModel()
            {
                Product = product,
                Variants = product.ProductVariants.ToList()
            };
            return View(pv);
        }

        // 📌 Tạo sản phẩm + biến thể
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ProductFormViewModel();
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", vm.Product.CategoryID);
                return View(vm);
            }

            // 🖼 Upload ảnh đại diện sản phẩm
            if (vm.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_env.WebRootPath, "img");
                Directory.CreateDirectory(uploadsDir);

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageUpload.FileName);
                string filePath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageUpload.CopyToAsync(stream);
                }

                vm.Product.Image = fileName;
            }

            vm.Product.CreateAt = DateTime.Now;
            vm.Product.slug = vm.Product.ProductName.ToLower().Replace(" ", "-");

            // Lưu sản phẩm
            _context.Products.Add(vm.Product);
            await _context.SaveChangesAsync();

            // 📝 Lưu các biến thể
            if (vm.Variants != null && vm.Variants.Any())
            {
                foreach (var variant in vm.Variants)
                {
                    variant.ProductID = vm.Product.ProductID;

                    // upload ảnh variant (nếu có)
                    if (variant.ImageUpload != null)
                    {
                        string variantDir = Path.Combine(_env.WebRootPath, "img");
                        Directory.CreateDirectory(variantDir);

                        string fileName = Guid.NewGuid() + Path.GetExtension(variant.ImageUpload.FileName);
                        string filePath = Path.Combine(variantDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await variant.ImageUpload.CopyToAsync(stream);
                        }

                        variant.Image = fileName;
                    }

                    _context.ProductVariants.Add(variant);
                }
                await _context.SaveChangesAsync();
            }

            TempData["success"] = "Thêm sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        // 📌 Chỉnh sửa sản phẩm + biến thể
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            var vm = new ProductFormViewModel
            {
                Product = product,
                Variants = product.ProductVariants.ToList()
            };

            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductFormViewModel vm)
        {
            if (id != vm.Product.ProductID) return NotFound();

            var product = await _context.Products
                .Include(p => p.ProductVariants)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", vm.Product.CategoryID);
                return View(vm);
            }

            // update ảnh sản phẩm
            if (vm.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_env.WebRootPath, "img");
                Directory.CreateDirectory(uploadsDir);

                // xóa ảnh cũ
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var oldPath = Path.Combine(uploadsDir, product.Image);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ImageUpload.FileName);
                string filePath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageUpload.CopyToAsync(stream);
                }

                product.Image = fileName;
            }

            // update field chung
            product.ProductName = vm.Product.ProductName;
            product.ShortDescription = vm.Product.ShortDescription;
            product.Description = vm.Product.Description;
            product.CategoryID = vm.Product.CategoryID;
            product.Status = vm.Product.Status;
            product.slug = vm.Product.ProductName.ToLower().Replace(" ", "-");

            _context.Products.Update(product);

            // --- Xử lý variants ---
            var existingVariants = product.ProductVariants.ToList();

            // Xóa biến thể không còn trong form
            foreach (var oldVariant in existingVariants)
            {
                if (!vm.Variants.Any(v => v.VariantID == oldVariant.VariantID))
                {
                    _context.ProductVariants.Remove(oldVariant);
                }
            }

            // Xử lý variants mới
            
            foreach (var variant in vm.Variants)
            {
                if (variant.VariantID == 0) // biến thể mới
                {
                    var newVariant = new ProductVariant
                    {
                        ProductID = product.ProductID,
                        Color = variant.Color,
                        Size = variant.Size,
                        Price = variant.Price,
                        Quantity = variant.Quantity
                    };
                    Console.WriteLine($"Add new variant: ProductID={newVariant.ProductID}, Color={newVariant.Color}, Size={newVariant.Size}");

                    if (variant.ImageUpload != null)
                    {
                        string variantDir = Path.Combine(_env.WebRootPath, "img");
                        Directory.CreateDirectory(variantDir);

                        string fileName = Guid.NewGuid() + Path.GetExtension(variant.ImageUpload.FileName);
                        string filePath = Path.Combine(variantDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await variant.ImageUpload.CopyToAsync(stream);
                        }

                        newVariant.Image = fileName;
                    }

                    _context.ProductVariants.Add(newVariant);
                }
                else // Biến thể cũ
                {
                    var oldVariant = existingVariants.First(v => v.VariantID == variant.VariantID);
                    oldVariant.Color = variant.Color;
                    oldVariant.Size = variant.Size;
                    oldVariant.Price = variant.Price;
                    oldVariant.Quantity = variant.Quantity;

                    if (variant.ImageUpload != null)
                    {
                        string variantDir = Path.Combine(_env.WebRootPath, "img");
                        Directory.CreateDirectory(variantDir);

                        // Xóa ảnh cũ nếu có
                        if (!string.IsNullOrEmpty(oldVariant.Image))
                        {
                            var oldPath = Path.Combine(variantDir, oldVariant.Image);
                            if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                        }

                        string fileName = Guid.NewGuid() + Path.GetExtension(variant.ImageUpload.FileName);
                        string filePath = Path.Combine(variantDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await variant.ImageUpload.CopyToAsync(stream);
                        }

                        oldVariant.Image = fileName;
                    }

                    _context.ProductVariants.Update(oldVariant);
                }
            }

            await _context.SaveChangesAsync();


            TempData["success"] = "Cập nhật sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }

        // 📌 Xóa mềm
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.isDeteled = true;
            _context.Update(product);
            await _context.SaveChangesAsync();

            TempData["success"] = "Đã chuyển sản phẩm vào thùng rác!";
            return RedirectToAction(nameof(Index));
        }

        // 📌 Trash
        public async Task<IActionResult> Trash()
        {
            var trashed = await _context.Products
                .Where(p => p.isDeteled)
                .ToListAsync();

            return View(trashed);
        }

        // 📌 Khôi phục
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.isDeteled = false;
            _context.Update(product);
            await _context.SaveChangesAsync();

            TempData["success"] = "Khôi phục thành công!";
            return RedirectToAction(nameof(Trash));
        }

        // 📌 Xóa vĩnh viễn
        [HttpPost]
        public async Task<IActionResult> DeletePermen(int id)
        {
            var variantProduct = await _context.ProductVariants.Where(vp => vp.ProductID == id).ToListAsync();
            if(variantProduct != null)
            {
                // Xóa ảnh variant
                foreach (var v in variantProduct)
                {
                    if (!string.IsNullOrEmpty(v.Image))
                    {
                        var path = Path.Combine(_env.WebRootPath, "img", v.Image);
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    }
                }

                foreach (var vp in variantProduct)
                {
                    _context.ProductVariants.Remove(vp);
                }
                
            }
            

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null) return NotFound();

            // Xóa ảnh sản phẩm
            if (!string.IsNullOrEmpty(product.Image))
            {
                var path = Path.Combine(_env.WebRootPath, "img", product.Image);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }

            

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["success"] = "Đã xóa vĩnh viễn!";
            return RedirectToAction(nameof(Trash));
        }
    }
}
