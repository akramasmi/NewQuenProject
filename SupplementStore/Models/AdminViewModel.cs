using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SupplementStore.Models
{
    // نموذج إدخال/تعديل المنتج في لوحة الإدارة (يدعم رفع صورة)
    public class ProductInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [Display(Name = "اسم المنتج")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "الفئة مطلوبة")]
        [Display(Name = "الفئة")]
        public string Category { get; set; } = "";

        [Required(ErrorMessage = "وصف المنتج مطلوب")]
        [Display(Name = "الوصف / التفاصيل")]
        public string Description { get; set; } = "";

        [Required]
        [Range(0.01, 1000000, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        [Display(Name = "السعر (ر.س)")]
        public decimal Price { get; set; }

        [Range(0, 1000000, ErrorMessage = "السعر القديم غير صالح")]
        [Display(Name = "السعر قبل الخصم (اختياري)")]
        public decimal? OldPrice { get; set; }

        [Range(0, 5, ErrorMessage = "التقييم من 0 إلى 5")]
        [Display(Name = "التقييم (0-5)")]
        public double Rating { get; set; } = 4.5;

        [Range(0, 1000000)]
        [Display(Name = "عدد التقييمات")]
        public int ReviewCount { get; set; }

        [Display(Name = "إيموجي بديل للصورة")]
        public string ImageEmoji { get; set; } = "💊";

        [Display(Name = "أو رابط صورة خارجي")]
        public string? ImageUrl { get; set; }

        [Display(Name = "فئة جديدة (اختياري)")]
        public string? NewCategory { get; set; }

        [Display(Name = "متوفر في المخزون")]
        public bool InStock { get; set; } = true;

        // ملف الصورة المرفوع من الجهاز
        [Display(Name = "رفع صورة من الجهاز (JPG/PNG/GIF/WebP)")]
        public IFormFile? ImageFile { get; set; }

        public Product ToProduct() => new()
        {
            Id = Id,
            Name = Name.Trim(),
            Category = !string.IsNullOrWhiteSpace(NewCategory) ? NewCategory.Trim() : Category.Trim(),
            Description = Description.Trim(),
            Price = Price,
            OldPrice = OldPrice > 0 ? OldPrice : null,
            Rating = Rating,
            ReviewCount = ReviewCount,
            ImageEmoji = string.IsNullOrWhiteSpace(ImageEmoji) ? "💊" : ImageEmoji,
            ImageUrl = string.IsNullOrWhiteSpace(ImageUrl) ? null : ImageUrl.Trim(),
            InStock = InStock
        };
    }
}
