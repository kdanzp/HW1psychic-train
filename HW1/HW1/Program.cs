// See https://aka.ms/new-console-template for more information
using System.Text;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var openLoopsRepository = new OpenLoopsRepository();

{
    Console.WriteLine("Что вас беспокоет сейчас?");

    string? note = null;

    do
    {
        note = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(note));

    openLoopsRepository.Add(new OpenLoop
    {
        Note = note,
        CreatedDate = DateTimeOffset.UtcNow
    });
}

{
    var openLoops = openLoopsRepository.Get();
    var group = openLoops.GroupBy(x => new DateTime(x.CreatedDate.Year, x.CreatedDate.Month, x.CreatedDate.Day));

    foreach (var groupOfOpenLoops in group)
    {
        Console.WriteLine($"Ваши заботы за: {groupOfOpenLoops.Key:dd.MM.yyyy}");

        foreach (var openLoop in groupOfOpenLoops.ToArray())
        {
            Console.WriteLine(openLoop.Note);
        }
    }
}

