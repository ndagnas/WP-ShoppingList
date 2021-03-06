﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : Instance.xaml.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du point d'entrée de l'application
// Créé le       : 31/05/2014
// Modifié le    : 04/06/2014
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using ShoppingList.Resources;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "ShoppingList"
//*******************************************************************************************************************************
namespace ShoppingList
	{

	//  #       ###    ###    ###   #      #  #####   ###   #####  #   ###   #   #
	//  #      #   #  #   #  #   #  #      #     #   #   #    #    #  #   #  ##  #
	//  #      #   #  #      #####  #      #    #    #####    #    #  #   #  # # #
	//  #      #   #  #   #  #   #  #      #   #     #   #    #    #  #   #  #  ##
	//  #####   ###    ###   #   #  #####  #  #####  #   #    #    #   ###   #   #

	//***************************************************************************************************************************
	// Classe LocalizedStrings
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Permet d'accéder aux ressources de chaîne.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class LocalizedStrings
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private static AppResources _localizedResources = new AppResources ();
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Accéder aux ressources de chaîne
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public AppResources LocalizedResources { get { return _localizedResources; } }
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "ShoppingList"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
