namespace GtaosPing
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }

        private static void Add_Click(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString()+"was clicked");
        }
    }
}