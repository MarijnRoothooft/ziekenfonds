namespace Project_ZF.ViewModels
{
    public class RolEditViewModel
    {
        public string Naam { get; set; }
        public string Email { get; set; }

        public bool IsActief { get; set; }
        public List<string> BeschikbareRollen { get; set; }
        public string GeselecteerdeRol { get; set; }
    }
}
