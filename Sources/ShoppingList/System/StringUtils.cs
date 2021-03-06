﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : StringUtils.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet StringUtils
// Créé le       : 31/05/2013
// Modifié le    : 06/09/2013
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Windows.Media;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System"
//*******************************************************************************************************************************
namespace System
	{

	//   ####  #####  ####   #  #   #   ###           #   #  #####  #  #       ####
	//  #        #    #   #  #  ##  #  #   #          #   #    #    #  #      #    
	//   ###     #    ####   #  # # #  #       #####  #   #    #    #  #       ### 
	//      #    #    #   #  #  #  ##  #   ##         #   #    #    #  #          #
	//  ####     #    #   #  #  #   #   ### #          ###     #    #  #####  #### 

	//***************************************************************************************************************************
	// Classe StringUtils
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit des méthodes utilisées pour manipuler les chaines.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public static class StringUtils
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Secvtion des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private static Regex WhiteSpaces = new Regex ( @"\s+" );
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Renvoie une chaîne de caractères spécifique de taille déterminée. Cette chaîne peut 
		/// être tronquée ou complétée par des espaces (ou par un caractère) pour atteindre 
		/// la taille requise.
		/// </summary>
		/// <param name="Self">
		/// Chaîne de caractères initiale à compléter ou à tronquer. Cette chaîne de caractères 
		/// n'est pas modifiée.
		/// </param>
		/// <param name="Size">
		/// Nouvelle taille de la chaîne de caractères (supérieure ou égale à 0).
		/// </param>
		/// <param name="Car">
		/// Caractère utilisé pour compléter la chaîne. Par défaut, ce caractère correspond à un 
		/// espace.
		/// </param>
		/// <returns>
		/// Chaîne de caractères à la taille spécifiée, complétée ou tronquée.
		/// </returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string Complete ( this string Self, int Size, string Car )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return Self.Complete ( Size, Car, false );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Renvoie une chaîne de caractères spécifique de taille déterminée. Cette chaîne peut 
		/// être tronquée ou complétée par des espaces (ou par un caractère) pour atteindre 
		/// la taille requise.
		/// </summary>
		/// <param name="Self">
		/// Chaîne de caractères initiale à compléter ou à tronquer. Cette chaîne de caractères 
		/// n'est pas modifiée.
		/// </param>
		/// <param name="Size">
		/// Nouvelle taille de la chaîne de caractères (supérieure ou égale à 0).
		/// </param>
		/// <param name="Car">
		/// Caractère utilisé pour compléter la chaîne. Par défaut, ce caractère correspond à un 
		/// espace.
		/// </param>
		/// <param name="Left">
		/// Indique si le complément doit être fait à gauche de la chaine.
		/// </param>
		/// <returns>
		/// Chaîne de caractères à la taille spécifiée, complétée ou tronquée.
		/// </returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string Complete ( this string Self, int Size, string Car, bool Left )
			{
			//-------------------------------------------------------------------------------------------------------------------
			string Str = Self;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			while ( Str.Length < Size )
				{
				if ( Left ) Str = Car + Str;
				else        Str = Str + Car;
				}
			//-------------------------------------------------------------------------------------------------------------------
			
			//-------------------------------------------------------------------------------------------------------------------
			return Str.Substring ( 0, Size );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Compare les 2 chaines de caractères sont tenir compte de la casse.
		/// </summary>
		/// <param name="Self">Première chaine de caractères à comparer.</param>
		/// <param name="Str">Seconde chaine de caractères à comparer.</param>
		/// <returns><b>True</b> si les 2 chaines sont égales, sinon <b>False</b>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static bool EqualsIgnoreCase ( this string Self, string Str )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return (string.Compare (Self, Str, StringComparison.InvariantCultureIgnoreCase) == 0);
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Décode une chaîne codée en HTML et retourne la chaîne décodée.
		/// </summary>
		/// <param name="Self">Chaîne HTML à décoder.</param>
		/// <param name="Wrap">Indique s'il faut garder les tabulations.</param>
		/// <returns>Texte décodé.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string HtmlDecode ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( string.IsNullOrEmpty ( Self ) || Self.IndexOf ( '&' ) < 0 )
				return WhiteSpaces.Replace ( Self.Replace ( "\t", " " ), " " );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return System.Net.HttpUtility.HtmlDecode ( Self );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Décode une chaîne codée en HTML et retourne la chaîne décodée.
		/// </summary>
		/// <param name="Self">Chaîne HTML à décoder.</param>
		/// <param name="Wrap">Indique s'il faut garder les tabulations.</param>
		/// <returns>Texte décodé.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string HtmlWrapDecode ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( string.IsNullOrEmpty ( Self ) || Self.IndexOf ( '&' ) < 0 ) return Self;

			return System.Net.HttpUtility.HtmlDecode ( Self );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Indique si le contenue de la chaine peut être un booléen valant <b>False</b>.
		/// </summary>
		/// <param name="Self">
		/// Chaîne de caractères initiale à compléter ou à tronquer. Cette chaîne de caractères 
		/// n'est pas modifiée.
		/// </param>
		/// <returns><b>True</b> si false, no, 0, sinon <b>False</b>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static bool IsFalse ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( ("false").EqualsIgnoreCase ( Self ) ) return true;
			if ( ("no"   ).EqualsIgnoreCase ( Self ) ) return true;
			if ( ("0"    ).EqualsIgnoreCase ( Self ) ) return true;

			return false;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Indique si le contenue de la chaine peut être un booléen valant <b>True</b>.
		/// </summary>
		/// <param name="Self">
		/// Chaîne de caractères initiale à compléter ou à tronquer. Cette chaîne de caractères 
		/// n'est pas modifiée.
		/// </param>
		/// <returns><b>True</b> si true, yes, 1, sinon <b>False</b>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static bool IsTrue ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( ("true").EqualsIgnoreCase ( Self ) ) return true;
			if ( ("yes" ).EqualsIgnoreCase ( Self ) ) return true;
			if ( ("1"   ).EqualsIgnoreCase ( Self ) ) return true;

			return false;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens la chaine de la taille maximum à Length.
		/// </summary>
		/// <param name="Self">
		/// Chaîne de caractères initiale à contrôler.
		/// </param>
		/// <param name="Size">
		/// Nouvelle taille de la chaîne de caractères (supérieure ou égale à 0).
		/// </param>
		/// <returns>Chaîne de caractères à la taille spécifiée, inchangée ou tronquée.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string MaxLength ( this string Self, int Length )
			{
			//-------------------------------------------------------------------------------------------------------------------
			Length = Math.Max ( Length, 0 );

			return ( Self.Length < Length ) ? Self : Self.Substring ( 0, Length );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Retire tous les tags de la chaine.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string StripTags ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return ( Self != null ) ? Regex.Replace ( Self, "<.*?>", "" ) : "";
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Tente d'extraire la couleur de la chaine de caractères.
		/// </summary>
		/// <param name="Value">
		/// Chaine de caractères contenant la couleur au format Héxadécimal.
		/// </param>
		/// <returns>Objet <b>Color</b> contenue dans la chaine de caractères.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static Color ToColor ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			byte Alpha = 255;
			byte Red   = 255;
			byte Green = 255;
			byte Blue  = 255;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			string Value = Self;
			
			if ( Value.StartsWith ( "#" ) ) Value = Value.Substring ( 1 );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( Value.Length == 8 )
				{
				Alpha = Convert.ToByte ( Int32.Parse ( Value.Substring ( 0, 2 ), NumberStyles.AllowHexSpecifier ) );
				Red   = Convert.ToByte ( Int32.Parse ( Value.Substring ( 2, 2 ), NumberStyles.AllowHexSpecifier ) );
				Green = Convert.ToByte ( Int32.Parse ( Value.Substring ( 4, 2 ), NumberStyles.AllowHexSpecifier ) );
				Blue  = Convert.ToByte ( Int32.Parse ( Value.Substring ( 6, 2 ), NumberStyles.AllowHexSpecifier ) );
				}
			//-------------------------------------------------------------------------------------------------------------------
			else if ( Value.Length == 6 )
				{
				Red   = Convert.ToByte ( Int32.Parse ( Value.Substring ( 0, 2 ), NumberStyles.AllowHexSpecifier ) );
				Green = Convert.ToByte ( Int32.Parse ( Value.Substring ( 2, 2 ), NumberStyles.AllowHexSpecifier ) );
				Blue  = Convert.ToByte ( Int32.Parse ( Value.Substring ( 4, 2 ), NumberStyles.AllowHexSpecifier ) );
				}
			//-------------------------------------------------------------------------------------------------------------------
			else if ( Value.Length == 4 )
				{
				Alpha = Convert.ToByte ( Int32.Parse ( Value.Substring ( 0, 1 ), NumberStyles.AllowHexSpecifier ) );
				Red   = Convert.ToByte ( Int32.Parse ( Value.Substring ( 1, 1 ), NumberStyles.AllowHexSpecifier ) );
				Green = Convert.ToByte ( Int32.Parse ( Value.Substring ( 2, 1 ), NumberStyles.AllowHexSpecifier ) );
				Blue  = Convert.ToByte ( Int32.Parse ( Value.Substring ( 3, 1 ), NumberStyles.AllowHexSpecifier ) );
				}
			//-------------------------------------------------------------------------------------------------------------------
			else if ( Value.Length == 3 )
				{
				Red   = Convert.ToByte ( Int32.Parse ( Value.Substring ( 0, 1 ), NumberStyles.AllowHexSpecifier ) );
				Green = Convert.ToByte ( Int32.Parse ( Value.Substring ( 1, 1 ), NumberStyles.AllowHexSpecifier ) );
				Blue  = Convert.ToByte ( Int32.Parse ( Value.Substring ( 2, 1 ), NumberStyles.AllowHexSpecifier ) );
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return Color.FromArgb ( Alpha, Red, Green, Blue );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Tente d'extraire le pinceau de la chaine de caractères.
		/// </summary>
		/// <param name="Value">
		/// Chaine de caractères contenant la couleur au format Héxadécimal.
		/// </param>
		/// <returns>Objet <b>SolidColorBrush</b> contenue dans la chaine de caractères.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static SolidColorBrush ToColorBrush ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return new SolidColorBrush ( Self.ToColor () );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaine en Date.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static DateTime ToDateTime ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			DateTime Result = DateTime.Today;
					
			DateTime.TryParseExact ( Self, "yyyy-MM-dd", null, DateTimeStyles.None, out Result );

			return Result;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaine en Flotant.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static double ToFloat ( this string Self ) { return Self.ToFloat ( 0 ); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaine en Flotant.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <param name="Default">
		/// Valeur par défaut à renvoyer si la chaine n'est pas convertible.
		/// </param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static double ToFloat ( this string Self, double Default )
			{
			//-------------------------------------------------------------------------------------------------------------------
			CultureInfo Ci = CultureInfo.CurrentCulture;

			string Value = Self.Replace ( " ", "" );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			Value = Value.Replace ( " ", "");
			Value = Value.Replace ( ".", Ci.NumberFormat.NumberDecimalSeparator );
			Value = Value.Replace ( ",", Ci.NumberFormat.NumberDecimalSeparator );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			try { return double.Parse ( Value ); } catch {}

			return Default;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaine en Entier.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static int ToInteger ( this string Self ) { return Self.ToInteger ( 0 ); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaine en Entier.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <param name="Default">
		/// Valeur par défaut à renvoyer si la chaine n'est pas convertible.
		/// </param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static int ToInteger ( this string Self, int Default )
			{
			//-------------------------------------------------------------------------------------------------------------------
			string Value = Self.Replace ( " ", "" );
			//-------------------------------------------------------------------------------------------------------------------
			
			//-------------------------------------------------------------------------------------------------------------------
			int vPos = Value.IndexOf ( '.' );
			int pPos = Value.IndexOf ( ',' );
			//-------------------------------------------------------------------------------------------------------------------
			
			//-------------------------------------------------------------------------------------------------------------------
			try
				{
				if (vPos >= 0) return (int)Int64.Parse ( Value.Substring (0, vPos) );
				if (pPos >= 0) return (int)Int64.Parse ( Value.Substring (0, pPos) );

				return (int)Int64.Parse ( Value );
				}
			//-------------------------------------------------------------------------------------------------------------------
			catch {}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return Default;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaine en chaine interprétable par un RichTextBox.
		/// </summary>
		/// <param name="Self">Chaine représentant un Entier à convertir.</param>
		/// <returns>Chaine convertie.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string ToRichTextBox ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Self != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				string Str = Self.Replace ( "\n", "<LineBreak/>" ).Replace ( "\0", "" );

				Str = Regex.Replace ( Str, "<Hyperlink.*>", M => string.Format ( "{0}", 
				                                    M.Groups[0].Value.Replace ( "&", "&amp;" ) ) );
				Str = Str.Replace ( " </Span>", "</Span> " );
				//---------------------------------------------------------------------------------------------------------------
				
				//---------------------------------------------------------------------------------------------------------------
				return
					"<Section xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" +
						"<Paragraph>" + Str + "<LineBreak/>" + "</Paragraph>"                       +
					"</Section>";
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return "";
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Convertit la chaîne spécifiée en initiales majuscules.
		/// </summary>
		/// <param name="Self">Chaine à traiter.</param>
		/// <returns>Chaîne spécifiée convertie en initiales majuscules.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string ToTitleCase ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			bool          ToUpper = false;
			StringBuilder Result  = new StringBuilder ();
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			for ( int Index = 0 ; Index < Self.Length ; Index ++ )
				{
				//---------------------------------------------------------------------------------------------------------------
				char Code = Self[Index];
				//---------------------------------------------------------------------------------------------------------------
				
				//---------------------------------------------------------------------------------------------------------------
				if ( Index == 0 )
					{
					//-----------------------------------------------------------------------------------------------------------
					ToUpper = (Code<48 || (Code>=58 && Code<=64) || (Code>=91 && Code<=96));
					//-----------------------------------------------------------------------------------------------------------

					//-----------------------------------------------------------------------------------------------------------
					if ( ! ToUpper )
						Result.Append ( Self.Substring ( Index, 1 ).ToUpper () );
					//-----------------------------------------------------------------------------------------------------------
					else
						Result.Append ( Self.Substring ( Index, 1 ).ToLower () );
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				else if ( ToUpper )
					{
					//-----------------------------------------------------------------------------------------------------------
					Result.Append ( Self.Substring ( Index, 1 ).ToUpper () );

					ToUpper = false;
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				else
					{
					//-----------------------------------------------------------------------------------------------------------
					ToUpper = (Code<48 || (Code>=58 && Code<=64) || (Code>=91 && Code<=96));

					Result.Append ( Self.Substring ( Index, 1 ).ToLower () );
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return Result.ToString ();
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Décode une chaîne URL et retourne la chaîne décodée.
		/// </summary>
		/// <param name="Self">Chaîne de texte à décoder.</param>
		/// <returns>Texte décodé.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string UrlDecode ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return System.Net.HttpUtility.UrlDecode ( Self );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Encode une chaîne URL et retourne la chaîne encodée.
		/// </summary>
		/// <param name="Self">Chaîne de texte à encoder.</param>
		/// <returns>Texte encodé.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string UrlEncode ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return System.Net.HttpUtility.UrlEncode ( Self );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
