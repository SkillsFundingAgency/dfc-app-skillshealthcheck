using System;
using System.Configuration;
using System.IO;
using Aspose.Words;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    public class DocxConverter
    {
        private const string ApsoseLicenseFilePathkey = "AsposeLicenseFilePath";

        #region | Public Methods |
        /// <summary>
        /// Converts docx document to specified file format
        /// </summary>
        /// <param name="docxContent">byte array of docx document content</param>
        /// <returns>byte array of converted target type document content</returns>
        public static byte[] ConvertDocx(byte[] docxContent, DocxTargetFormat format)
        {
            SaveFormat targetFormat = GetTargetFormat(format);
            string asposeLicKeyFilePath = GetAsposeLicKeyFilePath();

            byte[] pdfContent = null;
            try
            {
                using (MemoryStream docxStream = new MemoryStream())
                {
                    if (!string.IsNullOrEmpty(asposeLicKeyFilePath))
                    {
                        Aspose.Words.License license = new Aspose.Words.License();
                        license.SetLicense(asposeLicKeyFilePath);
                    }

                    docxStream.Write(docxContent, 0, (int)docxContent.Length);
                    Document converter = new Document(docxStream);
                    using (MemoryStream pdfStream = new MemoryStream())
                    {
                        converter.Save(pdfStream, targetFormat);
                        pdfContent = pdfStream.ToArray();
                    }
                }

                return pdfContent;
            }
            catch (Exception ex)
            {
                //TODO: Handle exception
                throw new Exception(string.Format("Error while creating pdf report using aspose.words, targetFormat: '{0}', aspose.word.lic.path: '{1}'", targetFormat.ToString(), asposeLicKeyFilePath), ex);
            }
        }

        /// <summary>
        /// Get the aspose word licence key path
        /// </summary>
        /// <returns></returns>
        private static string GetAsposeLicKeyFilePath()
        {
            string asposeLicKeyFilePath = ConfigurationManager.AppSettings[ApsoseLicenseFilePathkey];

            try
            {
                if (RoleEnvironment.IsAvailable)
                {
                    asposeLicKeyFilePath = Path.Combine(Environment.GetEnvironmentVariable("RoleRoot") + "\\approot\\bin\\", asposeLicKeyFilePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error trying to find the aspose.word.lic path from configuraiton : '{0}'",
                    ConfigurationManager.AppSettings[ApsoseLicenseFilePathkey]), ex);
            }

            return asposeLicKeyFilePath;
        }
        #endregion

        #region | Private Method |
        /// <summary>
        /// Maps the target format specified to aspose save format 
        /// </summary>
        /// <param name="format"></param>
        private static SaveFormat GetTargetFormat(DocxTargetFormat format)
        {
            SaveFormat targetFormat;
            switch (format)
            {
                case DocxTargetFormat.Doc:
                    targetFormat = SaveFormat.Doc;
                    break;
                case DocxTargetFormat.JPEG:
                    targetFormat = SaveFormat.Jpeg;
                    break;
                case DocxTargetFormat.MHTML:
                    targetFormat = SaveFormat.Mhtml;
                    break;
                case DocxTargetFormat.PDF:
                    targetFormat = SaveFormat.Pdf;
                    break;
                case DocxTargetFormat.Text:
                    targetFormat = SaveFormat.Text;
                    break;
                case DocxTargetFormat.TIFF:
                    targetFormat = SaveFormat.Tiff;
                    break;
                default:
                    throw new NotSupportedException(string.Format("The specified document conversion format {0} is not supported", format.ToString()));
            }

            return targetFormat;
        }
        #endregion
    }
}

