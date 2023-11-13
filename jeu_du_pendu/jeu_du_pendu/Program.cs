using System;
using System.Collections.Generic;
using System.IO;
using AsciiArt;

namespace jeu_du_pendu
{
    internal class Program
    {
        // fonction -> Afficher le mot
        static void AfficherMot(string mot, List<char> lettres)
        {
            for(int i = 0; i < mot.Length; i++) 
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
            } 
        }
        // Cas ou on a gagné -> Toutes les lettres sont devinées
        static bool TouteLettreDevinées(string mot, List<char> lettres)
        {
            foreach(var lettre in lettres)
            {
                mot = mot.Replace(lettre.ToString(), "");
            }
            if(mot.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // fonction -> Demander la lettre 
        static char DemanderUneLettre(string message = "Rentrer une lettre :")
        {
            while(true) 
            {
                Console.Write(message);
                string reponse = Console.ReadLine();

                if (reponse.Length == 1)
                {
                    reponse = reponse.ToUpper();

                    return reponse[0];
                }
                Console.WriteLine("ERRREUR : vous devez rentrer une lettre");
            }
        }
        // fonction -> Deviner le mot
        static void DevinerMot(string mot)
        {
            List<char> lettresDevinees = new List<char>();
            List<char> lettresExclues = new List<char>();

            const int NB_VIES = 6;
            int viesRestantes = NB_VIES;
            while(viesRestantes > 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
                Console.WriteLine();

                AfficherMot(mot, lettresDevinees);
                Console.WriteLine();
                var lettre = DemanderUneLettre();
                Console.Clear();

                if(mot.Contains(lettre))
                {
                    Console.WriteLine("cette lettre est dans le mot");
                    lettresDevinees.Add(lettre);
                    // GAGNE
                    if(TouteLettreDevinées(mot, lettresDevinees))
                    {
                        //Console.WriteLine("GAGNE !");
                        //return;
                        break;
                    }
                }
                else
                {
                    if (!lettresExclues.Contains(lettre)) 
                    {
                        viesRestantes--;
                        lettresExclues.Add(lettre);
                    }
                    Console.WriteLine("vies restantes :" + viesRestantes);
                }
                if(lettresExclues.Count > 0)
                {
                    Console.WriteLine("Le mot ne contient pas les lettres : " + string.Join(", ", lettresExclues));
                }
                Console.WriteLine();
            }
            Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
            if (viesRestantes == 0)
            {
                Console.WriteLine("PERDU !, Le mot était " + mot);
            }
            else
            {
                AfficherMot(mot, lettresDevinees);
                Console.WriteLine();
                Console.WriteLine("GAGNE !");
            }
        }
        // fonction -> Pour charger les mots 
        static string[] ChargerMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erreur de lecture du fichier : " + nomFichier + "(" + ex.Message + ")");
            }
            return null;
        }
        static bool DemanderDeRejouer()
        {
            char reponse = DemanderUneLettre("Voulez vous rejouer (o/n)");
            if((reponse == 'o') || (reponse == 'O'))
            {
                return true;
            }
            else if((reponse == 'n') || (reponse == 'N'))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Erreur : Vous devez repondre avec o ou n");
                return DemanderDeRejouer();
            }
        }
        static void Main(string[] args)
        {
            var mots = ChargerMots("mots.txt");
            if(mots  == null || mots.Length == 0)
            {
                Console.WriteLine("La liste de mots est vide ");
            }
            else
            {

                bool jouer = true;
                while(jouer)
                {
                    Random random = new Random();
                    int i = random.Next(0, mots.Length);
                    string mot = mots[i].Trim().ToUpper();
                    DevinerMot(mot);
                    if (!DemanderDeRejouer())
                    {
                        break;
                    }
                    Console.Clear();
                }
                Console.WriteLine("Merci et à bientot.");
            }
            /*
             *  // Trim() -> Pour supprimer de l'espace 
                // ToUpper() -> Pour convertir tout le mot en majuscule
             * char lettre = DemanderUneLettre(); 
             List<char> lettres = new List<char> { lettre };
             AfficherMot(mot, lettres);*/
        }
    }
}






