using System;
using System.Collections.Generic;
using System.Text;

namespace SolariPDV.Services
{
    public class Util
    {
        public static string StringUnicodeToUTF8(string texto)
        {
            try
            {
                texto = texto.Replace("u00E1", "á");
                texto = texto.Replace("u00E0", "à");
                texto = texto.Replace("u00E2", "â");
                texto = texto.Replace("u00E3", "ã");
                texto = texto.Replace("u00E4", "a");
                texto = texto.Replace("u00C1", "Á");
                texto = texto.Replace("u00C0", "À");
                texto = texto.Replace("u00C2", "Â");
                texto = texto.Replace("u00C3", "Ã");
                texto = texto.Replace("u00C4", "A");
                texto = texto.Replace("u00E9", "é");
                texto = texto.Replace("u00E8", "è");
                texto = texto.Replace("u00EA", "ê");
                texto = texto.Replace("u00C9", "É");
                texto = texto.Replace("u00C8", "È");
                texto = texto.Replace("u00CA", "Ê");
                texto = texto.Replace("u00ED", "í");
                texto = texto.Replace("u00EC", "ì");
                texto = texto.Replace("u00EE", "î");
                texto = texto.Replace("u00EF", "i");
                texto = texto.Replace("u00CD", "Í");
                texto = texto.Replace("u00CC", "Ì");
                texto = texto.Replace("u00CE", "Î");
                texto = texto.Replace("u00CF", "I");
                texto = texto.Replace("u00F3", "ó");
                texto = texto.Replace("u00F2", "ò");
                texto = texto.Replace("u00F4", "ô");
                texto = texto.Replace("u00F5", "õ");
                texto = texto.Replace("u00F6", "o");
                texto = texto.Replace("u00D3", "Ó");
                texto = texto.Replace("u00D2", "Ò");
                texto = texto.Replace("u00D4", "Ô");
                texto = texto.Replace("u00D5", "Õ");
                texto = texto.Replace("u00D6", "O");
                texto = texto.Replace("u00FA", "ú");
                texto = texto.Replace("u00F9", "ù");
                texto = texto.Replace("u00FB", "û");
                texto = texto.Replace("u00FC", "u");
                texto = texto.Replace("u00DA", "Ú");
                texto = texto.Replace("u00D9", "Ù");
                texto = texto.Replace("u00DB", "Û");
                texto = texto.Replace("u00E7", "ç");
                texto = texto.Replace("u00C7", "Ç");
                texto = texto.Replace("u00F1", "ñ");
                texto = texto.Replace("u00D1", "Ñ");
                texto = texto.Replace("u0026", "&");
                texto = texto.Replace("u0027", "'");

                return texto;
            }
            catch
            {
                return null;
            }                
        }

    }
}
