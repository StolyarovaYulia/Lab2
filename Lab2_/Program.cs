using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Radiostation.DAL;
using Radiostation.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string conStr = "Server=(localdb)\\MSSQLLocalDB;Database=radiostation_lab2;Trusted_Connection=True;MultipleActiveResultSets=true";

            var contextOptions = new DbContextOptionsBuilder<RadiostationContext>();
            contextOptions.UseSqlServer(conStr);

            var context = new RadiostationContext(contextOptions.Options);

            Console.WriteLine("1. Все данные из таблицы Genre");
            object data = await context.Genres
                .AsNoTracking()
                .ToListAsync();
            SerializeAndPrint(data);

            Console.WriteLine("2.Все Performer с IsGroup = истина");
            data = await context.Performers
                .AsNoTracking()
                .Where(p => p.IsGroup)
                .ToListAsync();
            SerializeAndPrint(data);

            Console.WriteLine("3. Количество Tracks сгрупированных по Genres");
            data = await context.Tracks
                .Include(t => t.Genre)
                .GroupBy(t => t.Genre.Name)
                .Select(t => new
                {
                    Genre = t.Key,
                    TrackCount = t.Count()
                })
                .ToListAsync();
            SerializeAndPrint(data);

            Console.WriteLine("4. вывод Tracks связанных с Genres и Performer");
            data = await context.Tracks
                .Include(t => t.Genre)
                .Include(t => t.Performer)
                .AsNoTracking()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    Genre = t.Genre.Name,
                    Performer = t.Performer.Name
                })
                .ToListAsync();

            Console.WriteLine("5. вывод Tracks связанных с Genres и Performer, где Genre = Рок");
            data = await context.Tracks
                .Include(t => t.Genre)
                .Include(t => t.Performer)
                .Where(t => t.Genre.Name == "Рок")
                .AsNoTracking()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    Genre = t.Genre.Name,
                    Performer = t.Performer.Name
                })
                .ToListAsync();

            Console.WriteLine("6. Вставка в таблицу Genre");
            var genreToInsert = new Genre
            {
                Name = "TestGenre",
                Description = "TestDescription",
            };
            context.Genres.Add(genreToInsert);
            await context.SaveChangesAsync();

            Console.WriteLine("7. Вставка Tracks");
            var trackToInsert = new Track
            {
                Name = "TestName",
                Duration = "3:00",
                CreationDate = DateTime.UtcNow,
                GenreId = genreToInsert.Id,
                Performer = new Performer
                {
                    Description = "TestDescription",
                    GroupList = "TestGroupList",
                    IsGroup = true,
                    Name = "TestName"
                },
                Rating = 5
            };
            context.Tracks.Add(trackToInsert);
            await context.SaveChangesAsync();

            Console.WriteLine("9. Удаление ранее созданного Track");
            context.Tracks.Remove(trackToInsert);
            await context.SaveChangesAsync();

            Console.WriteLine("8. Удаление ранее созданного Genre");
            context.Genres.Remove(genreToInsert);
            await context.SaveChangesAsync();

            Console.WriteLine("10. обновление всех Track с Rating = 3");
            var tracksToUpdate = await context.Tracks
                .Where(t => t.Rating == 3)
                .ToListAsync();
            tracksToUpdate
                .ForEach(t => t.Rating = 4);
            context.Tracks.UpdateRange(tracksToUpdate);
            await context.SaveChangesAsync();
        }

        static void SerializeAndPrint(object obj)
        {
            var serializedData = JsonConvert.SerializeObject(obj);
            Console.WriteLine(serializedData);
        }
    }
}
