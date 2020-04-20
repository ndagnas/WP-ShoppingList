﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : AboutPopup.xaml.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de la Popup AboutPopup
// Créé le       : 14/02/2014
// Modifié le    : 09/06/2014
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Microsoft.Phone.Tasks;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using ShoppingList.Controls;
using ShoppingList.Resources;
using ShoppingList.Popups.Primitives;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "ShoppingList.Popups"
//*******************************************************************************************************************************
namespace ShoppingList.Popups
	{

	//   ###   ####    ###   #   #  #####       ####    ###   ####   #   #  ####
	//  #   #  #   #  #   #  #   #    #         #   #  #   #  #   #  #   #  #   #
	//  #####  ####   #   #  #   #    #    ###  ####   #   #  ####   #   #  ####
	//  #   #  #   #  #   #  #   #    #         #      #   #  #      #   #  #
	//  #   #  ####    ###    ###     #         #       ###   #       ###   #

	//***************************************************************************************************************************
	// Classe AboutPopup
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit une fenêtre des paramètres.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public partial class AboutPopup : PopupBase
		{
		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>AboutPopup</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public AboutPopup ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.InitializeComponent ();
			//-------------------------------------------------------------------------------------------------------------------
			
			//-------------------------------------------------------------------------------------------------------------------
			// Version
			//-------------------------------------------------------------------------------------------------------------------
			this.VersionControl.Text = string.Format ( AppResources.AboutPopup_Button_Version, VersionUtils.Current );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
	
		//***********************************************************************************************************************
		#region // Section des Procédures liées aux Contrôles
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur <b>AppChallenge</b>.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnAppChallengeClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			string Guid = "003f0f56-9e5a-46ba-808e-f3220224b1f0";
			Uri    Uri  = new Uri ( "http://appchallenge.azurewebsites.net/redirect?app=" + Guid );

			try { ( new WebBrowserTask () { Uri = Uri } ).Show (); } catch {}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur <b>Évaluer et donner un avis</b>.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnReviewClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			try
				{
				//---------------------------------------------------------------------------------------------------------------
				IsolatedStorageSettings.ApplicationSettings["review-count"] = -1;

				IsolatedStorageSettings.ApplicationSettings.Save ();

				(new MarketplaceReviewTask ()).Show ();
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			catch {}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		// Description : Se produit lors d'un clic sur le bouton "version"
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur le bouton <b>version</b>.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnVersionClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			MarketplaceDetailTask Task = new MarketplaceDetailTask ();

			Task.ContentIdentifier = "1bb9f95a-1409-413c-9b97-ce9a9ba71318";
			Task.ContentType       = MarketplaceContentType.Applications;

			try { Task.Show (); } catch {}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur la croix.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnCloseButtonClick ( object Sender, EventArgs Args ) { base.Close (); }
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "ShoppingList.Popups"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
