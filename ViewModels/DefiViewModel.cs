using System.Collections.Generic;
using SAINTJWebApp.ViewModels;


namespace SAINTJWebApp.ViewModels
{
    public class ListeDefisViewModel
    {
        public Dictionary<string, List<DefiViewModel>> DefisParDifficulte { get; set; }
        public int DefiAjoutRestant { get; set; }
    }

    public class DefiViewModel
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int Points { get; set; }
        public string Difficulte { get; set; }
        public bool EstAccompli { get; set; }
    }
}