using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DocumentStorage.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            List<CategoryClass> categories = new List<CategoryClass>();

            categories.Add(new CategoryClass() { Id = (int)Category.All, Name = "Всі" });
            categories.Add(new CategoryClass() { Id = (int)Category.Fiz, Name = "Фіз. особи" });
            categories.Add(new CategoryClass() { Id = (int)Category.Jur, Name = "Юр. особи" });

            context.Category.AddRange(categories);
            context.SaveChanges();

            List<FileTypeClass> fileTypes = new List<FileTypeClass>();

            fileTypes.Add(new FileTypeClass() { Id = (int)FileType.Image, Name = "Зображення" });
            fileTypes.Add(new FileTypeClass() { Id = (int)FileType.Video, Name = "Відео" });
            fileTypes.Add(new FileTypeClass() { Id = (int)FileType.Audio, Name = "Аудіо" });
            fileTypes.Add(new FileTypeClass() { Id = (int)FileType.Text, Name = "Текст" });

            context.FileType.AddRange(fileTypes);
            context.SaveChanges();
        }
    }
}