#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_ef.Model;

namespace razor_ef.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameStoreContext _context;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty (SupportsGet =true, Name = "Query")]
        public string Query { get; set; }

        [BindProperty (SupportsGet =true, Name = "DateP")]
        public string DateP{get; set; }

        [BindProperty (SupportsGet =true, Name = "isBefore")]
        public bool isBefore { get; set;}

        [BindProperty (SupportsGet =true, Name = "isFilterByDate")]
        public bool isFilterByDate { get; set;}

        public IndexModel(GameStoreContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Game> Game { get;set; }

        public async Task OnGetAsync()
        {
            var games = from g in _context.Game select g;

            if (!string.IsNullOrEmpty(Query) && !string.IsNullOrEmpty(DateP))
            {
                if (isBefore && !isFilterByDate)
                {
                    games = games.Where(g => g.Title.ToLower().Contains(Query.ToLower()) &&
                                            g.DatePublished <= DateTime.Parse(DateP));
                    _logger.Log(LogLevel.Information, DateP);
                    _logger.Log(LogLevel.Information, Query);
                }
                
                else if (!isBefore && !isFilterByDate) {
                    games = games.Where(g => g.Title.ToLower().Contains(Query.ToLower()) &&
                                            g.DatePublished >= DateTime.Parse(DateP));
                }

                else if(isFilterByDate){
                    games = games.Where(g => g.Title.ToLower().Contains(Query.ToLower()));
                }
            }

            Game = await games.ToListAsync();
        }
    }
}
