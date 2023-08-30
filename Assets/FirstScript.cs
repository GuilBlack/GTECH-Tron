using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1er exercice
/*        Debug.Log(IsPair(2));
        Debug.Log(IsPair(3));

        // 2eme exercice
        Debug.Log(Lerp(0.5f, 100, 200));
        Debug.Log(Lerp(0.71f, 90, 180));

        // 3eme exercice
        Debug.Log(getDistance2D(2, 5, 3, 8));
        Debug.Log(getDistance2D(-2, 5, 3, -8));

        // 4eme exercice
        for (int i = 2; i < 15; i++)
        {
            Debug.Log(i + " : " + IsNombrePremier(i));
        }

        // 5eme exercice
        Debug.Log(NombreArabeToRomain(3400));
        Debug.Log(NombreArabeToRomain(1999));
        Debug.Log(NombreArabeToRomain(587));
        Debug.Log(NombreArabeToRomain(34));
        Debug.Log(NombreArabeToRomain(4));*/

        // 6eme exercice
        Debug.Log(JustifierTexte("En effet, si un texte justifié sur papier est tout de suite assimilé par le lecteur, sa forme change une fois qu’il est transposé sur Internet. Selon la longueur des mots employés, un texte justifié présente plus ou moins des espaces entre les mots.",
            30));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string IsPair(int nbr)
    {   
        if(nbr % 2 == 0)
            return "Pair";
        else
            return "Impair";
    }

    private float Lerp(float ratio, float min, float max) {
        return min + (max - min) * ratio;
    }

    private float getDistance2D(int xa, int  ya, int xb, int yb)
    {
        return (float)Math.Sqrt((xb - xa) * (xb - xa) + (yb - ya) * (yb - ya));
    }

    private string IsNombrePremier(int nbr)
    {
        if (nbr <= 1)
            return "Non premier";
        else
        {
            for (int i = 2; i < nbr; i++)
            {
                if (nbr % i == 0)
                    return "Non premier";
            }
            return "Premier";
        }
    }

    private string NombreArabeToRomain(int nbr)
    {
        int[] unites = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] dizaines = { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        int[] centaines = { 100, 200, 300, 400, 500, 600, 700, 800, 900 };
        int[] milliers = { 1000, 2000, 3000, 4000 };

        string[] r_unites = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
        string[] r_dizaines = { "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] r_centaines = { "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] r_milliers = { "M", "MM", "MMM", "MMMM" };

        string resultat = "";

        if(nbr >= 1000)
        {
            int index = nbr / 1000;
            resultat += r_milliers[index-1];
            nbr = nbr % 1000;
        }  
        if (nbr >= 100)
        {
            int index = nbr / 100;
            resultat += r_centaines[index - 1];
            nbr = nbr % 100;
        }
        if (nbr >= 10)
        {
            int index = nbr / 10;
            resultat += r_dizaines[index - 1];
            nbr = nbr % 10;
        }
        if (nbr >= 1)
        {
            resultat += r_unites[nbr - 1];
        }

        return resultat;
    }

    private string JustifierTexte(string text, int largeur)
    {
        string[] newText = text.Split(' ');
        string result = "";

        int nbrMots = 0;
        int tailleMots = 0;
        int index = 0;

        for (int i = 0; i < newText.Length; i++)
        {
            if (tailleMots + newText[i].Length +1 < largeur || nbrMots == 0)
            {
                nbrMots++;
                tailleMots += newText[i].Length +1;
            } else
            {
                int espacesAjouter = largeur - tailleMots;
                int nbrEspace = nbrMots - 1;
                int espaces = espacesAjouter / nbrEspace;
                int restants = espacesAjouter % nbrEspace;

                for (int j = index; j < index + nbrMots; j++)
                {
                    if(j == 1)
                    result += newText[j] + " ";
                    for(int k = 0; k < espaces; k++)
                    {
                        result += " ";
                    }
                }
                result += "\n";
                index += nbrMots;
                nbrMots = 0;
                tailleMots = 0;
            }
        }

        return result;
    }
}
