using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Agilico.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        [Display(Name = "Bonus Ball")]
        public bool IsBonus { get; set; }

        public async Task<IActionResult> OnPost()
        {
            int[] lottery = multipleRandomFromRange(IsBonus? 7: 6, 1, 49);
            Array.Sort(lottery);
            for (int i = 0; i < lottery.Length; i++)
            {
                ViewData["number"+i] = lottery[i];
                if (lottery[i] < 10) {
                    ViewData["bg" + i] = "grey";
                } else if (lottery[i] < 20) {
                    ViewData["bg" + i] = "blue";
                } else if (lottery[i] < 30) {
                    ViewData["bg" + i] = "pink";
                } else if (lottery[i] < 40) {
                    ViewData["bg" + i] = "green";
                } else {
                    ViewData["bg" + i] = "yellow";
                }
            }
            if(!IsBonus) { 
                ViewData["bg6"] = "notVisible";
            }
            return Page();
        }

        int[] multipleRandomFromRange (int amount, int start, int count) {
            int[] randoms = new int[amount];
            Random rnd = new Random();
            var nums = Enumerable.Range(start, count).ToList();
            for (int i = 0;i < amount; i++)
            {
                int random = rnd.Next(0, count - i);
                randoms[i] = nums[random];
                nums.RemoveAt(random);
            }
            return randoms;
        }
    }
}
