﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : ArticleItem.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du contrôle ArticleItem
// Créé le       : 27/10/2013
// Modifié le    : 28/10/2013
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using ShoppingList.Data;
using ShoppingList.Media;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "ShoppingList.Controls"
//*******************************************************************************************************************************
namespace ShoppingList.Controls
	{

	//   ###   ####   #####  #   ###   #      #####         #  #####  #####  #   #
	//  #   #  #   #    #    #  #   #  #      #             #    #    #      ## ##
	//  #####  ####     #    #  #      #      ###    #####  #    #    ###    # # #
	//  #   #  #   #    #    #  #   #  #      #             #    #    #      #   #
	//  #   #  #   #    #    #   ###   #####  #####         #    #    #####  #   #

	//***************************************************************************************************************************
	// Classe ArticleItem
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit un objet article.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class ArticleItem : TiltButtonBase
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs Statiques
		//-----------------------------------------------------------------------------------------------------------------------
		public  static readonly DependencyProperty CheckVisibilityProperty;
		//-----------------------------------------------------------------------------------------------------------------------
		public  static readonly DependencyProperty DescriptionProperty;
		public  static readonly DependencyProperty IsCheckedProperty;
		public  static readonly DependencyProperty PhotoProperty;
		public  static readonly DependencyProperty TitleProperty;
		//-----------------------------------------------------------------------------------------------------------------------

		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private Article ArticleInternal = null;
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		#region // Section des Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Constructeur statique de l'objet <b>ArticleItem</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		static ArticleItem ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			CheckVisibilityProperty = DependencyProperty.Register ( "CheckVisibility", 
				typeof (Visibility ), typeof (ArticleItem), new PropertyMetadata ( Visibility.Collapsed      ) );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			DescriptionProperty     = DependencyProperty.Register ( "Description"    , 
				typeof (string      ), typeof (ArticleItem), new PropertyMetadata ( ""                        ) );
			IsCheckedProperty       = DependencyProperty.Register ( "IsChecked"      , 
				typeof (bool       ), typeof (ArticleItem), new PropertyMetadata ( false, OnIsCheckedChanged ) );
			PhotoProperty           = DependencyProperty.Register ( "Photo"          , 
				typeof (ImageSource ), typeof (ArticleItem), new PropertyMetadata ( null                      ) );
			TitleProperty           = DependencyProperty.Register ( "Title"          , 
				typeof (string      ), typeof (ArticleItem), new PropertyMetadata ( ""                        ) );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>ArticleItem</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public ArticleItem ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			base.DefaultStyleKey = typeof (ArticleItem);
			//-------------------------------------------------------------------------------------------------------------------
			
			//-------------------------------------------------------------------------------------------------------------------
			TakesWorker.TakeLoaded += ( object Sender, TakeLoadedEventArgs Args ) =>
				{
				//---------------------------------------------------------------------------------------------------------------
				if ( this.Article != null && this.Parent != null )
					{ if ( Args.ArticleID == this.Article.ArticleID ) this.Photo = Args.Take; }
				//---------------------------------------------------------------------------------------------------------------
				};
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section des Procédures
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Est appelé quand la propriété IsChecked change.
		/// </summary>
		/// <param name="Sender">
		/// <b>DependencyObject</b> dans lequel la propriété a modifié une valeur.
		/// </param>
		/// <param name="Args">
		/// Données d'événement publiées par un événement qui suit les modifications apportées à 
		/// la valeur effective de cette propriété.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private static void OnIsCheckedChanged ( DependencyObject Sender, DependencyPropertyChangedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			ArticleItem Self = Sender as ArticleItem;

			if ( Self != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				if ( Self.IsChecked )
					Self.SetValue ( CheckVisibilityProperty, Visibility.Visible   );
				else
					Self.SetValue ( CheckVisibilityProperty, Visibility.Collapsed );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit l'objet <b>Article</b> rataché au contrôle.
		/// </summary>
		/// <returns><b>Article</b> rataché au contrôle.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public Article Article
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return this.ArticleInternal; }
			//-------------------------------------------------------------------------------------------------------------------
			set
				{
				//---------------------------------------------------------------------------------------------------------------
				this.ArticleInternal = value;
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				if ( this.ArticleInternal != null )
					{
					//-----------------------------------------------------------------------------------------------------------
					ImageSource Take = TakesWorker.GetTake ( Article.ArticleID );

					if ( Take != null ) this.Photo = Take;
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				else { this.Photo = null; }
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Indique si l'article dispose d'une photo.
		/// </summary>
		/// <returns>
		/// <b>True</b> indique que l'article dispose d'une photo, sinon <b>False</b>.
		/// </returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool HavePhoto { get { return ( this.Photo != null ); } }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit la description de l'article.
		/// </summary>
		/// <returns>Chaine contenant la description de l'article.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public string Description
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return (string)base.GetValue ( DescriptionProperty ); }
			//-------------------------------------------------------------------------------------------------------------------
			set { base.SetValue ( DescriptionProperty, value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Indique si l'article est coché.
		/// </summary>
		/// <returns>
		/// <b>True</b> indique que l'article est coché, sinon <b>False</b>.
		/// </returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool IsChecked
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return (bool)base.GetValue ( IsCheckedProperty ); }
			//-------------------------------------------------------------------------------------------------------------------
			set { base.SetValue ( IsCheckedProperty, value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtient ou définit la photo de l'article. 
		/// </summary>
		/// <returns>Objet <b>ImageSource</b> contenant la photo de l'article.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public ImageSource Photo
			{
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			get { return (ImageSource)base.GetValue ( PhotoProperty ); }
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			set { base.SetValue ( PhotoProperty, value ); }
			//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit l'objet <b>Shopping</b> rataché au contrôle.
		/// </summary>
		/// <returns><b>Shopping</b> rataché au contrôle.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public Shopping Shopping { get; set; }
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit le titre de l'article.
		/// </summary>
		/// <returns>Chaine contenant le titre de l'article.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public string Title
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return (string)base.GetValue ( TitleProperty ); }
			//-------------------------------------------------------------------------------------------------------------------
			set { base.SetValue ( TitleProperty, value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "ShoppingList.Controls"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
