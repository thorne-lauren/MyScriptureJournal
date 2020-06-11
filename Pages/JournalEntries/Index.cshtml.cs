using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.JournalEntries
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Models.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Models.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public string BookNameSort { get; set; }
        public string DateSort { get; set; }


        public IList<JournalEntry> JournalEntry { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string BookSearch { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> bookQuery = from j in _context.JournalEntry
                                            orderby j.BookName
                                            select j.BookName;
            var journalentries = from j in _context.JournalEntry
                         select j;
            if (!string.IsNullOrEmpty(SearchString))
            {
                journalentries = journalentries.Where(s => s.Notes.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(BookSearch))
            {
                journalentries = journalentries.Where(s => s.BookName.Contains(BookSearch));
            }
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());

            JournalEntry = await journalentries.ToListAsync();
        }
        

        public async Task OnGetSortAsync(string sortOrder)
        {
            BookNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<JournalEntry> entry = from e in _context.JournalEntry
                                             select e;

            switch (sortOrder)
            {
                case "name_desc":
                    entry = entry.OrderByDescending(e => e.BookName);
                    break;
                case "Date":
                    entry = entry.OrderBy(e => e.DateAdded);
                    break;
                case "date_desc":
                    entry = entry.OrderByDescending(e => e.DateAdded);
                    break;
                default:
                    entry = entry.OrderBy(e => e.BookName);
                    break;
            }

            JournalEntry = await entry.AsNoTracking().ToListAsync();
        }
    }
}
