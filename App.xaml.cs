using MotMariusLab7.Data;

namespace MotMariusLab7
{
    public partial class App : Application
    {
        static ShopListDatabase database;
        public static ShopListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    //database = new ShoppingListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db3"));
                    database = new ShopListDatabase(Path.Combine(FileSystem.AppDataDirectory, "ShoppingList.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
