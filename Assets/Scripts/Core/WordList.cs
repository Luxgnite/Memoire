using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WordTheme
{
    Acquis,
    Action,
    Anarchie,
    Arbitraire,
    Autonomie,
    Autorite,
    Autre,
    Autrui,
    Axiologie,
    Bien,
    Causalite,
    Chaos,
    Chose,
    Citoyen,
    Collectivite,
    Communaute,
    Communication,
    Communisme,
    Connaissance,
    Conscience,
    Constitution,
    Contingence,
    Contrat,
    Corps,
    Cosmos,
    Courage,
    Croyance,
    Culture,
    Desir,
    Desordre,
    Determinisme,
    Devoir,
    Dialectique,
    Different,
    Dignite,
    Distinct,
    Donne,
    Droit,
    echange,
    economie,
    ego,
    Entropie,
    Espace,
    Esprit,
    Essence,
    Esthetique,
    Estime,
    etat,
    eternite,
    ethique,
    Etre,
    Existence,
    Experience,
    Faculte,
    Fait,
    Fatalisme,
    Fin,
    Fonction,
    Force,
    Gouvernement,
    Hasard,
    Heredite,
    Histoire,
    Humain,
    Hypothese,
    Idee,
    Identique,
    Identite,
    Ignorance,
    Imperatif,
    Individu,
    Individualisme,
    Inne,
    Instinct,
    Institution,
    Intuition,
    Je,
    Jugement,
    Justice,
    Langage,
    Liberte,
    Loi,
    Mal,
    Manifestation,
    Matiere,
    Merite,
    Metaphysique,
    Methode,
    Moeur,
    Moi,
    Monde,
    Morale,
    Moralite,
    Moyen,
    Nation,
    Nature,
    Neant,
    Necessite,
    Norme,
    Objectivite,
    Obligation,
    On,
    Opinion,
    Organisation,
    Pensee,
    Personnalisme,
    Personnalite,
    Personne,
    Phenomene,
    Politique,
    Pouvoir,
    Pratique,
    Principe,
    Production,
    Puissance,
    Raison,
    Realite,
    Relation,
    Responsabilite,
    Sanction,
    Savoir,
    Science,
    Singularite,
    Socialisme,
    Societe,
    Speculation,
    Spontane,
    Structure,
    Substance,
    Sujet,
    Synthese,
    Systeme,
    Temperament,
    Temps,
    Theorie,
    Tolerance,
    Totalitarisme,
    Unite,
    Univers,
    Universalite,
    Valeurs,
    Verite,
    Violence,
    Vivant
}

[System.Serializable]
public class WordData
{
    public string word;
    public List<WordTheme> themes;
}

[CreateAssetMenu(fileName = "New Word List", menuName = "Word list")]
public class WordList : ScriptableObject
{
    public List<WordData> wordData;
    public int neededCommonTheme = 1;

    public bool IsSameTheme(string word1, string word2)
    {
        WordData word1Data = wordData.Find(item => item.word.ToLower() == word1.ToLower());
        WordData word2Data = wordData.Find(item => item.word.ToLower() == word2.ToLower());

        if (word1Data == null || word2Data == null)
        {
            return false;
        }

        int nbCommonTheme = 0;

        foreach(WordTheme theme in word1Data.themes)
        {
            if (word2Data.themes.Exists(item => item == theme))
            {
                nbCommonTheme++;
            }
        }

        if (nbCommonTheme >= neededCommonTheme)
            return true;
        else
            return false;
    }

}
