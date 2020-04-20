﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : StreamUtils.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet StreamUtils
// Créé le       : 28/10/2013
// Modifié le    : 28/10/2013
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows.Media.Imaging;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System.IO"
//*******************************************************************************************************************************
namespace System.IO
	{

	//   ####  #####  ####   #####   ###   #   #         #   #  #####  #  #       ####
	//  #        #    #   #  #      #   #  ## ##         #   #    #    #  #      #    
	//   ###     #    ####   ###    #####  # # #  #####  #   #    #    #  #       ### 
	//      #    #    #   #  #      #   #  #   #         #   #    #    #  #          #
	//  ####     #    #   #  #####  #   #  #   #          ###     #    #  #####  #### 

	//***************************************************************************************************************************
	// Classe StreamUtils
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit des méthodes utilisées pour manipuler les flux.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public static class StreamUtils
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Retourne une image contenue dans un flux.
		/// </summary>
		/// <param name="Self">Première chaine de caractères à comparer.</param>
		/// <param name="Angle">Angle de rotation.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static Stream Rotate ( this Stream Self, int Angle )
			{
			//-------------------------------------------------------------------------------------------------------------------
			Self.Position = 0;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( Angle % 90 != 0 || Angle < 0 ) throw new ArgumentException();

			if ( Angle % 360 == 0 ) return Self;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			BitmapImage Bitmap = new BitmapImage ();

			Bitmap.SetSource ( Self );

			WriteableBitmap WbSource = new WriteableBitmap ( Bitmap );

			WriteableBitmap WbTarget = null;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( Angle % 180 == 0 )
				WbTarget = new WriteableBitmap ( WbSource.PixelWidth , WbSource.PixelHeight );
			else
				WbTarget = new WriteableBitmap ( WbSource.PixelHeight, WbSource.PixelWidth  );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			int SH = WbSource.PixelHeight;
			int SW = WbSource.PixelWidth;

			int TH = WbTarget.PixelHeight;
			int TW = WbTarget.PixelWidth;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			for ( int X = 0 ; X < SW ; X ++ )
				{
				//---------------------------------------------------------------------------------------------------------------
				for ( int Y = 0 ; Y < SH ; Y ++ )
					{
					//-----------------------------------------------------------------------------------------------------------
					switch ( Angle % 360 )
						{
						//-------------------------------------------------------------------------------------------------------
						case 90:
							WbTarget.Pixels[(SH - Y - 1) + X * TW] = 
							                                           WbSource.Pixels[X + Y * SW];
							break;
						//-------------------------------------------------------------------------------------------------------
						case 180:
							WbTarget.Pixels[(SW - X - 1) + (SH - Y - 1) * SW] = 
							                                           WbSource.Pixels[X + Y * SW];
							break;
						//-------------------------------------------------------------------------------------------------------
						case 270:
							WbTarget.Pixels[Y + (SW - X - 1) * TW] = 
							                                           WbSource.Pixels[X + Y * SW];
							break;
						//-------------------------------------------------------------------------------------------------------
						}
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			MemoryStream NewStream = new MemoryStream ();

			WbTarget.SaveJpeg ( NewStream, TW, TH, 0, 100 );

			return NewStream;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************
	
	} // Fin du namespace "System.IO"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
