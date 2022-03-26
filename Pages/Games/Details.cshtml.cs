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
    public class DetailsModel : PageModel
    {
        private readonly GameStoreContext _context;
        private readonly ILogger _logger;

        public DetailsModel(ILogger<DetailsModel> logger, GameStoreContext context)
        {
            _context = context;
            _logger = logger;
        }

        public Game Game { get; set; }

        [BindProperty(Name="curr_time", SupportsGet = true)]
        public DateTime TheDate { get; set; } = DateTime.MinValue;

        [BindProperty(SupportsGet = true)]
        public string QName { get; set; } = "Fred";

        [BindProperty(SupportsGet = true)]
        public string GameGenre{ get; set; }

        public string QueryValues { get; set; } = "";
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // foreach (var qp in Request.Query){
            //     QueryValues += $"<div>{qp.Key}:{qp.Value}</div>";
            // }
            if (Request.Query.ContainsKey("id")) {
                QueryValues = Request.Query["id"];
            }


            // QName = q_name;
            // GameGenre = gameGenre;

            Game = await _context.Game.FirstOrDefaultAsync(m => m.GameId == id);

            if (Game == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
