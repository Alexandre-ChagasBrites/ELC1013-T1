using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace ELC1013_T1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string[] srcs =
            {
                """
                ifthen{Se p{o time joga bem}, q{ganha o campeonato}}.
                ifthen{Se o time not{não p{joga bem}}, r{o técnico é culpado}}.
                ifthen{Se q{o time ganha o campeonato}, s{os torcedores ficam contentes}}.
                Os torcedores not{não s{estão contentes}}.
                Logo, r{o técnico é culpado}.
                """,
                """
                thenif{p{O participante vai ao paredão} se or{q{o lider o indica} ou r{os colegas o escolhem}}}.
                ifthen{Se and{p{o participante vai ao paredão} e s{chora}}, então t{ele conquista o público}}.
                ifthen{Se t{o participante conquista o público}, ele not{não u{é eliminado}}}.
                and{q{O lider indicou um participante} e u{ele foi eliminado}}.
                Logo, o participante not{não s{chorou}}.
                """,
                """
                ifthen{Se or{p{o programa é bom} ou q{passa no horário nobre}}, r{o público o assiste}}.
                ifthen{Se and{r{o público assiste} e s{gosta}}, então t{a audiência é alta}}.
                ifthen{Se t{a audiência é alta}, u{a propaganda é cara}}.
                O programa, and{q{passa no horário nobre}, mas not{u{a propaganda é barata}}}.
                Logo, o público not{não s{gosta do programa}}.
                """
            };
            string src = srcs[2];
            Parser p = new(src);
            p.Parse();
            Debug.WriteLine($"Is {(DirectTable.IsValid(p.Atomics, p.Premises) ? "" : "not ")}valid.");
        }
    }
}
