/*
 * FCKeditor - The text editor for Internet - http://www.fckeditor.net
 * Copyright (C) 2003-2008 Frederico Caldeira Knabben
 *
 * == BEGIN LICENSE ==
 *
 * Licensed under the terms of any of the following licenses at your
 * choice:
 *
 *  - GNU General Public License Version 2 or later (the "GPL")
 *    http://www.gnu.org/licenses/gpl.html
 *
 *  - GNU Lesser General Public License Version 2.1 or later (the "LGPL")
 *    http://www.gnu.org/licenses/lgpl.html
 *
 *  - Mozilla Public License Version 1.1 or later (the "MPL")
 *    http://www.mozilla.org/MPL/MPL-1.1.html
 *
 * == END LICENSE ==
 *
 * Configuration settings used by the XHTML 1.1 sample page (sample14.html).
 */

// Our intention is force all formatting features to use CSS classes or
// semantic aware elements.

/**
 * Core styles.
 */
FCKConfig.CoreStyles.Bold			= { Element : 'b' } ;
FCKConfig.CoreStyles.Italic			= { Element : 'i' } ;
FCKConfig.CoreStyles.Underline		= { Element : 'u' } ;
FCKConfig.CoreStyles.StrikeThrough	= { Element : 'strike' } ;

/**
 * Font face
 */
// Define the way font elements will be applied to the document. The "span"
// element will be used. When a font is selected, the font name defined in the
// above list is passed to this definition with the name "Font", being it
// injected in the "class" attribute.
// We must also instruct the editor to replace span elements that are used to
// set the font (Overrides).
FCKConfig.CoreStyles.FontFace =
	{
		Element		: 'font',
		Attributes	: { 'face' : '#("Font")' }
	} ;

/**
 * Font sizes.
 */
FCKConfig.FontSizes		= '8/8pt;9/9pt;10/10pt;11/11pt;12/12pt;14/14pt;16/16pt;18/18pt;20/20pt;22/22pt;24/24pt;26/26pt;28/28pt;36/36pt' ;
FCKConfig.CoreStyles.Size =
	{
		Element		: 'font',
		Attributes	: { 'size' : '#("Size")' }
	} ;

/**
 * Font colors.
 */
FCKConfig.EnableMoreFontColors = true ;
FCKConfig.CoreStyles.Color =
	{
		Element		: 'font',
		Attributes	: { 'color' : '#("Color")' }
	} ;

FCKConfig.CoreStyles.BackColor =
	{
		Element		: 'font',
		Styles		: { 'background-color' : '#("Color","color")' }
	} ;

/**
 * Styles combo.
 */
FCKConfig.StylesXmlPath = '' ;
FCKConfig.CustomStyles =
	{
		'Computer Code' : { Element : 'code' },
		'Keyboard Phrase' : { Element : 'kbd' },
		'Sample Text' : { Element : 'samp' },
		'Variable' : { Element : 'var' },

		'Deleted Text' : { Element : 'del' },
		'Inserted Text' : { Element : 'ins' },

		'Cited Work' : { Element : 'cite' },
		'Inline Quotation' : { Element : 'q' }
	} ;
