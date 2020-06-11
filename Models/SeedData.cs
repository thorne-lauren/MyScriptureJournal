using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace MyScriptureJournal.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>()))
            {
                // Look for any journal entries.
                if (context.JournalEntry.Any())
                {
                    return;   // DB has been seeded
                }

                context.JournalEntry.AddRange(
                    new JournalEntry
                    {
                        BookName = "Mosiah",
                        DateAdded = DateTime.Parse("2020-2-12"),
                        Chapter = 2,
                        Verse = 17,
                        Notes = "Search Me"
                    },

                    new JournalEntry
                    {
                        BookName = "D&C",
                        DateAdded = DateTime.Parse("2020-2-13"),
                        Chapter = 19,
                        Verse = 23,
                        Notes = ""
                    },

                    new JournalEntry
                    {
                        BookName = "1 Peter",
                        DateAdded = DateTime.Parse("2020-2-23"),
                        Chapter = 4,
                        Verse = 6,
                        Notes = ""
                    },

                    new JournalEntry
                    {
                        BookName = "Alma",
                        DateAdded = DateTime.Parse("2020-4-15"),
                        Chapter = 41,
                        Verse = 10,
                        Notes = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
