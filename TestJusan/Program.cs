using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace TestJusan
{
    class MainClass
    {
        
        async static Task Main(string[] args)
        {
            //запись в файл
            Task ToWrite(List<PhysicalPerson> people)
            {
                JsonSerializer serializer = new JsonSerializer();
                string path = "../../users.json";
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    if (Path.GetExtension(path) != ".json") { throw new ArgumentException("Wrong file type"); }
                    serializer.Serialize(writer, people);
                    Console.WriteLine("Data has been saved to file");
                }

                return Task.CompletedTask;
            }

            //чтение из файла
            Task<List<PhysicalPerson>> ToRead()
            {
                JsonSerializer serializer = new JsonSerializer();
                string path = "../../users.json";
                using (StreamReader sw = new StreamReader(path))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    if (Path.GetExtension(path) != ".json") { throw new ArgumentException("Wrong file type"); }
                    List<PhysicalPerson> people = serializer.Deserialize<List<PhysicalPerson>>(reader);
                    Console.WriteLine("File readed");
                    return Task.FromResult(people);
                }

            }

            List<PhysicalPerson> physic_people = new List<PhysicalPerson>
            {
                new PhysicalPerson(1, "Алексей", "Кузнецов", "Александрович", 20, 0112233),
                new PhysicalPerson(2, "Денис", "Иванов", "Дмитриевич", 23, 0113233),
                new PhysicalPerson(3, "Леонид", "Соколов", "Игоревич", 40, 0112233),
                new PhysicalPerson(7, "Леонид", "Иванов", "Игоревич", 40, 0112233),
                new PhysicalPerson(4, "Максим", "Семёнов", "Сергеевич", 30, 0113233),
                new PhysicalPerson(5, "Матвей", "Егоров", "Ильич", 27, 0112233),
                new PhysicalPerson(6, "Владимир", "Степанов", "Someth", 35, 0113233)
            };

            // 1. запись и чтение коллекции данных классов в/из файл(а)
            Console.WriteLine("1. запись и чтение коллекции данных классов в/из файл(а)");
            //string path = "../../../users.json";
            await ToWrite(physic_people);

            List<PhysicalPerson> readed_people = await ToRead();

            Console.WriteLine("\nДанные загруженные в файл: ");
            foreach (var p in physic_people)
                Console.WriteLine($"{p.Last_name} - {p.Name} - {p.Otchestvo}");

            Console.WriteLine("\nДанные выгруженные из файла: ");
            foreach (var p in readed_people)
                Console.WriteLine($"{p.Last_name} - {p.Name} - {p.Otchestvo}");

            // 2. Обработка некорректного формата входного файла.
            /*
            Console.WriteLine("Обработка некорректного формата входного файла.");
            path = "../../../users.txt";
            await ToWriteAsync(path, physic_people);
            await ToReadAsync(path);
            */

            // 3. Сделать вывод списка физ лиц. Упорядочить список физ. лиц по Фамилии, Имени, Отчеству
            Console.WriteLine("\nСделать вывод списка физ лиц. Упорядочить список физ. лиц по Фамилии, Имени, Отчеству");
            var sortedPeople = from p in physic_people
                               orderby p.Last_name, p.Name, p.Otchestvo
                               select p;

            foreach (var p in sortedPeople)
                Console.WriteLine($"{p.Last_name} - {p.Name} - {p.Otchestvo}");


            //5 Сделать вывод 5 записей из списка юр. лиц. Упорядочить список юр. лиц  по количеству контактных лиц (по убыванию).
            Console.WriteLine("\nСделать вывод 5 записей из списка юр. лиц. Упорядочить список юр. лиц  по количеству контактных лиц (по убыванию).");
            List<JuridicalPerson> jurid_people = new List<JuridicalPerson>
            {
                new JuridicalPerson(1, "Logitech", 0112233, physic_people),
                new JuridicalPerson(2, "Apple", 0113233, physic_people.GetRange(0, 3)),
                new JuridicalPerson(3, "Samsung", 0112233, physic_people.GetRange(0, 4)),
                new JuridicalPerson(4, "Faberlic", 0113233, physic_people.GetRange(1, 2)),
                new JuridicalPerson(5, "Poket book", 0112233, physic_people.GetRange(0, 5)),
                new JuridicalPerson(6, "Bruno Visconty", 0113233, physic_people.GetRange(0, 1))
            };

            var sortedJPeople = jurid_people.OrderByDescending(p => p.CountPeople()).Take(5);

            foreach (var p in sortedJPeople)
                Console.WriteLine($"{p.Name} - {p.CountPeople()}");


            

        }
    }
}
