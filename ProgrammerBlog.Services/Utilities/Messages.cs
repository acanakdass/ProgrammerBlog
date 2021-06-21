using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Services.Utilities
{
    public static class Messages
    {
        public static class CategoryMessages
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural)
                    return "Hiçbir kategori bulunamadı.";
                else
                    return "Böyle bir kategori bulunamadı.";
            }
            public static string Added(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklendi.";
            }
            public static string Updated(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellendi.";
            }
            public static string Deleted(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silindi.";
            }
            public static string HardDeleted(string categoryName)
            {
                return $"{categoryName} adlı kategori kalıcı olarak silindi.";
            }
        }


        public static class ArticleMessages
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural)
                    return "Hiçbir makale bulunamadı.";
                else
                    return "Böyle bir makale bulunamadı.";
            }
            public static string Added(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla eklendi.";
            }
            public static string Updated(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla güncellendi.";
            }
            public static string Deleted(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale başarıyla silindi.";
            }
            public static string HardDeleted(string articleTitle)
            {
                return $"{articleTitle} başlıklı makale kalıcı olarak silindi.";
            }
        }
    }
}

