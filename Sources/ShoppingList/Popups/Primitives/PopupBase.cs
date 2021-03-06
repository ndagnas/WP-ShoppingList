﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : PopupBase.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du contrôle PopupBase
// Créé le       : 13/06/2013
// Modifié le    : 09/06/2014
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Shell = Microsoft.Phone.Shell;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "ShoppingList.Popups.Primitives"
//*******************************************************************************************************************************
namespace ShoppingList.Popups.Primitives
	{

	//  ####    ###   ####   #   #  ####          ####    ###    ####  #####
	//  #   #  #   #  #   #  #   #  #   #         #   #  #   #  #      #    
	//  ####   #   #  ####   #   #  ####   #####  ####   #####   ###   ###  
	//  #      #   #  #      #   #  #             #   #  #   #      #  #    
	//  #       ###   #       ###   #             ####   #   #  ####   #####

	//***************************************************************************************************************************
	// Classe PopupBase
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Définit un élément popup de base.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class PopupBase : UserControl
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		public static readonly DependencyProperty ApplicationBarProperty;
		//-----------------------------------------------------------------------------------------------------------------------
		private       readonly PopupContainer     FPopupContainer;
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Constructeur statique de l'objet <b>PopupBase</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		static PopupBase ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			ApplicationBarProperty = DependencyProperty.Register ( "ApplicationBar", 
				typeof(ApplicationBar), typeof(PopupBase), new PropertyMetadata ( 
				                 new PropertyChangedCallback ( ApplicationBarPropertyChanged ) ) );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>PopupBase</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public PopupBase ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.ApplicationButtons = new Shell.ApplicationBarIconButton[0];

			if ( ! DesignerProperties.IsInDesignTool )
				this.FPopupContainer = new PopupContainer ( true, true );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>PopupBase</b>.
		/// </summary>
		/// <param name="UseTransition">Indique s'il faut utiliser les animations.</param>
		/// <param name="CanGoBack">Indique que le bouton précédent ferme la popup.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public PopupBase ( bool UseTransition, bool CanGoBack )
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.ApplicationButtons = new Shell.ApplicationBarIconButton[0];

			this.FPopupContainer = new PopupContainer ( UseTransition, CanGoBack );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Procédures Privées
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Est appelé quand la propriété ApplicationBar change.
		/// </summary>
		/// <param name="Sender">
		/// <b>DependencyObject</b> dans lequel la propriété a modifié une valeur.
		/// </param>
		/// <param name="Args">
		/// Données d'événement publiées par un événement qui suit les modifications apportées à 
		/// la valeur effective de cette propriété.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private static void ApplicationBarPropertyChanged ( DependencyObject Sender, DependencyPropertyChangedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			PopupBase Self = Sender as PopupBase;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( Self != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				var Buttons = new List<Shell.ApplicationBarIconButton> ();

				foreach ( ApplicationBarButton Button in Self.ApplicationBar.Buttons )
					{
					Buttons.Add ( Button.Button );
					}

				Self.ApplicationButtons = Buttons.ToArray ();
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Procédures Publiques et Protégées
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Annule la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Abort () { this.FPopupContainer.Abort (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Ouvre la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public virtual void Open ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.FPopupContainer.Content = this;
			
			this.FPopupContainer.Open ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Ferme la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Close () { this.FPopupContainer.Close (); }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Charge la popup en mémoire.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void MountInCache () {}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'une pression sur le bouton Précédent.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>CancelEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public virtual void OnBackKeyPress ( object Sender, CancelEventArgs Args ) {}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'une pression sur le bouton Précédent.
		/// </summary>
		/// <param name="Args"><b>EventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public virtual void OnCancel ( EventArgs Args ) {}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Déclenche un événement <b>Closed</b>.
		/// </summary>
		/// <param name="Args"><b>EventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public virtual void OnClosed ( EventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.Closed != null ) this.Closed ( this, Args );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
	
		//***********************************************************************************************************************
		/// <summary>
		/// Déclenche un événement <b>Closed</b>.
		/// </summary>
		/// <param name="Args"><b>CancelEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public virtual void OnClosing ( CancelEventArgs Args ) {}
		//***********************************************************************************************************************
	
		//***********************************************************************************************************************
		/// <summary>
		/// Déclenche un événement <b>Opened</b>.
		/// </summary>
		/// <param name="Args"><b>EventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public virtual void OnOpened ( EventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.Opened != null ) this.Opened ( this, Args );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
	
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Accède à la barre d'icône de la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public ApplicationBar ApplicationBar
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return (ApplicationBar) base.GetValue ( ApplicationBarProperty ); }
			//-------------------------------------------------------------------------------------------------------------------
			set { base.SetValue ( ApplicationBarProperty, value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens les boutons de la popup actuelle.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public Shell.ApplicationBarIconButton[] ApplicationButtons { get; private set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Indique si la popup est ouverte.
		/// </summary>
		/// <returns><b>True</b> indique que la popup est ouverte, sinon <B>False</B>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool IsOpen { get { return this.FPopupContainer.IsOpen; } }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand la popup se ferme.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public event EventHandler Closed;
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand la popup s'ouvre.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public event EventHandler Opened;
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "ShoppingList.Popups.Primitives"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
