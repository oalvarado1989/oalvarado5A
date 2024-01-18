namespace oalvarado5A
{
    public partial class App : Application
    {
        public static PersonReporitory personRepo {  get; set; }
        public App(PersonReporitory personReporitory)
        {
            InitializeComponent();

            MainPage = new Vistas.vPrincipal();
            personRepo = personReporitory;
        }
    }
}
