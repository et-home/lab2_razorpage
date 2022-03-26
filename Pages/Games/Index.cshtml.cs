#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using razor_ef.Model;

namespace razor_ef.Pages.Games;

public class IndexModel : PageModel
{
    private readonly GameStoreContext _context;
    private readonly ILogger<IndexModel> _logger;

    // [BindProperty(SupportsGet = true, Name = "Query")]
    public string QueryTitle { get; set; }

    // [BindProperty(SupportsGet = true, Name = "PublishDate")]
    public string PublishDate { get; set; }

    [BindProperty(SupportsGet = true, Name = "isAfter")]
    public bool isAfter { get; set; }

    [BindProperty(SupportsGet = true, Name = "useDateFilter")]
    public bool useDateFilter { get; set; }

    public IndexModel(GameStoreContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IList<Game> Game { get; set; }

    public async Task OnGetAsync(string publishDate)
    {
        PublishDate = publishDate;

        string queryTitle = "";
        if(Request.Query.ContainsKey("QueryTitle")){
            queryTitle = Request.Query["QueryTitle"];
        }

        // _logger.Log(LogLevel.Information, queryTitle);

        var games = from g in _context.Game select g;

        if (!string.IsNullOrEmpty(queryTitle) && !string.IsNullOrEmpty(PublishDate) && useDateFilter)
        {
            games = games.Where(g => (isAfter ? g.DatePublished >= DateTime.Parse(PublishDate):
                                g.DatePublished <= DateTime.Parse(PublishDate)) &&
                                g.Title.ToLower().Contains(queryTitle.ToLower()));
        }

        else if (!useDateFilter && !string.IsNullOrEmpty(queryTitle))
        {
            games = games.Where(g => g.Title.ToLower().Contains(queryTitle.ToLower()));
        }

        Game = await games.ToListAsync();
    } 
}
