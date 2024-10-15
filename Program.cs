using System.Diagnostics;

namespace ELC1013_T1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if true
            string[] srcs =
            {
                """
                ifthen{Se p{o time joga bem}, q{ganha o campeonato}}.
                ifthen{Se o time not{n�o p{joga bem}}, r{o t�cnico � culpado}}.
                ifthen{Se q{o time ganha o campeonato}, s{os torcedores ficam contentes}}.
                Os torcedores not{n�o s{est�o contentes}}.
                Logo, r{o t�cnico � culpado}.
                """,
                """
                thenif{p{O participante vai ao pared�o} se or{q{o lider o indica} ou r{os colegas o escolhem}}}.
                ifthen{Se and{p{o participante vai ao pared�o} e s{chora}}, ent�o t{ele conquista o p�blico}}.
                ifthen{Se t{o participante conquista o p�blico}, ele not{n�o u{� eliminado}}}.
                and{q{O lider indicou um participante} e u{ele foi eliminado}}.
                Logo, o participante not{n�o s{chorou}}.
                """,
                """
                ifthen{Se or{p{o programa � bom} ou q{passa no hor�rio nobre}}, r{o p�blico o assiste}}.
                ifthen{Se and{r{o p�blico assiste} e s{gosta}}, ent�o t{a audi�ncia � alta}}.
                ifthen{Se t{a audi�ncia � alta}, u{a propaganda � cara}}.
                O programa, and{q{passa no hor�rio nobre}, mas not{u{a propaganda � barata}}}.
                Logo, o p�blico not{n�o s{gosta do programa}}.
                """
            };
            string src = srcs[2];
            Parser p = new(src);
            p.Parse();
            Debug.WriteLine($"Is {(DirectTable.IsValid(p.Atomics, p.Premises) ? "" : "not ")}valid.");
#endif
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}