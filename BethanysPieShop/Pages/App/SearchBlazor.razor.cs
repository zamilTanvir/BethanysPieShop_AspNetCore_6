using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShop.Pages.App
{
    public partial class SearchBlazor
    {
        public string SearchText = "";
        public List<Pie> FilteredPies { get; set; } = new List<Pie>();

        [Inject]
        public IPieRepository? PieRepository { get; set; }

        private void Search()
        {
            FilteredPies.Clear();
            if(PieRepository != null)
            {
                if(SearchText.Length > 2)
                {
                    FilteredPies = PieRepository.SearchPies(SearchText).ToList();
                }
            }
        }
    }
}