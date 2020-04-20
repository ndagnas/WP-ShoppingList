﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : PopupContainer.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du contrôle PopupContainer
// Créé le       : 13/06/2013
// Modifié le    : 09/06/2014
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Microsoft.Phone.Controls;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "ShoppingList.Popups.Primitives"
//*******************************************************************************************************************************
namespace ShoppingList.Popups.Primitives
	{

	//  ####    ###   ####   #   #  ####           ###   #   #  #####
	//  #   #  #   #  #   #  #   #  #   #         #   #  ##  #    #  
	//  ####   #   #  ####   #   #  ####   #####  #      # # #    #  
	//  #      #   #  #      #   #  #             #   #  #  ##    #  
	//  #       ###   #       ###   #              ###   #   #    #  

	//***************************************************************************************************************************
	// Classe PopupContainer
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Définit le conteneur de popup gérant les transitions.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public sealed class PopupContainer : ContentControl
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private bool                  CanGoBack             = false;
		private bool                  UseTransition         = false;
		//-----------------------------------------------------------------------------------------------------------------------
		private Grid                  Container             = null;
		private IPhoneApplicationPage MainApplicationPage   = null;
		private Popup                 PopupInternal         = null;
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>PopupContainer</b>.
		/// </summary>
		/// <param name="UseTransition">Indique s'il faut utiliser les animations.</param>
		/// <param name="CanGoBack">Indique que le bouton précédent ferme la popup.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public PopupContainer ( bool UseTransition, bool CanGoBack )
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.DefaultStyleKey = typeof (PopupContainer);
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			this.CanGoBack     = CanGoBack;
			this.UseTransition = UseTransition;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			this.PopupInternal = new Popup ();

			this.PopupInternal.Child = this;

			this.PopupInternal.Opened += new EventHandler ( this.OnPopupOpened );
			this.PopupInternal.Closed += new EventHandler ( this.OnPopupClosed );
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
		/// Anime l'ouverture de la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		private void TranslateToOpen ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.UseTransition && this.Container != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				var T = this.Container.GetVerticalOffset ().Transform;

				var Db1 = new DoubleAnimation ()
					{
					To             = 0                                                      ,
					From           = T.Y                                                    ,
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
					Duration       = TimeSpan.FromMilliseconds ( 200 )                      ,
					};

				Storyboard.SetTarget         ( Db1, T                                            );
				Storyboard.SetTargetProperty ( Db1, new PropertyPath 
				                                                ( TranslateTransform.YProperty ) );
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				var Db2 = new DoubleAnimation ()
					{
					To             = 1                                                      ,
					From           = 0                                                      ,
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut },
					Duration       = TimeSpan.FromMilliseconds ( 50 )                       ,
					};

				Storyboard.SetTarget         ( Db2, this.Container                 );
				Storyboard.SetTargetProperty ( Db2, new PropertyPath ( "Opacity" ) );
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				var Sb = new Storyboard () { BeginTime = TimeSpan.FromMilliseconds ( 0 ) };

				Sb.Completed += (S, A) => this.OnOpenComplete ();

				Sb.Children.Add ( Db1 );
				Sb.Children.Add ( Db2 );
				Sb.Begin        (     );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			else if ( ! this.UseTransition ) { this.OnOpenComplete (); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit à la fin de l'animation de l'ouverture de la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnOpenComplete ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.MainApplicationPage != null )
				this.MainApplicationPage.ApplicationBar.OnPopupOpen ( this.Popup );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			PopupBase Popup = this.Content as PopupBase;

			if ( Popup != null ) Popup.OnOpened ( EventArgs.Empty );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Anime la fermeture de la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		private void TranslateToClose ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.MainApplicationPage != null )
				this.MainApplicationPage.ApplicationBar.OnPopupClosing ( this.Popup );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( this.UseTransition && this.Container != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				var T = this.Container.GetVerticalOffset ().Transform;

				var Db1 = new DoubleAnimation ()
					{
					To             = Application.Current.Host.Content.ActualHeight         ,
					From           = T.Y                                                   ,
					EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseIn },
					Duration       = TimeSpan.FromMilliseconds ( 160 )                     ,
					};

				Storyboard.SetTarget         ( Db1, T                                            );
				Storyboard.SetTargetProperty ( Db1, new PropertyPath 
				                                                ( TranslateTransform.YProperty ) );
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				var Db2 = new DoubleAnimation ()
					{
					To             = 0                                              ,
					From           = 1                                              ,
					EasingFunction = new SineEase { EasingMode = EasingMode.EaseIn },
					Duration       = TimeSpan.FromMilliseconds ( 40 )               ,
					};

				Storyboard.SetTarget         ( Db2, this.Container                 );
				Storyboard.SetTargetProperty ( Db2, new PropertyPath ( "Opacity" ) );
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				var Sb = new Storyboard () { BeginTime = TimeSpan.FromMilliseconds ( 0 ) };

				Sb.Completed += (S, A) => this.OnCloseComplete ();

				Sb.Children.Add ( Db1 );
				Sb.Children.Add ( Db2 );
				Sb.Begin        (     );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			else { this.OnCloseComplete (); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit à la fin de l'animation de la fermeture de la popup.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>EventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnCloseComplete ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.PopupInternal.IsOpen = false;

			if ( this.MainApplicationPage != null )
				{
				this.MainApplicationPage.ApplicationBar.OnPopupClosed ( this.Popup );

				this.MainApplicationPage.BeginBackKeyPress -= new EventHandler<CancelEventArgs> ( this.OnBackKeyPress );
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Procédures Liées aux Evénements
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'une pression sur le bouton Précédent.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>CancelEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnBackKeyPress ( object Sender, CancelEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( ! Args.Cancel )
				{
				//---------------------------------------------------------------------------------------------------------------
				Args.Cancel = true;

				if ( this.CanGoBack )
					{
					if ( this.Content is PopupBase )
						{
						CancelEventArgs NewArgs = new CancelEventArgs ( false );
					
						((PopupBase)this.Content).OnBackKeyPress ( Sender, NewArgs );

						if ( NewArgs.Cancel ) return;
						}

					((PopupBase)this.Content).OnCancel ( EventArgs.Empty );

					this.Close ();
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit à l'ouverture de la popup.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>EventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnPopupOpened ( object Sender, EventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.UseTransition ) { this.TranslateToOpen (); }
			//-------------------------------------------------------------------------------------------------------------------
			else { this.Dispatcher.Async ( () => { this.OnOpenComplete (); } ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit à la fermeture de la popup.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>EventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnPopupClosed ( object Sender, EventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.IsOpen = false;

			PopupBase Popup = this.Content as PopupBase;

			if ( Popup != null ) Popup.OnClosed ( Args );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Procédures Dérivées
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Génère l'arborescence d'éléments visuels pour le contrôle.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public override void OnApplyTemplate ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			base.OnApplyTemplate ();

			this.Container = this.GetTemplateChild ( "Container" ) as Grid;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( this.Container != null && this.UseTransition )
				{
				//-----------------------------------------------------------------------------------------------------------
				this.Container.GetVerticalOffset ().Transform.Y = 
					                                Application.Current.Host.Content.ActualHeight;
				if ( this.IsOpen )
					this.Dispatcher.Async ( () => { this.TranslateToOpen (); }, 25 );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section des Procédures Publiques
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Ouvre la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Abort ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( ! this.IsOpen ) return;

			this.TranslateToClose ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Ouvre la popup.
		/// </summary>
		/// <param name="CanGoBack">Indique si le retour est autorisé.</param>
		/// <param name="UseTransition">Indique s'il faut utiliser une animation.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Open ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.IsOpen = true;
			this.Width  = Application.Current.Host.Content.ActualWidth;
			this.Height = Application.Current.Host.Content.ActualHeight;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			PhoneApplicationFrame frame = Application.Current.RootVisual as PhoneApplicationFrame;

			if ( frame != null )
				{
				this.MainApplicationPage = frame.Content as IPhoneApplicationPage;

				if ( this.MainApplicationPage != null )
					{
					this.MainApplicationPage.BeginBackKeyPress += 
						                 new EventHandler<CancelEventArgs> ( this.OnBackKeyPress );

					this.MainApplicationPage.ApplicationBar.OnPopupOpening ( this.Popup );
					}
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			this.Dispatcher.Async ( () => { this.PopupInternal.IsOpen = true; }, 50 );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Ouvre la popup.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void Close ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( ! this.IsOpen ) return;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			PopupBase Popup = this.Content as PopupBase;

			if ( Popup != null )
				{
				CancelEventArgs Args = new CancelEventArgs ( false );

				Popup.OnClosing ( Args );

				if ( Args.Cancel ) return;
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			this.TranslateToClose ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Indique si la popup est ouverte.
		/// </summary>
		/// <returns><b>True</b> indique que la popup est ouverte, sinon <B>False</B>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool IsOpen { get; private set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens l'objet <b>PopupBase</b> propriétaire.
		/// </summary>
		/// <returns>Objet <b>PopupBase</b> propriétaire.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		private PopupBase Popup { get { return ( this.Content as PopupBase ); } }
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