namespace SupplementStore.Models
{
    // مستودع بيانات بسيط (In-Memory) للمنتجات
    public static class ProductRepository
    {
        public static List<Product> Products { get; } = new()
        {
            new Product { Id = 1, Name = "ويت بروتين ستايمن 2 كجم", Category = "بروتين", Description = "بروتين مصل اللبن عالي الجودة لدعم بناء العضلات وتعافيها، بنكهة الشوكولاتة.", Price = 320, OldPrice = 380, Rating = 4.7, ReviewCount = 1245, ImageEmoji = "🥛" },
            new Product { Id = 2, Name = "كرياتين مونوهيدريت 300 جم", Category = "أداء وطاقة", Description = "كرياتين أحادي الهيدرات نقي لزيادة القوة والحجم العضلي والأداء أثناء التمارين.", Price = 145, OldPrice = null, Rating = 4.8, ReviewCount = 980, ImageEmoji = "⚡" },
            new Product { Id = 3, Name = "BCAA أحماض أمينية متفرعة", Category = "تعافي", Description = "نسبة 2:1:1 من الأحماض الأمينية المتفرعة لتقليل هدم العضلات وتسريع التعافي.", Price = 190, OldPrice = 220, Rating = 4.5, ReviewCount = 430, ImageEmoji = "💪" },
            new Product { Id = 4, Name = "محرّك التمرين ما قبل التدريب", Category = "أداء وطاقة", Description = "تركيبة كافيين وبيتا ألانين وأرجينين لمنحك طاقة وتركيزًا عاليًا قبل التمرين.", Price = 165, OldPrice = null, Rating = 4.4, ReviewCount = 356, ImageEmoji = "🔥" },
            new Product { Id = 5, Name = "جلوتامين 500 جم", Category = "تعافي", Description = "الجلوتامين يدعم تعافي العضلات ويقوي المناعة خلال فترات التدريب المكثف.", Price = 120, OldPrice = 140, Rating = 4.3, ReviewCount = 210, ImageEmoji = "🧬" },
            new Product { Id = 6, Name = "أوميغا 3 زيت سمك", Category = "صحة عامة", Description = "أحماض دهنية أساسية EPA و DHA لصحة المفاصل والقلب وتقليل الالتهابات.", Price = 95, OldPrice = null, Rating = 4.6, ReviewCount = 540, ImageEmoji = "🐟" },
            new Product { Id = 7, Name = "فيتامين د3 + زنك", Category = "صحة عامة", Description = "يدعم صحة العظام والمناعة ومستويات التستوستيرون الطبيعية للرياضيين.", Price = 75, OldPrice = 90, Rating = 4.5, ReviewCount = 300, ImageEmoji = "☀️" },
            new Product { Id = 8, Name = "واي بروتين آيزوليت خالٍ من اللاكتوز", Category = "بروتين", Description = "بروتين معزول بنسبة نقاء 90% مناسب لأنظمة التنشيف ومن لا يتحملون اللاكتوز.", Price = 410, OldPrice = null, Rating = 4.8, ReviewCount = 620, ImageEmoji = "💎" },
            new Product { Id = 9, Name = "ماس جينر زيادة الوزن 3 كجم", Category = "زيادة الوزن", Description = "سعرات حرارية عالية مع كربوهيدرات معقدة وبروتين لمساعدة نحيلي البنية على اكتساب الوزن.", Price = 280, OldPrice = 330, Rating = 4.2, ReviewCount = 185, ImageEmoji = "📈" },
            new Product { Id = 10, Name = "حارق الدهون L-كارنيتين", Category = "تنشيف", Description = "يساعد على تحويل الدهون إلى طاقة ودعم مرحلة التنشيف مع التمرين المنتظم.", Price = 130, OldPrice = 155, Rating = 4.1, ReviewCount = 260, ImageEmoji = "❄️" },
            new Product { Id = 11, Name = "كولاجين مفصل الرياضي", Category = "صحة عامة", Description = "ببتيدات كولاجين type II لدعم صحة الأربطة والمفاصل والغضاريف.", Price = 175, OldPrice = null, Rating = 4.4, ReviewCount = 95, ImageEmoji = "🦴" },
            new Product { Id = 12, Name = "ملتي فيتامين رياضي", Category = "صحة عامة", Description = "مزيج متكامل من الفيتامينات والمعادن الأساسية لسد الفجوات الغذائية.", Price = 85, OldPrice = 100, Rating = 4.3, ReviewCount = 410, ImageEmoji = "🍊" },
        };

        public static List<string> Categories =>
            Products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();

        public static List<Product> Search(string? category, string? query)
        {
            var items = Products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(category))
                items = items.Where(p => p.Category == category);

            if (!string.IsNullOrWhiteSpace(query))
                items = items.Where(p =>
                    p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(query, StringComparison.OrdinalIgnoreCase));

            return items.ToList();
        }

        public static Product? GetById(int id) => Products.FirstOrDefault(p => p.Id == id);
    }
}
