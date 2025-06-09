namespace MauiApp2
{
    public partial class App : Application
    {
        public static Modelos.PersonRepositorty personRepo{ get; set; }
        public static Modelos.DatabaseService dbService { get; set; }
        public App(Modelos.PersonRepositorty personRepositorty, Modelos.DatabaseService DBService)
        {
            InitializeComponent();
            personRepo = personRepositorty;
            dbService = DBService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new NavigationPage(new Views.vPrincipal_bd()));

            return new Window(new NavigationPage(new Views.LoginPage()));
        }
    }
}